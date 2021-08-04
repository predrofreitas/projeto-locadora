using Locadora.Dominio.Entidades;
using Locadora.Dominio.Interfaces;
using System.Linq;

namespace Locadora.Dados.Repositorios
{
    public class RepositorioAluguelItem : IRepositorioAluguelItem
    {
        private readonly LocadoraContext _locadoraContext;

        public RepositorioAluguelItem(LocadoraContext locadoraContext)
        {
            _locadoraContext = locadoraContext;
        }


        public void Salvar(AluguelItem aluguelItem)
        {
            _locadoraContext.Add(aluguelItem);

            _locadoraContext.SaveChanges();
        }

        public AluguelItem ObterPorId(int id)
        {
            var aluguelItem = _locadoraContext.AlugueisItem.FirstOrDefault(x => x.Id == 1);

            return aluguelItem;
        }
    }
}
