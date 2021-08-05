using Locadora.Comuns.Dtos;
using MediatR;

namespace Locadora.WebAPI.Commands.ContextoCliente
{
    public class ObterClientePorIdCommand : IRequest<ClienteDto>
    {
        public int Id { get; }

        public ObterClientePorIdCommand(int id)
        {
            Id = id;
        }
    }
}
