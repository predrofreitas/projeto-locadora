using Locadora.Dominio.Entidades;

namespace Locadora.Dominio.Interfaces
{
    public interface IRepositorioAluguel
    {
        void Salvar(Aluguel aluguel);
        Aluguel ObterPorId(int id);
    }
}
