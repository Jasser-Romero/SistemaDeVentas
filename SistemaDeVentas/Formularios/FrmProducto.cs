using SistemaDeVentas.AppCore.Interfaces;
using SistemaDeVentas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        string path;
        public FrmProducto()
        {
            InitializeComponent();
        }

        private void FrmProducto_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
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
            cmbCategoria.SelectedIndex = -1;
            txtNombre.Text = "";
            rtbDescripcion.Text = "";
            txtCodigo.Text = "";
            txtExistencias.Text = "";
            txtPrecVentas.Text = "";
            pbProducto.Image = null;
            id = 0;
        }

        private void LoadDataGridView()
        {
            dgvproductos.DataSource = null;
            dgvproductos.DataSource = ProductoService.GetAll();
            dgvproductos.Columns[7].Visible = false;
        }

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!Validate())
                {
                    MessageBox.Show("No puede dejar un espacio en blanco");
                    return;
                }

                byte[] file = null;
                Stream myStream = openFileDialog1.OpenFile();
                using (MemoryStream ms = new MemoryStream())
                {
                    myStream.CopyTo(ms);
                    file = ms.ToArray();
                }

                Producto producto = new Producto()
                {
                    Categoria = cmbCategoria.Text,
                    Nombre = txtNombre.Text,
                    Descripcion = rtbDescripcion.Text,
                    Codigo = txtCodigo.Text,
                    Stock = int.Parse(txtExistencias.Text),
                    PrecioVenta = decimal.Parse(txtPrecVentas.Text),
                    Imagen = file
                };
                ProductoService.Create(producto);

                Clear();
                LoadDataGridView();
            }
            catch (Exception)
            {
                MessageBox.Show("Ha surgido un error","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
        }

        private void dgvproductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = (int)dgvproductos.CurrentRow.Cells[0].Value;

            Producto producto = ProductoService.FindById(id);
            txtNombre.Text = producto.Nombre;
            rtbDescripcion.Text = producto.Descripcion;
            cmbCategoria.SelectedItem = producto.Categoria;
            txtCodigo.Text = producto.Codigo;
            txtExistencias.Text = producto.Stock.ToString();
            txtPrecVentas.Text = producto.PrecioVenta.ToString();

            MemoryStream ms = new MemoryStream(producto.Imagen);
            Bitmap image = new Bitmap(ms);

            pbProducto.Image = image;
            pbProducto.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void btnImagen_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Archivos jpg (*.jpg)|*.jpg|Archivos png (*.png)|*.png";
            openFileDialog1.FilterIndex = 1;
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pbProducto.Image = Image.FromFile(openFileDialog1.FileName);
                pbProducto.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            
        }

        private void btnBorrar_Click(object sender, EventArgs e)
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

        private void btnActualizar_Click_1(object sender, EventArgs e)
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

            byte[] file = null;
            Stream myStream = openFileDialog1.OpenFile();
            using (MemoryStream ms = new MemoryStream())
            {
                myStream.CopyTo(ms);
                file = ms.ToArray();
            }

            Producto producto = new Producto()
            {
                Id = (int)dgvproductos.CurrentRow.Cells[0].Value,
                Categoria = cmbCategoria.Text,
                Nombre = txtNombre.Text,
                Descripcion = rtbDescripcion.Text,
                Codigo = txtCodigo.Text,
                Stock = int.Parse(txtExistencias.Text),
                PrecioVenta = decimal.Parse(txtPrecVentas.Text),
                Imagen = file
            };
            ProductoService.Update(producto);
            dgvproductos.DataSource = ProductoService.GetAll();
            Clear();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
