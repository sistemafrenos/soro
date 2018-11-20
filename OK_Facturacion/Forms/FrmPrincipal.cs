using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Data.Linq;
using System.Configuration;
using DevExpress.XtraBars;
using HK.Forms;
using HK.Formas;

namespace HK
{
    public partial class FrmPrincipal : DevExpress.XtraBars.Ribbon.RibbonForm
    {        
        public FrmPrincipal()
        {
            InitializeComponent();
        }
        FrmConsultarProductos frmConsultarProductos = null;
        FrmGrupos frmGrupos = null;
        FrmProductos frmProductos = null;
        FrmCostos frmCostos = null;
        //object UsarFiscal = Application.CommonAppDataRegistry.GetValue("ImpresoraFiscal");

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            //DevExpress.XtraBars.Ribbon.RibbonPage Pagina = new DevExpress.XtraBars.Ribbon.RibbonPage("Pruebas");
            //Pagina.Groups.Add(new DevExpress.XtraBars.Ribbon.RibbonPageGroup("PRUEBAS"));
            //this.ribbonControl1.PageCategories.DefaultCategory.Pages.Add(Pagina);
            //this.ribbonControl1.PageCategories.Add( new DevExpress.XtraBars.Ribbon.RibbonPageCategory("Prueba", new Color()));
            //ImprimirFacturas();
            //if (!cBasicas.ProbarConexion())
            //{
            //    //FrmCadenaConexion F = new FrmCadenaConexion();
            //    //F.ShowDialog();
            //    //if (F.DialogResult != DialogResult.OK)
            //    {
            //        Application.Exit();
            //        return;
            //    }
            //}            
            //ActivarFiscal();
            ElejirUsuario();
            if (FactoryUsuarios.UsuarioActivo == null)
            {
                Application.Exit();
                return;
            }
            this.Usuario.Text = FactoryUsuarios.UsuarioActivo.Nombre + "-" + FactoryUsuarios.UsuarioActivo.Tipo;
            
        }
        private void ElejirUsuario()
        {
            FrmLogin F = new FrmLogin();
            if (F.ShowDialog() == DialogResult.OK)
            {
                FactoryUsuariosDerechos.LimpiarDerechos();
                AsegurarPantalla();
            }
            else
            {
                FactoryUsuarios.UsuarioActivo = null;
            }
        }
        private void AsegurarPantalla()
        {
            foreach (object Item in this.ribbonControl1.Items)
            {

                if (Item.GetType().Name == "BarButtonItem")
                {
                    DevExpress.XtraBars.BarButtonItem MyItem =  (DevExpress.XtraBars.BarButtonItem)Item;
                    if (FactoryUsuarios.UsuarioActivo.Tipo == "SUPER USUARIO")
                    {
                        MyItem.Enabled = true;
                    }
                    else
                    {
                        if (!AsegurarItem(MyItem.Caption))
                        {
                            MyItem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                        }
                    }
                }
            }  
            this.Usuario.Text = FactoryUsuarios.UsuarioActivo.Nombre + "-" + FactoryUsuarios.UsuarioActivo.Tipo;
        }
        private bool AsegurarItem(String x)
        {
            foreach (UsuariosDerechos d in FactoryUsuariosDerechos.UsuarioDerechos(FactoryUsuarios.UsuarioActivo))
            {
                
                if (d.Opcion == x || d.SubOpcion == x)
                {
                    return (bool)d.Habilitado;
                }
            }
            return true;
        }
        private void Productos_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmProductos == null)
            {
                frmProductos = new FrmProductos();
                frmProductos.MdiParent = this;
                frmProductos.Show();
            }
            else
            {
                frmProductos.Activate();
            }
        }

        private void Personas_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmPersonas frmPersonas = new FrmPersonas();
            frmPersonas.MdiParent = this;
            frmPersonas.Show();
        }

        private void barCotizaciones_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmPresupuestos F = new FrmPresupuestos();
            F.MdiParent = this;
            F.Show();
        }

        private void barButtonFacturacion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmFacturas F = new FrmFacturas();
            F.MdiParent = this;
            F.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {      
            this.timer1.Enabled = false;
            this.Text = string.Format("OK FACTURACION - {0} - {1}", FactoryParametros.Item().Empresa, DateTime.Today.ToShortDateString());
            //if ((string)UsarFiscal == "SI")
            //{
            //    #region custom
            //            switch (FactoryParametros.Item().Empresa)
            //            {
            //                case "CENTRO AUTOMOTRIZ SUCRE,C.A.":
            //                        break;
            //                default:
            //                      this.timer1.Enabled = false;
            //                      ImprimirFacturas();
            //                      this.timer1.Enabled = true;
            //                      break;
            //            }
            //        #endregion                  
            //}            
        }
        private void ImprimirFacturas()
        {
            if (cBasicas.ImpresoraOcupada)
                return;
            using (var dc = new SoroEntities())
            {
                var Q = from p in dc.Documentos
                        where (p.Tipo == "FACTURA") && (p.Numero == null)
                        select p;
                foreach (Documentos d in Q)
                {
                    cBasicas.ImprimirFactura(d);
                }
            }
  
        }
        private void ActivarFiscal()
        {
            //if (UsarFiscal == null)
            //{
            //    UsarFiscal = "NO";
            //    Application.CommonAppDataRegistry.SetValue("ImpresoraFiscal", "NO");                     
            //}
            //if (UsarFiscal.ToString() == "SI")
            //{
            //    this.barActivarImpresora.ImageIndex = 6;
            //    cBasicas.ImpresoraOcupada = false;
            //}
            //else
            //{
            //    cBasicas.ImpresoraOcupada = true;
            //    this.barActivarImpresora.ImageIndex = 5;
            //}
        }
        private void barActivarImpresora_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (barActivarImpresora.Checked)
            {
                try
                {
                    FiscalBixolon.OpenFpctrl(FactoryParametros.Item().PuertoImpresora);
                }
                catch( Exception x )
                {
                    MessageBox.Show(x.Message);
                }
                try
                {
                    if (FiscalBixolon.CheckFprinter())
                    {
                        Application.CommonAppDataRegistry.SetValue("ImpresoraFiscal", "SI");
                    }
                    else
                    {
                        throw new Exception("Error de Conexion con impresora Fiscal");
                    }
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message);
                }
            }
            else
            {
                Application.CommonAppDataRegistry.SetValue("ImpresoraFiscal", "NO");
            }
            ActivarFiscal();
 
        }

        private void barButtonReporteX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            timer1.Enabled = false;            
            FiscalBixolon Fiscal = new FiscalBixolon();
           // Fiscal.ConectarImpresora();
            Fiscal.ReporteX();
          //  Fiscal.LiberarImpresora();
            timer1.Enabled = true;
            Fiscal = null;
        }

        private void barButtonReporteZ_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("Esta seguro de emitir el reporte Z para cierre de dia", "ATENCION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                timer1.Enabled = false;
                FiscalBixolon Fiscal = new FiscalBixolon();
           //     Fiscal.ConectarImpresora();
                Fiscal.ReporteZ();
           //     Fiscal.LiberarImpresora();
                timer1.Enabled = true;
                if (Fiscal.IsOK())
                {
                    using (var dc = new SoroEntities())
                    {
                        Parametros p = FactoryParametros.Item(dc);
                        try
                        {
                            if (!p.UltimoReporteZ.HasValue)
                                p.UltimoReporteZ = 1;
                            p.UltimoReporteZ = p.UltimoReporteZ.Value + 1;
                            dc.SaveChanges();
                        }
                        catch
                        {

                        }
                    }
                }
                Fiscal = null;
            }
        }

        private void btnCostos_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmCostos == null)
            {
                frmCostos = new FrmCostos();
                frmCostos.MdiParent = this;
                frmCostos.Show();
            }
            else
            {
                frmCostos.Activate();
            }
        }

        private void barButtonCxC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmCobranzas F = new FrmCobranzas();
            F.MdiParent = this;
            F.Show();
        }

        private void barButtonUsuarios_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmUsuarios F = new FrmUsuarios();
            F.MdiParent = this;
            F.Show();
        }

        private void barButtonParametros_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmParametros F = new FrmParametros();
            F.MdiParent = this;
            F.Show();
        }

        private void barButtonCompras_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmCompras F = new FrmCompras();
            F.MdiParent = this;
            F.Show();
        }

        private void Lineas_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmGrupos == null)
            {
                frmGrupos = new FrmGrupos();
                frmGrupos.MdiParent = this;
                frmGrupos.Show();
            }
            else
            {
                frmGrupos.Activate();
            }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmLibroCompras F = new FrmLibroCompras();
            F.MdiParent = this;
            F.Show();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmLibroVentas F = new FrmLibroVentas();
            F.MdiParent = this;
            F.Show();
        }

        private void barButtonLibroInventarios_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmLibroInventarios F = new FrmLibroInventarios();
            F.MdiParent = this;
            F.Show();
        }

        private void Àjustes_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmAjustes F = new FrmAjustes();
            F.MdiParent = this;
            F.Show();
        }

        private void barButtonCxP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmPagosProveedor F = new FrmPagosProveedor();
            F.MdiParent = this;
            F.Show();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmReportes F = new FrmReportes();
            F.MdiParent = this;
            F.MovimientosDeCaja();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmReportes F = new FrmReportes();
            F.MdiParent = this;
            F.LibroDeVentasDia();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmUsuarios F = new FrmUsuarios();
            F.MdiParent = this;
            F.Show();
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmParametros F = new FrmParametros();
            F.MdiParent = this;
            F.Show();
        }

        private void botonPedidos_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (FactoryParametros.Item().Empresa == "COMERCIAL PADRE E HIJO,C.A.")
            {
                FrmFacturarPedidos f = new FrmFacturarPedidos();
                f.ShowDialog();
            }
            else
            {
                FrmPedidos F = new FrmPedidos();
                F.MdiParent = this;
                F.Show();
            }
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmCuentasBancarias F = new FrmCuentasBancarias();
            F.MdiParent = this;
            F.Show(); 
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmBancosMovimientos F = new FrmBancosMovimientos();
            F.MdiParent = this;
            F.Show();
        }

        private void barConsulta_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmConsultarProductos == null)
            {
                frmConsultarProductos = new FrmConsultarProductos();
                frmConsultarProductos.MdiParent = this;
                frmConsultarProductos.Show();
            }
            else
            {
                frmConsultarProductos.Activate();
            }
        }

        private void xtraTabbedMdiManager1_PageRemoved(object sender, DevExpress.XtraTabbedMdi.MdiTabPageEventArgs e)
        {
            switch (e.Page.MdiChild.Name)
            {
                case "FrmGrupos":
                    frmGrupos = null;
                    break;
                case "FrmProductos":
                    frmProductos = null;
                    break;
                case "FrmPersonas":
                //    frmPersonas = null;
                    break;
                case "FrmCostos":
                    frmCostos = null;
                    break;
                case "FrmConsultarProductos":
                    frmConsultarProductos = null;
                    break;
            }
        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmReportes F = new FrmReportes();
            F.Reporte = "LISTADO INVENTARIOS";
            F.ReporteInventario();
        }

        private void barButtonResumenCaja_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmReportes F = new FrmReportes();
            F.MdiParent = this;
            F.Reporte = "RESUMEN DE CAJA";
            F.ResumenDiario();
        }

        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmReportes F = new FrmReportes();
            F.Reporte = "VENTAS POR VENDEDOR";
            F.VentasPorVendedor();
        }

        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmReportes F = new FrmReportes();
            F.Reporte = "RESUMEN GERENCIAL";
            F.ResumenGerencial();
        }

        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmContarDinero f = new FrmContarDinero();
            f.ShowDialog();
        }

        private void btnCalculadora_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void ribbonControl1_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem19_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmDevoluciones2 f = new FrmDevoluciones2();
            f.MdiParent = this;
            f.Show();
        }

        private void barCalculator_ItemClick(object sender, ItemClickEventArgs e)
        {

            
            System.Diagnostics.Process.Start("calc");
        }

        private static void CargarProveedores()
        {
            using (var dc = new SoroEntities())
            {
                foreach (Productos item in dc.Productos)
                {
                    VistaFactura t = dc.VistaFactura.OrderByDescending(x => x.Fecha).FirstOrDefault(x => x.IdProducto == item.IdProducto && x.Tipo == "COMPRA");
                    if (t != null)
                    {
                        item.IdProveedor = t.IdTercero;
                    }
                }
                dc.SaveChanges();
            }
        }

        private void buttonGuia_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmGuiaCarga f = new FrmGuiaCarga();
            f.MdiParent = this;
            f.Show(); 
        }

        private void barButtonListaPrecios_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmReportes f = new FrmReportes();
            f.ListaPrecios("TODOS");

        }

        private void barButtonItem20_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmPedidosProveedor f = new FrmPedidosProveedor();
            f.MdiParent = this;
            f.Show(); 
        }

        private void barButtonItem21_ItemClick(object sender, ItemClickEventArgs e)
        {
            CargarProveedores();
        }

        private void barButtonItem21_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            using (var x = new SoroEntities())
            {
                foreach (Terceros Tercero in x.Terceros)
                {
                    if (Tercero.CedulaRif.StartsWith("V") || Tercero.CedulaRif.StartsWith("E"))
                    {
                        Tercero.TipoContribuyente = "NO CONTRIBUYENTE";
                    }
                    else
                    {
                        Tercero.TipoContribuyente = "CONTRIBUYENTE";
                    }
                }
                x.SaveChanges();
            }
        }

        private void btnVentasxProducto_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmReportes f = new FrmReportes();
            f.MdiParent = this;
            f.VentasxProducto();
        }

        private void barButtonItem21_ItemClick_2(object sender, ItemClickEventArgs e)
        {

        }

        private void barInventarioxProveedor_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmReportes F = new FrmReportes();
            F.Reporte = "LISTADO INVENTARIOS POR PROVEEDOR";
            F.ReporteInventarioProveedor();
        }

        private void barButtonItem22_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmRevisarPagos f = new FrmRevisarPagos();
            f.MdiParent = this;
            f.Show();
        }

        private void barButtonItem24_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmAplicarPagosExternos f = new FrmAplicarPagosExternos();
            f.MdiParent = this;
            f.Show();
        }

        private void CalcularProducto(Productos producto)
        {
            using (var dc = new SoroEntities())
            {
                List<ProductoHistorial> Lista = new List<ProductoHistorial>();
                var q = from Item in dc.DocumentosProductos
                        join Doc in dc.VistaDocumento on Item.IdDocumento equals Doc.IdDocumento
                        where Item.IdProducto == producto.IdProducto
                            && (Doc.Tipo == "FACTURA" || Doc.Tipo == "AJUSTE" || Doc.Tipo == "COMPRA" || Doc.Tipo == "DEVOLUCION")
                        select new ProductoHistorial
                        {
                            Numero = Doc.Numero,
                            Cantidad = Item.Cantidad,
                            MontoNeto = Item.MontoNeto,
                            Fecha = Doc.Fecha,
                            Entrada = Doc.Tipo == "COMPRA" || Doc.Tipo == "DEVOLUCION" || Doc.Tipo == "AJUSTE" ? Item.Cantidad : 0,
                            Salida = Doc.Tipo == "FACTURA" ? Item.Cantidad : 0,
                            ExistenciaAnterior = Item.ExistenciaAnterior,
                            RazonSocial = Doc.RazonSocial,
                            Tipo = Doc.Tipo,
                            Saldo = Item.ExistenciaAnterior + (Doc.Tipo == "COMPRA" || Doc.Tipo == "DEVOLUCION" || Doc.Tipo == "AJUSTE" ? Item.Cantidad : 0) - (Doc.Tipo == "FACTURA" ? Item.Cantidad : 0),
                        };
                Lista = q.OrderBy(x => x.Fecha).ToList();
                double? Total = 0;
                if (Lista.Count() != 0)
                {
                    double? Existencia = Lista[0].ExistenciaAnterior;
                    if (!Existencia.HasValue)
                    {
                        Existencia = 0;
                    }
                    foreach (ProductoHistorial p in Lista)
                    {
                        p.Saldo = Existencia - p.Salida + p.Entrada;
                        Existencia = p.Saldo;
                    }
                    Total = Lista.LastOrDefault().Saldo;
                }
                if( Total != producto.Existencia )
                {
                    Documentos documento = new Documentos();
                    documento.Activo = true;
                    documento.Tipo = "AJUSTE";
                    documento.Fecha = DateTime.Today;
                    documento.IdDocumento = FactoryContadores.GetLast("IdDocumento");
                    DocumentosProductos documentosProducto = new DocumentosProductos();
                    documentosProducto.Activo = true;
                    documentosProducto.Costo = producto.CostoActual;
                    documentosProducto.Precio = producto.Precio;
                    documentosProducto.Cantidad = producto.Existencia-Total;
                    documentosProducto.Codigo = producto.Codigo;
                    documentosProducto.Descripcion = producto.Descripcion;
                    documentosProducto.IdDetalleDocumento = FactoryContadores.GetLast("IdDetalleDocumento");
                    documentosProducto.IdProducto = producto.IdProducto;
                    documento.DocumentosProductos.Add(documentosProducto);
                    dc.Documentos.Add(documento);                    
                    dc.SaveChanges();
                }
            }
        }
        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            //using (var dc = new SoroEntities())
            //{
            //    foreach (Productos Item in dc.Productos.OrderBy(x=>x.Descripcion))
            //    {
            //        if (Item.Activo==true)
            //        {
            //            CalcularProducto(Item);                         
            //        }
            //    }             
            //}
        }

        private void barRevisionCobranza_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmRevisionCobranza f = new FrmRevisionCobranza();
            f.MdiParent = this;
            f.Show();
        }

        private void barButtonRevisionPagos_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmRevisionPagos f = new FrmRevisionPagos();
            f.MdiParent = this;
            f.Show();
        }

        private void barListItem1_ListItemClick(object sender, ListItemClickEventArgs e)
        {
            FrmReportes f = new FrmReportes();
            DevExpress.XtraBars.BarListItem item = (DevExpress.XtraBars.BarListItem)sender;
            f.ListaPrecios(item.ItemLinks[e.Index].Caption);           
        }

        private void barButtonNotaEntrega_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmNotaEntrega F = new FrmNotaEntrega();
            F.MdiParent = this;
            F.Show();
        }

        private void barButtonGenerarNC_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmGenerarNotaCredito f = new FrmGenerarNotaCredito();
            f.ShowDialog();
        }
    }
}
