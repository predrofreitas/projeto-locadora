using Locadora.Comuns.Dtos;
using MediatR;

namespace Locadora.WebAPI.Commands.ContextoCliente
{
    public class ObterClientePorNomeCommand : IRequest<ClienteDto>
    {
        public string Nome { get; }

        public ObterClientePorNomeCommand(string nome)
        {
            Nome = nome;
        }
    }
}
