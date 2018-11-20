using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Linq;


namespace HK
{
    public partial class FrmReportes : Form
    {
        public FrmReportes()
        {
            InitializeComponent();
            this.Load += new EventHandler(FrmReportes_Load);
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - 100;
            this.Width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - 50;
            this.CenterToScreen();
        }

        void FrmReportes_Load(object sender, EventArgs e)
        {
            this.FormClosed += new FormClosedEventHandler(FrmReportes_FormClosed);
        }

        void FrmReportes_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.reportViewer1.LocalReport.ReleaseSandboxAppDomain();
        }
        public string Reporte = "";
        public void PedidoProveedor(Documento documento)
        {
            Entities dc = new Entities();         
            List<Tercero> prov = new List<Tercero>();
            List<Documento> d = new List<Documento>();
            List<Parametro> p = new List<Parametro>();
            Tercero proveedor = FactoryProveedores.Item(documento.IdTercero);
            Documento Pedido = FactoryDocumentos.Item(dc, documento.IdDocumento);
            Parametro parametro = FactoryParametros.Item();
            d.Add(Pedido);
            p.Add(parametro);
            prov.Add(proveedor);
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "PedidoProveedor.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", p));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Proveedor", prov));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Documento", d));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DocumentoProductos",Pedido.DocumentosProductos.ToList() ));
            this.reportViewer1.RefreshReport();
            this.Show();
        }
        public void ListaPrecios(string TipoPrecio)
        {
            using(var Db = new Entities())
            {
                var p = Db.Productos.OrderBy(x => x.Descripcion);
                reportViewer1.ProcessingMode = ProcessingMode.Local;
                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "ListaPrecios.rdlc";
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Productos",p.ToList()));
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", Db.Parametros.ToList() ));
                this.reportViewer1.RefreshReport();
                this.Show();   
            }
        }
        public void GuiaDeCarga(List<Pedido> pedidos)
        {
            string Facturas = "";
            List<PedidosDetalle> TodosLosProductos = new List<PedidosDetalle>();
            foreach (var item in pedidos)
            {
                TodosLosProductos.AddRange(item.PedidosDetalles);
                Facturas += item.Numero + " ";
            }
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "GuiaCarga.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("PedidosDetalles", TodosLosProductos.OrderBy(x => x.Descripcion)));
            ReportParameter p = new
                 ReportParameter("ListaFacturas", Facturas);
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p });
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter("Empresa", FactoryParametros.Item().Empresa));
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter("EmpresaDireccion", FactoryParametros.Item().EmpresaDireccion));

            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
    }
}