using Locadora.Dominio.Entidades;
using Locadora.Dominio.Interfaces;
using System.Linq;

namespace Locadora.Dados.Repositorios
{
    public class RepositorioEstoque : IRepositorioEstoque
    {
        private readonly LocadoraContext _locadoraContext;

        public RepositorioEstoque(LocadoraContext locadoraContext)
        {
            _locadoraContext = locadoraContext;
        }
        public void Salvar(Estoque estoque)
        {
            _locadoraContext.Add(estoque);
        }

        public void Remover(Estoque estoque)
        {
            _locadoraContext.Remove(estoque);
            _locadoraContext.SaveChanges();
        }

        public void RemoverPorItemId(int itemId)
        {
            var estoque = _locadoraContext.Estoques.FirstOrDefault(x => x.ItemId == itemId);
            _locadoraContext.Remove(estoque);
            _locadoraContext.SaveChanges();
        }

        public void Atualizar(Estoque estoque)
        {
            _locadoraContext.Update(estoque);
            _locadoraContext.SaveChanges();
        }

        public Estoque BuscarPorId(int id)
        {
            var estoque = _locadoraContext.Estoques.FirstOrDefault(x => x.Id == id);

            return estoque;
        }

        public Estoque BuscarPorItemId(int itemId)
        {
            var estoque = _locadoraContext.Estoques.FirstOrDefault(x => x.Item.Id == itemId);

            return estoque;
        }

    }
}