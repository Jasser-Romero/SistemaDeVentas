using SistemaDeVentas.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SistemaDeVentas.AppCore.Interfaces
{
    public interface IClienteService : IService<Cliente>
    {
        Cliente FindById(int id);
        Cliente FindByName(string name);
        Cliente FindByEmail(string email);
        bool SetProductoToCliente(Cliente cliente, Producto producto, DateTime efectiveDate);
        bool SetProductosToCliente (Cliente cliente, List<Producto> productos, DateTime efectiveDate);
        bool UnSetProductosToCliente(Cliente cliente, List<Producto> productos, DateTime efectiveDate);
    }
}
