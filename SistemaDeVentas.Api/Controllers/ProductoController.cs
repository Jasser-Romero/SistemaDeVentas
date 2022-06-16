using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaDeVentas.Api.Contratos;
using SistemaDeVentas.Domain.Entities;
using SistemaDeVentas.Domain.SistemaDeVentasEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaDeVentas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly SistemaDeVentasDBContext sistemaDeVentasDBContext;

        public ProductoController(SistemaDeVentasDBContext dBContext)
        {
            this.sistemaDeVentasDBContext = dBContext;
        }
        [HttpPost]
        public IActionResult Create(Producto t)
        {
            try
            {
                if (t is null)
                {
                    throw new ArgumentNullException(nameof(t));
                }

                sistemaDeVentasDBContext.Productos.Add(t);
                sistemaDeVentasDBContext.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpDelete]
        public IActionResult Delete(Producto t)
        {
            try
            {
                if (t is null)
                {
                    throw new ArgumentNullException(nameof(t));
                }

                Producto producto = sistemaDeVentasDBContext.Productos.First(p => p.Id == t.Id);

                if (producto == null)
                {
                    throw new Exception($"El objeto producto con id {t.Id} no existe");
                }

                sistemaDeVentasDBContext.Productos.Remove(producto);

                int result = sistemaDeVentasDBContext.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("FindByCategoria/{categoria}")]
        public List<Producto> FindByCategoria(string categoria)
        {
            return sistemaDeVentasDBContext.Productos.Where(x => x.Categoria.Equals(categoria)).ToList();
        }
        [HttpGet]
        [Route("FindByCode/{code}")]
        public Producto FindByCode(string code)
        {
            return sistemaDeVentasDBContext.Productos.FirstOrDefault(x => x.Codigo.Equals(code));
        }
        [HttpGet("{id}")]
        public Producto FindById(int id)
        {
            return sistemaDeVentasDBContext.Productos.FirstOrDefault(x => x.Id == id);
        }
        [HttpGet]
        public List<Producto> GetAll()
        {
            return sistemaDeVentasDBContext.Productos.ToList();
        }
        [HttpPut]
        public void Update(Producto t)
        {
            try
            {
                if (t is null)
                {
                    throw new ArgumentNullException(nameof(t));
                }

                Producto producto = sistemaDeVentasDBContext.Productos.First(x => x.Id == t.Id);

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
                sistemaDeVentasDBContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
