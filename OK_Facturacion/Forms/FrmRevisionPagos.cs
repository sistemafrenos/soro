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
    public partial class FrmRevisionPagos : Form
    {
        public FrmRevisionPagos()
        {
            InitializeComponent();
        }
        Terceros tercero = new Terceros();
        SoroEntities dc = new SoroEntities();
        List<RegistroPagoProveedor> lista = new List<RegistroPagoProveedor>();
        private void Frm_Load(object sender, EventArgs e)
        {
            dc = new SoroEntities();
            this.txtCedulaTitular.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCedulaTitular_ButtonClick);
        }

        void TxtCedulaTitular_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmBuscarEntidades F = new FrmBuscarEntidades
            {
                myLayout = "PROVEEDORES",
                dc = dc
            };
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
            var query = from p in dc.RegistroPagoProveedor
                        where p.IdProveedor == tercero.IdTercero
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
