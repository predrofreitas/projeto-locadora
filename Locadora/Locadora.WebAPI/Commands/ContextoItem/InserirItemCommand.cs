using Locadora.Comuns.Dtos;
using MediatR;

namespace Locadora.WebAPI.Commands.ContextoItem
{
    public class InserirItemCommand : IRequest<ItemDto>
    {
        public ItemDto ItemDto { get; set; }

        public InserirItemCommand(ItemDto itemDto)
        {
            ItemDto = itemDto;
        }
    }
}
