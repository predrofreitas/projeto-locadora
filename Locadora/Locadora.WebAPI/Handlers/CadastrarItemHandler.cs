using Locadora.Comuns.Dtos;
using Locadora.Dados;
using Locadora.Dominio.Entidades;
using Locadora.Dominio.Interfaces;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Locadora.WebAPI.Handlers
{
    public class CadastrarItemHandler
    {
        private readonly LocadoraContext _locadoraContext;
        private readonly IRepositorioItem _repositorioItem;
        private readonly IConnection _rabbitConnection;

        public CadastrarItemHandler(LocadoraContext locadoraContext,
            IRepositorioItem repositorioItem,
            IConnection rabbitConnection)
        {
            _locadoraContext = locadoraContext;
            _repositorioItem = repositorioItem;
            _rabbitConnection = rabbitConnection;
        }

        public void Criar(ItemDto itemDto)
        {
            var item = Map(itemDto);

            using (var transacao = _locadoraContext.Database.BeginTransaction())
            {
                _repositorioItem.Salvar(item);
                _locadoraContext.SaveChanges();
                transacao.Commit();
            }

            using (var canal = _rabbitConnection.CreateModel())
            {
                canal.QueueDeclare(queue: "qu.solicitacao.cadastro.item",
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);


                string mensagem = JsonSerializer.Serialize(itemDto);
                var corpo = Encoding.UTF8.GetBytes(mensagem);
                canal.BasicPublish(exchange: "",
                                    routingKey: "qu.solicitacao.cadastro.item",
                                    basicProperties: null,
                                    body: corpo);
            }
        }

        public void Atualizar(ItemDto itemDto)
        {
            var item = Map(itemDto);

            using (var transacao = _locadoraContext.Database.BeginTransaction())
            {
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
            var itens = _repositorioItem.BuscarPorCategoria(categoria);
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
