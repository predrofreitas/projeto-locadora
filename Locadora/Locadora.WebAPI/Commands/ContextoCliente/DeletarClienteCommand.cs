using MediatR;

namespace Locadora.WebAPI.Commands.ContextoCliente
{
    public class DeletarClienteCommand : IRequest
    {
        public int Id { get; }

        public DeletarClienteCommand(int id)
        {
            Id = id;
        }
    }
}
