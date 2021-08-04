using Locadora.Dominio.Entidades;
using Locadora.Dominio.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Locadora.Dados.Repositorios
{
    public class RepositorioMidia : IRepositorioMidia
    {
        private readonly LocadoraContext _locadoraContext;

        public RepositorioMidia(LocadoraContext locadoraContext)
        {
            _locadoraContext = locadoraContext;
        }
        public void Salvar(Item midia)
        {
            _locadoraContext.Add(midia);
        }

        public List<Item> ObterPorCategoria(string categoria)
        {
            var midias = _locadoraContext.Midias.Where(x => x.Categoria == categoria);

            return midias.ToList();
        }

        public Item ObterPorId(int id)
        {
            var midia = _locadoraContext.Midias.FirstOrDefault(x => x.Id == id);

            return midia;
        }

        public Item ObterPorNome(string nome)
        {
            var midia = _locadoraContext.Midias.FirstOrDefault(x => x.Nome == nome);

            return midia;
        }
    }
}