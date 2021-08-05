using Locadora.Comuns.Dtos;
using MediatR;

namespace Locadora.WebAPI.Commands.ContextoItem
{
    public class ObterItemPorIdCommand : IRequest<ItemDto>
    {
        public int Id { get; }

        public ObterItemPorIdCommand(int id)
        {
            Id = id;
        }
    }
}
