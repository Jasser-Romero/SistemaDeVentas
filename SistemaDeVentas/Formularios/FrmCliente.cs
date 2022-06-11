using SistemaDeVentas.AppCore.Interfaces;
using SistemaDeVentas.Domain.Entities;
using System;
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

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.CurrentRow.Selected == false)
            {
                MessageBox.Show("Seleccione un cliente");
                return;
            }

            Cliente cliente = new Cliente()
            {
                Id = (int)dgvClientes.CurrentRow.Cells[0].Value,
                Nombres = txtNombres.Text,
                Apellidos = txtApellidos.Text,
                Cedula = txtCedula.Text,
                Direccion = rtbDireccion.Text,
                Email = txtEmail.Text,
                Telefono = txtTelefono.Text,
            };

            ClienteService.Update(cliente);
            dgvClientes.DataSource = ClienteService.GetAll();

            EmptyTextBoxes();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.Rows.GetRowCount(DataGridViewElementStates.Selected) == 0)
            {
                MessageBox.Show("Error, seleccione");
                return;
            }
            int id = (int)dgvClientes.CurrentRow.Cells[0].Value;
            Cliente cliente = ClienteService.FindById(id);
            ClienteService.Delete(cliente);
            LoadDatagridView();
        }
    }
}
