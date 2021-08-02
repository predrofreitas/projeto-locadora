using Locadora.Dominio.Entidades;
using Locadora.Dominio.Interfaces;
using System.Linq;

namespace Locadora.Dados.Repositorios
{
    public class RepositorioAluguel : IRepositorioAluguel
    {
        private readonly LocadoraContext _locadoraContext;

        public RepositorioAluguel(LocadoraContext locadoraContext)
        {
            _locadoraContext = locadoraContext;
        }


        public void Salvar(Aluguel aluguel)
        {
            _locadoraContext.Add(aluguel);

            _locadoraContext.SaveChanges();
        }

        public Aluguel ObterPorId(int id)
        {
            var aluguel = _locadoraContext.Alugueis.FirstOrDefault(x => x.Id == 1);

            return aluguel;
        }
    }
}
