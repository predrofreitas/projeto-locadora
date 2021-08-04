using Locadora.Dominio.Entidades;
using System.Collections.Generic;

namespace Locadora.Dominio.Interfaces
{
    public interface IRepositorioMidia
    {
        void Salvar(Item midia);
        Item ObterPorId(int id);
        Item ObterPorNome(string nome);
        List<Item> ObterPorCategoria(string categoria);
    }
}
