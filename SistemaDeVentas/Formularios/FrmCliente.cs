using SistemaDeVentas.AppCore.Interfaces;
using SistemaDeVentas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SistemaDeVentas.Formularios
{
    public partial class FrmCliente : Form
    {
        public IClienteService ClienteService { get; set; }
        public int id = 0;
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
            if (!ValidarCampos())
            {
                MessageBox.Show("Todos los campos deben ser llenados","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            string patronCedula = @"\A[0-9]{3}(\-)[0-9]{6}(\-)[0-9]{4}[A-Z]\Z";
            if (!Regex.IsMatch(txtCedula.Text, patronCedula))
            {
                MessageBox.Show("Error, el formato de la cédula ingresada es invalida", "Mensaje de Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string patronTelefono = @"\A[0-9]{4}(\-)[0-9]{4}\Z";
            if (!Regex.IsMatch(txtTelefono.Text, patronTelefono))
            {
                MessageBox.Show("Error, el formato del telefono ingresado es invalido","Mensaje de Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            string patronCorreo = @"\A(\w+\.?\w*\@\w+\.)(com)\Z";
            if (!Regex.IsMatch(txtEmail.Text, patronCorreo))
            {
                MessageBox.Show("Error, el formato del correo ingresado es invalido","Mensaje de Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
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
            if (id == 0)
            {
                MessageBox.Show("No se ha seleccionado ningun cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dgvClientes.CurrentRow.Selected == false)
            {
                MessageBox.Show("Seleccione un cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Error, seleccione", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int id = (int)dgvClientes.CurrentRow.Cells[0].Value;
            Cliente cliente = ClienteService.FindById(id);
            ClienteService.Delete(cliente);
            LoadDatagridView();
            EmptyTextBoxes();
        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = (int)dgvClientes.CurrentRow.Cells[0].Value;
            Cliente cliente = ClienteService.FindById(id);
            txtNombres.Text = cliente.Nombres;
            txtApellidos.Text = cliente.Apellidos;
            txtCedula.Text = cliente.Cedula;
            rtbDireccion.Text = cliente.Direccion;
            txtEmail.Text = cliente.Email;
            txtTelefono.Text = cliente.Telefono;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            EmptyTextBoxes();
            id = 0;
            LoadDatagridView();
        }

        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cmbBusqueda.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe seleccionar una opcion");
                    return;
                }
                switch (cmbBusqueda.SelectedIndex)
                {
                    case 0:
                        if (string.IsNullOrWhiteSpace(txtBusqueda.Text))
                        {
                            MessageBox.Show("Debe escribir un Id","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                            return;
                        }
                        if (!int.TryParse(txtBusqueda.Text, out int id2))
                        {
                            MessageBox.Show("No ingreso un id");
                            return;
                        }
                        if (int.Parse(txtBusqueda.Text) == 0)
                        {
                            MessageBox.Show("El Id no puede ser cero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        
                        Cliente cliente = ClienteService.FindById(id2);
                        List<Cliente> clientes = new List<Cliente> { cliente };
                        dgvClientes.DataSource = clientes;
                        break;

                    case 1:
                        if (string.IsNullOrWhiteSpace(txtBusqueda.Text))
                        {
                            MessageBox.Show("Debe escribir el correo","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                            return;
                        }
                        string patronCorreo = @"\A(\w+\.?\w*\@\w+\.)(com)\Z";
                        if (!Regex.IsMatch(txtBusqueda.Text, patronCorreo))
                        {
                            MessageBox.Show("Error, el formato del correo ingresado es invalido", "Mensaje de Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        Cliente cliente2 = ClienteService.FindByEmail(txtBusqueda.Text);
                        List<Cliente> clientes2 = new List<Cliente> { cliente2 };
                        dgvClientes.DataSource = clientes2;
                        break;

                    case 2:
                        if (string.IsNullOrWhiteSpace(txtBusqueda.Text))
                        {
                            MessageBox.Show("Debe escribir un nombre","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                            return;
                        }
                        Cliente cliente3 = ClienteService.FindByName(txtNombres.Text);
                        List<Cliente> clientes3 = new List<Cliente> { cliente3 };
                        dgvClientes.DataSource = clientes3;

                        break;


                }
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(txtNombres.Text) || string.IsNullOrEmpty(txtApellidos.Text) ||
                string.IsNullOrEmpty(txtCedula.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtTelefono.Text) ||
                string.IsNullOrEmpty(rtbDireccion.Text))
            {
                return false;
            }
            return true;
        }
    }
}
