using Locadora.Comuns.Dtos;
using MediatR;

namespace Locadora.WebAPI.Commands.ContextoItem
{
    public class ObterItemPorNomeCommand : IRequest<ItemDto>
    {
        public string Nome { get; }

        public ObterItemPorNomeCommand(string nome)
        {
            Nome = nome;
        }
    }
}
