using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

    namespace HK
    {
        class CFacturas
        {
            public static void CargarInventario(Documentos Doc)
            {
                using (var dc = new SoroEntities())
                {
                    Documentos Factura = FactoryDocumentos.Item(dc, Doc.IdDocumento);
                    Factura.Status = "INVENTARIO";
                    foreach (DocumentosProductos Item in Doc.DocumentosProductos)
                    {
                       Productos Producto = FactoryProductos.Item(Item.IdProducto);
                       if ((bool)Producto.Activo && (bool)Producto.LlevaInventario)
                       {
                           FactoryLibroInventarios.EscribirLibroInventario(Doc.Fecha.Value, Item,"FACTURA");                           
                           Item.ExistenciaAnterior = Producto.Existencia;
                           Producto.Existencia = Producto.Existencia - Item.Cantidad;
                           FactoryProductos.Guardar(Producto);                           
                        }
                    }
                    dc.SaveChanges();
                }                
            }
            public static void EscribirCuentaxCobrar(Documentos Doc)
            { 
                Cuentas CuentaxCobrar = new Cuentas();
                using (SoroEntities dc = new SoroEntities())
                {
                    CuentaxCobrar.Concepto = "FACTURA # "+ Doc.Numero;
                    CuentaxCobrar.Fecha = Doc.Fecha;
                    CuentaxCobrar.IdCuenta = FactoryContadores.GetLast("IdCuenta");
                    CuentaxCobrar.IdDocumento = Doc.IdDocumento;
                    CuentaxCobrar.IdTercero = Doc.IdTercero;
                    CuentaxCobrar.Monto = Doc.MontoTotal;
                    CuentaxCobrar.Numero = Doc.Numero;
                    CuentaxCobrar.Saldo = Doc.Saldo;
                    CuentaxCobrar.Tipo  = "CXC";
                    CuentaxCobrar.TipoDocumento = "FACTURA";
                    CuentaxCobrar.Vence = Doc.Vence;
                    dc.Cuentas.Add(CuentaxCobrar);
                    dc.SaveChanges();
                }
            }
            public static void LibroDeVentas(Documentos Doc)
            {
                using (var dc = new SoroEntities())
                {
                    Terceros Tercero = FactoryTerceros.Item(Doc.IdTercero);
                    Parametros p = FactoryParametros.Item(new SoroEntities());
                LibroVentas Registro = new LibroVentas
                {
                    BaseImponible = Doc.BaseImponible,
                    CedulaRif = Tercero.CedulaRif,
                    Control = Doc.Numero,
                    Factura = Doc.Numero,
                    FacturaAfectada = "",
                    Fecha = Doc.Fecha.Value,
                    IdLibroVentas = FactoryContadores.GetLast("IdLibroDeVentas"),
                    ImpuestoIva = Doc.MontoIva,
                    IvaRetenido = 0,
                    MontoIncluyentoIva = Doc.MontoTotal,
                    RazonSocial = Tercero.RazonSocial,
                    TasaIva = Doc.TasaIVA,
                    TipoTransaccion = "01",
                    VentasNoGravadas = Doc.MontoExento,
                    ReporteZ = (p.UltimoReporteZ.Value + 1).ToString("0000"),
                    NroRegistroMaquinaFiscal = p.RegistroMaquinaFiscal,
                    Año = Doc.Fecha.Value.Year,
                    Mes = Doc.Fecha.Value.Month
                };
                dc.LibroVentas.Add(Registro);
                    dc.SaveChanges();
                }
            }
            public static void LibroDeVentasNotaCredito(Documentos Doc,string FacturaAfectada)
            {
                using (var dc = new SoroEntities())
                {
                    Terceros Tercero = FactoryTerceros.Item(Doc.IdTercero);
                    Parametros p = FactoryParametros.Item(new SoroEntities());
                LibroVentas Registro = new LibroVentas
                {
                    BaseImponible = Doc.BaseImponible,
                    CedulaRif = Tercero.CedulaRif,
                    Control = Doc.Numero,
                    Factura = Doc.Numero,
                    FacturaAfectada = "",
                    Fecha = Doc.Fecha.Value
                };
                Registro.FacturaAfectada = FacturaAfectada;
                    Registro.IdLibroVentas = FactoryContadores.GetLast("IdLibroDeVentas");
                    Registro.ImpuestoIva = Doc.MontoIva*-1;
                    Registro.IvaRetenido = 0;
                    Registro.MontoIncluyentoIva = Doc.MontoTotal*-1;
                    Registro.RazonSocial = Tercero.RazonSocial;
                    Registro.TasaIva = Doc.TasaIVA;
                    Registro.TipoTransaccion = "02";
                    Registro.VentasNoGravadas = Doc.MontoExento*-1;
                    Registro.ReporteZ = (p.UltimoReporteZ.Value + 1).ToString("0000");
                    Registro.NroRegistroMaquinaFiscal = p.RegistroMaquinaFiscal;
                    Registro.Año = Doc.Fecha.Value.Year;
                    Registro.Mes = Doc.Fecha.Value.Month;
                    dc.LibroVentas.Add(Registro);
                    dc.SaveChanges();
                }
            }
            public static void DevolverCaja(Documentos Devolucion,Documentos Doc)
            {
                RegistroPagos Recibo = new RegistroPagos();
                using (var dc = new SoroEntities())
                {
                    var Pago = (from p in dc.RegistroPagos
                               where p.IdDocumento== Doc.IdDocumento
                               select p).FirstOrDefault();
                   
                    if (Pago == null)
                        return;
                    Recibo.IdRegistroPago = FactoryContadores.GetLast("IdRegistroPago");
                    Recibo.BancoCheque = Pago.BancoCheque;
                    Recibo.BancoDeposito = Pago.BancoDeposito;
                    Recibo.Cambio = Pago.Cambio *-1;
                    Recibo.Cheque = Pago.Cheque * -1;
                    Recibo.Deposito = Doc.Deposito * -1;
                    Recibo.Documento = Devolucion.Numero;
                    Recibo.Efectivo = Pago.Efectivo * -1;
                    Recibo.Fecha = Devolucion.Fecha;
                    Recibo.IdDocumento = Devolucion.IdDocumento;
                    Recibo.IdTercero = Doc.IdTercero;
                    Recibo.Modulo = "DEVOLUCION";
                    Recibo.Momento = DateTime.Now;
                    Recibo.MontoPagado = Pago.MontoPagado * -1;
                    Recibo.MontoPagar = Pago.MontoPagar * -1;
                    Recibo.NumeroCheque = Pago.NumeroCheque;
                    Recibo.NumeroDeposito = Pago.NumeroDeposito;
                    Recibo.PuntoCredito = Pago.PuntoCredito;
                    Recibo.PuntoDebito = Pago.PuntoDebito;
                    Recibo.RazonSocial = Pago.RazonSocial;
                    Recibo.RetencionISLR = Pago.RetencionISLR * -1;
                    Recibo.RetencionIVA = Pago.RetencionIVA * -1;
                    Recibo.SaldoPendiente = Pago.SaldoPendiente;
                    Recibo.TCredito = Pago.TCredito * -1;
                    Recibo.TDebito = Pago.TDebito * -1;
                    Recibo.Tipo = Pago.Tipo;
                //    Recibo.Transferencia = Pago.Transferencia * -1;
                    Recibo.Fecha = DateTime.Today;                    
                    dc.RegistroPagos.Add(Recibo);
                    dc.SaveChanges();
                }
            }
            public static void DevolverInventario(Documentos Doc)
            {
                foreach (DocumentosProductos Item in Doc.DocumentosProductos)
                {
                    Productos Producto = FactoryProductos.Item(Item.IdProducto);
                    if(Producto!=null)
                    {
                    if ((bool)Producto.Activo && (bool)Producto.LlevaInventario)
                    {
                        FactoryLibroInventarios.EscribirLibroInventario(Doc.Fecha.Value, Item, "DEVOLUCION");
                        Item.ExistenciaAnterior = Producto.Existencia;
                        Producto.Existencia = Producto.Existencia + Item.Cantidad;
                        FactoryProductos.Guardar(Producto);
                    }
                    }
                }
            }            
            public static void DevolverCuentaxCobrar(Documentos Doc, Documentos Original)
            {
                using (SoroEntities dc = new SoroEntities())
                {
                    Cuentas Factura = FactoryCuentas.Item(dc, Original.IdDocumento);
                    double MontoDevolucion = Doc.MontoTotal.Value;
                    if (Factura != null)
                    {
                        Factura.Saldo = Factura.Saldo.Value - MontoDevolucion;
                    }
                    dc.SaveChanges();
                }
            }
            public static void DevolucionLibroDeVentas(Documentos Doc,string NumeroDevolucion)
            {
                using (var dc = new SoroEntities())
                {
                    Terceros Tercero = FactoryTerceros.Item(Doc.IdTercero);
                    Parametros p = FactoryParametros.Item();
                LibroVentas Registro = new LibroVentas
                {
                    Año = Doc.Fecha.Value.Year,
                    Mes = Doc.Fecha.Value.Month,
                    BaseImponible = Doc.BaseImponible * -1,
                    CedulaRif = Tercero.CedulaRif,
                    Control = NumeroDevolucion,
                    Factura = NumeroDevolucion,
                    FacturaAfectada = Doc.Numero,
                    Fecha = Doc.Fecha.Value,
                    IdLibroVentas = FactoryContadores.GetLast("IdLibroDeVentas"),
                    ImpuestoIva = Doc.MontoIva * -1,
                    IvaRetenido = 0,
                    MontoIncluyentoIva = Doc.MontoTotal * -1,
                    RazonSocial = Tercero.RazonSocial,
                    TasaIva = Doc.TasaIVA,
                    TipoTransaccion = "02",
                    VentasNoGravadas = Doc.MontoExento * -1,
                    ReporteZ = p.UltimoReporteZ.Value.ToString("000000"),
                    NroRegistroMaquinaFiscal = p.RegistroMaquinaFiscal
                };
                dc.LibroVentas.Add(Registro);
                    dc.SaveChanges();
                }
            }
            public static void Devolucion(Documentos Devolucion,Documentos Doc  )
            {
                using (var dc = new SoroEntities())
                {
                    try
                    {
                        if (Doc.Tipo == "FACTURA")
                        {

                            if (FactoryParametros.Item().TipoImpresora.Contains("FISCAL"))
                            {
                                FiscalBixolon f = new FiscalBixolon();
                                if (!f.ImprimeDevolucion(Devolucion.IdDocumento))
                                {
                                    throw new Exception( "No se pudo imprimir devolucion revise");
                                }
                            }
                          //  Documentos DocumentoDevolucion = GuardarDevolucion(Devolucion);
                            DevolverCaja(Devolucion,Doc);
                            DevolverInventario(Devolucion);
                            DevolverCuentaxCobrar(Devolucion,Doc);
                         //   string Numero = FactoryContadores.GetLast("NumeroDevolucion");
                            DevolucionLibroDeVentas(Devolucion, Devolucion.Numero);

                        }
                        if (Doc.Tipo == "PEDIDO")
                        {
                            DevolverCaja(Devolucion,Doc);
                            DevolverInventario(Devolucion);
                            DevolverCuentaxCobrar(Devolucion, Doc);
                        }
                        dc.SaveChanges();
                    }
                    catch (Exception x)
                    {
                        throw x;
                    }

                }
            }
            public static Documentos GuardarDevolucion(Documentos Documento)
            {
                Documentos Doc = new Documentos();
                using (var dc = new SoroEntities())
                {
                    Doc.Tipo = "DEVOLUCION";
                    Doc.TasaIVA = FactoryParametros.Item().TasaIVA;
                    Doc.Activo = true;
                    if (!Doc.Fecha.HasValue)
                    {
                        Doc.Fecha = DateTime.Today;
                    }
                    Doc.Momento = DateTime.Now;
                    Doc.Saldo = 0;
                    Doc.Status = "ABIERTA";
                    Doc.IdTercero = Documento.IdTercero;
                    if (string.IsNullOrEmpty(Doc.IdTercero))
                    {
                        throw new Exception("Error Devolucion sin cliente");
                    }
                    if (string.IsNullOrEmpty(Doc.IdDocumento))
                    {
                        Doc.IdDocumento = FactoryContadores.GetLast("IdDocumento");
                        dc.Documentos.Add(Doc);
                    }
                    try
                    {
                        dc.SaveChanges();
                    }
                    catch (Exception x)
                    {
                        throw x;
                    }
                }
                return Doc;
            }

        }
    }