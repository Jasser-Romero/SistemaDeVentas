using SistemaDeVentas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeVentas.Domain.Interfaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Cliente FindById(int id);
        Cliente FindByName(string name);
        Cliente FindByEmail(string email);
    }
}
