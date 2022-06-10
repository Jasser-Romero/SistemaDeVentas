using SistemaDeVentas.Domain.Entities;
using SistemaDeVentas.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeVentas.Infrastructure.Repositories
{
    public class EFClienteRepository : IClienteRepository
    {
        private ISistemaDeVentasDBContext sistemaDeVentasDBContext;
        public EFClienteRepository(ISistemaDeVentasDBContext sistemaDeVentasDBContext)
        {
            this.sistemaDeVentasDBContext = sistemaDeVentasDBContext;
        }

        public int Create(Cliente t)
        {
            sistemaDeVentasDBContext.Clientes.Add(t);
            return sistemaDeVentasDBContext.SaveChanges();
        }

        public bool Delete(Cliente t)
        {
            sistemaDeVentasDBContext.Clientes.Remove(t);
            return sistemaDeVentasDBContext.SaveChanges()>0;
        }

        public Cliente FindByEmail(string email)
        {
            return sistemaDeVentasDBContext.Clientes.FirstOrDefault(x => x.Email.Equals(email));
        }

        public Cliente FindById(int id)
        {
            return sistemaDeVentasDBContext.Clientes.FirstOrDefault(x => x.Id == id);
        }

        public Cliente FindByName(string name)
        {
            return sistemaDeVentasDBContext.Clientes.FirstOrDefault(x => x.Nombres.Equals(name));
        }

        public List<Cliente> GetAll()
        {
            return sistemaDeVentasDBContext.Clientes.ToList();
        }

        public int Update(Cliente t)
        {
            sistemaDeVentasDBContext.Clientes.Update(t);
            return sistemaDeVentasDBContext.SaveChanges();
        }
    }
}
