using System;
using System.Collections.Generic;

#nullable disable

namespace SistemaDeVentas.Domain.Entities
{
    public partial class Venta
    {
        public int Id { get; set; }
        public int? ProductoId { get; set; }
        public int? ClienteId { get; set; }
        public DateTime Fecha { get; set; }
        public string Codigo { get; set; }
        public decimal Total { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
