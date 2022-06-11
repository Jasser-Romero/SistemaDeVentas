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
    }
}
