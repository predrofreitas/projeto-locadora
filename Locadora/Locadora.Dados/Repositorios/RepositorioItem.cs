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

        public void Remover(Item item)
        {
            _locadoraContext.Remove(item);
            _locadoraContext.SaveChanges();
        }

        public void Atualizar(Item item)
        {
            _locadoraContext.Update(item);
            _locadoraContext.SaveChanges();
        }

        public List<Item> BuscarPorCategoria(string categoria)
        {
            var midias = _locadoraContext.Itens.Where(x => x.Categoria == categoria);

            return midias.ToList();
        }

        public Item BuscarPorId(int id)
        {
            var midia = _locadoraContext.Itens.FirstOrDefault(x => x.Id == id);

            return midia;
        }

        public Item BuscarPorNome(string nome)
        {
            var midia = _locadoraContext.Itens.FirstOrDefault(x => x.Nome == nome);

            return midia;
        }
    }
}