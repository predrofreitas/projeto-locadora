using Locadora.Comuns.Dtos;
using MediatR;

namespace Locadora.WebAPI.Commands.ContextoAluguel
{
    public class DevolverAluguelCommand : IRequest<string>
    {
        public AluguelDto AluguelDto { get; }

        public DevolverAluguelCommand(AluguelDto aluguelDto)
        {
            AluguelDto = aluguelDto;
        }
    }
}
