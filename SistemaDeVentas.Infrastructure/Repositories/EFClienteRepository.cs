using Microsoft.EntityFrameworkCore.Storage;
using SistemaDeVentas.Domain.Entities;
using SistemaDeVentas.Domain.Interfaces;
using SistemaDeVentas.Domain.SistemaDeVentasEntities;
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
            try
            {
                sistemaDeVentasDBContext.Clientes.Add(t);
                return sistemaDeVentasDBContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public bool Delete(Cliente t)
        {
            try
            {
                if (t == null)
                {
                    throw new ArgumentNullException("El campo Producto no puede ser null.");
                }

                Cliente cliente = FindById(t.Id);
                if (cliente == null)
                {
                    throw new Exception($"El cliente con id {t.Id} no existe.");
                }

                sistemaDeVentasDBContext.Clientes.Remove(cliente);
                int result = sistemaDeVentasDBContext.SaveChanges();

                return result > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Cliente FindByEmail(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    throw new Exception($"El email no tiene el formato correcto.");
                }

                return sistemaDeVentasDBContext.Clientes.FirstOrDefault(x => x.Email.Equals(email));
            }
            catch
            {
                throw;
            }
        }

        public Cliente FindById(int id)
        {
            try
            {
                return sistemaDeVentasDBContext.Clientes.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Cliente FindByName(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new Exception($"El nombre no tiene el formato correcto");
                    
                }
                return sistemaDeVentasDBContext.Clientes
                                            .FirstOrDefault(x => x.Nombres.Equals(name));
            }
            catch
            {
                throw;
            }
        }

        public List<Cliente> GetAll()
        {
            return sistemaDeVentasDBContext.Clientes.ToList();
        }

        public int Update(Cliente t)
        {
            try
            {
                if (t == null)
                {
                    throw new ArgumentNullException("El cliente no puede ser null.");
                }

                Cliente cliente = FindById(t.Id);
                if (cliente == null)
                {
                    throw new Exception($"El cliente con ese id no existe.");
                }

                cliente.Nombres = t.Nombres;
                cliente.Apellidos = t.Apellidos;
                cliente.Direccion = t.Direccion;
                cliente.Telefono = t.Telefono;
                cliente.Email = t.Email;
                cliente.Cedula = t.Cedula;

                sistemaDeVentasDBContext.Clientes.Update(cliente);
                return sistemaDeVentasDBContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        public IDbContextTransaction GetTransaction()
        {
            return ((SistemaDeVentasDBContext)sistemaDeVentasDBContext).Database.BeginTransaction();
        }
    }
}
