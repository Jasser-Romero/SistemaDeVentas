using SistemaDeVentas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeVentas.AppCore.Interfaces
{
    public interface IVentaService : IService<Venta>
    {
        List<Venta> FindByCode(string code);
    }
}
