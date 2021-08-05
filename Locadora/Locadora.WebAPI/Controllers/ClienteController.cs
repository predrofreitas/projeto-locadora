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
        public IActionResult Post(ClienteDto clienteDto)
        {
            try
            {
                var cadastrarCliente = new CadastrarClienteHandler(_locadoraContext, _repositorioCliente, _rabbitConnection);
                var id = cadastrarCliente.Criar(clienteDto);
                return CreatedAtAction(nameof(Post), id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, "Erro ao criar cliente.");
            }
        }

        [HttpGet("{id}")]
        public ClienteDto Get(int id)
        {
            var cadastrarCliente = new CadastrarClienteHandler(_locadoraContext, _repositorioCliente, _rabbitConnection);
            var cliente = cadastrarCliente.BuscarPorId(id);
            return cliente;
        }

        [HttpGet("nome/{nome}")]
        public ClienteDto Get(string nome)
        {
            var cadastrarCliente = new CadastrarClienteHandler(_locadoraContext, _repositorioCliente, _rabbitConnection);
            var cliente = cadastrarCliente.BuscarPorNome(nome);
            return cliente;
        }

        //Necessário preencher todos os campos do cliente.
        [HttpPost("{id}")]
        public IActionResult Post(int id, [FromBody] ClienteDto cliente)
        {
            try
            {
                var cadastrarCliente = new CadastrarClienteHandler(_locadoraContext, _repositorioCliente, _rabbitConnection);
                cadastrarCliente.Atualizar(cliente, id);
                return Ok("Dados do Cliente Atualizado.");
            }
            catch(Exception)
            {
                return StatusCode(500, "Erro ao atualizar cliente.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
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
                return StatusCode(500, "Erro ao deletar cliente.");
            }
        }
    }
}
