using MediatR;

namespace Locadora.WebAPI.Commands.ContextoItem
{
    public class DeletarItemCommand : IRequest
    {
        public int Id { get; }

        public DeletarItemCommand(int id)
        {
            Id = id;
        }
    }
}
