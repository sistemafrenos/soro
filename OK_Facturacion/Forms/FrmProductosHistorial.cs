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

    public partial class FrmProductosHistorial : Form
    {

        public FrmProductosHistorial()
        {
            InitializeComponent();
        }

        public Productos producto = null;
        SoroEntities dc = new SoroEntities();
        List<ProductoHistorial> Lista = new List<ProductoHistorial>();
        private void GridControl1_Click(object sender, EventArgs e)
        {

        }

        private void FrmProductosHistorialCompras_Load(object sender, EventArgs e)
        {
            txtDesde.DateTime = DateTime.Today.AddDays(-30);
            txtHasta.DateTime = DateTime.Today;
            Busqueda();
        }

        private void Busqueda()
        {
            var inicioEntradas = (from item in dc.DocumentosProductos
                                  join doc in dc.Documentos on item.IdDocumento equals doc.IdDocumento
                                  where 
                                     ( doc.Fecha <= txtDesde.DateTime.Date )
                                  && ( doc.Tipo == "COMPRA" || doc.Tipo == "DEVOLUCION" || doc.Tipo == "AJUSTE" )
                                  && ( item.IdProducto == producto.IdProducto)
                                  select item.Cantidad).Sum();
            var inicioSalidas = (from item in dc.DocumentosProductos
                                  join doc in dc.Documentos on item.IdDocumento equals doc.IdDocumento
                                  where doc.Fecha <= txtDesde.DateTime.Date
                                  && doc.Tipo == "FACTURA"
                                  && item.IdProducto == producto.IdProducto
                                  select item.Cantidad).Sum();
            double? cantidadInicial = inicioEntradas.GetValueOrDefault(0) - inicioSalidas.GetValueOrDefault(0);
            var q = from Item in dc.DocumentosProductos
                    join Doc in dc.VistaDocumento on Item.IdDocumento equals Doc.IdDocumento
                    where Item.IdProducto == producto.IdProducto
                        && Doc.Fecha>= txtDesde.DateTime.Date
                        && Doc.Fecha <= txtHasta.DateTime.Date
                        && (Doc.Tipo == "FACTURA" || Doc.Tipo == "AJUSTE" || Doc.Tipo == "COMPRA" || Doc.Tipo == "DEVOLUCION") 
                    select new ProductoHistorial
                    {
                        Numero = Doc.Numero,  
                        Cantidad = Item.Cantidad,
                        MontoNeto = Item.MontoNeto,
                        Fecha = Doc.Fecha,
                        Entrada = Doc.Tipo == "COMPRA" || Doc.Tipo == "DEVOLUCION" || Doc.Tipo == "AJUSTE" ? Item.Cantidad : 0,
                        Salida =  Doc.Tipo == "FACTURA" ?  Item.Cantidad :0,                        
                        ExistenciaAnterior = 0,
                        RazonSocial = Doc.RazonSocial,
                        Tipo = Doc.Tipo,
                        Saldo =0
                    };
            if( txtTipo.Text != "TODOS" )
            {
                q = q.Where(x => x.Tipo == txtTipo.Text);
            }  
            Lista = q.OrderBy(x=>x.Fecha).ToList();
            foreach( ProductoHistorial p in  Lista)
            {
                p.ExistenciaAnterior = cantidadInicial;
                p.Saldo = p.ExistenciaAnterior + p.Entrada - p.Salida;
                cantidadInicial = p.Saldo;
            }
            this.bindingSource1.DataSource = Lista;
            this.bindingSource1.ResetBindings(true);
        }
        private void GridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void BotonBuscar_Click(object sender, EventArgs e)
        {
            Busqueda();
        }

        private void Imprimir_Click(object sender, EventArgs e)
        {
            FrmReportes f = new FrmReportes();            
            f.ProductosHistorial(Lista,producto.Descripcion);
        }
    }
   
}
