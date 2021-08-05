using Locadora.Comuns.Dtos;
using Locadora.WebAPI.Commands.ContextoAluguel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Locadora.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AluguelController : ControllerBase
    {
        private readonly ILogger<AluguelController> _logger;
        private readonly IMediator _mediator;

        public AluguelController(ILogger<AluguelController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("reserva")]
        public async Task<IActionResult> PostReserva(AluguelDto aluguelDto)
        {
            try
            {
                var command = new ReservarAluguelCommand(aluguelDto);
                await _mediator.Send(command);

                return Ok("Reserva de aluguel feita com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, "Erro ao criar cliente.");
            }
        }

        [HttpPut]
        [Route("processamento")]
        public async Task<IActionResult> PostAluguel(AluguelDto aluguelDto)
        {
            try
            {
                var command = new ReservarAluguelCommand(aluguelDto);
                await _mediator.Send(command);

                return Ok("Aluguel atendido e já enviado!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, "Erro ao criar cliente.");
            }
        }

        [HttpPost]
        [Route("devolucao")]
        public async Task<IActionResult> PostDevolucao(AluguelDto aluguelDto)
        {
            try
            {
                var command = new DevolverAluguelCommand(aluguelDto);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, "Erro ao criar cliente.");
            }
        }
    }
}
