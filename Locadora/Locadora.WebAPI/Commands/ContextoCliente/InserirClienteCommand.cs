using Locadora.Comuns.Dtos;
using MediatR;

namespace Locadora.WebAPI.Commands.ContextoCliente
{
    public class InserirClienteCommand : IRequest<int>
    {
        public ClienteDto ClienteDto { get; }

        public InserirClienteCommand(ClienteDto clienteDto)
        {
            ClienteDto = clienteDto;
        }
    }
}
