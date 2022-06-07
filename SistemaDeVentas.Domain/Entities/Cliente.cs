﻿using System;
using System.Collections.Generic;

#nullable disable

namespace SistemaDeVentas.Domain.Entities
{
    public partial class Cliente
    {
        public Cliente()
        {
            Venta = new HashSet<Venta>();
        }

        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Cedula { get; set; }

        public virtual ICollection<Venta> Venta { get; set; }
    }
}
