using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Locadora.Dados
{
    public class TransacaoResiliente
    {
        private LocadoraContext _contexto;
        private TransacaoResiliente(LocadoraContext contexto) =>
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));

        public static TransacaoResiliente New(LocadoraContext contexto) =>
            new TransacaoResiliente(contexto);

        public async Task ExecuteAsync(Func<Task> action)
        {
            var strategy = _contexto.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                await using var transacao = await _contexto.Database.BeginTransactionAsync();
                await action();
                await transacao.CommitAsync();
            });
        }
    }
}
