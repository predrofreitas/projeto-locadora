using Locadora.Comuns.Dtos;
using MediatR;

namespace Locadora.WebAPI.Commands.ContextoItem
{
    public class AtualizarItemCommand : IRequest
    {
        public ItemDto ItemDto { get; }

        public AtualizarItemCommand(ItemDto itemDto)
        {
            ItemDto = itemDto;
        }
    }
}
