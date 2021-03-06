using Locadora.Comuns.Dtos;
using Locadora.Dados;
using Locadora.Dominio.Entidades;
using Locadora.Dominio.Interfaces;
using Locadora.WebAPI.Commands.ContextoCliente;
using MediatR;
using RabbitMQ.Client;
using System.Threading;
using System.Threading.Tasks;

namespace Locadora.WebAPI.Handlers
{
    public class CadastrarClienteHandler : 
        IRequestHandler<AtualizarClienteCommand>,
        IRequestHandler<DeletarClienteCommand>,
        IRequestHandler<InserirClienteCommand, int>,
        IRequestHandler<ObterClientePorIdCommand, ClienteDto>,
        IRequestHandler<ObterClientePorNomeCommand, ClienteDto>
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

        public async Task<ClienteDto> Handle(ObterClientePorNomeCommand request, CancellationToken cancellationToken)
        {
            var cliente = _repositorioCliente.BuscarPorNome(request.Nome);
            if (cliente == null)
                return null;

            var clienteDto = Map(cliente);
            return clienteDto;
        }

        public async Task<ClienteDto> Handle(ObterClientePorIdCommand request, CancellationToken cancellationToken)
        {
            var cliente = _repositorioCliente.BuscarPorId(request.Id);
            if (cliente == null)
                return null;

            var clienteDto = Map(cliente);
            return clienteDto;
        }

        public async Task<int> Handle(InserirClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = Map(request.ClienteDto);
            int id = 0;

            using (var transacao = _locadoraContext.Database.BeginTransaction())
            {
                id = _repositorioCliente.Salvar(cliente);
                _locadoraContext.SaveChanges();
                transacao.Commit();
            }

            return id;
        }

        public async Task<Unit> Handle(DeletarClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = _repositorioCliente.BuscarPorId(request.Id);

            using (var transacao = _locadoraContext.Database.BeginTransaction())
            {
                _repositorioCliente.Remover(cliente);
                _locadoraContext.SaveChanges();
                transacao.Commit();
            }

            return Unit.Value;
        }

        public async Task<Unit> Handle(AtualizarClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = Map(request.ClienteDto);

            using (var transacao = _locadoraContext.Database.BeginTransaction())
            {
                _repositorioCliente.Atualizar(cliente);
                _locadoraContext.SaveChanges();
                transacao.Commit();
            }

            return Unit.Value;
        }

        //public int Criar(ClienteDto clienteDto)
        //{
        //    var cliente = Map(clienteDto);
        //    int id = 0;

        //    using (var transacao = _locadoraContext.Database.BeginTransaction())
        //    {
        //        id =_repositorioCliente.Salvar(cliente);
        //        _locadoraContext.SaveChanges();
        //        transacao.Commit();
        //    }

        //    return id;

        //    //using (var canal = _rabbitConnection.CreateModel())
        //    //{
        //    //    canal.QueueDeclare(queue: "qu.solicitacao.cadastro.cliente",
        //    //                        durable: false,
        //    //                        exclusive: false,
        //    //                        autoDelete: false,
        //    //                        arguments: null);


        //    //    string mensagem = JsonSerializer.Serialize(clienteDto);
        //    //    var corpo = Encoding.UTF8.GetBytes(mensagem);
        //    //    canal.BasicPublish(exchange: "",
        //    //                        routingKey: "qu.solicitacao.cadastro.cliente",
        //    //                        basicProperties: null,
        //    //                        body: corpo);
        //    //}
        //}

        //public void Atualizar(ClienteDto clienteDto, int id)
        //{
        //    var cliente = Map(clienteDto);

        //    using (var transacao = _locadoraContext.Database.BeginTransaction())
        //    {
        //        cliente.Id = id;
        //        _repositorioCliente.Atualizar(cliente);
        //        _locadoraContext.SaveChanges();
        //        transacao.Commit();
        //    }
        //}

        //public void Remover(int id)
        //{
        //    var cliente = _repositorioCliente.BuscarPorId(id);

        //    using (var transacao = _locadoraContext.Database.BeginTransaction())
        //    {
        //        _repositorioCliente.Remover(cliente);
        //        _locadoraContext.SaveChanges();
        //        transacao.Commit();
        //    }
        //}

        //public ClienteDto BuscarPorId(int id)
        //{
        //    var cliente = _repositorioCliente.BuscarPorId(id);
        //    if (cliente == null)
        //        return null;

        //    var clienteDto = Map(cliente);
        //    return clienteDto;
        //}

        //public ClienteDto BuscarPorNome(string nome)
        //{
        //    var cliente = _repositorioCliente.BuscarPorNome(nome);
        //    if (cliente == null)
        //        return null;

        //    var clienteDto = Map(cliente);
        //    return clienteDto;
        //}

        private Cliente Map(ClienteDto clienteDto)
        {
            var cliente = new Cliente()
            {
                Id = clienteDto.Id,
                Nome = clienteDto.Nome,
                DataNascimento = clienteDto.DataNascimento,
                Cpf = clienteDto.Cpf,
                Email = clienteDto.Email,
                Ativo = clienteDto.Ativo,
                Rua = clienteDto.Rua,
                Numero = clienteDto.Numero,
                Bairro = clienteDto.Bairro,
                Cep = clienteDto.Cep,
                Cidade = clienteDto.Cidade,
                Estado = clienteDto.Estado,
            };

            return cliente;
        }

        private ClienteDto Map(Cliente cliente)
        {
            var clienteDto = new ClienteDto()
            {
                Nome = cliente.Nome,
                DataNascimento = cliente.DataNascimento,
                Cpf = cliente.Cpf,
                Email = cliente.Email,
                Ativo = cliente.Ativo,
                Rua = cliente.Rua,
                Numero = cliente.Numero,
                Bairro = cliente.Bairro,
                Cep = cliente.Cep,
                Cidade = cliente.Cidade,
                Estado = cliente.Estado,
            };

            return clienteDto;
        }
    }
}
