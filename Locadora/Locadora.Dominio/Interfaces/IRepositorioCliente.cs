using Locadora.Dominio.Entidades;

namespace Locadora.Dominio.Interfaces
{
    public interface IRepositorioCliente
    {
        void Salvar(Cliente cliente);
        void Remover(Cliente cliente);
        void Atualizar(Cliente cliente);
        Cliente BuscarPorId(int id);
        Cliente BuscarPorNome(string nome);
    }
}
