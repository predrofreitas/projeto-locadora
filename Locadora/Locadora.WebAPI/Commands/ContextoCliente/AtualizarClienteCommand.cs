using Locadora.Comuns.Dtos;
using MediatR;

namespace Locadora.WebAPI.Commands.ContextoCliente
{
    public class AtualizarItemCommand : IRequest
    {
        public ClienteDto ClienteDto { get; }

        public AtualizarItemCommand(ClienteDto clienteDto)
        {
            ClienteDto = clienteDto;
        }
    }
}
