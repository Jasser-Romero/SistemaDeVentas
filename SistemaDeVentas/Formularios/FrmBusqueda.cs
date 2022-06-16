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
    public partial class FrmBusqueda : Form
    {
        public int busquedaId = 0;
        public FrmBusqueda()
        {
            InitializeComponent();
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wnsg, int wparam, int lparam);

        private void dgvBusqueda_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //busquedaId = (int)dgvBusqueda.CurrentRow.Cells[0].Value;
        }

        private void dgvBusqueda_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            busquedaId = (int)dgvBusqueda.CurrentRow.Cells[0].Value;
            Dispose();
        }

        private void pboxCerrar_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        private void FrmBusqueda_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pboxMinimize_Click(object sender, EventArgs e)
        {
            
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
           
        }
    }
}
