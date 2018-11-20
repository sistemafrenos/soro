using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HK
{
    public partial class FrmParametros : Form
    {
        public FrmParametros()
        {
            InitializeComponent();
        }
        SoroEntities dc = new SoroEntities();
        Parametros Parametro = new Parametros();
        private void FrmParametros_Load(object sender, EventArgs e)
        {
           Parametro = FactoryParametros.Item(dc);
           if (string.IsNullOrEmpty(Parametro.TipoIva))
           {
               Parametro.TipoIva = "EXCLUIDO";
           }
           this.parametrosBindingSource.DataSource = Parametro;
           this.parametrosBindingSource.ResetBindings(true);
           try
           {
               foreach (String strPrinter in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
               {
                   cmbImpresoraFacturas.Properties.Items.Add(strPrinter);
               }
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);
           }
           this.cmbImpresoraFacturas.Text = Parametro.ImpresoraFacturas;
         }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.parametrosBindingSource.EndEdit();
            dc.SaveChanges();
            Parametro.ImpresoraFacturas = cmbImpresoraFacturas.Text;
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
