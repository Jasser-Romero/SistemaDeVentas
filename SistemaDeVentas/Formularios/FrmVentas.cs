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
        List<int> productosIds = new List<int>();
        List<int> stocks = new List<int>();
        int productoId = 0;
        int clienteId = 0;
        private List<Producto> productos = new List<Producto>();
        public List<ProductoVentaDTO> vender = new List<ProductoVentaDTO>();
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
            frmBusqueda.ShowDialog();

            productoId = frmBusqueda.busquedaId;
            Producto producto = ProductoService.FindById(productoId);
            if(producto == null)
            {
                MessageBox.Show("Error");
                return;
            }
            
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
            dataGridView1.DataSource = vender;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[4].DefaultCellStyle.Format = "N1";
        }
        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if(productoId != 0) 
            {
                bool exist = false;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if ((int)row.Cells[0].Value == productoId)
                    {
                        exist = true;
                        return;
                    }
                }
                if (string.IsNullOrWhiteSpace(txtCodigoVenta.Text))
                {
                    MessageBox.Show("Un campo se encuentra vacio");
                    return;
                }
                if (int.Parse(txtExistencias.Text) < (int)nudCantidadVender.Value)
                {
                    MessageBox.Show("No puede comprar mas existencias que las disponibles","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if ((int)nudCantidadVender.Value == 0)
                {
                    MessageBox.Show("No ha seleccionado las existencias", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Producto producto = ProductoService.FindById(productoId);
                ProductoVentaDTO productoVentaDTO = new ProductoVentaDTO()
                {
                    IdProducto = producto.Id,
                    Producto = producto.Nombre,
                    Cantidad = (int)nudCantidadVender.Value,
                    Precio = producto.PrecioVenta,
                    SubTotal = ((int)nudCantidadVender.Value * producto.PrecioVenta)
                };

                stocks.Add(productoVentaDTO.Cantidad);
                productos.Add(producto);
                vender.Add(productoVentaDTO);
                productosIds.Add(producto.Id);

                CalcularTotal();

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
            if(cliente == null)
            {
                MessageBox.Show("Error");
                return;
            }
            txtNombreCliente.Text = cliente.Nombres;
            txtTelefono.Text = cliente.Telefono;
            txtEmail.Text = cliente.Email;
        }

        private void btnRealizarVenta_Click(object sender, EventArgs e)
        {
            //TODO: Hacer este boton y validar stock a vender
            int i = 0;
            if(productoId == 0 || clienteId == 0)
            {
                MessageBox.Show("No ha seleccionado ningun producto o ningun cliente","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if (!vender.Any())
            {
                MessageBox.Show("No se ha agregado ningun producto");
                return;
            }
            Cliente cliente = ClienteService.FindById(clienteId);

            foreach (int id in productosIds)
            {
                Producto producto = ProductoService.FindById(id);
                Venta venta = new Venta()
                {
                    Categoria = producto.Categoria,
                    Cliente = cliente.Nombres,
                    Producto = producto.Nombre,
                    Cantidad = stocks[i],
                    Precio = producto.PrecioVenta,
                    Codigo = txtCodigoVenta.Text,
                    Email = cliente.Email,
                    Estado = "Activo",
                    Fecha = DateTime.Today.Date,
                    Total = CalcularTotal()
                };
                VentaService.Create(venta);

                producto.Stock -= stocks[i];

                ProductoService.Update(producto);
                i++;
            }
            
            MessageBox.Show("La venta se ha realizado con exito", "Informacion",MessageBoxButtons.OK,MessageBoxIcon.Information);

        }
        private decimal CalcularTotal()
        {
            decimal total = 0;
            if(dataGridView1.Rows.Count > 0)
            {
                foreach (ProductoVentaDTO producto in vender)
                    total += producto.SubTotal;
            }
            lblTotalPagar.Text = total.ToString();
            return total;
        }
        private void FrmVentas_Load(object sender, EventArgs e)
        {
            lblFecha.Text = DateTime.Today.ToString("d");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
