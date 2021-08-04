using Locadora.Comuns.Dtos;
using Locadora.Dados;
using Locadora.Dominio.Entidades;
using Locadora.Dominio.Interfaces;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Locadora.WebAPI.Handlers
{
    public class AlugarHandler
    {
        private readonly LocadoraContext _locadoraContext;
        private readonly IRepositorioAluguel _repositorioAluguel;
        private readonly IRepositorioAluguelItem _repositorioAluguelItem;
        private readonly IRepositorioEstoque _repositorioEstoque;
        private readonly IConnection _rabbitConnection;

        public AlugarHandler(LocadoraContext locadoraContext,
            IRepositorioAluguel repositorioAluguel,
            IRepositorioAluguelItem repositorioAluguelItem,
            IConnection rabbitConnection)
        {
            _locadoraContext = locadoraContext;
            _repositorioAluguel = repositorioAluguel;
            _repositorioAluguelItem = repositorioAluguelItem;
            _rabbitConnection = rabbitConnection;
        }

        //public void CriarAluguel(List<Item> itens)
        //{
        //    var aluguel = new Aluguel();
        //    var estoque = new Estoque();

        //    itens.ForEach(item => )


        //    using (var transacao = _locadoraContext.Database.BeginTransaction())
        //    {
        //        _repositorioAluguel.Salvar(aluguel);
        //        _locadoraContext.SaveChanges();
        //        transacao.Commit();
        //    }

        //    using (var canal = _rabbitConnection.CreateModel())
        //    {
        //        canal.QueueDeclare(queue: "qu.solicitacao.cadastro.aluguel",
        //                            durable: false,
        //                            exclusive: false,
        //                            autoDelete: false,
        //                            arguments: null);


        //        string mensagem = JsonSerializer.Serialize(aluguelDto);
        //        var corpo = Encoding.UTF8.GetBytes(mensagem);
        //        canal.BasicPublish(exchange: "",
        //                            routingKey: "qu.solicitacao.cadastro.aluguel",
        //                            basicProperties: null,
        //                            body: corpo);
        //    }
        //}

        /*   public void Atualizar(ItemDto itemDto)
           {
               var item = Map(itemDto);

               using (var transacao = _locadoraContext.Database.BeginTransaction())
               {
                   _repositorioItem.Atualizar(item);
                   _locadoraContext.SaveChanges();
                   transacao.Commit();
               }
           }
   */
        /*      public void Remover(int id)
              {
                  var item = _repositorioItem.BuscarPorId(id);

                  using (var transacao = _locadoraContext.Database.BeginTransaction())
                  {
                      _repositorioItem.Remover(item);
                      _locadoraContext.SaveChanges();
                      transacao.Commit();
                  }
              }
        */

        /*      public ItemDto BuscarPorId(int id)
              {
                  var item = _repositorioItem.BuscarPorId(id);
                  if (item == null)
                      return null;

                  return Map(item);
              }
        */


        public Aluguel Map(AluguelDto aluguelDto)
        {
            var aluguel = new Aluguel()
            {
                DataPedido = aluguelDto.DataPedido,
                DataDevolucao = aluguelDto.DataDevolucao
            };

            return aluguel;
        }

        public AluguelDto Map(Aluguel aluguel)
        {
            var aluguelDto = new AluguelDto()
            {
                DataPedido = aluguel.DataPedido,
                DataDevolucao = aluguel.DataDevolucao
            };
            return aluguelDto;
        
        }
    }
}
