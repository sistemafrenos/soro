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
            this.Load+= new EventHandler(FrmReportes_Load);
        }

        void FrmReportes_Load(object sender, EventArgs e)
        {
            this.FormClosed+=new FormClosedEventHandler(FrmReportes_FormClosed);
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - 100;
            this.Width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - 50;
            this.CenterToScreen();
        }
        public string Reporte = "";
        public TercerosConSaldos cliente;
        Terceros Titular = null;
        List<Terceros> Titulares = new List<Terceros>();
        public void ResumenGerencial()
        {
            List<cResumen> Registros = new List<cResumen>();
            cResumen Item = new cResumen
            {
                Bancos = FactoryBancos.SaldoFinal(),
                Caja = FactoryCaja.SaldoFinal(),
                Cuentasxcobrar = FactoryCuentas.CuentasxCobrarSaldo(),
                Cuentasxpagar = FactoryCuentas.CuentasxPagarSaldo(),
                Inventarios = FactoryProductos.MontoInventarios()
            };
            Registros.Add(Item);
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "ResumenGerencial.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_cResumen", Registros));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
            this.reportViewer1.RefreshReport();
            this.Show();
        }
        public void ReporteTercerosMovimientos()
        {
            this.txtDesde.DateTime = DateTime.Today;
            this.txtHasta.DateTime = DateTime.Today;
            this.Reporte = "MOVIMIENTOS CUENTAS";
            this.FrmDesdeHasta.Visible = true;
            this.Text = this.Reporte;
            this.Show();
        }
        public void RegistroPagosExternos()
        {
            this.txtDesde.DateTime = DateTime.Today;
            this.txtHasta.DateTime = DateTime.Today;
            this.Reporte = "Registro Pagos Externos";
            this.FrmDesdeHasta.Visible = true;
            this.Text = this.Reporte;
            this.Show();
        }
        public void ReciboPago(RegistroPagos Item)
        {
            List<Documentos> FacturasCanceladas = new List<Documentos>();
            string[] x = Item.Documento.Split(' ');
            using (var dc = new SoroEntities())
            {
                foreach (string s in x)
                {
                    if (!string.IsNullOrWhiteSpace(s))
                    {
                        Documentos Factura = FactoryDocumentos.ItemxNumero(dc, s);
                        if (Factura != null)
                        {
                            var q = (from p in dc.RegistroPagosDetalles
                                     where p.IdDocumento == Factura.IdDocumento && p.IdRegistroPago == Item.IdRegistroPago
                                     select p).FirstOrDefault();
                            if (q != null)
                            {
                                Factura.MontoTotal = q.Monto;                                
                            }
                        }
                        FacturasCanceladas.Add(Factura);
                    }
                }
            }
            List<RegistroPagos> Registros = new List<RegistroPagos>
            {
                Item
            };
            Terceros T = FactoryTerceros.Item(Item.IdTercero);
            T.LimiteCredito = T.MontoPendienteActual;
            List<Terceros> lista = new List<Terceros>
            {
                T
            };
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "ReciboPagos2.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Registros", Registros));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Terceros", lista));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Facturas", FacturasCanceladas));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
            this.reportViewer1.RefreshReport();
            this.Show();
        }
        public void ReciboPagoExterno(RegistroPagosExternos Item)
        {
            List<Documentos> FacturasCanceladas = new List<Documentos>();
            string[] x = Item.Facturas.Split(' ');
            using(var dc = new SoroEntities())
            {
                foreach (string s in x)
                {
                    if (!string.IsNullOrWhiteSpace(s))
                    {
                        Documentos Factura = FactoryDocumentos.ItemxNumero(dc, s);
                        if (Factura != null)
                        {
                            Cuentas Cuenta = FactoryCuentas.Item(dc, Factura.IdDocumento);
                            if (Cuenta != null)
                            {
                                if (Cuenta.Saldo > 0)
                                {
                                    Factura.MontoTotal = Cuenta.Monto - Cuenta.Saldo;
                                }
                            }
                            FacturasCanceladas.Add(Factura);
                        }
                    }
                }
            }
            List<RegistroPagosExternos> Registros = new List<RegistroPagosExternos>();
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            Registros.Add(Item);
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "ReciboPagosExternos.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Registros", Registros));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Facturas", FacturasCanceladas));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
            this.reportViewer1.RefreshReport();
            this.Show();
        }

        public void ListaPrecios(string TipoPrecio)
        {
            List<VistaProductos> p = FactoryProductos.Buscar("");
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            switch (TipoPrecio)
            {
                case "PRECIO 1":
                    this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "ListaPrecios1.rdlc";
                    break;
                case "PRECIO 2":
                    this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "ListaPrecios2.rdlc";
                    break;
                case "PRECIO 3":
                    this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "ListaPrecios3.rdlc";
                    break;
                case "PRECIO 4":
                    this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "ListaPrecios4.rdlc";
                    break;
                case "TODOS":
                    this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "ListaPrecios.rdlc";
                    break;
            }
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_VistaProductos", p));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
            this.reportViewer1.RefreshReport();
            this.Show();
        }
        public void ProductosHistorial(List<ProductoHistorial> Registros,string Producto)
        {
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "ProductoHistorial.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_ProductoHistorial", Registros));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", FactoryParametros.SelectAll()));
            ReportParameter p = new
                ReportParameter("Producto", Producto);
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p });
            this.reportViewer1.RefreshReport();
            this.Show();
        }
        public void CuentasBancarias(List<Bancos> Registros)
        {
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "CuentasBancarias.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Bancos", Registros));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
            this.reportViewer1.RefreshReport();
            this.Show();
        }
        public void ReporteGuiaCarga(List<VistaDocumento> ListaDocumentos)
        {
            string Facturas = "";
            List<VistaFactura> ListaFacturas = new List<VistaFactura>();
            foreach (VistaDocumento d in ListaDocumentos)
            {
                List<VistaFactura> ItemVista = FactoryDocumentos.VistaFacturas(new SoroEntities(), d.IdDocumento);
                ListaFacturas.AddRange(ItemVista);
                Facturas += d.Numero + " ";
            }
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "GuiaCarga.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_VistaFactura", ListaFacturas.OrderBy(x=> x.Descripcion)));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
            ReportParameter p = new
                 ReportParameter("ListaFacturas", Facturas);
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p });
            this.reportViewer1.RefreshReport();
            this.Show();

        }
        public void MovimientosBancarios(Bancos Banco, List<BancosMovimientos> Registros)
        {
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "MovimientosBancarios.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Bancos", new List<Bancos>(new Bancos[] { Banco })));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_BancosMovimientos", Registros));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
            this.reportViewer1.RefreshReport();
            this.Show();
        }
        public void PedidoProveedor(Documentos Documento)
        {
            List<VistaFactura> Pedido = FactoryDocumentos.VistaFacturas(new SoroEntities(), Documento.IdDocumento);
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "PedidoProveedor.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_VistaFactura", Pedido));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
            this.reportViewer1.RefreshReport();
            this.Show();
        }
        public void ReciboCobro(List<VistaRecibo> Registros)
        {
            if (Registros.Count > 0)
            {
                reportViewer1.ProcessingMode = ProcessingMode.Local;
                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "ReciboCobro.rdlc";
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_VistaRecibo", Registros));
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_RecibosPagos", FactoryRecibos.ItemxId(new SoroEntities(), Registros[0].IdRecibo).RecibosPagos));
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
                this.reportViewer1.RefreshReport();
                this.Show();
            }
        }
        public void ReciboPago(List<VistaRecibo> Registros)
        {
            if (Registros.Count > 0)
            {
                reportViewer1.ProcessingMode = ProcessingMode.Local;
                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "ReciboPago.rdlc";
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_VistaRecibo", Registros));
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_RecibosPagos", FactoryRecibos.ItemxId(new SoroEntities(), Registros[0].IdRecibo).RecibosPagos));
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
                this.reportViewer1.RefreshReport();
                this.Show();
            }
        }

        public void EstadoDeCuentaxCobrar(Terceros _Titular)
        {
            Titular = _Titular;
            this.txtFecha.DateTime = DateTime.Today;
            this.Reporte = "CUENTAS X COBRAR";
            this.FrmFecha.Visible = true;
            this.Text = this.Reporte;
            this.Show();
        }
        public void VentasxProducto()
        {
            
            this.txtDesde.DateTime = DateTime.Today;
            this.txtHasta.DateTime = DateTime.Today;
            this.Reporte = "VENTAS POR PRODUCTO";
            this.FrmDesdeHasta.Visible = true;
            this.Text = this.Reporte;
            this.Show();
        }
        public void ResumenDiario()
        {
            this.txtDesde.DateTime = DateTime.Today;
            this.txtHasta.DateTime = DateTime.Today;
            this.Reporte = "RESUMEN DIARIO";
            this.FrmDesdeHasta.Visible = true;
            this.Text = this.Reporte;
            this.Show();
        }
        public void VentasPorVendedor()
        {
            this.txtDesde.DateTime = DateTime.Today;
            this.txtHasta.DateTime = DateTime.Today;
            this.Reporte = "VENTAS POR VENDEDOR";
            this.FrmDesdeHasta.Visible = true;
            this.Text = this.Reporte;
            this.Show();
        }
        public void ListadoCuentasCxC()
        {
            this.txtFecha.DateTime = DateTime.Today;
            this.Reporte = "LISTADO CUENTAS X COBRAR";
            this.FrmFecha.Visible = true;
            this.Text = this.Reporte;
            this.Show();
        }
        public void ResumenCxC()
        {
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "ResumenCuentasxCobrar.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_VistaCuentas", FactoryCuentas.VistaCuentas("CXC")));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
            this.reportViewer1.RefreshReport();
            this.Show();
        }
        public void EstadoDeCuentaxPagar(Terceros _Titular)
        {
            Titular = _Titular;
            this.txtFecha.DateTime = DateTime.Today;
            this.Reporte = "CUENTAS X PAGAR";
            this.FrmFecha.Visible = true;
            this.Text = this.Reporte;
            this.Show();
        }
        public void ListadoCuentasCxP()
        {
            this.txtFecha.DateTime = DateTime.Today;
            this.Reporte = "LISTADO CUENTAS X PAGAR";
            this.FrmFecha.Visible = true;
            this.Text = this.Reporte;
            this.Show();
        }
        public void ResumenCxP()
        {
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "ResumenCuentasxPagar.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_VistaCuentas", FactoryCuentas.VistaCuentas("CXP")));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
            this.reportViewer1.RefreshReport();
            this.Show();
        }
        public void MovimientosDeCaja()
        {

            this.txtDesde.DateTime = DateTime.Today;
            this.txtHasta.DateTime = DateTime.Today;
            this.Reporte = "MOVIMIENTOS DE CAJA";
            this.FrmDesdeHasta.Visible = true;
            this.Text = this.Reporte;
            this.Show();
        }
        public void LibroDeVentasDia()
        {
            this.txtFecha.DateTime = DateTime.Today;
            this.Reporte = "LIBRO VENTAS DIA";
            this.FrmFecha.Visible = true;
            this.Text = this.Reporte;
            this.Show();
        }
        public void ReporteInventarioProveedor()
        {
            this.Reporte = "LISTADO INVENTARIO POR PROVEEDORES";
            this.groupGrupos.Visible = true;
            this.comboBoxGrupos.Text = "TODOS";
            this.lblGrupos.Text = "PROV.";
            using (var dc = new SoroEntities())
            {
                var Proveedores = (from p in dc.VistaProductos
                         orderby p.RazonSocial
                         where p.RazonSocial != null
                         select p.RazonSocial).Distinct();
                comboBoxGrupos.Properties.Items.AddRange(Proveedores.ToArray());
            }
            this.Show();
        }
        public void ReporteInventario()
        {
            this.Reporte = "LISTADO INVENTARIO";
            this.groupGrupos.Visible = true;
            this.comboBoxGrupos.Text = "TODOS";
            using (var dc = new SoroEntities())
            {
                foreach (Grupos Item in dc.Grupos)
                {
                    try
                    {
                        comboBoxGrupos.Properties.Items.Add(Item.Grupo);
                    }
                    catch { }
                }
            }
            this.Show();
        }
        public void LibroDeCompras(List<LibroCompras> Registros)
        {
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "LibroDeCompras.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_LibroCompras", Registros));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
            this.reportViewer1.RefreshReport();
            this.Show();
        }
        public void LibroDeVentas(List<LibroVentas> Registros)
        {
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "LibroDeVentas.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_LibroVentas", Registros));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
            this.reportViewer1.RefreshReport();
            this.Show();
        }
        public void LibroDeVentasResumido(List<LibroVentas> Registros)
        {
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "LibroDeVentasResumido.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_LibroVentas", Registros));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
            this.reportViewer1.RefreshReport();
            this.Show();
        }
        public void LibroDeVentasReportesZ(List<LibroVentas> Registros)
        {
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "LibroDeVentasReportesZ.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_LibroVentas", Registros));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
            this.reportViewer1.RefreshReport();
            this.Show();
        }
        public void LibroDeInventarios(List<LibroInventarios> Registros)
        {
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "LibroDeInventarios.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_LibroInventarios", Registros));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
            this.reportViewer1.RefreshReport();
            this.Show();
        }
        public void ReporteDevolucion(List<VistaFactura> Registros)
        {
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "Devolucion.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_VistaFactura", Registros));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
        public void ReportePresupuesto(List<VistaFactura> Registros)
        {
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "Presupuesto.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_VistaFactura", Registros));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
        public void ReportePedido(List<VistaFactura> Registros)
        {
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "Pedido.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_VistaFactura", Registros));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
        public void ReporteCompras(List<VistaCompras> Registros)
        {
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "Compras.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_VistaCompras", Registros));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
        public void ReporteFactura(Documentos Documento)
        {
            using (var dc = new SoroEntities())
            {
                List<VistaFactura> Registros = FactoryDocumentos.VistaFacturas(dc, Documento.IdDocumento);
                List<ResumenCuentas> Resumen = new List<ResumenCuentas>();
                ResumenCuentas Item = new ResumenCuentas();
                try
                {
                    Item.TotalPagado = Documento.Pagos();
                    Item.TotalPendiente = (double)(Documento.MontoTotal + Documento.DeudaAnterior - Documento.Pagos());
                }
                catch { }
                Resumen.Add(Item);
                List<ResumenCuentas> Documentos = new List<ResumenCuentas>();
                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "Factura.rdlc";
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_ResumenCuentas", Resumen));
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_VistaFactura", Registros.OrderBy(X=>X.Descripcion).ToList()));
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
                this.reportViewer1.RefreshReport();
                this.ShowDialog();
            }
        }
        public void ReporteNotaCredito(Documentos Documento)
        {
            using (var dc = new SoroEntities())
            {
                List<VistaFactura> Registros = FactoryDocumentos.VistaFacturas(dc, Documento.IdDocumento);                
                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "NotaCredito.rdlc";                
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_VistaFactura", Registros.OrderBy(X => X.Descripcion).ToList()));
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
                this.reportViewer1.RefreshReport();
                this.ShowDialog();
            }
        }
        public void ReporteNotaEntrega(Documentos Documento)
        {
            using (var dc = new SoroEntities())
            {
                List<ResumenCuentas> Resumen = new List<ResumenCuentas>();
                ResumenCuentas Item = new ResumenCuentas();
                try
                {
                    Item.TotalPagado = Documento.Pagos();
                    Item.TotalPendiente = (double)(Documento.MontoTotal + Documento.DeudaAnterior - Documento.Pagos());
                }
                catch { }
                Resumen.Add(Item);
                List<ResumenCuentas> Documentos = new List<ResumenCuentas>();
                List<VistaFactura> Registros = FactoryDocumentos.VistaFacturas(dc, Documento.IdDocumento);

                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "NotaEntrega.rdlc";
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_ResumenCuentas", Resumen));
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_VistaFactura", Registros.OrderBy(x=> x.Descripcion).ToList()));
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter("Numero",Documento.Numero));
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter("Fecha", Documento.Fecha.Value.ToShortDateString()));
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter("Empresa", FactoryParametros.Item().Empresa));
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter("EmpresaRif", FactoryParametros.Item().EmpresaRif));
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter("ClienteRIF", Registros[0].CedulaRif));
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter("ClienteRazonSocial", Registros[0].RazonSocial));
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter("ClienteDireccion", Registros[0].Direccion));
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter("ClienteTelefonos", Registros[0].Telefonos));
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter("Condiciones", Registros[0].Comentarios));
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter("Peso", Documento.PesoFactura.Value.ToString("N2")));
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter("DeudaAnterior", Documento.DeudaAnterior.Value.ToString("N2")));
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter("EstePedido", Documento.MontoTotal.Value.ToString("N2")));
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter("Pagos", Resumen[0].TotalPagado.ToString("N2")));
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter("Saldo", Resumen[0].TotalPendiente.ToString("N2")));
                this.reportViewer1.RefreshReport();
                this.ShowDialog();
            }
        }

        public void ReporteDevolucion(Documentos Documento)
        {
            using (var dc = new SoroEntities())
            {
                List<VistaFactura> Registros = FactoryDocumentos.VistaFacturas(dc, Documento.IdDocumento);
                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "Devolucion.rdlc";
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_VistaFactura", Registros));
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
                this.reportViewer1.RefreshReport();
                this.ShowDialog();
            }
        }
        public void ReportePedido(Documentos Documento)
        {
            using (var dc = new SoroEntities())
            {
                List<VistaFactura> Registros = FactoryDocumentos.VistaFacturas(dc, Documento.IdDocumento);
                List<ResumenCuentas> Resumen = new List<ResumenCuentas>();
                ResumenCuentas Item = new ResumenCuentas
                {
                    TotalPagado = Documento.Pagos(),
                    TotalPendiente = (double)(Documento.MontoTotal + Documento.DeudaAnterior - Documento.Pagos())
                };
                Resumen.Add(Item);
                List<ResumenCuentas> Documentos = new List<ResumenCuentas>();
                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "Pedido.rdlc";
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_ResumenCuentas", Resumen));
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_VistaFactura", Registros));
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
                this.reportViewer1.RefreshReport();
                this.ShowDialog();
            }
        }
        private void FrmReporteTraslados_Load(object sender, EventArgs e)
        {

        }
        public void CierreDeCaja(DateTime Fecha)
        {
            this.ShowDialog();
        }

        public void ReporteUsuarios(List<Usuarios> Registros)
        {
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "Usuarios.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Usuarios", Registros));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
        public void ReporteProveedores(List<Terceros> Registros)
        {
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "Proveedores.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Myds_Terceros", Registros));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }

        public void ReporteServicios(List<Productos> Registros)
        {
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "Servicios.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Productos", Registros));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
        public void ReporteTerceros(List<Terceros> Registros)
        {
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "Terceros.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Myds_Terceros", Registros));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }

        private void CargarReporte_Click(object sender, EventArgs e)
        {
            switch (this.Reporte.ToUpper())
            {
                case "REGISTRO PAGOS EXTERNOS":
                    using(var db = new SoroEntities())
                    {
                    List<RegistroPagosExternos> Lista = new List<RegistroPagosExternos>();
                
                    var Consulta = from q in db.RegistroPagosExternos
                                   where q.Fecha >= txtDesde.DateTime && q.Fecha <= txtHasta.DateTime
                                   orderby q.Fecha,q.Equipo
                                   select q;                                  
                    reportViewer1.ProcessingMode = ProcessingMode.Local;
                    this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "RegistroPagosExternos.rdlc";
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Registros", Consulta.ToList()));
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
                    this.reportViewer1.RefreshReport();
                    }
                    break;
                case "MOVIMIENTOS CUENTAS":                    
                    using(var db = new SoroEntities())
                    {
                    double? CargosaLaFecha = (from x in db.VistaDocumento
                                            where x.IdTercero == cliente.IdTercero && x.Fecha < txtDesde.DateTime 
                                            select x.MontoTotal).Sum();
                    double? PagosaLaFecha = (from x in db.RegistroPagos
                                         where x.IdTercero == cliente.IdTercero && x.Fecha <txtDesde.DateTime 
                                          select x.MontoPagado).Sum();
                    double SaldoAnterior = CargosaLaFecha.GetValueOrDefault(0) - PagosaLaFecha.GetValueOrDefault(0);
                    List<TercerosMovimientos> Lista = new List<TercerosMovimientos>();
                
                    var Facturas30 = (from p in db.VistaDocumento
                                      where p.CedulaRif == cliente.CedulaRif && p.Fecha >= txtDesde.DateTime && p.Fecha <= txtHasta.DateTime  && p.Tipo == "FACTURA"
                                    select p).ToList();
                    var Pagos30 = (from q in db.RegistroPagos
                                   where q.IdTercero == cliente.IdTercero && q.Fecha >= txtDesde.DateTime && q.Fecha <= txtHasta.DateTime
                                   select q).ToList();
                    foreach (VistaDocumento Doc in Facturas30)
                    {
                            TercerosMovimientos newItem = new TercerosMovimientos
                            {
                                Concepto = "FACTURA NRO:" + Doc.Numero,
                                Debito = Doc.MontoTotal,
                                Numero = Doc.Numero,
                                Fecha = Doc.Fecha,
                                Vence = Doc.Vence,
                                IdTercero = cliente.IdTercero,
                                Tipo = "FAC"
                            };
                            Lista.Add(newItem);
                    }
                    foreach (RegistroPagos r in Pagos30)
                    {                        
                        var Detalles = from p in db.RegistroPagosDetalles
                                       
                                       select p;
                        TercerosMovimientos newItem = new TercerosMovimientos();
                        if (r.Modulo == "DEVOLUCION")
                        {
                            newItem.Concepto = "DEVOLUCION" + r.Documento;
                            newItem.Tipo = "NC";
                        }
                        else
                        {
                            newItem.Concepto = "CANCELACION FACTURAS:" + r.Documento;
                            newItem.Tipo = "REC";
                        }
                        newItem.Credito = r.MontoPagado;
                        newItem.Fecha = r.Fecha;
                        newItem.IdTercero = cliente.IdTercero;
                        newItem.Numero = r.IdRegistroPago;
                        if( r.Cheque.HasValue )
                        {
                            newItem.Detalles = "CH:"+r.BancoCheque + " " + r.NumeroCheque + "\n" + r.Cheque.Value.ToString("N2");
                        }
                        if (r.Deposito.HasValue)
                        {
                            newItem.Detalles = "DP:" + r.BancoDeposito + " " + r.NumeroDeposito + "\n" + r.Deposito.Value.ToString("N2"); ;
                        }
                        if (r.MontoPagado > 0)
                        {
                            Lista.Add(newItem);
                        }
                    }
                    foreach (TercerosMovimientos item in Lista.OrderBy(x=> x.Fecha ))
                    {
                        item.Saldo = SaldoAnterior + item.Debito - item.Credito;
                        SaldoAnterior = (double)item.Saldo;
                    }
                    List<TercerosConSaldos> Terceros = new List<HK.TercerosConSaldos>();
                    reportViewer1.ProcessingMode = ProcessingMode.Local;
                    Terceros.Add(cliente);
                    this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "TercerosMovimientos.rdlc";
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("TercerosMovimientos", Lista.OrderBy(x => x.Fecha).ToList()));
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Terceros", Terceros));
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", FactoryParametros.SelectAll()));
                    this.reportViewer1.RefreshReport();
                    }
                    break;
                case "VENTAS POR PRODUCTO":
                    using (var dc = new SoroEntities())
                    {

                        var Documentos = from p in dc.VistaFactura
                                         where (p.Fecha >= this.txtDesde.DateTime.Date && p.Fecha <= this.txtHasta.DateTime.Date)
                                         && (p.Tipo == "FACTURA" || p.Tipo == "PEDIDO")
                                         orderby p.Fecha, p.Descripcion
                                         group p by new {  p.Fecha.Value.Date, p.Descripcion } into g                                         
                                         select new VentasxProducto
                                         {  
                                             Fecha = g.Key.Date,                                              
                                             Descripcion = g.Key.Descripcion,
                                             Cantidad = g.Sum(p=> p.Cantidad),
                                             Bolivares = g.Sum(p=> p.MontoNeto * p.Cantidad)
                                         };
                        this.reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.ProcessingMode = ProcessingMode.Local;
                        this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "VentasxProducto.rdlc";
                        this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("VentasxProducto", Documentos.ToList()));
                        this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
                        this.reportViewer1.RefreshReport();
                    }
                    break;
                case "VENTAS POR VENDEDOR":
                    using (var dc = new SoroEntities())
                    {
                        var Documentos = from p in dc.VistaDocumento
                                         where (p.Fecha >= this.txtDesde.DateTime.Date && p.Fecha <= this.txtHasta.DateTime.Date)
                                         && (p.Tipo == "FACTURA" || p.Tipo == "PEDIDO")
                                         select p;
                        this.reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.ProcessingMode = ProcessingMode.Local;
                        this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "VentasxVendedor.rdlc";
                        this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_VistaDocumento", Documentos.ToList()));
                        this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
                        this.reportViewer1.RefreshReport();
                    }
                    break;
                case "RESUMEN DIARIO":
                    using (var dc = new SoroEntities())
                    {
                        var MovimientosCaja = from p in dc.RegistroPagos
                                              where (p.Fecha >= this.txtDesde.DateTime.Date && p.Fecha <= this.txtHasta.DateTime.Date)
                                              orderby p.Modulo, p.Documento
                                              select p;
                        this.reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.ProcessingMode = ProcessingMode.Local;
                        this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "ResumenDiario.rdlc";
                        this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_RegistroPagos", MovimientosCaja.ToList()));
                        this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));

                        this.reportViewer1.RefreshReport();
                    }
                    break;
                case "MOVIMIENTOS DE CAJA":
                    using (var dc = new SoroEntities())
                    {
                        var MovimientosCaja = from p in dc.RegistroPagos
                                              where (p.Fecha >= this.txtDesde.DateTime.Date && p.Fecha <= this.txtHasta.DateTime.Date)
                                              orderby p.Modulo, p.Documento
                                              select p;
                        this.reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.ProcessingMode = ProcessingMode.Local;
                        this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "MovimientosDeCaja.rdlc";
                        this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_RegistroPagos", MovimientosCaja.ToList()));
                        this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
                        this.reportViewer1.RefreshReport();
                    }

                    //&& (p.Modulo== "CxC"
                    //|| p.Modulo == "FACTURACION"
                    //|| p.Modulo == "PEDIDO"
                    //|| p.Modulo == "PEDIDOS"
                    //)
                    break;

                case "LIBRO VENTAS DIA":
                    using (var dc = new SoroEntities())
                    {
                        var LibroVentasDia = from p in dc.LibroVentas
                                             where (p.Fecha == this.txtFecha.DateTime.Date) && (p.TipoTransaccion == "01")
                                             select p;
                        this.reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.ProcessingMode = ProcessingMode.Local;
                        this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "LibroDeVentas.rdlc";
                        this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_LibroVentas", LibroVentasDia.ToList()));
                        this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
                        this.reportViewer1.RefreshReport();
                    }
                    this.Show();
                    break;
                case "CUENTAS X COBRAR":
                    reportViewer1.ProcessingMode = ProcessingMode.Local;
                    var CuentasxCobrar = FactoryCuentas.DocumentosPendientesCobrar(new SoroEntities(), Titular.IdTercero, this.txtFecha.DateTime);
                    Titulares = new List<Terceros>
                    {
                        Titular
                    };
                    this.reportViewer1.LocalReport.DataSources.Clear();
                    this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "EstadoCuentaxCobrar.rdlc";
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Terceros", Titulares));
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Cuentas", CuentasxCobrar));
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
                    this.reportViewer1.RefreshReport();
                    break;
                case "CUENTAS X PAGAR":
                    reportViewer1.ProcessingMode = ProcessingMode.Local;
                    var CuentasxPagar = FactoryCuentas.DocumentosPendientesPagar(new SoroEntities(), Titular.IdTercero, this.txtFecha.DateTime);
                    Titulares = new List<Terceros>
                    {
                        Titular
                    };
                    this.reportViewer1.LocalReport.DataSources.Clear();
                    this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "EstadoCuentaxPagar.rdlc";
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Terceros", Titulares));
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Cuentas", CuentasxPagar));
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
                    this.reportViewer1.RefreshReport();
                    break;
                case "LISTADO CUENTAS X COBRAR":
                    var ListadoCuentasxCobrar = FactoryCuentas.VistaCuentas("CXC", this.txtFecha.DateTime);
                    reportViewer1.ProcessingMode = ProcessingMode.Local;
                    this.reportViewer1.LocalReport.DataSources.Clear();
                    this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "ListadoCuentasxCobrar.rdlc";
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_VistaCuentas", ListadoCuentasxCobrar));
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
                    this.reportViewer1.RefreshReport();
                    break;
                case "LISTADO CUENTAS X PAGAR":
                    var ListadoCuentasxPagar = FactoryCuentas.VistaCuentas("CXP", this.txtFecha.DateTime);
                    reportViewer1.ProcessingMode = ProcessingMode.Local;
                    this.reportViewer1.LocalReport.DataSources.Clear();
                    this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "ListadoCuentasxPagar.rdlc";
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_VistaCuentas", ListadoCuentasxPagar));
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
                    this.reportViewer1.RefreshReport();
                    break;
            }
        }

        private void BotonReportePorLineas_Click(object sender, EventArgs e)
        {
            switch (this.Reporte.ToUpper())
            {
                case "LISTADO INVENTARIO":
                    List<VistaProductos> Lista = new List<VistaProductos>();
                    using (var dc = new SoroEntities())
                    {
                        
                        var Listado = from p in dc.VistaProductos
                                      where p.Existencia != 0
                                      orderby p.Grupo, p.Descripcion
                                      select p;
                        if (comboBoxGrupos.Text != "TODOS")
                        {
                            Lista = Listado.Where(x => x.Grupo == comboBoxGrupos.Text).ToList();
                        }
                        else
                        {
                            Lista = Listado.ToList();
                        }
                    }
                    var Total = Lista.Sum(x => x.Costo * x.Existencia);
                    this.reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.ProcessingMode = ProcessingMode.Local;
                    this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "inventarios.rdlc";
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_VistaProductos", Lista));
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
                    this.reportViewer1.RefreshReport();
                    break;
                case "LISTADO INVENTARIO POR PROVEEDORES":
                    List<VistaProductos> ListaProductosxProveedor = new List<VistaProductos>();
                    using (var dc = new SoroEntities())
                    {

                        var Listado = from p in dc.VistaProductos
                                      where p.Existencia != 0
                                      orderby p.RazonSocial, p.Descripcion
                                      select p;
                        if (comboBoxGrupos.Text != "TODOS")
                        {
                            Lista = Listado.Where(x => x.RazonSocial == comboBoxGrupos.Text).ToList();
                        }
                        else
                        {
                            Lista = Listado.ToList();
                        }
                    }
                    this.reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.ProcessingMode = ProcessingMode.Local;
                    this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\" + "InventariosxProveedor.rdlc";
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_VistaProductos", Lista));
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HK_Parametros", FactoryParametros.SelectAll()));
                    this.reportViewer1.RefreshReport();
                    break;

            }
        }

        private void FrmReportes_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.reportViewer1.LocalReport.ReleaseSandboxAppDomain();
        }
    }
}