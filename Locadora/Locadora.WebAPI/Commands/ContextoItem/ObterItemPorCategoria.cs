using Locadora.Comuns.Dtos;
using MediatR;
using System.Collections.Generic;

namespace Locadora.WebAPI.Commands.ContextoItem
{
    public class ObterItemPorCategoriaCommand : IRequest<IEnumerable<ItemDto>>
    {
        public string Categoria { get; }

        public ObterItemPorCategoriaCommand(string categoria)
        {
            Categoria = categoria;
        }
    }
}
