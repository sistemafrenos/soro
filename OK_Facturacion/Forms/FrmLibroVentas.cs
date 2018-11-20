using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;

namespace HK
{
    public partial class FrmLibroVentas : Form
    {
        public FrmLibroVentas()
        {
            InitializeComponent();
        }
        SoroEntities dc = new SoroEntities();
        List<LibroVentas> Libro = new List<LibroVentas>();
        LibroVentas nuevoItem = null;
        List<LibroVentas> libroResumido = new List<LibroVentas>();
        private void FrmLibroVentas_Load(object sender, EventArgs e)
        {
            for (int mes = 1; mes <= 12; mes++)
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
            Libro = FactoryLibroVentas.LibroVentas(dc, Convert.ToInt16(this.txtMes.Text), Convert.ToInt16(this.txtAño.Text));
            this.libroBindingSource.DataSource = Libro;
        }
        private void CargarNumeroReporteZ()        
        {
            var x = this.gridView1.GetSelectedCells();
            if (x.Count() > 0)
            {
                FrmInputBox f = new FrmInputBox();
                f.Text = "Introduzca el numero dew reporte Z";
                f.ShowDialog();
                if(f.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    foreach (var y in x)
                    {
                        LibroVentas Item = (LibroVentas)this.libroBindingSource.List[y.RowHandle];
                        Item.ReporteZ = f.texto;
                    }
                    this.libroBindingSource.EndEdit();
                    dc.SaveChanges();
                }
            }
            this.gridView1.RefreshData();
        }
        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (gridView1.ActiveEditor == null)
            {
                if (e.KeyCode == Keys.Return)
                {
                    CargarNumeroReporteZ();
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

        }

        private void Editar_Click(object sender, EventArgs e)
        {
            EditarRegistro();
            Busqueda();
            
        }
        private void AgregarRegistro()
        {
            FrmLibroVentasItem F = new FrmLibroVentasItem();
            F.ShowDialog();
            if (F.DialogResult == DialogResult.OK)
            {
                this.libroBindingSource.Add(F.Registro);
            }
        }
        private void EditarRegistro()
        {

            this.libroBindingSource.EndEdit();
            dc.SaveChanges();
            FrmLibroVentasItem F = new FrmLibroVentasItem();
            LibroVentas Registro = (LibroVentas)this.libroBindingSource.Current;
            if (Registro == null)
                return;
            F.Registro = Registro;
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
                LibroVentas Registro = (LibroVentas)this.libroBindingSource.Current;
                try
                {
                    dc.LibroVentas.Remove(Registro);
                    dc.SaveChanges();
                }
                catch { }
                this.libroBindingSource.Remove(Registro);
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

        private void lToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmReportes F = new FrmReportes();
            F.LibroDeVentas(Libro);
        }

       private void libroDeVentasResumidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResumirLibro();
        }
       private void ResumirLibro()
       {
           SoroEntities mydc = new SoroEntities();
           List<LibroVentas> Libro = FactoryLibroVentas.LibroVentas(mydc, Convert.ToInt16(this.txtMes.Text), Convert.ToInt16(this.txtAño.Text));
           string UltimoZ = Libro[0].ReporteZ;
           DateTime? UltimaFecha = Libro[0].Fecha.Value.Date;
           nuevoItem = null;
           foreach (LibroVentas item in Libro)
           {
               if (
                    item.ReporteZ != UltimoZ
                    || item.Fecha.Value.Date != UltimaFecha.Value.Date)
               {
                   CerrarItem(nuevoItem);
               }
               if (item.CedulaRif[0] == 'J'
                   || item.CedulaRif[0] == 'G')
               {

                   CerrarItem(nuevoItem);
                   CerrarItem(item);
               }
               else
               {
                   AcumularItem(item);
               }
               UltimaFecha = item.Fecha.Value.Date;
               UltimoZ = item.ReporteZ;
           }
           //FrmReportes F = new FrmReportes();
           //F.LibroDeVentasResumido(libroResumido);
           Reportes.ReporteLibroVentas r = new Reportes.ReporteLibroVentas();
           r.DataSource = libroResumido.OrderBy(p => p.Fecha).ToList();
           r.txtEmpresa.Text = FactoryParametros.Item().Empresa;
           r.txtEmpresaRif.Text = FactoryParametros.Item().EmpresaRif;
           r.txtDireccion.Text = FactoryParametros.Item().EmpresaDireccion;
           r.txtTitulo.Text = string.Format("LIBRO DE VENTAS IVA MES:{0} AÑO:{1}", this.txtMes.Text, this.txtAño.Text);
           using (ReportPrintTool printTool = new ReportPrintTool(r))
           {
               printTool.ShowRibbonPreviewDialog();
           }
       }
        private void CerrarItem(LibroVentas item)
        {
            if (item != null)
            {
                if (item.Control != item.Factura)
                {
                    item.Factura = item.Factura + " a " + item.Control;

                }
                libroResumido.Add(item);
                nuevoItem = null;
            }
        }
        private void AcumularItem(LibroVentas Item)
        {
            if (nuevoItem == null)
            {
                nuevoItem = new LibroVentas();
                nuevoItem.BaseImponible = 0;
                nuevoItem.ImpuestoIva = 0;
                nuevoItem.IvaRetenido = 0;
                nuevoItem.MontoIncluyentoIva = 0;
                nuevoItem.TasaIva = 0;
                nuevoItem.VentasNoGravadas = 0;
                nuevoItem.Factura = Item.Factura;
                nuevoItem.NroRegistroMaquinaFiscal = Item.NroRegistroMaquinaFiscal;
                nuevoItem.ReporteZ = Item.ReporteZ;
                nuevoItem.Fecha = Item.Fecha;
                nuevoItem.TasaIva = 0;
                nuevoItem.MontoExento = 0;
                nuevoItem.MontoExentoNoContribuyentes = 0;
                nuevoItem.ImpuestoIvaNoContribuyentes = 0;
                nuevoItem.BaseImponibleNoContribuyentes = 0;
                nuevoItem.Año = Item.Fecha.Value.Year;
                nuevoItem.Mes = Item.Fecha.Value.Month;
                nuevoItem.CedulaRif = (Item.CedulaRif[0] == 'V' || Item.CedulaRif[0] == 'E') ? "V00000000" : Item.CedulaRif;
                nuevoItem.RazonSocial = (Item.CedulaRif[0] == 'V' || Item.CedulaRif[0] == 'E') ? "CONTADO" : Item.CedulaRif;
            }
            nuevoItem.Control = Item.Factura;

            if (nuevoItem.CedulaRif == "V00000000")
            {
                nuevoItem.BaseImponibleNoContribuyentes = nuevoItem.BaseImponibleNoContribuyentes + Item.BaseImponible;
                nuevoItem.ImpuestoIvaNoContribuyentes = Item.ImpuestoIvaNoContribuyentes + Item.ImpuestoIva;
                nuevoItem.MontoExentoNoContribuyentes = Item.MontoExentoNoContribuyentes + Item.VentasNoGravadas;
            }
            else
            {
                nuevoItem.BaseImponible = nuevoItem.BaseImponible + Item.BaseImponible;
                nuevoItem.ImpuestoIva = nuevoItem.ImpuestoIva + Item.ImpuestoIva;
                nuevoItem.VentasNoGravadas = Item.VentasNoGravadas + Item.VentasNoGravadas;
            }
            nuevoItem.MontoIncluyentoIva = nuevoItem.MontoIncluyentoIva + Item.MontoIncluyentoIva;
            nuevoItem.TipoTransaccion = Item.TipoTransaccion;
        }
        private void libroDeVentasReportesZToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmReportes F = new FrmReportes();
            F.LibroDeVentasReportesZ(Libro);
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
