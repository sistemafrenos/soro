using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using HK;
using HK.Clases;

namespace HK.Formas
{
    public partial class FrmRetencionesISLR : Form
    {
        Data d = new Data();
        List<RetencionesISLR> lista = new List<RetencionesISLR>();       

        public FrmRetencionesISLR()
        {
            InitializeComponent();
            this.Load += new EventHandler(FrmRencionesISLR_Load);
            this.FormClosed += new FormClosedEventHandler(FrmRencionesISLR_FormClosed);
        }

        void FrmRencionesISLR_FormClosed(object sender, FormClosedEventArgs e)
        {
            Pantallas.RetencionesISLRLista = null;
        }
        void FrmRencionesISLR_Load(object sender, EventArgs e)
        {
            this.gridControl1.KeyDown += new KeyEventHandler(gridControl1_KeyDown);
            this.gridControl1.DoubleClick += new EventHandler(gridControl1_DoubleClick);
            this.btnNuevo.Click += new EventHandler(btnNuevo_Click);
            this.btnEditar.Click += new EventHandler(btnEditar_Click);
            this.btnEliminar.Click += new EventHandler(btnEliminar_Click);
            this.btnBuscar.Click += new EventHandler(btnBuscar_Click);
            this.verTodosButton.Click += new EventHandler(verTodos_Click);
            this.btnImprimir.Click += new EventHandler(btnImprimir_Click);
            this.txtAño.Text = DateTime.Today.Year.ToString();
            this.txtMes.Text = DateTime.Today.Month.ToString("00");
            //this.btnExcel.Click += new EventHandler(btnExcel_Click);
            Busqueda();
        }
        //void btnExcel_Click(object sender, EventArgs e)
        //{
        //    SaveFileDialog s = new SaveFileDialog();
        //    s.CheckPathExists = true;
        //    s.FileName = "Retenciones ISLR " + txtAño.Text + txtMes.Text + ".xls";
        //    s.FileOk += new CancelEventHandler(s_FileOk);
        //    s.ShowDialog();
        //}

        //void s_FileOk(object sender, CancelEventArgs e)
        //{
        //    string filename = ((SaveFileDialog)sender).FileName;
        //    ExcelApp excel = new ExcelApp();
        //    excel.Visible = false;

        //    Workbook libro = excel.Workbooks.Add(Missing.Value);
        //    Worksheet hoja = (Worksheet)excel.ActiveSheet;

        //    int row = 1;
        //    int col = 1;

        //    foreach (RetencionesISLR t in lista)
        //    {
        //        col = 1;
        //        hoja.Cells[row, col++] = Basicas.parametros().CedulaRif;
        //        hoja.Cells[row, col++] = t.Periodo;
        //        hoja.Cells[row, col++] = "'" + t.Fecha.Value.Year.ToString() + "-" + t.Fecha.Value.Month.ToString("00") + "-" + t.Fecha.Value.Day.ToString("00");
        //        hoja.Cells[row, col++] = t.CedulaRif;
        //        hoja.Cells[row, col++] = t.NumeroFactura.PadLeft(20);
        //        hoja.Cells[row, col++] = t.MontoDocumento.Value.ToString("N2").PadLeft(15).Replace(".", "").Replace(",", ".");
        //        hoja.Cells[row, col++] = t.BaseImponible.Value.ToString("N2").PadLeft(15).Replace(".", "").Replace(",", ".");
        //        hoja.Cells[row, col++] = t.ImpuestoRetenido.Value.ToString("N2").PadLeft(15).Replace(".", "").Replace(",", ".");
        //        hoja.Cells[row, col++] = "0";
        //        hoja.Cells[row, col++] = "'" + t.Numero.PadLeft(14);
        //        hoja.Cells[row, col++] = t.PorcentajeRetencion.Value.ToString("N2").PadLeft(5).Replace(".", "").Replace(",", ".");
        //        hoja.Cells[row++, col++] = "0";
        //    }
        //    hoja.SaveAs(filename);
        //    hoja.Application.Quit();
        //    excel = null;
        //}

        void btnImprimir_Click(object sender, EventArgs e)
        {
            RetencionesISLR registro = (RetencionesISLR)this.bs.Current;
            if (registro == null)
                return;
            FrmReportes f = new FrmReportes();
            f.ImprimirRetencionISLR(registro);
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
        void verTodos_Click(object sender, EventArgs e) 
        {
            var q = from p in d.RetencionesISLR
                    orderby p.Fecha
                    descending
                    select p;
            lista = q.ToList();
            this.bs.DataSource = lista;
        }
        void btnEditar_Click(object sender, EventArgs e)
        {
            EditarRegistro();
        }
        void btnNuevo_Click(object sender, EventArgs e)
        {
            AgregarRegistro();
        }
        private void Buscar_Click(object sender, EventArgs e)
        {
            Busqueda();
        }
        private void Busqueda()
        {
            string periodoImpositivo = txtAño.Text + txtMes.Text;
            var q = from p in d.RetencionesISLR
                    where p.Periodo==periodoImpositivo
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
        private void AgregarRegistro()
        {
            FrmRetencionesISLRItem F = new FrmRetencionesISLRItem();
            F.Incluir(txtMes.Text,txtAño.Text);
            if (F.DialogResult == DialogResult.OK)
            {
                GuardarRegistro(F.data);
                Busqueda();
            }
        }
        private void GuardarRegistro(List<RetencionesISLR> data)
        {
            foreach (var item in data)
            {
                var registro = new RetencionesISLR();
                registro.BaseImponible = item.BaseImponible;
                registro.CedulaRif = item.CedulaRif;
                registro.Direccion = item.Direccion;
                registro.Fecha = item.Fecha;
                registro.FechaFactura = item.FechaFactura;
                registro.IdRetencionISLR = FactoryContadores.GetMax("IdRetencionISLR");
                registro.ImpuestoRetenido = item.ImpuestoRetenido;
                registro.MontoDocumento = item.MontoDocumento;
                registro.NombreRazonSocial = item.NombreRazonSocial;
                registro.Numero = item.Numero;
                registro.NumeroFactura = item.NumeroFactura;
                registro.Periodo = item.Periodo;
                registro.PorcentajeRetencion = item.PorcentajeRetencion;
                registro.Sustraendo = item.Sustraendo;
                registro.Control = item.Control;
                d.AddToRetencionesISLR(registro);
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
        private void EditarRegistro()
        {
            FrmRetencionesISLRItem F = new FrmRetencionesISLRItem();
            RetencionesISLR registro = (RetencionesISLR)this.bs.Current;
            if (registro == null)
                return;
            F.registro = registro;
            F.Modificar();
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
                proveedor.Direccion = F.registro.Direccion;
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
                RetencionesISLR Registro = (RetencionesISLR)this.bs.Current;
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
