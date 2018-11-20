using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HK
{
    class FactoryLibroInventarios
    {
        public static List<LibroInventarios> LibroInventarios(SoroEntities dc, int mes, int año)
        {
            var consulta = from p in dc.LibroInventarios
                           where p.Mes == mes && p.Año == año && p.Final>0
                           select p;
            return consulta.ToList();
        }
        public static bool IniciarMes(int mes, int año)
        {
            using (var dc = new SoroEntities())
            {
                var ProductosConInventario = from p in dc.Productos
                                             where (bool)p.Activo && (bool)p.LlevaInventario
                                             select p;
                foreach (Productos producto in ProductosConInventario)
                {
                        LibroInventarios q = new LibroInventarios();
                        q.Año = año;
                        q.Inicial = producto.Existencia;
                        q.InicialBs = producto.Existencia * producto.Costo;
                        q.Entradas = 0;
                        q.EntradasBs = 0;
                        q.Salidas = 0;
                        q.SalidasBs = 0;
                        q.Final = q.Inicial + q.Entradas - q.Salidas;
                        q.FinalBs = q.InicialBs + q.EntradasBs - q.SalidasBs;
                        q.IdProducto = producto.IdProducto;
                        q.Codigo = producto.Codigo;
                        q.Descripcion = producto.Descripcion;
                        q.Mes = mes;
                        q.Id = FactoryContadores.GetLast("IdLibroInventarios");
                        dc.LibroInventarios.Add(q);
                }
                dc.SaveChanges();
            }
            return true;
        }
        public static void EscribirLibroInventario(DateTime Fecha, DocumentosProductos Item, string Tipo)
        {
            using (SoroEntities dc = new SoroEntities())
            {
                var LibroVacio = (from p in dc.LibroInventarios
                                  where (p.Mes == Fecha.Month) && p.Año == (Fecha.Year)
                                  select p).FirstOrDefault();
                if (LibroVacio == null)
                {
                    FactoryLibroInventarios.IniciarMes(Fecha.Month, Fecha.Year);
                }
                var producto = (from _p in dc.Productos
                                where _p.IdProducto == Item.IdProducto
                                select _p).FirstOrDefault();
                var q = (from p in dc.LibroInventarios
                         where (p.IdProducto == Item.IdProducto) && (p.Mes == Fecha.Month) && p.Año == (Fecha.Year)
                         select p).FirstOrDefault();


                if (q == null)
                {
                    q = new LibroInventarios();
                    q.Año = Fecha.Year;
                    q.Inicial = producto.Existencia;
                    q.InicialBs = producto.Existencia * producto.Costo;
                    q.Entradas = 0;
                    q.EntradasBs = 0;
                    q.Salidas = 0;
                    q.SalidasBs = 0;
                    q.Final = 0;
                    q.FinalBs = 0;
                    q.IdProducto = producto.IdProducto;
                    q.Codigo = producto.Codigo;
                    q.Descripcion = producto.Descripcion;
                    q.Mes = Fecha.Month;
                    q.Id = FactoryContadores.GetLast("IdLibroInventarios");
                    dc.LibroInventarios.Add(q);
                }
                switch (Tipo)
                {
                    case "DEVOLUCION":
                        q.Entradas = q.Entradas + Item.Cantidad;
                        q.EntradasBs = q.EntradasBs + (Item.Cantidad * Item.Costo);
                        break;
                    case "FACTURA":
                        q.Salidas = q.Salidas + Item.Cantidad;
                        q.SalidasBs = q.SalidasBs + (Item.Cantidad * producto.Costo);
                        break;
                    case "COMPRA":
                        q.Entradas = q.Entradas + Item.Cantidad;
                        q.EntradasBs = q.EntradasBs + (Item.Cantidad * Item.Costo);
                        break;
                    case "AJUSTE":
                        if (Item.Cantidad < 0)
                        {
                            q.Salidas = q.Salidas + (Item.Cantidad * -1);
                            q.SalidasBs = q.SalidasBs + (Item.Cantidad * producto.Costo * -1);
                        }
                        else
                        {
                            q.Entradas = q.Entradas + Item.Cantidad;
                            q.EntradasBs = q.EntradasBs + (Item.Cantidad * Item.Costo);
                        }
                        break;
                }
                q.Final = q.Inicial + q.Entradas - q.Salidas;
                q.FinalBs = q.InicialBs + q.EntradasBs - q.SalidasBs;
                dc.SaveChanges();
            }
        }
        public static void EscribirLibroInventario(DateTime Fecha, Productos producto, double ExistenciaNueva)
        {
            using (SoroEntities dc = new SoroEntities())
            {
                var LibroVacio = (from p in dc.LibroInventarios
                                  where (p.Mes == Fecha.Month) && p.Año == (Fecha.Year)
                                  select p).FirstOrDefault();
                if (LibroVacio == null)
                {
                    FactoryLibroInventarios.IniciarMes(Fecha.Month, Fecha.Year);
                }
                var q = (from p in dc.LibroInventarios
                         where (p.IdProducto == producto.IdProducto) && (p.Mes == Fecha.Month) && p.Año == (Fecha.Year)
                         select p).FirstOrDefault();
                if (q == null)
                {
                    q = new LibroInventarios();
                    q.Año = Fecha.Year;
                    q.Inicial = producto.Existencia;
                    q.InicialBs = producto.Existencia * producto.Costo;
                    q.Entradas = 0;
                    q.EntradasBs = 0;
                    q.Salidas = 0;
                    q.SalidasBs = 0;
                    q.Final = 0;
                    q.FinalBs = 0;
                    q.IdProducto = producto.IdProducto;
                    q.Codigo = producto.Codigo;
                    q.Descripcion = producto.Descripcion;
                    q.Mes = Fecha.Month;
                    q.Id = FactoryContadores.GetLast("IdLibroInventarios");
                    dc.LibroInventarios.Add(q);
                }                
                if( ExistenciaNueva > producto.Existencia )
                {
                    q.Entradas = q.Entradas + ExistenciaNueva - producto.Existencia;
                    q.EntradasBs = q.EntradasBs + ((ExistenciaNueva - producto.Existencia) * producto.Costo);
                      
                }
                else
                {
                    q.Salidas = q.Salidas + (producto.Existencia- ExistenciaNueva);
                    q.SalidasBs = q.SalidasBs + ((producto.Existencia - ExistenciaNueva) * producto.Costo);
                }
                q.Final = q.Inicial + q.Entradas - q.Salidas;
                q.FinalBs = q.InicialBs + q.EntradasBs - q.SalidasBs;
                dc.SaveChanges();
            }
        }

    }
}