using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HK
{
    class cCompras
    {
        public static bool Guardar(SoroEntities dc, Documentos Doc,Terceros Tercero)
        {
                Doc.Tipo = "COMPRA";
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
                    using( var newdc = new SoroEntities())
                    {
                        Tercero.IdTercero = FactoryContadores.GetLast("IdTercero");
                        Tercero.DiasCredito = 0;
                        Tercero.Tipo  = "PROVEEDOR";
                        Tercero.Activo = true;
                         if( Tercero.CedulaRif.StartsWith("V") || Tercero.CedulaRif.StartsWith("E"))
                        {
                            Tercero.TipoContribuyente = "NO CONTRIBUYENTE";
                        }
                        else
                        {
                            Tercero.TipoContribuyente = "CONTRIBUYENTE";
                        }
                        newdc.Terceros.Add(Tercero);
                        newdc.SaveChanges();
                    }
                }
                if (!Tercero.DiasCredito.HasValue)
                {
                    Tercero.DiasCredito = 0;
                }
                Doc.Vence = Doc.Fecha.Value.AddDays((double)Tercero.DiasCredito);
                if (string.IsNullOrEmpty(Doc.IdDocumento))
                {
                    Doc.IdDocumento = FactoryContadores.GetLast("IdDocumento");
                    Doc.Comentarios = FactoryContadores.GetLast("CorrelativoCompra");
                    dc.Documentos.Add(Doc);
                }
                Doc.IdTercero = Tercero.IdTercero;
                foreach (DocumentosProductos Item in Doc.DocumentosProductos)
                {
                    if( string.IsNullOrEmpty( Item.IdDetalleDocumento ) )
                    {
                        Item.IdDetalleDocumento = FactoryContadores.GetLast("IdDetalleDocumento"); 
                    }
                }
                try
                {
                    dc.SaveChanges();
                }
                catch( Exception x )
                {
                    string s = x.Message;
          //          return false;
                }
            return true;
        }
        public static void CargarInventario(Documentos Doc )
        {
            
            using (var dc = new SoroEntities())
            {
                try
                {
                    Parametros Parametro = FactoryParametros.Item();
                    Documentos Factura = FactoryDocumentos.Item(dc, Doc.IdDocumento);
                    Factura.Status = "INVENTARIO";
                    foreach (DocumentosProductos Item in Factura.DocumentosProductos)
                    {
                        Productos Producto = FactoryProductos.Item(Item.IdProducto);
                        if ((bool)Producto.Activo && (bool)Producto.LlevaInventario)
                        {
                            if (Doc.Control != "000000")
                            {
                                FactoryLibroInventarios.EscribirLibroInventario(Doc.Fecha.Value, Item, "COMPRA");
                            }
                            {
                                Item.ExistenciaAnterior = Producto.Existencia;
                                Producto.Existencia = Producto.Existencia + Item.Cantidad;
                            }
                            Producto.CostoActual = Item.MontoNeto;
                            Producto.Utilidad = Item.Utilidad;
                            Producto.PrecioIva = Item.PrecioIva;

                            Producto.Utilidad2 = Item.Utilidad2;
                            Producto.Utilidad3 = Item.Utilidad3;
                            Producto.Utilidad4 = Item.Utilidad4;
                            Producto.Iva = Item.TasaIva;
                            Producto.Precio = Item.Precio;
                            Producto.Costo = Item.Costo;
                            
                            Producto.parametros = Parametro;
                            
                            Producto.Precio2 = Item.Precio2;
                            Producto.Precio3 = Item.Precio3;
                            Producto.Precio4 = Item.Precio4;
                            Producto.IdProveedor = Doc.IdTercero;
                            Producto.PVP = Item.Pvs;
                            FactoryProductos.Guardar(Producto);
                       //     dc.SaveChanges();
                        }
                    }
                    dc.SaveChanges();
                }                     
                catch (Exception x)
                {
                    string s = x.Message;
                }
            }
           
        }
        public static void EscribirCuentaxPagar(Documentos Doc)
        {
            Cuentas CuentaxPagar = new Cuentas();
            using (SoroEntities dc = new SoroEntities())
            {
                CuentaxPagar.Concepto = "FACTURA # " + Doc.Numero;
                CuentaxPagar.Fecha = Doc.Fecha;
                CuentaxPagar.IdCuenta = FactoryContadores.GetLast("IdCuenta");
                CuentaxPagar.IdDocumento = Doc.IdDocumento;
                CuentaxPagar.IdTercero = Doc.IdTercero;
                CuentaxPagar.Monto = Doc.MontoTotal;
                CuentaxPagar.Numero = Doc.Numero;
                CuentaxPagar.Saldo = Doc.Saldo;
                CuentaxPagar.TipoDocumento = Doc.Tipo;
                CuentaxPagar.Tipo = "CXP";
                CuentaxPagar.Vence = Doc.Vence;
                dc.Cuentas.Add(CuentaxPagar);
                dc.SaveChanges();
            }
        }
        public static void LibroDeCompras( Documentos Doc )
        {
            using (var dc = new SoroEntities())
            {
                Terceros Tercero = FactoryTerceros.Item(Doc.IdTercero);
                LibroCompras Registro = new LibroCompras();
                Registro.BaseImponible = Doc.BaseImponible;
                Registro.CedulaRif = Tercero.CedulaRif;
                Registro.Fecha = Doc.Fecha;
                Registro.TasaIva = Doc.TasaIVA;
                Registro.ImpuestoIVA = Doc.MontoIva;
                Registro.Numero = Doc.Numero;
                Registro.RazonSocial = Tercero.RazonSocial;
                Registro.TotalIncluyendoIva = Doc.MontoTotal;
                Registro.ComprasNoSujetas = Doc.MontoExento;
                Registro.ComprasSinCreditoIVA = 0;
                Registro.IdLibroCompras = FactoryContadores.GetLast("IdLibroCompras");
                Registro.Año = Doc.Año;
                Registro.Mes = Doc.Mes;
                Registro.Control = Doc.Control;
                dc.LibroCompras.Add(Registro);
                dc.SaveChanges();
            }
        }
    }
}
