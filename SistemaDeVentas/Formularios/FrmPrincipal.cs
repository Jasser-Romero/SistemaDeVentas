﻿using SistemaDeVentas.AppCore.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaDeVentas.Formularios
{
    public partial class FrmPrincipal : Form
    {
        private IClienteService clienteService;
        private IProductoService productoService;
        public FrmPrincipal(IClienteService clienteService, IProductoService productoService)
        {
            this.productoService = productoService;
            this.clienteService = clienteService;
            
            InitializeComponent();
        }
    }
}
