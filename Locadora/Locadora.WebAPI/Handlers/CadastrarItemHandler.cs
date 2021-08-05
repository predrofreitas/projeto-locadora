using Locadora.Comuns.Dtos;
using Locadora.Dados;
using Locadora.Dominio.Entidades;
using Locadora.Dominio.Interfaces;
using Locadora.WebAPI.Commands.ContextoItem;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Locadora.WebAPI.Handlers
{
    public class CadastrarItemHandler :
        IRequestHandler<AtualizarItemCommand>,
        IRequestHandler<DeletarItemCommand>,
        IRequestHandler<InserirItemCommand, ItemDto>,
        IRequestHandler<ObterItemPorCategoriaCommand, IEnumerable<ItemDto>>,
        IRequestHandler<ObterItemPorIdCommand, ItemDto>,
        IRequestHandler<ObterItemPorNomeCommand, ItemDto>
    {
        private readonly LocadoraContext _locadoraContext;
        private readonly IRepositorioItem _repositorioItem;
        private readonly IRepositorioEstoque _repositorioEstoque;

        public CadastrarItemHandler(LocadoraContext locadoraContext,
            IRepositorioItem repositorioItem,
            IRepositorioEstoque repositorioEstoque)
        {
            _locadoraContext = locadoraContext;
            _repositorioItem = repositorioItem;
            _repositorioEstoque = repositorioEstoque;
        }

        public async Task<Unit> Handle(AtualizarItemCommand request, CancellationToken cancellationToken)
        {
            var item = Map(request.ItemDto);

            using (var transacao = _locadoraContext.Database.BeginTransaction())
            {
                item.Id = request.ItemDto.Id;
                _repositorioItem.Atualizar(item);
                _locadoraContext.SaveChanges();
                transacao.Commit();
            }

            return Unit.Value;
        }

        public async Task<Unit> Handle(DeletarItemCommand request, CancellationToken cancellationToken)
        {
            var item = _repositorioItem.BuscarPorId(request.Id);

            using (var transacao = _locadoraContext.Database.BeginTransaction())
            {
                _repositorioEstoque.RemoverPorItemId(request.Id);
                _repositorioItem.Remover(item);
                _locadoraContext.SaveChanges();
                transacao.Commit();
            }

            return Unit.Value;
        }

        public async Task<ItemDto> Handle(InserirItemCommand request, CancellationToken cancellationToken)
        {
            var item = Map(request.ItemDto);

            using (var transacao = _locadoraContext.Database.BeginTransaction())
            {
                request.ItemDto.Id = _repositorioItem.Salvar(item);
                _repositorioEstoque.Salvar(new Estoque { Quantidade = 3, Item = item });
                _locadoraContext.SaveChanges();
                transacao.Commit();
            }

            return request.ItemDto;
        }

        public async Task<IEnumerable<ItemDto>> Handle(ObterItemPorCategoriaCommand request, CancellationToken cancellationToken)
        {
            var itens = _repositorioItem.BuscarPorCategoria(request.Categoria).ToList();
            if (itens == null)
                return null;

            List<ItemDto> itensDto = new List<ItemDto>(itens.Count);

            itens.ForEach(item => itensDto.Add(Map(item)));

            return itensDto;
        }

        public async Task<ItemDto> Handle(ObterItemPorIdCommand request, CancellationToken cancellationToken)
        {
            var item = _repositorioItem.BuscarPorId(request.Id);
            if (item == null)
                return null;

            return Map(item);
        }

        public async Task<ItemDto> Handle(ObterItemPorNomeCommand request, CancellationToken cancellationToken)
        {
            var item = _repositorioItem.BuscarPorNome(request.Nome);
            if (item == null)
                return null;

            return Map(item);
        }

        public Item Map(ItemDto itemDto)
        {
            var item = new Item()
            {
                Nome = itemDto.Nome,
                Descricao = itemDto.Descricao,
                TipoMidia = itemDto.TipoMidia,
                Categoria = itemDto.Categoria,
                Preco = itemDto.Preco
            };

            return item;
        }

        public ItemDto Map(Item item)
        {
            var itemDto = new ItemDto()
            {
                Id = item.Id,
                Nome = item.Nome,
                Descricao = item.Descricao,
                TipoMidia = item.TipoMidia,
                Categoria = item.Categoria,
                Preco = item.Preco
            };

            return itemDto;
        }
    }
}
