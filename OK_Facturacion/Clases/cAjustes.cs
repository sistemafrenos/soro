using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HK
{
    class cAjustes
    {
            public static bool Guardar(SoroEntities dc, Documentos Doc)
            {
                Doc.Tipo = "AJUSTE";
                Doc.Activo = true;
                if (!Doc.Fecha.HasValue)
                {
                    Doc.Fecha = DateTime.Today;
                }
                Doc.Momento = DateTime.Now;
                Doc.Saldo = 0;

                if (string.IsNullOrEmpty(Doc.IdDocumento))
                {
                    Doc.IdDocumento = FactoryContadores.GetLast("IdDocumento");
                    Doc.Numero = FactoryContadores.GetLast("AJUSTE");
                    Doc.Tipo = "AJUSTE";
                    dc.Documentos.Add(Doc);
                }
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
                    CargarInventario(Doc);
                }
                catch (Exception x)
                {
                    string s = x.Message;
                    return false;
                }
                return true;
            }
            public static void CalcularTotales(Documentos Doc)
            {
                Doc.BaseImponible = 0;
                Doc.MontoExento = 0;
                Doc.MontoIva = 0;
                Doc.MontoTotal = 0;
                if (Doc.DocumentosProductos == null)
                    return;
                var BaseImponible = (from p in Doc.DocumentosProductos
                                     where p.Iva != 0
                                     select p.Costo * p.Cantidad).Sum();
                if (BaseImponible.HasValue)
                    Doc.BaseImponible = BaseImponible.Value;

                var Exento = (from p in Doc.DocumentosProductos
                              where p.Iva == 0
                              select p.Total).Sum();
                if (Exento.HasValue)
                    Doc.MontoExento = Exento.Value;

                var Iva = (from p in Doc.DocumentosProductos
                           where p.Iva != 0
                           select p.Cantidad * (p.CostoIva - p.Costo)).Sum();
                if (Iva.HasValue)
                    Doc.MontoIva = Iva.Value;

                var Total = (from p in Doc.DocumentosProductos
                             select p.Total).Sum();
                if (Total.HasValue)
                    Doc.MontoTotal = Total.Value;
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
                            FactoryLibroInventarios.EscribirLibroInventario(Doc.Fecha.Value, Item,"AJUSTE");
                            Item.ExistenciaAnterior = Producto.Existencia;
                            Producto.Existencia = Producto.Existencia + Item.Cantidad;
                            FactoryProductos.Guardar(Producto);
                            dc.SaveChanges();
                        }
                    }
                }
            }
        }
    }

