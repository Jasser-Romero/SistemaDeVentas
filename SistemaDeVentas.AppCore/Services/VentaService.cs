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
    public class VentaService : IVentaService
    {
        private IVentaRepository ventaRepository;
        public VentaService(IVentaRepository ventaRepository)
        {
            this.ventaRepository = ventaRepository;
        }
        public int Create(Venta t)
        {
            return ventaRepository.Create(t);
        }

        public bool Delete(Venta t)
        {
            return ventaRepository.Delete(t);
        }

        public List<Venta> GetAll()
        {
            return ventaRepository.GetAll();
        }

        public int Update(Venta t)
        {
            return ventaRepository.Update(t);
        }
    }
}
