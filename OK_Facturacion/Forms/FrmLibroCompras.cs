using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraExport;
using DevExpress.XtraGrid.Export;
using HK.Reportes;
using DevExpress.XtraReports.UI;

namespace HK
{
    public partial class FrmLibroCompras : Form
    {
        
        public FrmLibroCompras()
        {
            InitializeComponent();
        }
        SoroEntities dc = new SoroEntities();
        List<LibroCompras> Libro = new List<LibroCompras>();
        private void FrmLibroCompras_Load(object sender, EventArgs e)
        {
            for (int mes=1; mes <= 12; mes++)
            {
                this.txtMes.Items.Add(mes);
            }
            for (int año = DateTime.Today.Year - 5; año <= DateTime.Today.Year; año++)
            {
                this.txtAño.Items.Add(año);            
            }
            this.txtAño.Text = DateTime.Today.Year.ToString();
            this.txtMes.Text = DateTime.Today.Month.ToString();
        }

        private void Buscar_Click(object sender, EventArgs e)
        {
            Busqueda();
        }

        private void Busqueda()
        {
            dc = new SoroEntities();
            Libro = FactoryLibroCompras.LibroCompras(dc, Convert.ToInt16(this.txtMes.Text), Convert.ToInt16(this.txtAño.Text));
            this.libroComprasBindingSource.DataSource = Libro;
        }
        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (gridView1.ActiveEditor == null)
            {
                if (e.KeyCode == Keys.Return )
                {
                    EditarRegistro();
                }
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Subtract)
                {
                    EliminarRegistro();
                }
                if (e.KeyCode == Keys.Insert)
                {
                    AgregarRegistro();
                }
            }

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void Imprimir_Click(object sender, EventArgs e)
        {
            ReporteLibroCompras r = new ReporteLibroCompras();
            r.DataSource = Libro.OrderBy(p => p.Fecha).ToList();
            r.txtEmpresa.Text = FactoryParametros.Item().Empresa;
            r.txtEmpresaRif.Text = FactoryParametros.Item().EmpresaRif;
            using (ReportPrintTool printTool = new ReportPrintTool(r))
            {
                printTool.ShowRibbonPreviewDialog();
            }
            //FrmReportes F = new FrmReportes();
            //    F.LibroDeCompras(Libro);
        }

        private void Editar_Click(object sender, EventArgs e)
        {
            EditarRegistro();
            Busqueda();
        }
        private void AgregarRegistro()
        {
            FrmLibroComprasItem F = new FrmLibroComprasItem();
            F.ShowDialog();
            if (F.DialogResult == DialogResult.OK)
            {
                this.libroComprasBindingSource.Add(F.Registro);
            }
        }
        private void EditarRegistro()
        {
            FrmLibroComprasItem F = new FrmLibroComprasItem();
            LibroCompras Registro = (LibroCompras)this.libroComprasBindingSource.Current;
            if (Registro == null)
                return;
            F.Registro =  Registro;
            F.ShowDialog();
            if (F.DialogResult == DialogResult.OK)
            {
                Registro = F.Registro;
            }
        }
        private void EliminarRegistro()
        {
            if (this.gridView1.IsFocusedView)
            {
                LibroCompras Registro = (LibroCompras)this.libroComprasBindingSource.Current;
                try
                {
                    dc.LibroCompras.Remove(Registro);
                    dc.SaveChanges();
                }
                catch { }
                this.libroComprasBindingSource.Remove(Registro);
            }
        }

        private void Nuevo_Click(object sender, EventArgs e)
        {
            AgregarRegistro();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }

       private void txtMes_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                int mes = Convert.ToInt16(this.txtMes.Text);
                if (mes < 1 || mes > 12)
                {
                    this.txtMes.Text = DateTime.Today.Month.ToString();
                }

            }
            catch
            {
                this.txtMes.Text = DateTime.Today.Month.ToString();
            }
        }

    }
}
