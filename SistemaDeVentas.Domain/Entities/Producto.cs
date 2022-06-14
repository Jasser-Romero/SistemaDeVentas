using System;
using System.Collections.Generic;

#nullable disable

namespace SistemaDeVentas.Domain.Entities
{
    public partial class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Codigo { get; set; }
        public string Categoria { get; set; }
        public int Stock { get; set; }
        public decimal PrecioVenta { get; set; }
        public byte[] Imagen { get; set; }
    }
}
