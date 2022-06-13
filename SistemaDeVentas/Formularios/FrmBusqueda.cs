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
    public partial class FrmBusqueda : Form
    {
        public int busquedaId = 0;
        public FrmBusqueda()
        {
            InitializeComponent();
        }

        private void dgvBusqueda_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //busquedaId = (int)dgvBusqueda.CurrentRow.Cells[0].Value;
        }

        private void dgvBusqueda_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            busquedaId = (int)dgvBusqueda.CurrentRow.Cells[0].Value;
            Dispose();
        }
    }
}
