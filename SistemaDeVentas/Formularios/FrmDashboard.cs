using Microsoft.Data.SqlClient;
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
    public partial class FrmDashboard : Form
    {
        SqlConnection Conexion = new SqlConnection("Server=localhost\\SQLEXPRESS;Initial Catalog=SistemaDeVentasDB;Integrated Security=true;");
        SqlCommand cmd;
        SqlDataReader dr;
        public FrmDashboard()
        {
            InitializeComponent();
        }
        //TODO: Agregar progressbar o grafica
        private void DashboardDatos()
        {
            cmd=new SqlCommand("DashboardDatos",Conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter total = new SqlParameter("@totVentas",0);
            SqlParameter nprod = new SqlParameter("@nprod",0);
            SqlParameter nclient = new SqlParameter("@nclient",0);
            total.Direction = ParameterDirection.Output;
            nprod.Direction = ParameterDirection.Output;
            nclient.Direction=ParameterDirection.Output;
            cmd.Parameters.Add(total);
            cmd.Parameters.Add(nprod);
            cmd.Parameters.Add(nclient);
            Conexion.Open();
            cmd.ExecuteNonQuery();
            lblTotalVentas.Text = cmd.Parameters["@totVentas"].Value.ToString();
            lblProductosTotales.Text = cmd.Parameters["@nprod"].Value.ToString();
            lblClientesTotales.Text = cmd.Parameters["@nclient"].Value.ToString();
            Conexion.Close();
        }

        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            DashboardDatos();
        }
    }
}
