using Locadora.Comuns.Dtos;
using MediatR;

namespace Locadora.WebAPI.Commands.ContextoItem
{
    public class ObterItemPorCategoriaCommand : IRequest<ItemDto>
    {
        public string Categoria { get; }

        public ObterItemPorCategoriaCommand(string categoria)
        {
            Categoria = categoria;
        }
    }
}
