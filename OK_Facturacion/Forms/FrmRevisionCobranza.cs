using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HK.Forms
{
    public partial class FrmRevisionCobranza : Form
    {
        Terceros tercero = new Terceros();
        SoroEntities dc = new SoroEntities();
        List<RegistroPagos> lista = new List<RegistroPagos>();
        public FrmRevisionCobranza()
        {
            InitializeComponent();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void FrmRevisionCobranza_Load(object sender, EventArgs e)
        {
            dc=new SoroEntities();
            this.txtCedulaTitular.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(txtCedulaTitular_ButtonClick);
        }

        void txtCedulaTitular_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmBuscarEntidades F = new FrmBuscarEntidades();
            F.myLayout = "TERCEROS";
            F.Filtro = "CLIENTE";
            F.dc = dc;
            F.ShowDialog();
            if (F.Registro != null)
            {
                tercero = (Terceros)F.Registro;
                tercero = FactoryTerceros.Item(tercero.IdTercero);
            }
            else
            {
                tercero = new Terceros();
            }
            var query = from p in dc.RegistroPagos
                        where p.IdTercero == tercero.IdTercero
                        orderby p.Fecha descending
                        select p;
            lista = query.ToList();
            this.vistaDocumentoBindingSource.DataSource = lista;
            this.vistaDocumentoBindingSource.ResetBindings(true);
            this.tercerosBindingSource.DataSource = tercero;
            this.tercerosBindingSource.ResetBindings(true);
        }
    }
}
