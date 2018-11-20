using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HK.Forms;

namespace HK
{
    public partial class FrmPrincipal : Form
    {

        public FrmPrincipal()
        {
            InitializeComponent();
        }
        private void CerrarVentanas()
        {

            
            //foreach(DevExpress.XtraTabbedMdi.XtraMdiTabPage  p in this.xtraTabbedMdiManager1.Pages)
            //{
            //   p.MdiChild.Close();
            //}
        }
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CerrarVentanas();
            try
            {
                CargarDatos(null);
            }
            catch
            {
                MessageBox.Show("Error de coneccion con el servidor", "Atencion");
            }
        }

        private static void CargarDatos(string mensaje)
        {            
            Producto p = null;
            Entities Db = new Entities();
            try
            {
                Db.Database.ExecuteSqlCommand("Delete Productos");
                Db.Database.ExecuteSqlCommand("Delete Clientes");
                Db.Database.ExecuteSqlCommand("Delete Cuentas");
                Db.Database.ExecuteSqlCommand("Delete Terceros");
                Db.Database.ExecuteSqlCommand("Delete DocumentosProductos");
                Db.Database.ExecuteSqlCommand("Delete Documentos");

                using (var SQL = new SQLDataContext(Program.cn))
                {
                    #region PasarProductos
                    var qProductos = from prod in SQL.SQLProductos
                                     where prod.Activo == true
                                     select prod;
                    foreach (SQLProducto t in qProductos)
                    {
                        p = new Producto();
                        p.Descripcion = t.Descripcion;
                        p.Existencia = t.Existencia;
                        p.IdProducto = t.IdProducto;
                        p.Iva = t.Iva;
                        p.Peso = t.Peso;
                        p.Precio = t.Precio;
                        p.Precio2 = t.Precio2;
                        p.Precio3 = t.Precio3;
                        p.Precio4 = t.Precio4;
                        p.Precio5 = t.Precio5;
                        p.PrecioIVA = p.Precio + (p.Precio * p.Iva / 100);
                        p.PrecioIVA2 = p.Precio2 + (p.Precio2 * p.Iva / 100);
                        p.PrecioIVA3 = p.Precio3 + (p.Precio3 * p.Iva / 100);
                        p.PrecioIVA4 = p.Precio4 + (p.Precio4 * p.Iva / 100);
                        p.PrecioIVA5 = p.Precio5 + (p.Precio5 * p.Iva / 100);
                        p.Costo = t.CostoActual;
                        p.Pvs = t.PVP;
                        p.UnidadMedida = t.UnidadMedida;
                        p.Codigo = t.Codigo;
                        p.Maximo = t.Maximo;
                        p.Minimo = t.Minimo;
                        p.IdProveedor = t.IdProveedor;
                        Db.Productos.Add(p);
                    }
                    Db.SaveChanges();
                    #endregion
                    #region PasarClientes
                    var qClientes = from prod in SQL.SQLTerceros
                                    where prod.Activo == true && prod.Tipo == "CLIENTE"
                                    select prod;

                    foreach (SQLTercero t in qClientes)
                    {
                        Cliente c = new Cliente();
                        SQLSaldosPendiente Saldo = SQL.SQLSaldosPendientes.FirstOrDefault(x => x.IdTercero == t.IdTercero);
                        c.Ciudad = t.Ciudad;
                        c.Condiciones = t.Condiciones;
                        c.Direccion = t.Direccion;
                        c.IdTercero = t.IdTercero;
                        c.RazonSocial = t.RazonSocial;
                        c.Zona = t.Zona;
                        c.CedulaRif = t.CedulaRif;
                        c.SaldoDeudor = 0;
                        c.DescuentoPorcentaje = t.DescuentoPorcentaje;
                        c.TipoPrecio = t.TipoPrecio;
                        if (Saldo != null)
                        {
                            c.SaldoDeudor = Saldo.SaldoPendiente;
                        }
                        Db.Clientes.Add(c);
                    }
                    Db.SaveChanges();
                    #endregion
                    #region PasarProveedores
                    var qProveedores = from prod in SQL.SQLTerceros
                                       where prod.Activo == true && prod.Tipo == "PROVEEDOR"
                                       select prod;

                    foreach (SQLTercero t in qProveedores)
                    {
                        Tercero c = new Tercero();
                        c.Ciudad = t.Ciudad;
                        c.Condiciones = t.Condiciones;
                        c.Direccion = t.Direccion;
                        c.IdTercero = t.IdTercero;
                        c.RazonSocial = t.RazonSocial;
                        c.Zona = t.Zona;
                        c.CedulaRif = t.CedulaRif;
                        c.DescuentoPorcentaje = t.DescuentoPorcentaje;
                        c.TipoPrecio = t.TipoPrecio;
                        c.Tipo = t.Tipo;
                        c.Activo = true;
                        Db.Terceros.Add(c);
                    }
                    Db.SaveChanges();
                    #endregion
                    #region PasarCuentas
                    foreach (SQLCuentas t in SQL.SQLCuentas.Where(x => x.Saldo > 0))
                    {
                        Cuenta c = new Cuenta();
                        c.Concepto = t.Concepto;
                        c.Fecha = t.Fecha;
                        c.IdCuenta = t.IdCuenta;
                        c.IdDocumento = t.IdDocumento;
                        c.IdSesion = t.IdSesion;
                        c.IdTercero = t.IdTercero;
                        c.Momento = t.Momento;
                        c.Monto = t.Monto;
                        c.Numero = t.Numero;
                        c.Saldo = t.Saldo;
                        c.Seleccion = t.Seleccion;
                        c.Tipo = t.Tipo;
                        c.TipoDocumento = t.TipoDocumento;
                        c.Vence = t.Vence;
                        Db.Cuentas.Add(c);
                    }
                    Db.SaveChanges();
                    #endregion
                    mensaje = mensaje == null ? "Listo datos cargados existosamente" : mensaje + "\n" + "Listo datos cargados existosamente";
                    MessageBox.Show(mensaje, "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmReportes f = new FrmReportes();
            f.WindowState = FormWindowState.Maximized;
            f.MdiParent = this;
            f.ListaPrecios("Todas");
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmCobranzas f = new FrmCobranzas();
            f.WindowState = FormWindowState.Maximized;
            f.MdiParent = this;           
            f.Show();
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmConsultarProductos f = new FrmConsultarProductos();
            f.MdiParent = this;
            f.Show();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmPedidosItem f = new FrmPedidosItem();
            f.Incluir();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CerrarVentanas();
            PasarPedidos();
            PasarPagos();
            CargarDatos("Los pedidos fueron enviados correctamente");
        }
        private void PasarPagos()
        {            
            Entities Db = new Entities();
            SQLDataContext SQL = new SQLDataContext(Program.cn);
            foreach (RegistroPago Pago in Db.RegistroPagos.Where(x=> x.Enviado==false))
            {
                SQLRegistroPagosExterno Registro = new SQLRegistroPagosExterno();
                Registro.BancoCheque = Pago.BancoCheque;
                Registro.BancoCheque2 = Pago.BancoCheque2;
                Registro.BancoDeposito = Pago.BancoDeposito;
                Registro.BancoDeposito2 = Pago.BancoDeposito2;
                Registro.Cambio = Pago.Cambio;
                Registro.Cheque = Pago.Cheque;
                Registro.Cheque2 = Pago.Cheque2;
                Registro.Deposito = Pago.Deposito;
                Registro.Deposito2 = Pago.Deposito2;
                Registro.Detalles = Pago.Detalles;
                Registro.Efectivo = Pago.Efectivo;
                Registro.Fecha = Pago.Fecha;
                Registro.IdRegistroPagoRemoto = FactoryContadores.SQLGetLast("IdRegistroPagoRemoto");
                Registro.NumeroCheque = Pago.NumeroCheque;
                Registro.NumeroCheque2 = Pago.NumeroCheque2;
                Registro.NumeroDeposito = Pago.NumeroDeposito;
                Registro.NumeroDeposito2 = Pago.NumeroDeposito2;
                Registro.Facturas = Pago.Documento;
                Registro.RazonSocial = Pago.RazonSocial;
                Registro.IdTercero = Pago.IdTecero;
                Registro.Monto = Pago.MontoPagado;
                Registro.Equipo = FactoryParametros.Item().Equipo;
                foreach (PagosDetalle d in Pago.PagosDetalles)
                {
                    SQLRegistroPagosExternosDetalle RegistroDetalle = new SQLRegistroPagosExternosDetalle();
                    RegistroDetalle.IDDocumento = d.IdDocumento;
                    RegistroDetalle.Monto = d.Monto;
                    RegistroDetalle.Numero = d.Numero;
                    RegistroDetalle.IdRegistroPagoExternoDetalle = FactoryContadores.SQLGetLast("IdRegistroPagoExternoDetalle");
                    Registro.SQLRegistroPagosExternosDetalles.Add(RegistroDetalle);
                }
                Pago.Enviado = true;
                SQL.SQLRegistroPagosExternos.InsertOnSubmit(Registro);
            }
            try
            {
                SQL.SubmitChanges();
                Db.SaveChanges();
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        private void PasarPagosOLD()
        {
            Entities Db = new Entities();
            SQLDataContext SQL = new SQLDataContext(Program.cn);
            SQLParametros parametros = SQL.SQLParametros.FirstOrDefault();            
            Parametro parametrosItem = FactoryParametros.Item();
            foreach (PagosDetalle Pago in Db.PagosDetalles.Where(x => x.Tipo == "CXC"))
            {
                    SQLCuentas Cuenta = BuscarCuenta(SQL, Pago.IdDocumento);
                    if (Cuenta != null)
                    {
                        Cuenta.Saldo = Cuenta.Saldo - Pago.Monto;                        
                    }
                    SQL.SubmitChanges();
            }
            foreach (RegistroPago Pago in Db.RegistroPagos)
            {
                SQLRegistroPago RegistroPago = new SQLRegistroPago();
                RegistroPago.BancoCheque = Pago.BancoCheque;
                RegistroPago.BancoDeposito = Pago.BancoDeposito;
                RegistroPago.Cambio = Pago.Cambio;
                RegistroPago.Cheque = Pago.Cheque+Pago.Cheque2;
                RegistroPago.Deposito = Pago.Deposito+Pago.Deposito2;
                RegistroPago.Documento = Pago.Documento;
                RegistroPago.Efectivo = Pago.Efectivo;
                RegistroPago.Fecha = Pago.Fecha;
                RegistroPago.IdDocumento = Pago.IdDocumento;
                RegistroPago.IdRegistroPago = FactoryContadores.SQLGetLast("IdRegistroPago");
                RegistroPago.IdTercero = Pago.IdTecero;
                RegistroPago.Modulo = "CXC"+parametrosItem.Equipo;
                RegistroPago.Momento = DateTime.Now;
                RegistroPago.MontoPagado = Pago.MontoPagado;
                RegistroPago.MontoPagar = Pago.MontoPagado;
                RegistroPago.NumeroCheque = Pago.NumeroCheque + Pago.NumeroCheque2;                   
                RegistroPago.NumeroDeposito = Pago.NumeroDeposito;
                RegistroPago.RazonSocial = Pago.RazonSocial;
                RegistroPago.SaldoPendiente = Pago.SaldoPendiente;
                RegistroPago.Tipo = Pago.Tipo;
                RegistroPago.Detalles = Pago.Detalles;
                SQL.SQLRegistroPagos.InsertOnSubmit(RegistroPago);
            }
            foreach (PagosDetalle Pago in Db.PagosDetalles)
            {
                SQLRegistroPagosDetalle r = new SQLRegistroPagosDetalle();
                r.IdRegistroPagosDetalle = FactoryContadores.SQLGetLast("IdRegistroPagosDetalle");
                r.IdDocumento = Pago.IdDocumento;
                r.Monto = Pago.Monto;
                r.Numero = Pago.Numero;
                r.Tipo = Pago.Tipo;
                SQL.SQLRegistroPagosDetalles.InsertOnSubmit(r);
            }
            SQL.SubmitChanges();
            Db.Database.ExecuteSqlCommand("Delete RegistroPagos");
            Db.Database.ExecuteSqlCommand("Delete PagosDetalles");
        }

        private static SQLCuentas BuscarCuenta(SQLDataContext SQL,string IdDocumento)
        {
            var Cuenta = (from c in SQL.SQLCuentas
                          where c.IdDocumento == IdDocumento
                          select c).FirstOrDefault();
            return Cuenta;
        }
        private void PasarPedidos()
        {
            Entities Db = new Entities();
            SQLDataContext SQL = new SQLDataContext(Program.cn);
            SQLParametros parametros = SQL.SQLParametros.FirstOrDefault();
            foreach (Pedido pedido in Db.Pedidos.Where(x => x.Enviado != true))
            {
                SQLDocumentos Documento = new SQLDocumentos();
                Documento.IdTercero = pedido.IdCliente;
                Documento.Mes = DateTime.Today.Month;
                Documento.Status = "ABIERTA";
                Documento.TasaIVA = parametros.TasaIVA;
                Documento.Tipo = "PEDIDO";
                Documento.Activo = true;
                Documento.Año = DateTime.Today.Year;
                Documento.Fecha = DateTime.Today;
                Documento.Vence = Documento.Fecha.Value.AddDays(30);
                Documento.DescuentoBs = 0;
                Documento.DescuentoPorcentaje = 0;
                Documento.IdDocumento =FactoryContadores.SQLGetLast("IdDocumento");
                Documento.Numero = pedido.Numero;
                Documento.MontoTotal = 0;
                Cliente c = FactoryTerceros.Item(pedido.IdCliente);                
                if (c.Nuevo.HasValue)
                {
                    c.Nuevo = false;
                    SQLTercero Tercero = new SQLTercero();
                    Tercero.Activo = true;
                    Tercero.CedulaRif = c.CedulaRif;
                    Tercero.Ciudad = c.Ciudad;
                    Tercero.Condiciones = c.Condiciones;
                    Tercero.DescuentoPorcentaje = 0;
                    Tercero.DiasCredito = 0;
                    Tercero.Direccion = c.Direccion;
                    Tercero.IdTercero = FactoryContadores.SQLGetLast("IdTercero");
                    Tercero.LimiteCredito = 0;
                    Tercero.MontoPendiente = 0;
                    Tercero.RazonSocial = c.RazonSocial;
                    Tercero.Telefonos = c.Telefonos;
                    Tercero.Tipo = "CLIENTE";
                    Tercero.TipoContribuyente = "CONTRIBUYENTE";
                    Tercero.TipoPrecio = c.TipoPrecio;
                    Tercero.Zona = c.Zona;
                    SQL.SQLTerceros.InsertOnSubmit(Tercero);
                    Documento.IdTercero = Tercero.IdTercero;
                }
                foreach (PedidosDetalle Item in Db.PedidosDetalles.Where(x=> x.IdPedido == pedido.IdPedido))
                {
                    SQLDocumentosProductos newItem = new SQLDocumentosProductos();
                    SQLProducto p = SQL.SQLProductos.FirstOrDefault(x => x.IdProducto == Item.IdProducto);
                    if (p != null)
                    {
                        newItem.Activo = true;
                        newItem.BloqueoPrecio = true;
                        newItem.Cantidad = Item.Cantidad;
                        newItem.Codigo = Item.Codigo;
                        newItem.Costo = p.Costo;
                        newItem.Descripcion = Item.Descripcion;
                        newItem.DescuentoBs = Item.DescuentoBolivares;
                        newItem.DescuentoPorcentaje = Item.DescuentoPorcentaje;
                        newItem.IdProducto = Item.IdProducto;
                        newItem.Iva = Item.Iva;
                        newItem.MontoNeto = Item.PrecioNeto;
                        newItem.Precio = Item.Precio;                        
                        newItem.Total = Item.Total;
                        newItem.TasaIva = Item.TasaIva;
                        newItem.Tipo = "PRODUCTO";
                        newItem.UnidadMedida = Item.UnidadMedida;
                        newItem.ExistenciaAnterior = p.Existencia;
                        newItem.PrecioIva = cBasicas.Round( (Item.Total) / Item.Cantidad);
                        newItem.PesoTotal = p.Peso * Item.Cantidad;
                        newItem.PesoUnitario = p.Peso;
                        newItem.IdDetalleDocumento = FactoryContadores.SQLGetLast("IdDetalleDocumento");
                        Documento.MontoTotal = Documento.MontoTotal + newItem.Total;
                        Documento.SQLDocumentosProductos.Add(newItem);
                    }
                }
                Documento.Saldo = pedido.Saldo;
                Documento.DeudaAnterior = 0;
                Documento.Efectivo = pedido.Efectivo;
                Documento.Cheque = pedido.Cheque;
                Documento.Deposito = pedido.Deposito;
                Documento.TarjetaCredito = pedido.TarjetaCredito;
                Documento.TarjetaDebito = pedido.TarjetaDebito;
                Documento.Transferencias = pedido.Transferencias;
                Documento.Vence = Documento.Fecha;
                Documento.MontoTotal = pedido.MontoTotal;
                Documento.Año = pedido.Fecha.Value.Year;
                Documento.Mes = pedido.Fecha.Value.Month;
                Documento.PesoFactura = Documento.SQLDocumentosProductos.Sum(x => x.PesoTotal);
                SQL.SQLDocumentos.InsertOnSubmit(Documento);
                SQL.SubmitChanges();
                pedido.Enviado = true;
            }
            Db.SaveChanges();
         }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmContarDinero f = new FrmContarDinero();
            f.ShowDialog();
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("Esta opcion limpia todos los pedidos y pagos, esta seguro de hacerlo", "Atencion", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
            {
                return;
            }
            using(var Db = new Entities())
            {
                DateTime fecha = DateTime.Today.AddDays(-30);
                foreach(Pedido p in Db.Pedidos.Where(x=>x.Fecha<fecha))
                {
                    Db.Pedidos.Remove(p);
                    Db.Database.ExecuteSqlCommand(string.Format("Delete PedidosDetalles where IdPedido='{0}'",p.IdPedido) );
                }
                foreach (RegistroPago p in Db.RegistroPagos.Where(x => x.Fecha < fecha))
                {
                    Db.RegistroPagos.Remove(p);
                }
                Db.SaveChanges();
                Db.Database.ExecuteSqlCommand("Delete PagosDetalles");
            }
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmParametros f = new FrmParametros();
            f.ShowDialog();           
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            cBasicas.ConectarDB();
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmPedidos f = new FrmPedidos();
            f.MdiParent = this;
            f.Show();
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmCrearClientes f = new FrmCrearClientes();
            f.ShowDialog();
        }

        private void barRegistroPagos_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmPago F = new FrmPago();
            F.ShowDialog();
        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmRevisarPagos f = new FrmRevisarPagos();
            f.MdiParent = this;
            f.Show();

        }

        private void barButtonPedidoProveedor_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmPedidosProveedor f = new FrmPedidosProveedor();
            f.MdiParent = this;
            f.Show();

        }

        private void barButtonGuiaCarga_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmGuiaCarga f = new FrmGuiaCarga();
            f.ShowDialog();
        }

        private void barButtonEnviarPendDrive_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
                        FolderBrowserDialog f = new FolderBrowserDialog();
            f.RootFolder = Environment.SpecialFolder.MyComputer;
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (f.SelectedPath == string.Empty)
                    return;
                if (System.IO.File.Exists(f.SelectedPath + "respaldo.bak"))
                {
                    System.IO.File.Copy(f.SelectedPath + "respaldo.bak", f.SelectedPath + "respaldo.old", true);
                }
                try
                {
                    System.IO.File.Copy(Application.StartupPath + "\\OK_Pedidos.sdf", f.SelectedPath + "respaldo.bak", true);
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message);
                }
            }
        }
        void barButtonRecuperacion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            f.RootFolder = Environment.SpecialFolder.MyComputer;
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (f.SelectedPath == string.Empty)
                    return;
                if (!System.IO.File.Exists(f.SelectedPath + "respaldo.bak"))
                {
                    MessageBox.Show("No se encontro un respaldo en ese sitio");
                    return;
                }
                try
                {
                    System.IO.File.Copy(Application.StartupPath + "\\OK_Pedidos.sdf", Application.StartupPath + "\\OK_Pedidos.old", true);
                    System.IO.File.Copy(f.SelectedPath + "\\respaldo.bak", Application.StartupPath + "\\OK_Pedidos.sdf", true);
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message);
                }
            }
        }
      }
    }

