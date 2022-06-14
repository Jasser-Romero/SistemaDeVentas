using System;
using System.Collections.Generic;

#nullable disable

namespace SistemaDeVentas.Domain.Entities
{
    public partial class Venta
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public string Categoria { get; set; }
        public string Cliente { get; set; }
        public string Email { get; set; }
        public decimal Total { get; set; }
    }
}
