using SistemaDeVentas.Domain.Entities;
using SistemaDeVentas.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeVentas.Infrastructure.Repositories
{
    public class EFProductoRepository : IProductoRepository
    {
        private ISistemaDeVentasDBContext sistemaDeVentasDBContext;
        public EFProductoRepository(ISistemaDeVentasDBContext sistemaDeVentasDBContext)
        {
            this.sistemaDeVentasDBContext = sistemaDeVentasDBContext;
        }
        public int Create(Producto t)
        {
            sistemaDeVentasDBContext.Productos.Add(t);
            return sistemaDeVentasDBContext.SaveChanges();
        }

        public int Delete(Producto t)
        {
            sistemaDeVentasDBContext.Productos.Remove(t);
            return sistemaDeVentasDBContext.SaveChanges();
        }

        public List<Producto> GetAll()
        {
            return sistemaDeVentasDBContext.Productos.ToList();
        }

        public int Update(Producto t)
        {
            sistemaDeVentasDBContext.Productos.Update(t);
            return sistemaDeVentasDBContext.SaveChanges();
        }
    }
}
