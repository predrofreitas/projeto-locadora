using Locadora.Comuns.Dtos;
using MediatR;

namespace Locadora.WebAPI.Commands.ContextoCliente
{
    public class AtualizarClienteCommand : IRequest
    {
        public ClienteDto ClienteDto { get; }

        public AtualizarClienteCommand(ClienteDto clienteDto)
        {
            ClienteDto = clienteDto;
        }
    }
}