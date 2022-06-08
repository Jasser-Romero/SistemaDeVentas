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
    public class ProductoService : IProductoService
    {
        private IProductoRepository productoRepository;
        public ProductoService(IProductoRepository productoRepository)
        {
            this.productoRepository = productoRepository;
        }
        public int Create(Producto t)
        {
            return productoRepository.Create(t);
        }

        public bool Delete(Producto t)
        {
            return productoRepository.Delete(t);
        }

        public Producto FindById(int id)
        {
           return productoRepository.FindById(id);
        }

        public List<Producto> GetAll()
        {
            return productoRepository.GetAll();
        }

        public int Update(Producto t)
        {
            return productoRepository.Update(t);
        }
    }
}
