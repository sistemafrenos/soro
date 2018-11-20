using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace HK
{
    public partial class FrmCadenaConexion : Form
    {
        public FrmCadenaConexion()
        {
            InitializeComponent();
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }
        System.Data.SqlClient.SqlConnectionStringBuilder sb = new System.Data.SqlClient.SqlConnectionStringBuilder();

        private void FrmCadenaConexion_Load(object sender, EventArgs e)
        {
            sb.ConnectionString = global::HK.Properties.Settings.Default.SoroConnectionString;
            this.txtServidor.Text = sb.DataSource;
            this.txtBaseDeDatos.Text = sb.InitialCatalog;
        }

        private void Aceptar_Click(object sender, EventArgs e)
        {
            sb.DataSource = this.txtServidor.Text;
            sb.InitialCatalog = this.txtBaseDeDatos.Text;
            if (cBasicas.ProbarConexion(sb.ConnectionString))
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.ConnectionStrings.ConnectionStrings["HK.Properties.Settings.OK_FacturacionConnectionString"].ConnectionString = sb.ConnectionString;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("ConnectionStrings");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
