using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;

namespace HK
{
    public class VentasxProducto
    {
        DateTime? fecha;

        public DateTime? Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        string descripcion = "";

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        double? cantidad = 0;

        public double? Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
        double? bolivares = 0;

        public double? Bolivares
        {
            get { return bolivares; }
            set { bolivares = value; }
        }
    }
    public class ProductoHistorial
    {

        DateTime? fecha = DateTime.Today;

        public DateTime? Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
        double? cantidad = 0;

        public double? Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
        string razonSocial = null;

        public string RazonSocial
        {
            get { return razonSocial; }
            set { razonSocial = value; }
        }
        double? montoneto = 0;

        public double? MontoNeto
        {
            get { return montoneto; }
            set { montoneto = value; }
        }
        double? existenciaAnterior = 0;

        public double? ExistenciaAnterior
        {
            get { return existenciaAnterior; }
            set { existenciaAnterior = value; }
        }
        string tipo = null;

        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        double? saldo = 0;
        public double? Saldo
        {
            get { return saldo; }
            set { saldo = value; }
        }
        string numero = null;

        public string Numero
        {
            get { return numero; }
            set { numero = value; }
        }
        private double? entrada = 0;

        public double? Entrada
        {
            get { return entrada; }
            set { entrada = value; }
        }
        private double? salida = 0;

        public double? Salida
        {
            get { return salida; }
            set { salida = value; }
        }

    }
    partial class Productos
    {


        double? costoIva;
        double? precioIva;
        double? precioIva2;
        double? precioIva3;
        double? precioIva4;
        double? precioIva5;
        double? utilidad = 0;
        double? utilidad2 = 0;
        double? utilidad3 = 0;
        double? utilidad4 = 0;
        double? utilidad5 = 0;

        double? pedido = 0;

        public double? Pedido
        {
            get { return pedido; }
            set { pedido = value; }
        }

        public double? CostoIva
        {
            get 
            {
                try
                {
                    double? mIva = Costo * Iva / 100;
                    return Costo + mIva;
                }                
                catch
                {
                    return Costo;
                }
            }
            set 
            {
                double? mIva = value / (Iva / 100);
                costoIva = Costo + mIva;
            }
        }
        public double? PrecioIva
        {
            get
            {
                try
                {
                    double? mIva = Precio * Iva / 100;
                    return cBasicas.Round( Precio + mIva );
                }
                catch
                {
                    return Precio;
                }
            }
            set
            {
                Precio = value / (1 + (Iva / 100));
                precioIva = value;
            }
        }
        public double? PrecioIva2
        {
            get
            {
                try
                {
                    double? mIva = Precio2 * Iva / 100;
                    return cBasicas.Round( Precio2 + mIva);
                }
                catch
                {
                    return Precio2;
                }
            }
            set
            {
                Precio2 = value / (1 + (Iva / 100));
                precioIva2 = value;
            }
        }
        public double? PrecioIva3
        {
            get
            {
                try
                {
                    double? mIva = Precio3 * Iva / 100;
                    return cBasicas.Round( Precio3 + mIva );
                }
                catch
                {
                    return Precio3;
                }
            }
            set
            {
                Precio3 = value / (1 + (Iva / 100));
                precioIva3 = value;
            }
        }
        public double? PrecioIva4
        {
            get
            {
                try
                {
                    double? mIva = Precio4 * Iva / 100;
                    return cBasicas.Round( Precio4 + mIva );
                }
                catch
                {
                    return Precio4;
                }
            }
            set
            {
                Precio4 = value / (1+ (Iva / 100));
                precioIva4 = value;
                
            }
        }
        public double? PrecioIva5
        {
            get
            {
                try
                {
                    double? mIva = Precio5 * Iva / 100;
                    return cBasicas.Round(Precio5 + mIva);
                }
                catch
                {
                    return Precio5;
                }
            }
            set
            {
                Precio5 = value / (1 + (Iva / 100));
                precioIva5 = value;

            }
        }
        public Parametros parametros = FactoryParametros.Item();
        public void CalcularPrecio1()
        {
            if (parametros.CalculoPrecios == "SOBRE COSTOS")
            {
                Precio = CostoActual + (CostoActual * utilidad / 100);
            }
            else
            {
                Precio = CostoActual / (1 - (utilidad / 100));
            }
            if (!Precio.HasValue)
            {
                Precio = 0;
            }
            Precio = cBasicas.Round(Precio.Value);
        }
        public void CalcularPrecio2()
        {
            if (parametros.CalculoPrecios == "SOBRE COSTOS")
            {
                Precio2 = CostoActual + (CostoActual * utilidad2 / 100);
            }
            else
            {
                Precio2 = CostoActual / (1 - (utilidad2 / 100));
            }
            if (!Precio2.HasValue)
            {
                Precio2 = 0;
            }
            Precio2 = cBasicas.Round(Precio2.Value);
        }
        public void CalcularPrecio3()
        {
            if (parametros.CalculoPrecios == "SOBRE COSTOS")
            {
                Precio3 = CostoActual + (CostoActual * utilidad3 / 100);
            }
            else
            {
                Precio3 = CostoActual / (1 - (utilidad3 / 100));
            }
            if (!Precio3.HasValue)
            {
                Precio3 = 0;
            }
            Precio3 = cBasicas.Round(Precio3.Value);
        }
        public void CalcularPrecio4()
        {
            if (parametros.CalculoPrecios == "SOBRE COSTOS")
            {
                Precio4 = CostoActual + (CostoActual * utilidad4 / 100);
            }
            else
            {
                Precio4 = CostoActual / (1 - (utilidad4 / 100));
            }
            if (!Precio4.HasValue)
            {
                Precio4 = 0;
            }
            Precio4 = cBasicas.Round(Precio4.Value);
        }
        public void CalcularPrecio5()
        {
            if (parametros.CalculoPrecios == "SOBRE COSTOS")
            {
                Precio5 = CostoActual + (CostoActual * utilidad5 / 100);
            }
            else
            {
                Precio5 = CostoActual / (1 - (utilidad5 / 100));
            }
            if (!Precio5.HasValue)
            {
                Precio5 = 0;
            }
            Precio5 = cBasicas.Round(Precio5.Value);
        }
        public System.Nullable<double> Utilidad
        {
            get
            {
                double? Retorno = 0;
                if (CostoActual == 0 || Precio == 0)                
                    return 0;
                try
                {
                    if (parametros.CalculoPrecios == "SOBRE COSTOS")
                    {
                        Retorno = (Precio / CostoActual);
                        Retorno = (Retorno - 1) * 100;
                    }
                    else
                    {
                        Retorno = ((Precio - CostoActual) * 100) / Precio;
                    }

                }
                catch { }
                try
                {
                    utilidad = Retorno.Value;
                    return Retorno.Value;
                }
                catch { return 0; }
            }
            set
            {
                utilidad = value;
                CalcularPrecio1();
            }
        }
        public System.Nullable<double> Utilidad2
        {
            get
            {
                double? Retorno = 0;
                if (CostoActual == 0 || Precio2 == 0)
                    return 0;
                try
                {
                    if (parametros.CalculoPrecios == "SOBRE COSTOS")
                    {
                        Retorno = (Precio2 / CostoActual);
                        Retorno = (Retorno - 1) * 100;
                    }
                    else
                    {
                        Retorno = ((Precio2 - CostoActual) * 100) / Precio2;
                    }

                }
                catch { }
                try
                {
                    utilidad2 = Retorno.Value;
                    return Retorno.Value;
                }
                catch { return 0; }
            }
            set
            {
                utilidad2 = value;
                CalcularPrecio2();
            }
        }
        public System.Nullable<double> Utilidad3
        {
            get
            {
                double? Retorno = 0;
                if (CostoActual == 0 || Precio3 == 0)
                    return 0;
                try
                {
                    if (parametros.CalculoPrecios == "SOBRE COSTOS")
                    {
                        Retorno = (Precio3 / CostoActual);
                        Retorno = (Retorno - 1) * 100;
                    }
                    else
                    {
                        Retorno = ((Precio3 - CostoActual) * 100) / Precio3;
                    }

                }
                catch { }
                try
                {
                    utilidad3 = Retorno.Value;
                    return Retorno.Value;
                }
                catch { return 0; }
            }
            set
            {
                utilidad3 = value;
                CalcularPrecio3();
            }
        }
        public System.Nullable<double> Utilidad4
        {
            get
            {
                double? Retorno = 0;
                if (CostoActual == 0 || Precio4 == 0)
                    return 0;
                try
                {
                    if (parametros.CalculoPrecios == "SOBRE COSTOS")
                    {
                        Retorno = (Precio4 / CostoActual);
                        Retorno = (Retorno - 1) * 100;
                    }
                    else
                    {
                        Retorno = ((Precio4 - CostoActual) * 100) / Precio4;
                    }

                }
                catch { }
                try
                {
                    utilidad4 = Retorno.Value;
                    return Retorno.Value;
                }
                catch { return 0; }
            }
            set
            {
                utilidad4 = value;
                CalcularPrecio4();
            }
        }
        public System.Nullable<double> Utilidad5
        {
            get
            {
                double? Retorno = 0;
                if (CostoActual == 0 || Precio5 == 0)
                    return 0;
                try
                {
                    if (parametros.CalculoPrecios == "SOBRE COSTOS")
                    {
                        Retorno = (Precio5 / CostoActual);
                        Retorno = (Retorno - 1) * 100;
                    }
                    else
                    {
                        Retorno = ((Precio5 - CostoActual) * 100) / Precio5;
                    }

                }
                catch { }
                try
                {
                    utilidad5 = Retorno.Value;
                    return Retorno.Value;
                }
                catch { return 0; }
            }
            set
            {
                utilidad5 = value;
                CalcularPrecio5();
            }
        }

    }
    class FactoryProductos
    {
        public static List<VistaProductos> CargarProductosProveedor(string IdProveedor)
        {
            using (var dc = new SoroEntities())
            {
                return dc.VistaProductos.Where(x => x.IdProveedor == IdProveedor).ToList();
            }
        }
        public static List<VistaProductos> CargarProductosMinimo()
        {
            using (var dc = new SoroEntities())
            {
                return dc.VistaProductos.Where(x => x.Existencia <= x.Minimo).ToList();
            }
        }
        public static List<String> Marcas()
        {
            using (var dc = new SoroEntities())
            {
                var q = (from p in dc.Productos
                         orderby p.Marca
                         select p.Marca).Distinct();
                return q.ToList();
            }
        }
        public static List<String> Unidades()
        {
            using (var dc = new SoroEntities())
            {
                var q = (from p in dc.Productos
                         orderby p.UnidadMedida
                         select p.UnidadMedida).Distinct();
                return q.ToList();
            }
        }
        public static double MontoInventarios()
        {
            using (var dc = new SoroEntities())
            {
                var q = (from p in dc.Productos
                         where p.Activo == true && p.Existencia > 0
                         select (p.Existencia * p.Costo)).Sum();
                if (!q.HasValue)
                    q = 0;
                return q.Value;
            }
        }

        public static int Count()
        {
            using (var oEntidades = new SoroEntities())
            {
                return oEntidades.Productos.Count();
            }
        }
        public static bool VerificarDuplicidad(Productos producto)
        {
            using (var oEntidades = new SoroEntities())
            {
                Productos Producto = oEntidades.Productos.FirstOrDefault(x => (x.Codigo == producto.Codigo));
                if (Producto != null)
                {
                    if (Producto.IdProducto != producto.IdProducto)
                        return true;
                }
                return false;
            }
        }
        public static bool VerificarIntegridad(string Id)
        {
            using (var oEntidades = new SoroEntities())
            {
                DocumentosProductos Doc = oEntidades.DocumentosProductos.FirstOrDefault(x => x.IdProducto == Id);
                return (Doc != null);
            }
        }
        public static bool Delete(string Id)
        {
            using (var oEntidades = new SoroEntities())
            {
                Productos Item = oEntidades.Productos.FirstOrDefault(x => x.IdProducto == Id);
                if (Item == null)
                    return false;
                if (!VerificarIntegridad(Id))
                {
                    try
                    {
                        oEntidades.Productos.Remove(Item);
                        oEntidades.SaveChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        string s = ex.Message;
                        return false;
                    }
                }
                else
                {
                    try
                    {
                        Item.Activo = false;
                        oEntidades.SaveChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        string s = ex.Message;
                        return false;
                    }
                }
            }
        }
        public static List<Productos> BuscarP(string Texto)
        {
            using (var Db = new SoroEntities())
            {
                return Db.Productos.Where(x => x.Descripcion.Contains(Texto) &&  x.Existencia > 0 && x.Activo==true).OrderBy(x => x.Descripcion).ToList();
            }

        }
        public static List<VistaProductos> Buscar(string Texto)
        {
            using (var dc = new SoroEntities())
            {
                var Q = from producto in dc.VistaProductos
                        where (producto.Grupo.Contains(Texto) || producto.Descripcion.Contains(Texto) || producto.Codigo.Contains(Texto) || producto.Referencia.Contains(Texto) || producto.Marca.Contains(Texto) || producto.Modelo.Contains(Texto) || producto.CodigoBarras.Contains(Texto)) && producto.Activo == true
                        orderby producto.Descripcion
                        select producto;
                return Q.ToList();
            }
        }
        public static List<VistaProductos> BuscarConExistencia(string Texto)
        {
            using (var dc = new SoroEntities())
            {
                var Q = from producto in dc.VistaProductos
                        where (producto.Grupo.Contains(Texto) || producto.Descripcion.Contains(Texto) || producto.Codigo.Contains(Texto) || producto.Referencia.Contains(Texto) || producto.Marca.Contains(Texto) || producto.Modelo.Contains(Texto) || producto.CodigoBarras.Contains(Texto)) && producto.Activo == true && (producto.Existencia > 0 || producto.LlevaInventario == false)
                        orderby producto.Descripcion
                        select producto;
                return Q.ToList();
            }
        }
        public static List<VistaProductos> Buscar(SoroEntities dc, string Texto)
        {
            var Q = from p in dc.VistaProductos
                    where (p.Grupo.Contains(Texto) || p.Descripcion.Contains(Texto) || p.Codigo.Contains(Texto) || p.CodigoBarras.Contains(Texto) || p.Referencia.Contains(Texto) || p.Marca.Contains(Texto) || p.Modelo.Contains(Texto)) && p.Activo == true
                    orderby p.Descripcion
                    select p;
            return Q.ToList();
        }
        public static VistaProductos ItemVista(SoroEntities dc, string IdProducto)
        {
            return dc.VistaProductos.FirstOrDefault(x => x.IdProducto == IdProducto);
        }
        public static Productos Item(string IdProducto)
        {
            using (var dc = new SoroEntities())
            {
                return dc.Productos.FirstOrDefault(x => x.IdProducto == IdProducto);
            }
        }
        public static Productos ItemxProducto(SoroEntities dc, string Producto)
        {
            return dc.Productos.FirstOrDefault(x => x.Descripcion == Producto);
        }
        public static Productos ItemxCodigo(SoroEntities dc, string Codigo)
        {
            return dc.Productos.FirstOrDefault(x => x.Codigo == Codigo);
        }
        public static bool Guardar(Productos Producto)
        {
            using (var dc = new SoroEntities())
            {
                if (string.IsNullOrEmpty(Producto.Codigo))
                {
                    throw new Exception("Es Obligatorio el codigo");
                }
                if (string.IsNullOrEmpty(Producto.Descripcion))
                {
                    throw new Exception("Es Obligatoria la descripcion");
                }
                if (string.IsNullOrEmpty(Producto.IdGrupo))
                {
                    Producto.IdGrupo = "000001";
                    // throw new Exception("Es Obligatoria la linea");
                }
                if (FactoryProductos.VerificarDuplicidad(Producto))
                {
                    throw new Exception("Producto Duplicado");
                }
                if (string.IsNullOrEmpty(Producto.IdProducto))
                {
                    Producto.IdProducto = FactoryContadores.GetLast("IdProducto");
                    Producto.Activo = true;
                    dc.Productos.Add(Producto);
                }
                else
                {
                    dc.Productos.Attach(Producto);
                }
                try
                {
                    dc.SaveChanges();
                }
                catch (Exception x)
                {
                    throw x;
                }
                return true;
            }
        }

        internal static List<VistaFactura> BuscarNoDevueltosFactura(string Texto, List<VistaFactura> items)
        {
                var Q = from p in items
                        where (p.Descripcion.Contains(Texto) || p.Codigo.Contains(Texto)) 
                        orderby p.Descripcion
                        select p;
                return Q.ToList();
        }
    }
}

