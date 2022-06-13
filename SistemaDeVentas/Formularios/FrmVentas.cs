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
    public partial class FrmVentas : Form
    {
        int productoId = 0;
        int clienteId = 0;
        private List<Producto> productos = new List<Producto>();
        public IVentaService VentaService { get; set; }
        public IProductoService ProductoService { get; set; }
        public IClienteService ClienteService { get; set; }
        public FrmVentas()
        {
            InitializeComponent();
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            FrmBusqueda frmBusqueda = new FrmBusqueda();
            frmBusqueda.dgvBusqueda.DataSource = ProductoService.GetAll();
            frmBusqueda.dgvBusqueda.Columns[7].Visible = false;
            frmBusqueda.dgvBusqueda.Columns[8].Visible = false;
            frmBusqueda.ShowDialog();

            productoId = frmBusqueda.busquedaId;
            Producto producto = ProductoService.FindById(productoId);
            txtNombre.Text = producto.Nombre;
            txtExistencias.Text = producto.Stock.ToString();
            txtCategoria.Text = producto.Categoria;
            txtCodigo.Text = producto.Codigo;
            txtDescripcion.Text = producto.Descripcion;

            MemoryStream ms = new MemoryStream(producto.Imagen);
            Bitmap image = new Bitmap(ms);

            pbProducto.Image = image;
            pbProducto.SizeMode = PictureBoxSizeMode.StretchImage;

        }
        private void LoadDataGridView()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = productos;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
        }
        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if(productoId != 0) 
            {
                Producto producto = ProductoService.FindById(productoId);
                productos.Add(producto);
                LoadDataGridView();
            }
            
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            FrmBusqueda frmBusqueda = new FrmBusqueda();
            frmBusqueda.dgvBusqueda.DataSource = ClienteService.GetAll();
            frmBusqueda.ShowDialog();

            clienteId = frmBusqueda.busquedaId;
            Cliente cliente = ClienteService.FindById(clienteId);
            txtNombreCliente.Text = cliente.Nombres;
            txtTelefono.Text = cliente.Telefono;
            txtEmail.Text = cliente.Email;
        }

        private void btnRealizarVenta_Click(object sender, EventArgs e)
        {
            //TODO: Hacer este boton y validar stock a vender
        }
    }
}
