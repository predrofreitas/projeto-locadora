using Locadora.Dominio.Entidades;
using Locadora.Dominio.Interfaces;
using Locadora.WebAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Locadora.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly IRepositorioCliente _repositorioCliente;

        public ClienteController(ILogger<ClienteController> logger, IRepositorioCliente repositorioCliente)
        {
            _logger = logger;
            _repositorioCliente = repositorioCliente;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult CriarCliente(ClienteDto clienteDto)
        {
            try
            {
                var cliente = new Cliente();
                cliente.Nome = clienteDto.Nome;
                cliente.DataNascimento = clienteDto.DataNascimento;
                cliente.Cpf = clienteDto.Cpf;

                _repositorioCliente.Salvar(cliente);

                return CreatedAtAction(nameof(CriarCliente), cliente.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, "Erro ao criar cliente");
            }

        }
    }
}
