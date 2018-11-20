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
    public partial class FrmGuiaCarga : Form
    {
        SoroEntities dc = new SoroEntities();
        VistaDocumento VistaDocumento = new VistaDocumento();
        List<VistaDocumento> Lista = new List<VistaDocumento>();
        List<VistaDocumento> Facturas = new List<VistaDocumento>();
        public FrmGuiaCarga()
        {
            InitializeComponent();
        }
        private void FrmGuiaCarga_Load(object sender, EventArgs e)
        {
            var q = from p in dc.VistaDocumento
                    orderby p.Numero descending
                    where p.Fecha >= DateTime.Today.AddDays(-30) && p.Tipo == "FACTURA"
                    select p;
            Facturas = q.ToList();
            this.bs.DataSource = Facturas ;
            this.bs.ResetBindings(true);
            this.vistaDocumentoBindingSource.DataSource = Lista;
            this.vistaDocumentoBindingSource.ResetBindings(true);
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FrmReportes F = new FrmReportes();
            F.ReporteGuiaCarga(Lista);
            this.Close();
        }
        private void CargarFactura()
        {
            VistaDocumento = (VistaDocumento)this.bs.Current;
            Documentos doc = new Documentos();            
            if (VistaDocumento != null)
            {
                if (Lista.FirstOrDefault(x => x.IdDocumento == VistaDocumento.IdDocumento) == null)
                { 
                    Lista.Add(VistaDocumento);
                    this.vistaDocumentoBindingSource.ResetBindings(true);
                }
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridControl2_DoubleClick(object sender, EventArgs e)
        {
            CargarFactura();
        }

        private void gridControl2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                CargarFactura();
            }
        }

        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                gridView1.MoveBy(0);
            }
            if (gridView1.ActiveEditor == null)
            {
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Subtract)
                {
                    if (this.gridView1.IsFocusedView)
                    {
                        VistaDocumento Registro = (VistaDocumento)this.vistaDocumentoBindingSource.Current;
                        this.vistaDocumentoBindingSource.Remove(Registro);
                    }
                    e.Handled = true;
                }
            }
        }
    }
}
