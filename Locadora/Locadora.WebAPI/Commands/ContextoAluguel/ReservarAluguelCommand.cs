using Locadora.Comuns.Dtos;
using MediatR;

namespace Locadora.WebAPI.Commands.ContextoAluguel
{
    public class ReservarAluguelCommand : IRequest
    {
        public AluguelDto AluguelDto { get; }

        public ReservarAluguelCommand(AluguelDto aluguelDto)
        {
            AluguelDto = aluguelDto;
        }
    }
}
