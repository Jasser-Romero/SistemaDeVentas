using SistemaDeVentas.Domain.Entities;
using SistemaDeVentas.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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
            try
            {
                if (t is null)
                {
                    throw new ArgumentNullException(nameof(t));
                }

                sistemaDeVentasDBContext.Productos.Add(t);
                return sistemaDeVentasDBContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Delete(Producto t)
        {
            try
            {
                if (t is null)
                {
                    throw new ArgumentNullException(nameof(t));
                }

                Producto producto = FindById(t.Id);

                if (producto == null)
                {
                    throw new Exception($"El objeto producto con id {t.Id} no existe");
                }

                sistemaDeVentasDBContext.Productos.Remove(producto);

                int result = sistemaDeVentasDBContext.SaveChanges();
                return result > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Producto> FindByCategoria(string categoria)
        {
            return sistemaDeVentasDBContext.Productos.Where(x => x.Categoria.Equals(categoria)).ToList();
        }

        public Producto FindByCode(string code)
        {
            return sistemaDeVentasDBContext.Productos.FirstOrDefault(x => x.Codigo.Equals(code));
        }

        public Producto FindById(int id)
        {
            return sistemaDeVentasDBContext.Productos.FirstOrDefault(x => x.Id == id);
        }


        public List<Producto> GetAll()
        {
            return sistemaDeVentasDBContext.Productos.ToList();
        }

        public int Update(Producto t)
        {
            try
            {
                if (t is null)
                {
                    throw new ArgumentNullException(nameof(t));
                }

                Producto producto = FindById(t.Id);

                if (producto == null)
                {
                    throw new Exception($"El objeto producto con id {t.Id} no existe");
                }

                producto.Descripcion = t.Descripcion;
                producto.Nombre = t.Nombre;
                producto.Categoria = t.Categoria;
                producto.PrecioVenta = t.PrecioVenta;
                producto.Stock = t.Stock;

                sistemaDeVentasDBContext.Productos.Update(producto);
                return sistemaDeVentasDBContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
