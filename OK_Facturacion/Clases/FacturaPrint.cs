using System;
using System.IO;
using System.Data;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Collections.Generic;
using Microsoft.Reporting.WinForms;
using System.Linq;
using System.ComponentModel;
using System.Data.Common;
using System.Drawing;
using System.Windows.Forms;

namespace HK
{
    
    public class FacturaPrint : IDisposable
    {
        static int Contador = 0;
        private int m_currentPageIndex;
        private IList<Stream> m_streams;

        
        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding,
                                  string mimeType, bool willSeek)
        {
            Contador++;
            Stream stream = new FileStream(name + Contador.ToString() +"." + fileNameExtension, FileMode.Create);
            m_streams.Add(stream);
            return stream;
        }

        private void Export(LocalReport report,Documentos doc)
        {          
          //  string s = System.Drawing.Printing.PrinterSettings.InstalledPrinters[0];            
            string deviceInfo =
              "<DeviceInfo>" +
              "  <OutputFormat>EMF</OutputFormat>" +
              "  <PageWidth>8.5in</PageWidth>" +
              "  <PageHeight>8.4in</PageHeight>" +
              "  <MarginTop>0.25in</MarginTop>" +
              "  <MarginLeft>0.25in</MarginLeft>" +
              "  <MarginRight>0.25in</MarginRight>" +
              "  <MarginBottom>0.25in</MarginBottom>" +
              "</DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("image", deviceInfo, CreateStream, out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }

        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);
            ev.Graphics.DrawImage(pageImage, ev.PageBounds);
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        private void Print()
        {

            string printerName = FactoryParametros.Item().ImpresoraFacturas;

            if (m_streams == null || m_streams.Count == 0)
                return;

            PrintDocument printDoc = new PrintDocument();
            printDoc.PrinterSettings.PrinterName = printerName;
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception(String.Format("No se encuentra la impresora \"{0}\".", printerName));
            }
            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
            try
            {
                printDoc.Print();
            }
            catch { }
        }

        public void Run(Documentos Documento)
        {
            using (var dc = new SoroEntities())
            {
                LocalReport report = new LocalReport();
                List<VistaFactura> Registros = FactoryDocumentos.VistaFacturas(dc, Documento.IdDocumento);
                List<ResumenCuentas> Resumen = new List<ResumenCuentas>();
                ResumenCuentas Item = new ResumenCuentas();
                Item.TotalPagado = Documento.Pagos();
                Item.TotalPendiente = (double)(Documento.MontoTotal + Documento.DeudaAnterior - Documento.Pagos());
                Resumen.Add(Item);
                List<ResumenCuentas> Documentos = new List<ResumenCuentas>();
                if (Documento.Tipo == "FACTURA")
                {
                    report.ReportPath = Application.StartupPath + "\\Reportes\\" + "Factura.rdlc";
                }
                if (Documento.Tipo == "PEDIDO")
                {
                    report.ReportPath = Application.StartupPath + "\\Reportes\\" + "PedidoClientes.rdlc";
                }
                report.DataSources.Add(new ReportDataSource("HK_ResumenCuentas", Resumen));
                report.DataSources.Add(new ReportDataSource("HK_VistaFactura", Registros));
                report.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
                ReportParameter p = new
                     ReportParameter("NotasPiePagina", FactoryParametros.Item().NotaPieDeFactura);
                report.SetParameters(new ReportParameter[] { p }); 
                Export(report,Documento);
                m_currentPageIndex = 0;
                Print();
                report.ReleaseSandboxAppDomain();
            }
        }
        public FacturaPrint()
        {           

        }
        public void Dispose()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                {
                    stream.Close();
                }
           //     m_streams = null;
            }
        }
    }
}
