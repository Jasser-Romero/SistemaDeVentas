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
    public partial class FrmVentas : Form
    {
        public IVentaService VentaService { get; set; }
        public FrmVentas()
        {
            InitializeComponent();
        }
    }
}
