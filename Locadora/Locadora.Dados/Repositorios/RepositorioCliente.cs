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

        public int Salvar(Cliente cliente)
        {
            _locadoraContext.Add(cliente);
            _locadoraContext.SaveChanges();

            return cliente.Id;
        }

        public void Remover(Cliente cliente)
        {
            _locadoraContext.Remove(cliente);
            _locadoraContext.SaveChanges();
        }

        public void Atualizar(Cliente cliente)
        {

            _locadoraContext.Update(cliente);
            _locadoraContext.SaveChanges();
        }

        public Cliente BuscarPorId(int id)
        {
            var cliente = _locadoraContext.Clientes.FirstOrDefault(x => x.Id == id);

            return cliente;
        }

        public Cliente BuscarPorNome(string nome)
        {
            var clientes = _locadoraContext.Clientes.FirstOrDefault(x => x.Nome == nome);

            return clientes;
        }
    }
}
