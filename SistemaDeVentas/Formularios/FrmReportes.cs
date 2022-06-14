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
    public partial class FrmReportes : Form
    {
        List<ProductoVentaDTO> products = new List<ProductoVentaDTO>();
        public IVentaService VentaService { get; set; }
        public IProductoService ProductoService { get; set; }
        public FrmReportes()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FrmReportes_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
        }

        private void LoadDataGridView()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = products;
            dataGridView1.Columns[0].Visible = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                List<Venta> ventas = VentaService.FindByCode(txtCodigoVenta.Text);
                txtNombreCliente.Text = ventas[0].Cliente;
                txtFecha.Text = ventas[0].Fecha.ToString();
                txtEmail.Text = ventas[0].Email;
                txtEstado.Text = ventas[0].Estado;
                txtCodigo.Text = ventas[0].Codigo;

                foreach (Venta venta in ventas)
                {
                    ProductoVentaDTO productoVentaDTO = new ProductoVentaDTO()
                    {
                        Cantidad = venta.Cantidad,
                        Precio = venta.Precio,
                        Producto = venta.Producto,
                        SubTotal = (venta.Cantidad * venta.Precio)
                    };
                    products.Add(productoVentaDTO);
                }
                LoadDataGridView();
            }
            catch (Exception)
            {
                MessageBox.Show("Ha ocurrido un error");
                return;
            }
        }

        private void btnCancelarVenta_Click(object sender, EventArgs e)
        {
            try
            {
                if (VentaService.FindByCode(txtCodigo.Text).First().Estado == "Cancelada")
                {
                    MessageBox.Show("Esta venta ya ha sido cancelada");
                    return;
                }
                DialogResult result = MessageBox.Show("Desea cancelar esta venta?, una vez hecho no hay retorno", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    List<Venta> ventas = VentaService.FindByCode(txtCodigo.Text);
                    foreach (Venta venta in ventas)
                    {
                        venta.Estado = "Cancelada";
                        VentaService.Update(venta);

                        Producto producto = ProductoService.GetAll().First(x => x.Nombre.Equals(venta.Producto));
                        producto.Stock += venta.Cantidad;
                        ProductoService.Update(producto);
                    }
                    MessageBox.Show("Se ha cancelado la venta");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
                return;
            }
        }
    }
}
