using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 namespace HK
    {
        class cPedidos
        {
            public static bool Validate(SoroEntities dc, Documentos Doc, Terceros Tercero)
            {
                foreach (DocumentosProductos Item in Doc.DocumentosProductos)
                {
                    if (Item.IdProducto == null || Item.Cantidad == null || Item.Precio == null)
                    {
                        Doc.DocumentosProductos.Remove(Item);
                    }
                }                
                if (Doc.DocumentosProductos.Count < 1)
                {
                    throw new Exception( "Documento sin contenido");
                }
                Doc.CalcularTotales();
                if (Doc.MontoTotal == 0)
                {
                    throw new Exception("No se puede guardar un pedido en cero");
                }
                return true;
                
            }
            public static bool Guardar(SoroEntities dc, Documentos Doc, Terceros Tercero)
            {
                Doc.Tipo = "PEDIDO";
                Doc.TasaIVA = FactoryParametros.Item().TasaIVA;
                Doc.Activo = true;
                if (!Doc.Fecha.HasValue)
                {
                    Doc.Fecha = DateTime.Today;
                }
                Doc.Momento = DateTime.Now;
                Doc.Saldo = Doc.MontoTotal;
                Doc.Status = "ABIERTA";
                if (string.IsNullOrEmpty(Tercero.IdTercero))
                {
                    Tercero.IdTercero = FactoryContadores.GetLast("IdTercero");
                    Tercero.DiasCredito = 0;
                    Tercero.Tipo = "CLIENTE";
                    Tercero.Activo = true;
                    dc.Terceros.Add(Tercero);
                }
                if (!Tercero.DiasCredito.HasValue)
                {
                    Tercero.DiasCredito = 0;
                }
                Doc.Vence = Doc.Fecha.Value.AddDays((double)Tercero.DiasCredito);
                if (string.IsNullOrEmpty(Doc.IdDocumento))
                {
                    Doc.IdDocumento = FactoryContadores.GetLast("IdDocumento");
                    dc.Documentos.Add(Doc);
                }
                Doc.IdTercero = Tercero.IdTercero;

                foreach (DocumentosProductos Item in Doc.DocumentosProductos)
                {
                    if (string.IsNullOrEmpty(Item.IdDetalleDocumento))
                    {
                        Item.IdDetalleDocumento = FactoryContadores.GetLast("IdDetalleDocumento");
                    }
                }
                try
                {
                    dc.SaveChanges();
                }
                catch (Exception x)
                {
                    string s = x.Message;
                    return false;
                }
                return true;
            }
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
                          //  FactoryLibroInventarios.EscribirLibroInventario(Doc.Fecha.Value, Item, "FACTURA");
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
                if (!Doc.Saldo.HasValue)
                    return;
                if (Doc.Saldo < 1)
                    return;                
                Cuentas CuentaxCobrar = new Cuentas();
                using (SoroEntities dc = new SoroEntities())
                {
                    CuentaxCobrar.Concepto = "PEDIDO # " + Doc.Numero;
                    CuentaxCobrar.Fecha = Doc.Fecha;
                    CuentaxCobrar.IdCuenta = FactoryContadores.GetLast("IdCuenta");
                    CuentaxCobrar.IdDocumento = Doc.IdDocumento;
                    CuentaxCobrar.IdTercero = Doc.IdTercero;
                    CuentaxCobrar.Monto = Doc.MontoTotal;
                    CuentaxCobrar.Numero = Doc.Numero;
                    CuentaxCobrar.Saldo = Doc.Saldo;
                    CuentaxCobrar.Tipo = "CXC";
                    CuentaxCobrar.TipoDocumento = "PEDIDO";
                    CuentaxCobrar.Vence = Doc.Vence;
                    dc.Cuentas.Add(CuentaxCobrar);
                    dc.SaveChanges();
                }
            }
            public static bool AnulacionPedido(Documentos Factura)
            {
                using (var newdc = new SoroEntities())
                {
                    Documentos Doc = new Documentos();
                    Doc.Tipo = "DEVOLUCION";
                    Doc.TasaIVA = FactoryParametros.Item().TasaIVA;
                    Doc.Activo = true;
                    Doc.Fecha = DateTime.Today;
                    Doc.Momento = DateTime.Now;
                    Doc.MontoTotal = Factura.MontoTotal;
                    Doc.Saldo = Doc.MontoTotal;
                    Doc.Status = "ABIERTA";
                    Doc.IdSesion = FactorySesiones.SesionActiva.IdSesion;
                    Doc.IdTercero = Factura.IdTercero;
                    Doc.Vence = Doc.Fecha;
                    Doc.IdDocumento = FactoryContadores.GetLast("IdDocumento");
                    Doc.Numero = Factura.Numero;
                    newdc.Documentos.Add(Doc);
                    foreach (DocumentosProductos Item in Factura.DocumentosProductos)
                    {
                    DocumentosProductos NuevoItem = new DocumentosProductos
                    {
                        IdDocumento = Doc.IdDocumento,
                        IdDetalleDocumento = FactoryContadores.GetLast("IdDetalleDocumento"),
                        BloqueoPrecio = Item.BloqueoPrecio,
                        Cantidad = Item.Cantidad,
                        Codigo = Item.Codigo,
                        Comentarios = Item.Comentarios,
                        Costo = Item.Costo,
                        CostoIva = Item.CostoIva,
                        Descripcion = Item.Descripcion,
                        ExistenciaAnterior = Item.ExistenciaAnterior,
                        IdProducto = Item.IdProducto,
                        Iva = Item.Iva,
                        Precio = Item.Precio,
                        PrecioIva = Item.PrecioIva,
                        TasaIva = Item.TasaIva,
                        Tipo = Item.Tipo,
                        MontoNeto = Item.MontoNeto,
                        Total = Item.Total
                    };
                    newdc.DocumentosProductos.Add(NuevoItem);
                    }
                    try
                    {
                        newdc.SaveChanges();
                        Factura = FactoryDocumentos.Item(newdc, Factura.IdDocumento);
                        Factura.Status = "ANULADA";
                        newdc.SaveChanges();
                        DevolverInventario(Doc);
                        //  DevolverCuentaxCobrar(Factura);
                        //      DevolucionLibroDeVentas(Factura,  Factura.Numero);
                        DevolverCaja(Factura);
                    }
                    catch (Exception x)
                    {
                        string s = x.Message;
                        return false;
                    }
                    return true;
                }
            }
            public static void DevolverCaja(Documentos Doc)
            {
                Recibos Recibo = null;
                using (var dc = new SoroEntities())
                {
                    var IdRecibo = (from R in dc.RecibosDetalles
                                    where R.IdDocumento == Doc.IdDocumento
                                    select R.IdRecibo).FirstOrDefault();
                    if (!string.IsNullOrEmpty(IdRecibo))
                    {
                        Recibo = dc.Recibos.FirstOrDefault(x => x.IdRecibo == IdRecibo);
                    }
                    if (Recibo == null)
                        return;
                    Recibo.Monto = 0;
                    Recibo.Activo = false;
                    Recibo.Efectivo = 0;
                    Recibo.Cheques = 0;
                    Recibo.Depositos = 0;
                    Recibo.Cambio = 0;
                    Recibo.Tarjetas = 0;
                    Recibo.Transferencias = 0;
                    foreach (RecibosDetalles Item in Recibo.RecibosDetalles)
                    {
                        Item.MontoAbono = 0;
                    }
                    foreach (RecibosPagos Item in Recibo.RecibosPagos)
                    {
                        Item.Monto = 0;
                    }
                    dc.SaveChanges();
                }

            }
            public static void DevolverInventario(Documentos Doc)
            {
                using (var dc = new SoroEntities())
                {
                    foreach (DocumentosProductos Item in Doc.DocumentosProductos)
                    {
                        Productos Producto = FactoryProductos.Item(Item.IdProducto);
                        if ((bool)Producto.Activo && (bool)Producto.LlevaInventario)
                        {
                            FactoryLibroInventarios.EscribirLibroInventario(Doc.Fecha.Value, Item, "DEVOLUCION");
                            Item.ExistenciaAnterior = Producto.Existencia;
                            Producto.Existencia = Producto.Existencia + Item.Cantidad;
                            FactoryProductos.Guardar(Producto);
                            dc.SaveChanges();
                        }
                    }
                }
            }
            public static void DevolverCuentaxCobrar(Documentos Doc)
            {
                Cuentas CuentaxCobrar = new Cuentas();
                using (SoroEntities dc = new SoroEntities())
                {
                    CuentaxCobrar.Concepto = "DEVOLUCION # " + Doc.Numero;
                    CuentaxCobrar.Fecha = Doc.Fecha;
                    CuentaxCobrar.IdCuenta = FactoryContadores.GetLast("IdCuenta");
                    CuentaxCobrar.IdDocumento = Doc.IdDocumento;
                    CuentaxCobrar.IdTercero = Doc.IdTercero;
                    CuentaxCobrar.Monto = Doc.MontoTotal;
                    CuentaxCobrar.Numero = Doc.Numero;
                    CuentaxCobrar.Saldo = 0;
                    CuentaxCobrar.Tipo = "CXC";
                    CuentaxCobrar.TipoDocumento = "DEVOLUCION";
                    CuentaxCobrar.Vence = Doc.Vence;
                    dc.Cuentas.Add(CuentaxCobrar);
                    Cuentas Factura = FactoryCuentas.Item(dc, Doc.IdDocumento);
                    Factura.Saldo = 0;
                    dc.SaveChanges();
                }
            }
        }
    }
