using Locadora.WebAPI.Commands.ContextoCliente;
using Microsoft.AspNetCore.Mvc;
using Locadora.WebAPI.Handlers;
using Locadora.Comuns.Dtos;
using Microsoft.Extensions.Logging;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Locadora.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly IMediator _mediator;

        public ClienteController(ILogger<ClienteController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ClienteDto clienteDto)
        {
            try
            {
                var command = new InserirClienteCommand(clienteDto);
                var result = await _mediator.Send(command);

                return CreatedAtAction(nameof(Post), result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, "Erro ao criar cliente.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ClienteDto> Get(int id)
        {
            var command = new ObterClientePorIdCommand(id);
            var result = await _mediator.Send(command);

            return result;
        }

        [HttpGet("nome/{nome}")]
        public async Task<ClienteDto> Get(string nome)
        {
            var command = new ObterClientePorNomeCommand(nome);
            var result = await _mediator.Send(command);

            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([Required] int id, [Required][FromBody] ClienteDto clienteDto)
        {
            try
            {
                clienteDto.Id = id;
                var command = new AtualizarClienteCommand(clienteDto);
                await _mediator.Send(command);

                return Ok("Dados do Cliente Atualizado.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao atualizar cliente.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var command = new DeletarClienteCommand(id);
                await _mediator.Send(command);

                return Ok("Cliente deletado com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, "Erro ao deletar cliente.");
            }
        }
    }
}