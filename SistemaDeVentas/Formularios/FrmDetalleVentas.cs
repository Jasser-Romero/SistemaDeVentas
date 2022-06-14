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
    public partial class FrmDetalleVentas : Form
    {
        public IVentaService VentaService { get; set; }
        public FrmDetalleVentas()
        {
            InitializeComponent();
        }

        private void FrmDetalleVentas_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = VentaService.GetAll();
        }
    }
}
