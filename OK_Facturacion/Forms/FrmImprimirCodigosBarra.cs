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
    public partial class ImprimirCodigoBarras : Form
    {
        List<DocumentosProductos> Lista = new List<DocumentosProductos>();
        
        public ImprimirCodigoBarras()
        {
            InitializeComponent();
        }

        public Documentos Documento = null;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.documentosProductosBindingSource.EndEdit();
            List<DocumentosProductos> CodigosxImprimir = new List<DocumentosProductos>();
            CodigosBarra x = new CodigosBarra();
            for (int I = 1; I <txtEtiquetaInicial.Value; I++)
            {
                CodigosxImprimir.Add(new DocumentosProductos());
            }
            foreach (DocumentosProductos Item in Lista)
            {
                for (int I = 1; I <= Item.Cantidad; I++ )
                {
                    DocumentosProductos Barra = new DocumentosProductos();
                    Barra.Codigo= Item.Codigo;
                    Barra.Descripcion = Item.Descripcion;
                    Item.PrecioIva = Item.Precio + (Item.Precio * Item.TasaIva / 100);
                    Barra.Precio = Item.PrecioIva;
                    x.txtEmpresa.Text = FactoryParametros.Item().Empresa;
                    x.txtFecha.Text = Documento.Fecha.Value.ToShortDateString();
                    CodigosxImprimir.Add(Barra);
                }                
            }
            x.DataSource = CodigosxImprimir;
            x.MostrarCodigo = this.MostrarCodigo.Checked;
            x.MostrarDescripcion = this.MostrarDescripcion.Checked;
            x.MostrarEmpresa = this.MostrarEmpresa.Checked;
            x.MostrarFecha = this.MostrarFecha.Checked;
            x.MostrarPrecio = this.MostrarPrecio.Checked;
            using (ReportPrintTool printTool = new ReportPrintTool(x))
            {
                printTool.ShowRibbonPreviewDialog();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ImprimirCodigoBarras_Load(object sender, EventArgs e)
        {
            foreach (DocumentosProductos Item in Documento.DocumentosProductos)
            {
                DocumentosProductos Barra = new DocumentosProductos();
                Barra.Codigo = Item.Codigo;
                Barra.Descripcion = Item.Descripcion;
                Item.PrecioIva = Item.Precio + (Item.Precio * Item.TasaIva / 100);
                Barra.Cantidad = Item.Cantidad;
                Barra.Precio = Item.PrecioIva;
                Lista.Add(Barra);
            }
            this.documentosProductosBindingSource.DataSource = Lista;          
            this.documentosProductosBindingSource.ResetBindings(true);

        }
    }
}
