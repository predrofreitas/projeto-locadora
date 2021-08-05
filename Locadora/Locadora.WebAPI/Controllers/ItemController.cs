using Locadora.Comuns.Dtos;
using Locadora.WebAPI.Commands.ContextoItem;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Locadora.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IMediator _mediator;

        public ItemController(ILogger<ItemController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ItemDto itemDto)
        {
            try
            {
                var command = new InserirItemCommand(itemDto);
                var result = await _mediator.Send(command);

                return CreatedAtAction(nameof(Post), result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, "Erro ao criar item");
            }
        }

        [HttpGet("{id}")]
        public async Task<ItemDto> Get(int id)
        {
            var command = new ObterItemPorIdCommand(id);
            var result = await _mediator.Send(command);

            return result;
        }

        [HttpGet("nome/{nome}")]
        public async Task<ItemDto> Get(string nome)
        {
            var command = new ObterItemPorNomeCommand(nome);
            var result = await _mediator.Send(command);

            return result;
        }

        [HttpGet("categoria/{categoria}")]
        public async Task<IEnumerable<ItemDto>> GetByCategoria(string categoria)
        {
            var command = new ObterItemPorCategoriaCommand(categoria);
            var result = await _mediator.Send(command);

            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ItemDto item)
        {
            try
            {
                item.Id = id;
                var command = new AtualizarItemCommand(item);
                await _mediator.Send(command);

                return Ok("Dados do Item Atualizado.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao atualizar item.");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            try
            {
                var command = new DeletarItemCommand(id);
                await _mediator.Send(command);

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
