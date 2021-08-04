using Locadora.Dados;
using Locadora.Dominio.Entidades;
using Locadora.Dominio.Interfaces;
using Locadora.WebAPI.Dtos;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Locadora.WebAPI.Handlers
{
    public class CadastrarClienteHandler
    {
        private readonly LocadoraContext _locadoraContext;
        private readonly IRepositorioCliente _repositorioCliente;
        private readonly IConnection _rabbitConnection;

        public CadastrarClienteHandler(LocadoraContext locadoraContext,
            IRepositorioCliente repositorioCliente,
            IConnection rabbitConnection)
        {
            _locadoraContext = locadoraContext;
            _repositorioCliente = repositorioCliente;
            _rabbitConnection = rabbitConnection;
        }

        public void Criar(ClienteDto clienteDto)
        {
            var cliente = new Cliente(clienteDto.Nome, clienteDto.DataNascimento, clienteDto.Cpf, clienteDto.Email, false);

            using (var transacao = _locadoraContext.Database.BeginTransaction())
            {
                _repositorioCliente.Salvar(cliente);
                _locadoraContext.SaveChanges();
                transacao.Commit();
            }

            using (var canal = _rabbitConnection.CreateModel())
            {
                canal.QueueDeclare(queue: "qu.solicitacao.cadastro.cliente",
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);


                string mensagem = JsonSerializer.Serialize(clienteDto);
                var corpo = Encoding.UTF8.GetBytes(mensagem);
                canal.BasicPublish(exchange: "",
                                    routingKey: "qu.solicitacao.cadastro.cliente",
                                    basicProperties: null,
                                    body: corpo);
            }
        }

        public void Atualizar(ClienteDto clienteDto)
        {
            var cliente = new Cliente(clienteDto.Nome, clienteDto.DataNascimento, clienteDto.Cpf, clienteDto.Email, false);

            using (var transacao = _locadoraContext.Database.BeginTransaction())
            {
                _repositorioCliente.Atualizar(cliente);
                _locadoraContext.SaveChanges();
                transacao.Commit();
            }
        }

        public void Remover(int id)
        {
            var cliente = _repositorioCliente.BuscarPorId(id);

            using (var transacao = _locadoraContext.Database.BeginTransaction())
            {
                _repositorioCliente.Remover(cliente);
                _locadoraContext.SaveChanges();
                transacao.Commit();
            }
        }

        public Cliente BuscarPorId(int id)
        {
            return _repositorioCliente.BuscarPorId(id);
        }

        public Cliente BuscarPorNome(string nome)
        {
            return _repositorioCliente.BuscarPorNome(nome);
        }
    }
}
