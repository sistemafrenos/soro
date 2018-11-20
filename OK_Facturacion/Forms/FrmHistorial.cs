using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HK;
using HK.Clases;

namespace HK.Formas
{
    public partial class FrmHistorial : Form
    {
        public Productos producto = null;
        SoroEntities dc = new SoroEntities();
        List<ProductoHistorial> Lista = new List<ProductoHistorial>();

        public FrmHistorial()
        {
            InitializeComponent();
            this.Load += new EventHandler(FrmHistorial_Load);
        }

        void Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void FrmHistorial_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(FrmHistorial_KeyDown);
            Busqueda();
            this.Aceptar.Visible = false;
            this.Cancelar.Click += new EventHandler(Cancelar_Click);
            this.ButtonDesdeHasta.Click += new EventHandler(ButtonDesdeHasta_Click);
            this.Imprimir.Click+=new EventHandler(Imprimir_Click);
            this.ButtonDesdeHasta.Click+=new EventHandler(ButtonDesdeHasta_Click);
            ProductoHistorial primero = Lista.FirstOrDefault();
            ProductoHistorial ultimo = Lista.LastOrDefault();
            if (primero != null)
                txtDesde.DateTime = primero.Fecha.Value;
            if (ultimo != null)
                txtHasta.DateTime = ultimo.Fecha.Value;
            this.FrmDesdeHasta.Visible = true;
	        gridView1.OptionsBehavior.Editable = false;
        }

        void FrmHistorial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Cancelar.PerformClick();
            }
        }
        void ButtonDesdeHasta_Click(object sender, EventArgs e)
        {
            this.productoHistorialBindingSource.DataSource = Lista.Where(x => x.Fecha >= txtDesde.DateTime.Date && x.Fecha <= txtHasta.DateTime.Date).ToList();
            this.productoHistorialBindingSource.ResetBindings(true);

        }
        private void Busqueda()
        {
            var q = from Item in dc.DocumentosProductos
                    join Doc in dc.VistaDocumento on Item.IdDocumento equals Doc.IdDocumento
                    where Item.IdProducto == producto.IdProducto
                       && (Doc.Tipo == "FACTURA" || Doc.Tipo == "NOTA ENTREGA" || Doc.Tipo == "TICKET"
                        || Doc.Tipo == "AJUSTE" || Doc.Tipo == "COMPRA" || Doc.Tipo == "DEVOLUCION" )                       
                    select new ProductoHistorial
                    {
                        Numero = Doc.Numero,
                        Cantidad = Item.Cantidad,
                        MontoNeto = Item.MontoNeto,
                        Fecha = Doc.Fecha,
                        Entrada = Doc.Tipo == "COMPRA" || Doc.Tipo == "DEVOLUCION" || Doc.Tipo == "AJUSTE" || Doc.Tipo == "PRODUCCION" ? Item.Cantidad : null,
                        Salida = Doc.Tipo == "FACTURA" || Doc.Tipo == "NOTA ENTREGA" || Doc.Tipo == "TICKET" ? Item.Cantidad : null,                        
                        ExistenciaAnterior = Item.ExistenciaAnterior,
                        RazonSocial = Doc.RazonSocial,
                        Tipo = Doc.Tipo,
                    };
            if (q.FirstOrDefault() == null)
                return;
            Lista = q.OrderBy(x=>x.Fecha).ToList();
            double? Existencia = 0;
            foreach( ProductoHistorial p in  Lista)
            {
                p.ExistenciaAnterior = Existencia;
                p.Saldo = Existencia -  p.Salida.GetValueOrDefault(0) + p.Entrada.GetValueOrDefault(0);
                Existencia = p.Saldo;
            }
            //Lista.Where(x => x.Fecha >= txtDesde.DateTime.Date && x.Fecha <= txtHasta.DateTime.Date)
            this.productoHistorialBindingSource.DataSource = Lista;
            this.productoHistorialBindingSource.ResetBindings(true);
        }
        private void Imprimir_Click(object sender, EventArgs e)
        {
            FrmReportes f = new FrmReportes();            
            f.ProductosHistorial(
                Lista.Where(x => x.Fecha >= txtDesde.DateTime.Date && x.Fecha <= txtHasta.DateTime.Date).ToList()
                    ,producto.Descripcion);
        }
    }
 }