using Locadora.Comuns.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locadora.MVC.Services
{
    public interface IClienteService
    {
        public Task createCliente(ClienteDto cliente);
        public Task deleteCliente(int id);
        public Task<ClienteDto> getClienteId(int id);

    }
}
