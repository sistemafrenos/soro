using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
//using Microsoft.Office.Interop.Excel;
//using ExcelApp = Microsoft.Office.Interop.Excel.Application;
using System.Reflection;
using HK.Clases;


namespace HK.Formas
{
    public partial class FrmRetenciones : DevExpress.XtraEditors.XtraForm
    {
        Data d = new Data();
        List<Retenciones> lista = new List<Retenciones>();        
        public FrmRetenciones()
        {
            InitializeComponent();
        }
        private void FrmRetenciones_Load(object sender, EventArgs e)
        {
            this.FormClosed += new FormClosedEventHandler(FrmTerceroslista_FormClosed);
            this.gridControl1.KeyDown += new KeyEventHandler(gridControl1_KeyDown);
            this.gridControl1.DoubleClick += new EventHandler(gridControl1_DoubleClick);
            this.btnNuevo.Click += new EventHandler(btnNuevo_Click);
            this.btnEditar.Click += new EventHandler(btnEditar_Click);
            this.btnEliminar.Click += new EventHandler(btnEliminar_Click);
            this.btnBuscar.Click += new EventHandler(btnBuscar_Click);
            this.btnImprimir.Click += new EventHandler(btnImprimir_Click);
            this.btnImprimirDoble.Click += new EventHandler(btnImprimirDoble_Click);
            this.verTodosButton.Click += new EventHandler(verTodosButton_Click);
            this.txtAño.Text = DateTime.Today.Year.ToString();
            this.txtMes.Text = DateTime.Today.Month.ToString("00");
            //this.btnExcel.Click += new EventHandler(btnExcel_Click);
            this.btnTxt.Click += new EventHandler(btnTxt_Click);
            this.toolMontoCH.Click += new EventHandler(toolMontoCH_Click);
            if (DateTime.Today.Day > 15)
                this.txtPeriodo.Text = "SEGUNDO PERIODO";
            else
                this.txtPeriodo.Text = "PRIMER PERIODO";
            Busqueda();
        }

        void verTodosButton_Click(object sender, EventArgs e)
        {
            d = new Data();
            var q = from p in d.Retenciones
                    orderby p.FechaComprobante descending
                    select p;
            lista = q.ToList();
            this.bs.DataSource = lista;
        }

        void toolMontoCH_Click(object sender, EventArgs e)
        {
            Retenciones registro = (Retenciones)this.bs.Current;
            if (registro == null)
                return;
            FrmReportes f = new FrmReportes();
            f.ImprimirRetencionCH(registro);
            f = null;
        }

        void btnImprimirDoble_Click(object sender, EventArgs e)
        {
            Retenciones registro = (Retenciones)this.bs.Current;
            if (registro == null)
                return;
            FrmReportes f = new FrmReportes();
            f.ImprimirRetencionDoble(registro);
            f = null;
        }
        void btnTxt_Click(object sender, EventArgs e)
        {
            SaveFileDialog stxt = new SaveFileDialog();
            stxt.CheckPathExists = true;
            stxt.FileName = txtAño.Text + txtMes.Text + txtPeriodo.Text + ".txt";
            stxt.FileOk += new CancelEventHandler(stxt_FileOk);
            stxt.ShowDialog();
        }

        void stxt_FileOk(object sender, CancelEventArgs e)
        {
            string filename = ((SaveFileDialog)sender).FileName;
            System.IO.StreamWriter f = new System.IO.StreamWriter(filename);
                foreach (Retenciones t in lista)
                {

                    f.Write(Basicas.parametros().CedulaRif + "\t");
                    f.Write(t.PeriodoImpositivo + "\t");
                    f.Write(t.FechaDocumento.Value.Year.ToString() +"-"+ t.FechaDocumento.Value.Month.ToString("00") +"-"+ t.FechaDocumento.Value.Day.ToString("00") + "\t");
                    f.Write(t.TipoOperacion+ "\t");
                    f.Write(t.TipoDocumento + "\t");
                    f.Write(t.CedulaRif + "\t");
                    f.Write(t.NumeroDocumento + "\t");
                    f.Write(t.NumeroControlDocumento + "\t");
                    f.Write(t.MontoDocumento.Value.ToString("N2").PadLeft(15).Replace(".", "").Replace(",", ".") + "\t");
                    f.Write(t.BaseImponible.Value.ToString("N2").PadLeft(15).Replace(".", "").Replace(",", ".") + "\t");
                    f.Write(t.MontoIvaRetenido.Value.ToString("N2").PadLeft(15).Replace(".", "").Replace(",", ".") + "\t");
                    f.Write("0\t");
                    f.Write(t.NumeroComprobante.PadLeft(14) + "\t");
                    f.Write(t.MontoExentoIva.Value.ToString("N2").PadLeft(15).Replace(".", "").Replace(",", ".") + "\t");
                    f.Write(t.Alicuota.Value.ToString("N2").PadLeft(5).Replace(".", "").Replace(",", ".") + "\t");
                    f.WriteLine("0\t");
                }
                f.Close();
        }

        //void btnExcel_Click(object sender, EventArgs e)
        //{
        //    SaveFileDialog s = new SaveFileDialog();
        //    s.CheckPathExists = true;
        //    s.FileName = txtAño.Text + txtMes.Text +txtPeriodo.Text+".xls";
        //    s.FileOk+=new CancelEventHandler(s_FileOk);
        //    s.ShowDialog();
        //}

        //void s_FileOk(object sender, CancelEventArgs e)
        //{
        //    string filename = ((SaveFileDialog)sender).FileName ;
        //  //  this.gridControl1.ExportToXls(filename);
        //    ExcelApp excel = new ExcelApp();
        //    excel.Visible = false;

        //    Workbook libro = excel.Workbooks.Add(Missing.Value);
        //    Worksheet hoja = (Worksheet)excel.ActiveSheet; 

        //    int row = 1;
        //    int col = 1;

        //    foreach (Retenciones t in lista)
        //    {
        //        col = 1;
        //        hoja.Cells[row, col++] = Basicas.parametros().CedulaRif  ;
        //        hoja.Cells[row, col++] =  t.PeriodoImpositivo  ;
        //        hoja.Cells[row, col++] = "'" + t.FechaDocumento.Value.Year.ToString() + "-" + t.FechaDocumento.Value.Month.ToString("00") + "-" + t.FechaDocumento.Value.Day.ToString("00");
        //        hoja.Cells[row, col++] =  t.TipoOperacion  ;
        //        hoja.Cells[row, col++] =  t.TipoDocumento  ;
        //        hoja.Cells[row, col++] =  t.CedulaRif  ;
        //        hoja.Cells[row, col++] =  t.NumeroDocumento.PadLeft(20)  ;
        //        hoja.Cells[row, col++] =  t.NumeroControlDocumento.PadLeft(20)  ;
        //        hoja.Cells[row, col++] =  t.MontoDocumento.Value.ToString("N2").PadLeft(15).Replace(".", "").Replace(",", ".")  ;
        //        hoja.Cells[row, col++] =  t.BaseImponible.Value.ToString("N2").PadLeft(15).Replace(".", "").Replace(",", ".")  ;
        //        hoja.Cells[row, col++] =  t.MontoIvaRetenido.Value.ToString("N2").PadLeft(15).Replace(".", "").Replace(",", ".")  ;
        //        hoja.Cells[row, col++] =  "0";
        //        hoja.Cells[row, col++] =  "'"+t.NumeroComprobante.PadLeft(14)  ;
        //        hoja.Cells[row, col++] =  t.MontoExentoIva.Value.ToString("N2").PadLeft(15).Replace(".", "").Replace(",", ".")  ;
        //        hoja.Cells[row, col++] =  t.Alicuota.Value.ToString("N2").PadLeft(5).Replace(".", "").Replace(",", ".")  ;
        //        hoja.Cells[row++, col++] = "0";
        //    }
        //    hoja.SaveAs(filename);
        //    hoja.Application.Quit();
        //    excel = null;
        //}

        void btnImprimir_Click(object sender, EventArgs e)
        {
            Retenciones registro = (Retenciones)this.bs.Current;
            if (registro == null)
                return;
            FrmReportes f = new FrmReportes();
            f.ImprimirRetencion(registro);
            f = null;
        }
        void txtBuscar_Validating(object sender, CancelEventArgs e)
        {
            Busqueda();
        }
        void btnBuscar_Click(object sender, EventArgs e)
        {
            Busqueda();
        }
        void btnEditar_Click(object sender, EventArgs e)
        {
            EditarRegistro();
        }
        void btnNuevo_Click(object sender, EventArgs e)
        {
            AgregarRegistro();
        }
        void FrmTerceroslista_FormClosed(object sender, FormClosedEventArgs e)
        {
            Pantallas.RetencionesLista = null;
        }
        private void Buscar_Click(object sender, EventArgs e)
        {
            Busqueda();
        }
        private void Busqueda()
        {
            d = new Data();
            string periodoImpositivo = txtAño.Text + txtMes.Text;
            string periodo = txtPeriodo.Text;
            var q = from p in d.Retenciones
                    where p.PeriodoImpositivo==periodoImpositivo
                    select p;
            lista = q.ToList();
            this.bs.DataSource = lista;
        }
        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (gridView1.ActiveEditor == null)
            {
                if (e.KeyCode == Keys.Return)
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
        private void AgregarRegistro() {
            FrmRetencionIVA f = new FrmRetencionIVA();
            f.Nueva( txtAño.Text , txtMes.Text ) ;
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                GuardarRegistro(f.data);
                Busqueda();
            }
        }

        private void GuardarRegistro(List<Retenciones> data)
        {
            foreach (var item in data)
            {
                var registro = new Retenciones();
                registro.Alicuota = item.Alicuota;
                registro.BaseImponible = item.BaseImponible;
                registro.CedulaRif = item.CedulaRif;
                registro.FechaComprobante = item.FechaComprobante;
                registro.FechaDocumento = item.FechaDocumento;
                registro.Id = item.Id;
                registro.MontoDocumento = item.MontoDocumento;
                registro.MontoExentoIva = item.MontoExentoIva;
                registro.MontoIva = item.MontoIva;
                registro.MontoIvaRetenido = item.MontoIvaRetenido;
                registro.NombreRazonSocial = item.NombreRazonSocial;
                registro.NumeroComprobante = item.NumeroComprobante;
                registro.NumeroControlDocumento = item.NumeroControlDocumento;
                registro.NumeroDeOperacion = item.NumeroDeOperacion;
                registro.NumeroDocumento = item.NumeroDocumento;
                registro.NumeroDocumentoAfectado = item.NumeroDocumentoAfectado;
                registro.NumeroExpediente = item.NumeroExpediente;
                registro.Periodo = item.Periodo;
                registro.PeriodoImpositivo = item.PeriodoImpositivo;
                registro.PorcentajeRetencion = item.PorcentajeRetencion;
                registro.RifAgenteRetencion = item.RifAgenteRetencion;
                registro.TipoDocumento = item.TipoDocumento;
                registro.TipoOperacion = item.TipoOperacion;
                registro.NotaCredito = item.NotaCredito;
                registro.NotaDebito = item.NotaDebito;
                d.Retenciones.AddObject(registro);
            }
            try
            {
                d.SaveChanges();
            }
            catch (Exception x)
            {
                string s = x.Message;
            }
        }
        private void AgregarRegistrox()
        {
            FrmRetencionesItem F = new FrmRetencionesItem();
            F.Incluir(txtMes.Text,txtAño.Text);
            if (F.DialogResult == DialogResult.OK)
            {
                F.registro.CedulaRif = Basicas.CedulaRif(F.registro.CedulaRif);
                Proveedore proveedor = FactoryProveedores.ItemCedulaRif(d, F.registro.CedulaRif);
                if (proveedor == null)
                {
                    proveedor = new Proveedore();
                    proveedor.Activo = true;
                }
                proveedor.CedulaRif = F.registro.CedulaRif;
                proveedor.RazonSocial = F.registro.NombreRazonSocial;
                FactoryProveedores.Validar(proveedor);
                if (proveedor.IdProveedor == null)
                {
                    proveedor.IdProveedor = FactoryContadores.GetMax("IdProveedor");
                    d.Proveedores.AddObject(proveedor);
                }
                d.Retenciones.AddObject(F.registro);
                d.SaveChanges();
                Busqueda();
            }
        }
        private void EditarRegistro()
        {
            FrmRetencionesItem F = new FrmRetencionesItem();
            Retenciones registro = (Retenciones)this.bs.Current;
            if (registro == null)
                return;
            F.registro = registro;
            F.Modificar();
            if (F.DialogResult == DialogResult.OK)
            {
                F.registro.CedulaRif = Basicas.CedulaRif(F.registro.CedulaRif);
                Proveedore proveedor = FactoryProveedores.ItemCedulaRif(d,F.registro.CedulaRif);
                if (proveedor == null)
                {
                    proveedor = new Proveedore();
                    proveedor.Activo = true;
                }
                proveedor.CedulaRif = F.registro.CedulaRif;
                proveedor.RazonSocial = F.registro.NombreRazonSocial;
                FactoryProveedores.Validar(proveedor);
                if (proveedor.IdProveedor == null)
                {
                    proveedor.IdProveedor = FactoryContadores.GetMax("IdProveedor");
                    d.Proveedores.AddObject(proveedor);
                }
                d.SaveChanges();
                Busqueda();
            }
        }
        private void EliminarRegistro()
        {
            if (this.gridView1.IsFocusedView)
            {
                Retenciones Registro = (Retenciones)this.bs.Current;
                if (Registro == null)
                    return;
                if (MessageBox.Show("Esta seguro de eliminar este registro", "Atencion", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return;
                }
                try
                {
                    d.DeleteObject(Registro);
                    d.SaveChanges();
                    Busqueda();
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message);
                }
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
        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            EditarRegistro();
        }
    }
}
