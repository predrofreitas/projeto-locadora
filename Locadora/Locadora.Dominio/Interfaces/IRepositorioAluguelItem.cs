using Locadora.Dominio.Entidades;

namespace Locadora.Dominio.Interfaces
{
    public interface IRepositorioAluguelItem
    {
        void Salvar(AluguelItem aluguelItem);
        AluguelItem ObterPorId(int id);
    }
}
