using Microsoft.EntityFrameworkCore.Storage;
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
        private IProductoRepository productoRepository;
        private IVentaRepository ventaRepository;
        public ClienteService(IClienteRepository clienteRepository)
        {
            this.clienteRepository = clienteRepository;
            this.productoRepository = productoRepository;
            this.ventaRepository = ventaRepository;
        }
        public int Create(Cliente t)
        {
            return clienteRepository.Create(t);
        }

        public bool Delete(Cliente t)
        {
            return clienteRepository.Delete(t);
        }

        public Cliente FindByEmail(string email)
        {
            return clienteRepository.FindByEmail(email);
        }

        public Cliente FindById(int id)
        {
            return clienteRepository.FindById(id);
        }

        public Cliente FindByName(string name)
        {
            return clienteRepository.FindByName(name);
        }

        public List<Cliente> GetAll()
        {
            return clienteRepository.GetAll();
        }

        public bool SetProductosToCliente(Cliente cliente, List<Producto> productos, DateTime efectiveDate)
        {
            bool success = false;
            using IDbContextTransaction transaction = clienteRepository.GetTransaction();
            try
            {
                if (productos == null || productos.Count == 0)
                {
                    throw new ArgumentNullException("La lista no puede estar vacia");
                }
                foreach (Producto producto in productos)
                {
                    success = SetProductoToCliente(cliente, producto, efectiveDate);
                    if (!success)
                    {
                        throw new Exception($"Fallo al asignar el producto con id {producto.Id} al cliente con id {producto.Id}.");
                    }
                    success = productoRepository.Update(producto) > 0;

                    if (!success)
                    {
                        throw new Exception($"Fallo al actualizar el producto con id {producto.Id}");
                    }
                }

                if (success)
                {
                    transaction.Commit();
                }
                return success;

            }
            catch
            {
                throw;
            }
        }

        public bool SetProductoToCliente(Cliente cliente, Producto producto, DateTime efectiveDate)
        {
            try
            {
                validateVenta(cliente, producto);
                Venta venta = new Venta()
                {
                    ProductoId = producto.Id,
                    ClienteId = cliente.Id,
                    Date = efectiveDate
                };

                ventaRepository.Create(venta);
                return true;
            }
            catch
            {
                throw;
            }
        }

        public bool UnSetProductosToCliente(Cliente cliente, List<Producto> productos, DateTime efectiveDate)
        {
            bool success = false;
            using IDbContextTransaction transaction = clienteRepository.GetTransaction();

            try
            {
                if (productos == null || productos.Count == 0)
                {
                    throw new Exception("La lista no puede estar vacia");
                }

                foreach (Producto producto in productos)
                {
                    success = SetProductoToCliente(cliente, producto, efectiveDate);

                    if (!success)
                    {
                        throw new Exception($"Fallo al desasignar el producto con id {producto.Id} al cliente con id {cliente.Id}.");
                    }
                    success = productoRepository.Update(producto) > 0;

                    if (!success)
                    {
                        throw new Exception($"Fallo al actualizar el producto con id {producto.Id}. ");
                    }
                }
                if (success)
                {
                    transaction.Commit();
                }

                return success;
            }
            catch
            {
                throw;
            }
        }

        public int Update(Cliente t)
        {
            return clienteRepository.Update(t);
        }

        public void validateVenta(Cliente cliente, Producto producto)
        {
            if (cliente == null)
            {
                throw new ArgumentNullException(nameof(cliente));
            }
            if (producto == null)
            {
                throw new ArgumentNullException(nameof(producto));
            }
            Cliente clientetemp = clienteRepository.FindById(cliente.Id);
            if (clientetemp == null)
            {
                throw new ArgumentNullException($"El objeto {cliente.Nombres} no existe");
            }
        }
    }
}
