using SistemaDeVentas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeVentas.Domain.Interfaces
{
    public interface IProductoRepository : IRepository<Producto>
    {
        Producto FindById(int id);
        Producto FindByCode(string code);
        List<Producto> FindByCategoria(string categoria);
    }
}
