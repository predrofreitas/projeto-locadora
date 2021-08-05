using Locadora.Comuns.Dtos;
using Locadora.Dados;
using Locadora.Dominio.Interfaces;
using Locadora.WebAPI.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;

namespace Locadora.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AluguelController : ControllerBase
    {
        private readonly ILogger<AluguelController> _logger;
        private readonly IRepositorioCliente _repositorioCliente;
        private readonly IRepositorioAluguel _repositorioAluguel;
        private readonly IRepositorioAluguelItem _repositorioAluguelItem;
        private readonly IRepositorioEstoque _repositorioEstoque;
        private readonly IRepositorioItem _repositorioItem;
        private readonly LocadoraContext _locadoraContext;
        private readonly IConnection _rabbitConnection;

        public AluguelController(ILogger<AluguelController> logger,
            IRepositorioCliente repositorioCliente,
            IRepositorioAluguel repositorioAluguel,
            IRepositorioAluguelItem repositorioAluguelItem,
            IRepositorioEstoque repositorioEstoque,
            IRepositorioItem repositorioItem,
        LocadoraContext locadoraContext,
            IConnection rabbitConnection)
        {
            _logger = logger;
            _locadoraContext = locadoraContext;
            _repositorioCliente = repositorioCliente;
            _repositorioAluguel = repositorioAluguel;
            _repositorioAluguelItem = repositorioAluguelItem;
            _repositorioEstoque = repositorioEstoque;
            _repositorioItem = repositorioItem;
            _rabbitConnection = rabbitConnection;
        }

        [HttpPost]
        [Route("reserva")]
        public IActionResult PostReserva(AluguelDto aluguelDto)
        {
            try
            {
                var alugarHandler = new AlugarHandler(_locadoraContext, _repositorioAluguel, _repositorioAluguelItem, _repositorioEstoque, _repositorioCliente, _repositorioItem, _rabbitConnection);
                alugarHandler.CriarReserva(aluguelDto);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, "Erro ao criar cliente.");
            }
        }

        [HttpPost]
        [Route("processamento")]
        public IActionResult PostAluguel(AluguelDto aluguelDto)
        {
            try
            {
                var alugarHandler = new AlugarHandler(_locadoraContext, _repositorioAluguel, _repositorioAluguelItem, _repositorioEstoque, _repositorioCliente, _repositorioItem, _rabbitConnection);
                alugarHandler.CriarAluguel(aluguelDto);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, "Erro ao criar cliente.");
            }
        }

        [HttpPost]
        [Route("devolucao")]
        public IActionResult PostDevolucao(AluguelDto aluguelDto)
        {
            try
            {
                var alugarHandler = new AlugarHandler(_locadoraContext, _repositorioAluguel, _repositorioAluguelItem, _repositorioEstoque, _repositorioCliente, _repositorioItem, _rabbitConnection);
                var mensagem = alugarHandler.DevolverAluguel(aluguelDto);
                return Ok(mensagem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, "Erro ao criar cliente.");
            }
        }
    }
}
