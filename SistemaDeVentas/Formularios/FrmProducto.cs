using SistemaDeVentas.AppCore.Interfaces;
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
    public partial class FrmProducto : Form
    {
        public IProductoService ProductoService { get; set; }
        int id = 0;
        public FrmProducto()
        {
            InitializeComponent();
        }

        private void FrmProducto_Load(object sender, EventArgs e)
        {
            dgvproductos.DataSource = null;
            dgvproductos.DataSource = ProductoService.GetAll();
        }


        private bool Validate()
        {
            if (string.IsNullOrEmpty(cmbCategoria.Text) || string.IsNullOrEmpty(txtNombre.Text) ||
               string.IsNullOrEmpty(rtbDescripcion.Text) || string.IsNullOrEmpty(txtCodigo.Text) ||
               string.IsNullOrEmpty(txtPrecVentas.Text) || string.IsNullOrEmpty(txtExistencias.Text))
            {
                return false;
            }
            return true;
        }

        private void Clear()
        {
            cmbCategoria.Text = "";
            txtNombre.Text = "";
            rtbDescripcion.Text = "";
            txtCodigo.Text = "";
            txtExistencias.Text = "";
            txtPrecVentas.Text = "";
        }

        private void LoadDataGridView()
        {
            dgvproductos.DataSource = null;
            dgvproductos.DataSource = ProductoService.GetAll();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!Validate())
            {
                MessageBox.Show("No puede dejar un espacio en blanco");
                return;
            }

            Producto producto = new Producto()
            {
                Categoria = cmbCategoria.Text,
                Nombre = txtNombre.Text,
                Descripcion = rtbDescripcion.Text,
                Codigo = txtCodigo.Text,
                Stock = int.Parse(txtExistencias.Text),
                PrecioVenta = decimal.Parse(txtPrecVentas.Text)
            };
            ProductoService.Create(producto);

            Clear();
            LoadDataGridView();

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (id == 0)
            {
                MessageBox.Show("No se ha seleccionado ningun cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dgvproductos.CurrentRow.Selected == false)
            {
                MessageBox.Show("Seleccione un cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Producto producto = new Producto()
            {
                Id = (int)dgvproductos.CurrentRow.Cells[0].Value,
                Categoria = cmbCategoria.Text,
                Nombre = txtNombre.Text,
                Descripcion = rtbDescripcion.Text,
                Codigo = txtCodigo.Text,
                Stock = int.Parse(txtExistencias.Text),
                PrecioVenta = decimal.Parse(txtPrecVentas.Text)
            };
            ProductoService.Update(producto);
            dgvproductos.DataSource = ProductoService.GetAll();
            Clear();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvproductos.Rows.GetRowCount(DataGridViewElementStates.Selected) == 0)
            {
                MessageBox.Show("Error, seleccione", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int id = (int)dgvproductos.CurrentRow.Cells[0].Value;
            Producto producto = ProductoService.FindById(id);
            ProductoService.Delete(producto);
            LoadDataGridView();
            Clear();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Clear();
            id = 0;
        }
    }
}
