using Locadora.Dominio.Entidades;

namespace Locadora.Dominio.Interfaces
{
    public interface IRepositorioEstoque
    {
        void Salvar(Estoque estoque);
        void Remover(Estoque estoque);
        void Atualizar(Estoque estoque);
        Estoque BuscarPorId(int id);
        Estoque BuscarPorItemId(int itemId);
    }
}
