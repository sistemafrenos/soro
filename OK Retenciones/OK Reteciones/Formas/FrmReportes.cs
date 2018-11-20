using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.ReportingServices;
using Microsoft.Reporting.WinForms;
using System.Linq;

namespace HK.Formas
{
    public partial class FrmReportes : Form
    {
        public FrmReportes()
        {
            InitializeComponent();
            this.Load += new EventHandler(FrmReportes_Load);
            this.FormClosing += new FormClosingEventHandler(FrmReportes_FormClosing);
        }

        void FrmReportes_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.reportViewer1.LocalReport.ReleaseSandboxAppDomain();
        }
        void FrmReportes_Load(object sender, EventArgs e)
        {
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - 100;
            this.Width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - 50;
            this.CenterToScreen();
        }
        public void ListadoProveedores(List<Proveedore> lista)
        {
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\ListadoProveedores.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Proveedores", lista));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", new Parametros[] { Basicas.parametros() }));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
        public void ListadoRetenciones(List<Retenciones> Lista)
        {
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\ListadoPropietarios.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Propietarios", Lista.OrderBy(x=> x.NumeroComprobante).ToList()));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
        public void ImprimirRetencion(Retenciones item)
        {
            List<Retenciones> lista = new List<Retenciones>();
            List<Proveedore> proveedores = new List<Proveedore>();
            using (Data d = new Data())
            {
                var q = from p in d.Retenciones
                        where p.NumeroComprobante == item.NumeroComprobante
                        orderby p.Id
                        select p;
                lista = q.ToList();
                proveedores = (from p in d.Proveedores
                                where p.CedulaRif == item.CedulaRif
                                select p).ToList();

            }
          
            List<Parametros> listaparametros = new List<Parametros>();
            listaparametros.Add(Basicas.parametros());
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\Retencion.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Retencion", lista));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", listaparametros));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Proveedor", proveedores));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
        public void ImprimirRetencionDoble(Retenciones item)
        {
            List<Retenciones> lista = new List<Retenciones>();
            using (Data d = new Data())
            {
                var q = from p in d.Retenciones
                        where p.NumeroComprobante == item.NumeroComprobante
                        orderby p.Id
                        select p;
                lista = q.ToList();
            }
            List<Parametros> listaparametros = new List<Parametros>();
            listaparametros.Add(Basicas.parametros());
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\RetencionDoble.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Retencion", lista));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", listaparametros));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
        internal void ImprimirRetencionCH(Retenciones item)
        {
            List<Retenciones> lista = new List<Retenciones>();
            using (Data d = new Data())
            {
                var q = from p in d.Retenciones
                        where p.NumeroComprobante == item.NumeroComprobante
                        orderby p.Id
                        select p;
                lista = q.ToList();
            }
            List<Parametros> listaparametros = new List<Parametros>();
            listaparametros.Add(Basicas.parametros());
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\RetencionCH.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Retencion", lista));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", listaparametros));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
        public void ImprimirRetencionISLR(RetencionesISLR item)
        {
            List<RetencionesISLR> lista = new List<RetencionesISLR>();
            List<Proveedore> listaProveedores = new List<Proveedore>();
            using (Data d = new Data())
            {
                var q = from p in d.RetencionesISLR
                        where p.Numero == item.Numero
                        select p;
                lista = q.ToList();
                listaProveedores = (from p in d.Proveedores
                                     where p.CedulaRif == item.CedulaRif
                                     select p).ToList();

            }
            List<Parametros> listaparametros = new List<Parametros>();
            listaparametros.Add(Basicas.parametros());
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\RetencionISLR.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("RetencionISLR", lista));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Proveedor", listaProveedores));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", listaparametros));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
    }
}
