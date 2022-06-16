using SistemaDeVentas.Api.Contratos;
using SistemaDeVentas.Domain.Entities;
using SistemaDeVentas.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaDeVentas.Infrastructure.Repositories
{
    public class EFProductoRepository : IProductoRepository
    {
        private IProductoController productoController;
        public EFProductoRepository(IProductoController productoController)
        {
            this.productoController = productoController;
        }
        public int Create(Producto t)
        {
            productoController.Create(t);
            return 0;
        }

        public bool Delete(Producto t)
        {
            productoController.Delete(t);
            return true;
        }

        public List<Producto> FindByCategoria(string categoria)
        {
            return productoController.FindByCategoria(categoria);
        }

        public Producto FindByCode(string code)
        {
            return productoController.FindByCode(code);
        }

        public Producto FindById(int id)
        {
            return productoController.FindById(id);
        }

        public List<Producto> GetAll()
        {
            return productoController.GetAll();
        }

        public int Update(Producto t)
        {
            productoController.Update(t);
            return 0;
        }
    }
}
