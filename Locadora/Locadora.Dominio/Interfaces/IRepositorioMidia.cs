using Locadora.Dominio.Entidades;
using System.Collections.Generic;

namespace Locadora.Dominio.Interfaces
{
    public interface IRepositorioMidia
    {
        void Salvar(Midia midia);
        Midia ObterPorId(int id);
        Midia ObterPorNome(string nome);
        List<Midia> ObterPorCategoria(string categoria);
    }
}
