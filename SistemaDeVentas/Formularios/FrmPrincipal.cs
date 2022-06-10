using SistemaDeVentas.AppCore.Interfaces;
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
        private IVentaService ventaService;
        public FrmPrincipal(IClienteService clienteService, IProductoService productoService, IVentaService ventaService)
        {
            this.productoService = productoService;
            this.clienteService = clienteService;
            this.ventaService = ventaService;

            InitializeComponent();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            FrmCliente frmCliente = new FrmCliente();
            frmCliente.ClienteService = clienteService;
            frmCliente.ShowDialog();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            FrmProducto frmProducto = new FrmProducto();
            frmProducto.ProductoService = productoService;
            frmProducto.ShowDialog();
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            FrmVentas frmVentas = new FrmVentas();
            frmVentas.VentaService = ventaService;
            frmVentas.ShowDialog();
        }
    }
}
