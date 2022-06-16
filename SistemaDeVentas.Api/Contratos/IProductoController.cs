using SistemaDeVentas.Domain.Entities;
using System.Collections.Generic;

namespace SistemaDeVentas.Api.Contratos
{
    public interface IProductoController : IController<Producto>
    {
        Producto FindById(int id);
        Producto FindByCode(string code);
        List<Producto> FindByCategoria(string categoria);
    }
}
