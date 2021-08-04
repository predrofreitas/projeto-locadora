﻿using System;
using System.Collections.Generic;
using Locadora.Comuns.Dtos;
using Locadora.Dados;
using Locadora.Dominio.Interfaces;
using Locadora.WebAPI.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Locadora.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IRepositorioItem _repositorioItem;
        private readonly LocadoraContext _locadoraContext;
        private readonly IConnection _rabbitConnection;

        public ItemController(ILogger<ItemController> logger,
            IRepositorioItem repositorioItem,
            LocadoraContext locadoraContext,
            IConnection rabbitConnection)
        {
            _logger = logger;
            _locadoraContext = locadoraContext;
            _repositorioItem = repositorioItem;
            _rabbitConnection = rabbitConnection;
        }

        [HttpPost]
        public IActionResult CriarItem(ItemDto itemDto)
        {
            try
            {
                var cadastrarItem = new CadastrarItemHandler(_locadoraContext, _repositorioItem, _rabbitConnection);
                cadastrarItem.Criar(itemDto);
                return CreatedAtAction(nameof(CriarItem), Guid.NewGuid());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, "Erro ao criar item");
            }
        }

        [HttpGet("{id}")]
        public ItemDto Get(int id)
        {
            var cadastrarItem = new CadastrarItemHandler(_locadoraContext, _repositorioItem, _rabbitConnection);
            var item = cadastrarItem.BuscarPorId(id);
            return item;
        }

        [HttpGet("nome/{nome}")]
        public ItemDto Get(string nome)
        {
            var cadastrarItem = new CadastrarItemHandler(_locadoraContext, _repositorioItem, _rabbitConnection);
            var item = cadastrarItem.BuscarPorNome(nome);
            return item;
        }

        [HttpGet("categoria/{categoria}")]
        public IEnumerable<ItemDto> GetByCategoria(string categoria)
        {
            var cadastrarItem = new CadastrarItemHandler(_locadoraContext, _repositorioItem, _rabbitConnection);
            var itens = cadastrarItem.BuscarPorCategoria(categoria);
            return itens;
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Remover(int id)
        {
            try
            {
                var cadastrarItem = new CadastrarItemHandler(_locadoraContext, _repositorioItem, _rabbitConnection);
                cadastrarItem.Remover(id);

                return Ok("Item deletado com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, "Erro ao remover item");
            }
        }
    }
}