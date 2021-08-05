using Locadora.Dominio.Entidades;
using System.Collections.Generic;

namespace Locadora.Dominio.Interfaces
{
    public interface IRepositorioItem
    {
        void Salvar(Item item);
        void Remover(Item item);
        void Atualizar(Item item);
        Item BuscarPorId(int id);
        Item BuscarPorNome(string nome);
        IEnumerable<Item> BuscarPorCategoria(string categoria);
    }
}
