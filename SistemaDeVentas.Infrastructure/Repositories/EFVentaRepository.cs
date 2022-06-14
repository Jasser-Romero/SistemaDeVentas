using SistemaDeVentas.Domain.Entities;
using SistemaDeVentas.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeVentas.Infrastructure.Repositories
{
    public class EFVentaRepository : IVentaRepository
    {
        private ISistemaDeVentasDBContext sistemaDeVentasDBContext;
        public EFVentaRepository(ISistemaDeVentasDBContext sistemaDeVentasDBContext)
        {
            this.sistemaDeVentasDBContext = sistemaDeVentasDBContext;
        }
        public int Create(Venta t)
        {
            sistemaDeVentasDBContext.Ventas.Add(t);
            return sistemaDeVentasDBContext.SaveChanges();
        }

        public bool Delete(Venta t)
        {
            sistemaDeVentasDBContext.Ventas.Remove(t);
            return sistemaDeVentasDBContext.SaveChanges() > 0;
        }

        public List<Venta> FindByCode(string code)
        {
            return sistemaDeVentasDBContext.Ventas.Where(x => x.Codigo.Equals(code)).ToList();
        }

        public List<Venta> GetAll()
        {
            return sistemaDeVentasDBContext.Ventas.ToList();
        }

        public int Update(Venta t)
        {
            sistemaDeVentasDBContext.Ventas.Update(t);
            return sistemaDeVentasDBContext.SaveChanges();
        }
    }
}
