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
    public class CadastrarItemHandler
    {
        private readonly LocadoraContext _locadoraContext;
        private readonly IRepositorioItem _repositorioItem;
        private readonly IRepositorioEstoque _repositorioEstoque;
        private readonly IConnection _rabbitConnection;

        public CadastrarItemHandler(LocadoraContext locadoraContext,
            IRepositorioItem repositorioItem,
            IRepositorioEstoque repositorioEstoque,
            IConnection rabbitConnection)
        {
            _locadoraContext = locadoraContext;
            _repositorioItem = repositorioItem;
            _rabbitConnection = rabbitConnection;
            _repositorioEstoque = repositorioEstoque;
        }

        public ItemDto Criar(ItemDto itemDto)
        {
            var item = Map(itemDto);

            using (var transacao = _locadoraContext.Database.BeginTransaction())
            {
                itemDto.Id = _repositorioItem.Salvar(item);
                _repositorioEstoque.Salvar(new Estoque { Quantidade = 3, Item = item });
                _locadoraContext.SaveChanges();
                transacao.Commit();
            }

            return itemDto;
        }

        public void Atualizar(ItemDto itemDto, int id)
        {
            var item = Map(itemDto);

            using (var transacao = _locadoraContext.Database.BeginTransaction())
            {
                item.Id = id;
                _repositorioItem.Atualizar(item);
                _locadoraContext.SaveChanges();
                transacao.Commit();
            }
        }

        public void Remover(int id)
        {
            var item = _repositorioItem.BuscarPorId(id);

            using (var transacao = _locadoraContext.Database.BeginTransaction())
            {
                _repositorioEstoque.RemoverPorItemId(id);
                _repositorioItem.Remover(item);
                _locadoraContext.SaveChanges();
                transacao.Commit();
            }
        }

        public ItemDto BuscarPorId(int id)
        {
            var item = _repositorioItem.BuscarPorId(id);
            if (item == null)
                return null;

            return Map(item);
        }

        public ItemDto BuscarPorNome(string nome)
        {
            var item = _repositorioItem.BuscarPorNome(nome);
            if (item == null)
                return null;

            return Map(item);
        }

        public IEnumerable<ItemDto> BuscarPorCategoria(string categoria)
        {
            var itens = _repositorioItem.BuscarPorCategoria(categoria).ToList();
            if (itens == null)
                return null;

            List<ItemDto> itensDto = new List<ItemDto>(itens.Count);

            itens.ForEach(item => itensDto.Add(Map(item)));

            return itensDto;
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
