using System;
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

        [HttpDelete]
        [Route("remover")]
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
