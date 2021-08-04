using Locadora.Dominio.Entidades;
using Locadora.Dominio.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Locadora.Dados.Repositorios
{
    public class RepositorioItem : IRepositorioItem
    {
        private readonly LocadoraContext _locadoraContext;

        public RepositorioItem(LocadoraContext locadoraContext)
        {
            _locadoraContext = locadoraContext;
        }
        public void Salvar(Item item)
        {
            _locadoraContext.Add(item);
        }

        public List<Item> ObterPorCategoria(string categoria)
        {
            var midias = _locadoraContext.Itens.Where(x => x.Categoria == categoria);

            return midias.ToList();
        }

        public Item ObterPorId(int id)
        {
            var midia = _locadoraContext.Itens.FirstOrDefault(x => x.Id == id);

            return midia;
        }

        public Item ObterPorNome(string nome)
        {
            var midia = _locadoraContext.Itens.FirstOrDefault(x => x.Nome == nome);

            return midia;
        }
    }
}