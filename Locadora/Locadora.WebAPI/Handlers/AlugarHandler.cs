using Locadora.Comuns.Dtos;
using Locadora.Dados;
using Locadora.Dominio.Entidades;
using Locadora.Dominio.Interfaces;
using Locadora.WebAPI.Commands.ContextoAluguel;
using MediatR;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Locadora.WebAPI.Handlers
{
    public class AlugarHandler : 
        IRequestHandler<DevolverAluguelCommand, string>,
        IRequestHandler<ProcessarAluguelCommand>,
        IRequestHandler<ReservarAluguelCommand>
    {
        private readonly LocadoraContext _locadoraContext;
        private readonly IRepositorioAluguel _repositorioAluguel;
        private readonly IRepositorioAluguelItem _repositorioAluguelItem;
        private readonly IRepositorioEstoque _repositorioEstoque;
        private readonly IRepositorioCliente _repositorioCliente;
        private readonly IRepositorioItem _repositorioItem;
        private readonly IConnection _rabbitConnection;

        public AlugarHandler(LocadoraContext locadoraContext,
            IRepositorioAluguel repositorioAluguel,
            IRepositorioAluguelItem repositorioAluguelItem,
            IRepositorioEstoque repositorioEstoque,
            IRepositorioCliente repositorioCliente,
            IRepositorioItem repositorioItem,
            IConnection rabbitConnection)
        {
            _locadoraContext = locadoraContext;
            _repositorioAluguel = repositorioAluguel;
            _repositorioAluguelItem = repositorioAluguelItem;
            _repositorioCliente = repositorioCliente;
            _repositorioEstoque = repositorioEstoque;
            _repositorioItem = repositorioItem;
            _rabbitConnection = rabbitConnection;
        }

        public async Task<Unit> Handle(ReservarAluguelCommand request, CancellationToken cancellationToken)
        {
            var aluguel = Map(request.AluguelDto);
            using (var transacao = _locadoraContext.Database.BeginTransaction())
            {
                _repositorioAluguel.Salvar(aluguel);
                _locadoraContext.SaveChanges();
                transacao.Commit();
            }

            request.AluguelDto.Id = aluguel.Id;

            //posta o request.AluguelDto no RabbitMQ

            return Unit.Value;
        }

        public async Task<Unit> Handle(ProcessarAluguelCommand request, CancellationToken cancellationToken)
        {
            using (var transacao = _locadoraContext.Database.BeginTransaction())
            {
                var aluguel = _repositorioAluguel.ObterPorId(request.AluguelDto.Id);

                request.AluguelDto.AluguelItens.ForEach(aluguelItem =>
                {
                    var estoque = _repositorioEstoque.BuscarPorItemId(aluguelItem.ItemId);
                    estoque.RetirarDoEstoque();
                    _locadoraContext.SaveChanges();
                });

                aluguel.Aberto = true;
                aluguel.DataDevolucao = DateTime.Now.AddDays(7);
                aluguel.Status = Comuns.Enums.Status.ALUGADO;
                _locadoraContext.SaveChanges();
                transacao.Commit();
            }

            return Unit.Value;
        }

        public async Task<string> Handle(DevolverAluguelCommand request, CancellationToken cancellationToken)
        {
            using (var transacao = _locadoraContext.Database.BeginTransaction())
            {
                var aluguel = _repositorioAluguel.ObterPorId(request.AluguelDto.Id);

                request.AluguelDto.AluguelItens.ForEach(aluguelItem =>
                {
                    var item = _repositorioItem.BuscarPorId(aluguelItem.ItemId);
                    aluguel.AdicionarItem(new AluguelItem { Item = item });
                    var estoque = _repositorioEstoque.BuscarPorItemId(aluguelItem.ItemId);
                    estoque.ReporNoEstoque();
                    _locadoraContext.SaveChanges();
                });

                aluguel.Aberto = false;
                aluguel.Status = Comuns.Enums.Status.DEVOLVIDO;

                _locadoraContext.SaveChanges();
                transacao.Commit();

                return aluguel.CalcularMulta();
            }
        }

        public Aluguel Map(AluguelDto aluguelDto)
        {
            var aluguel = new Aluguel()
            {
                Id = aluguelDto.Id,
                ClienteId = aluguelDto.ClienteId,
                Aberto = aluguelDto.Aberto,
                Status = aluguelDto.Status,
                DataPedido = aluguelDto.DataPedido,
                DataDevolucao = aluguelDto.DataDevolucao
            };

            aluguelDto.AluguelItens.ForEach(aluguelItemDto => aluguel.AdicionarItem(Map(aluguelItemDto)));

            return aluguel;
        }
        public AluguelDto Map(Aluguel aluguel)
        {
            var aluguelDto = new AluguelDto()
            {
                Id = aluguel.Id,
                ClienteId = aluguel.ClienteId,
                Aberto = aluguel.Aberto,
                Status = aluguel.Status,
                DataPedido = aluguel.DataPedido,
                DataDevolucao = aluguel.DataDevolucao
            };

            return aluguelDto;
        }

        public AluguelItem Map(AluguelItemDto aluguelItemDto)
        {
            var aluguelItem = new AluguelItem()
            {
                ItemId = aluguelItemDto.ItemId
            };

            return aluguelItem;
        }

        public AluguelItemDto Map(AluguelItem aluguelItem)
        {
            var aluguelItemDto = new AluguelItemDto()
            {
                AluguelId = aluguelItem.AluguelId,
                ItemId = aluguelItem.ItemId
            };

            return aluguelItemDto;
        }
    }
}
