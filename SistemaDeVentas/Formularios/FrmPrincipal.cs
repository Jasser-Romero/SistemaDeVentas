using SistemaDeVentas.AppCore.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wnsg, int wparam, int lparam);

        private void btnClientes_Click(object sender, EventArgs e)
        {
            //FrmCliente frmCliente = new FrmCliente();
            //frmCliente.ClienteService = clienteService;
            //AbrirFormHija(frmCliente);
         
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            //FrmProducto frmProducto = new FrmProducto();
            //frmProducto.ProductoService = productoService;
            //frmProducto.ShowDialog();
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            //FrmVentas frmVentas = new FrmVentas();
            //frmVentas.VentaService = ventaService;
            //frmVentas.ShowDialog();
        }
        private void AbrirFormHija(object formHija)
        {
            if (this.pnlContenedor.Controls.Count > 0)
            {
                this.pnlContenedor.Controls.RemoveAt(0);
            }
            Form fh = formHija as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.pnlContenedor.Controls.Add(fh);
            this.pnlContenedor.Tag = fh;
            fh.Show();
        }

        private void btnrjClientes_Click(object sender, EventArgs e)
        {
            FrmCliente frmCliente = new FrmCliente();
            frmCliente.ClienteService = clienteService;
            AbrirFormHija(frmCliente);
        }

        private void btnrjProductos_Click(object sender, EventArgs e)
        {
            FrmProducto frmProducto = new FrmProducto();
            frmProducto.ProductoService = productoService;
            AbrirFormHija(frmProducto);
        }

        private void btnrjVentas_Click(object sender, EventArgs e)
        {
            FrmVentas frmVentas = new FrmVentas();
            frmVentas.ProductoService = productoService;
            frmVentas.ClienteService = clienteService;
            frmVentas.VentaService = ventaService;
            AbrirFormHija(frmVentas);
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {

        }
        //TODO: Falta agregar boton de agrandar y disminuir ventana
        private void FrmPrincipal_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pnlContenedor_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pboxClose_Click(object sender, EventArgs e)
        {
            
        }

        private void pboxCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pboxMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState=FormWindowState.Minimized;
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnrjReporte_Click(object sender, EventArgs e)
        {
            FrmReportes frmReportes = new FrmReportes();
            frmReportes.VentaService = ventaService;
            frmReportes.ProductoService = productoService;
            AbrirFormHija(frmReportes);
        }

        private void btnrjDetalle_Click(object sender, EventArgs e)
        {
            FrmDetalleVentas frmDetalleVentas = new FrmDetalleVentas();
            frmDetalleVentas.VentaService=ventaService;
            AbrirFormHija(frmDetalleVentas);
        }
    }
}
