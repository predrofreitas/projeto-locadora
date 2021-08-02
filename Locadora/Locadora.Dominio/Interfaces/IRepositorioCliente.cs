using Locadora.Dominio.Entidades;

namespace Locadora.Dominio.Interfaces
{
    public interface IRepositorioCliente
    {
        void Salvar(Cliente cliente);
        Cliente ObterPorId(int id);
        Cliente ObterPorNome(string nome);
    }
}
