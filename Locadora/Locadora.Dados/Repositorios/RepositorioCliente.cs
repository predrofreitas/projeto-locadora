using Locadora.Dominio.Entidades;
using Locadora.Dominio.Interfaces;
using System.Linq;

namespace Locadora.Dados.Repositorios
{
    public class RepositorioCliente : IRepositorioCliente
    {
        private readonly LocadoraContext _locadoraContext; 
        public RepositorioCliente(LocadoraContext locadoraContext)
        {
            _locadoraContext = locadoraContext;
        }

        public void Salvar(Cliente cliente)
        {
            _locadoraContext.Add(cliente);
            _locadoraContext.SaveChanges();
        }

        public Cliente ObterPorId(int id)
        {
            var cliente = _locadoraContext.Clientes.FirstOrDefault(x => x.Id == 1);

            return cliente;
        }

        public Cliente ObterPorNome(string nome)
        {
            var clientes = _locadoraContext.Clientes.FirstOrDefault(x => x.Nome == nome);

            return clientes;
        }
    }
}
