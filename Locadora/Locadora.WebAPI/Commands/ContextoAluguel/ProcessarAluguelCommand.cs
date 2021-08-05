using Locadora.Comuns.Dtos;
using MediatR;

namespace Locadora.WebAPI.Commands.ContextoAluguel
{
    public class ProcessarAluguelCommand : IRequest
    {
        public AluguelDto AluguelDto { get; }

        public ProcessarAluguelCommand(AluguelDto aluguelDto)
        {
            AluguelDto = aluguelDto;
        }
    }
}
