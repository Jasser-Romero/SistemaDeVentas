using SistemaDeVentas.AppCore.Interfaces;
using SistemaDeVentas.Domain.Entities;
using SistemaDeVentas.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeVentas.AppCore.Services
{
    public class ClienteService : IClienteService
    {
        private IClienteRepository clienteRepository;
        public ClienteService(IClienteRepository clienteRepository)
        {
            this.clienteRepository = clienteRepository;
        }
        public int Create(Cliente t)
        {
            return clienteRepository.Create(t);
        }

        public int Delete(Cliente t)
        {
            return clienteRepository.Delete(t);
        }

        public List<Cliente> GetAll()
        {
            return clienteRepository.GetAll();
        }

        public int Update(Cliente t)
        {
            return clienteRepository.Update(t);
        }
    }
}
