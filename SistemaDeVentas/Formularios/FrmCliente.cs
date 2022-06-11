using SistemaDeVentas.AppCore.Interfaces;
using SistemaDeVentas.AppCore.Services;
using SistemaDeVentas.Domain.Entities;
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
    public partial class FrmCliente : Form
    {
        public IClienteService ClienteService { get; set; }
        public FrmCliente()
        {
            InitializeComponent();
        }

        private void FrmCliente_Load(object sender, EventArgs e)
        {
            dgvClientes.DataSource = null;
            dgvClientes.DataSource = ClienteService.GetAll();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Cliente cliente = new()
            {
                Nombres = txtNombres.Text,
                Apellidos = txtApellidos.Text,
                Cedula = txtCedula.Text,
                Direccion = rtbDireccion.Text,
                Email = txtEmail.Text,
                Telefono = txtTelefono.Text
            };
            ClienteService.Create(cliente);

            EmptyTextBoxes();
            LoadDatagridView();
        }

        private void EmptyTextBoxes()
        {
            txtNombres.Clear();
            txtApellidos.Clear();
            txtCedula.Clear();
            rtbDireccion.Clear();
            txtEmail.Clear();
            txtTelefono.Clear();
        }

        private void LoadDatagridView()
        {
            dgvClientes.DataSource = null;
            dgvClientes.DataSource = ClienteService.GetAll();
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
