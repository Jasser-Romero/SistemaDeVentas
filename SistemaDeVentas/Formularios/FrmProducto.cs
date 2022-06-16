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
        public FrmProducto()
        {
            InitializeComponent();
        }

        private void FrmProducto_Load(object sender, EventArgs e)
        {
            comboBox1.Visible = false;
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
            dgvproductos.Columns[6].DefaultCellStyle.Format = "N1";
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
                MessageBox.Show("Ha surgido un error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void dgvproductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = (int)dgvproductos.CurrentRow.Cells[0].Value;
            if (id != 0)
                btnImagen.Enabled = false;

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
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Clear();
            btnImagen.Enabled = true;
            LoadDataGridView();
        }

        private void txtExistencias_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >=32 && e.KeyChar<=47) || (e.KeyChar>=58 && e.KeyChar<=255))
            {
               
                MessageBox.Show("Solo numeros debe ingresar","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                e.Handled=true;
                return;
            }

        }

        private void txtPrecVentas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
            {
                e.Handled = true;

                MessageBox.Show("No se pueden letras","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);

            }
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
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
                            MessageBox.Show("Debe escribir un Id", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                        Producto producto = ProductoService.FindById(id2);
                        List<Producto> productos = new List<Producto>() { producto };
                        dgvproductos.DataSource = productos;
                        break;

                    case 1:
                        if (string.IsNullOrWhiteSpace(txtBusqueda.Text))
                        {
                            MessageBox.Show("Debe escribir el codigo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        Producto producto1 = ProductoService.FindByCode(txtBusqueda.Text);
                        List<Producto> productos1 = new List<Producto>() { producto1 };
                        dgvproductos.DataSource = productos1;
                        break;

                    case 2:
                        
                        break;


                }
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                List<Producto> productos = ProductoService.FindByCategoria(comboBox1.Text);
                dgvproductos.DataSource = productos;
            }
        }

        private void cmbBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbBusqueda.SelectedIndex == 2)
            {
                comboBox1.Visible = true;
                txtBusqueda.Visible = false;
            }
            else
            {
                comboBox1.Visible = false;
                txtBusqueda.Visible = true;
            }
        }
    }
}
