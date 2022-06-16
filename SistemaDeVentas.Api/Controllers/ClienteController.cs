using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaDeVentas.Domain.Entities;
using SistemaDeVentas.Domain.SistemaDeVentasEntities;
using System.Collections.Generic;
using System.Linq;

namespace SistemaDeVentas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly SistemaDeVentasDBContext _context;

        public ClienteController(SistemaDeVentasDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Cliente>> GetAll()
        {
            return _context.Clientes.ToList();
        }
        [HttpPost]
        public ActionResult Create(Cliente t)
        {
            _context.Clientes.Add(t);
            _context.SaveChanges();
            return Ok();
        }
        [HttpDelete]
        public ActionResult Delete(Cliente t)
        {
            Cliente cliente = _context.Clientes.First(p => p.Id == t.Id);
            _context.Clientes.Remove(t);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public ActionResult Update(Cliente t)
        {
            Cliente cliente = _context.Clientes.First(p => p.Id == t.Id);

            cliente.Nombres = t.Nombres;
            cliente.Apellidos = t.Apellidos;
            cliente.Direccion = t.Direccion;
            cliente.Telefono = t.Telefono;
            cliente.Email = t.Email;
            cliente.Cedula = t.Cedula;

            _context.Clientes.Update(cliente);
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<Cliente> FindById(int id)
        {
            return _context.Clientes.FirstOrDefault(x => x.Id == id);
        }
    }
}
