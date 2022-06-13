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
using System.Runtime.InteropServices;

namespace SistemaDeVentas.Formularios
{
    public partial class FrmLogin : Form
    {
        private string username = "Admin";
        private string password = "Ventas2022";

        private IProductoService productoService;
        private IClienteService clienteService;
        private IVentaService ventaService;
        public FrmLogin(IProductoService productoService, IClienteService clienteService, IVentaService ventaService)
        {
            this.productoService = productoService;
            this.clienteService = clienteService;
            this.ventaService = ventaService;
            InitializeComponent();
        }
        //Para arrastrar el formulario
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wnsg, int wparam, int lparam);

        private void pboxClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSesion_Click(object sender, EventArgs e)
        {
          
            if (txtUser.Texts.Equals(username) && txtPassword.Texts.Equals(password))
            {
                this.Hide();
                FrmPrincipal frmPrincipal = new FrmPrincipal(clienteService,productoService, ventaService);
                frmPrincipal.ShowDialog();
                this.Close();
           
            }
            else
            {
                MessageBox.Show("El usuario o contraseña que ingreso no coinciden, vuelva intentarlo","Error de Verificacion",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
        }

        private void FrmLogin_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pboxMinimizar_Click(object sender, EventArgs e)
        {
           this.WindowState=FormWindowState.Minimized;
        }

        private void txtPassword__TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            //if(e.KeyCode == Keys.Enter)
            //{
            //    btnSesion_Click(sender, e);
            //}
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar.Equals(Keys.Enter))
            //{
            //    btnSesion_Click(sender, e);
            //}
        }
    }
}
