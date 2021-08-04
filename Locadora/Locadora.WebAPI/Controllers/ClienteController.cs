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
    public class ClienteController : ControllerBase
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly IRepositorioCliente _repositorioCliente;
        private readonly LocadoraContext _locadoraContext;
        private readonly IConnection _rabbitConnection;

        public ClienteController(ILogger<ClienteController> logger,
            IRepositorioCliente repositorioCliente,
            LocadoraContext locadoraContext,
            IConnection rabbitConnection)
        {
            _logger = logger;
            _locadoraContext = locadoraContext;
            _repositorioCliente = repositorioCliente;
            _rabbitConnection = rabbitConnection;
        }

        [HttpPost]
        public IActionResult CriarCliente(ClienteDto clienteDto)
        {
            try
            {
                var cadastrarCliente = new CadastrarClienteHandler(_locadoraContext, _repositorioCliente, _rabbitConnection);
                cadastrarCliente.Criar(clienteDto);
                return CreatedAtAction(nameof(CriarCliente), Guid.NewGuid());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, "Erro ao criar cliente");
            }
        }

        [HttpDelete]
        [Route("remover")]
        public IActionResult Remover(int id)
        {
            try
            {
                var cadastrarCliente = new CadastrarClienteHandler(_locadoraContext, _repositorioCliente, _rabbitConnection);
                cadastrarCliente.Remover(id);

                return Ok("Cliente deletado com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, "Erro ao criar cliente");
            }
        }
    }
}
