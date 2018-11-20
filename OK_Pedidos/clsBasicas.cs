using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace HK
{
    class cBasicas
    {
        public static List<string> Bancos()
        {
            using( var Entities = new Entities())
            {
                var q = from p in Entities.Bancos
                        orderby p.Banco1
                        select p.Banco1;
                return q.ToList();
            }
        }
        public static double Round(double? valor)
        {
            try
            {
                decimal myValor = (decimal)valor;
                myValor = decimal.Round(myValor, 2);
                return (double)myValor;
            }
            catch
            {
                return 0;
            }
        }
        public static bool IsNumeric(object Expression)
        {
            bool isNum;
            double retNum;

            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }
        public static string CedulaRif(string Texto)
        {
            if (string.IsNullOrEmpty(Texto))
            {
                return Texto;
            }
            Texto = Texto.ToUpper();
            Texto = Texto.Replace(":", "");
            Texto = Texto.Replace("-", "").Replace(".", "").Replace(" ", "").Replace(",", "").Replace("CI", "").Replace("RIF", "").Replace("C", "");
            if (Texto.Length > 0)
            {
                if (IsNumeric(Texto[0]))
                {
                    Texto = "V" + Texto;
                }
            }
            return Texto;
        }
        public static void ConectarDB()
        {
            Parametro p = FactoryParametros.Item();
            System.Data.SqlClient.SqlConnectionStringBuilder cs = new System.Data.SqlClient.SqlConnectionStringBuilder(Program.cn);
            cs.InitialCatalog = p.BaseDeDatos;
            cs.Password = "aleman";
            cs.UserID = "sa";
            cs.DataSource = p.Servidor;
            cs.PersistSecurityInfo = true;
            Program.cn = cs.ToString();
        }
    }
    class FactoryContadores
    {
        public static string GetLast(string Variable)
        {
            try
            {
                using (var oEntidades = new Entities())
                {
                    Contadore Contador = oEntidades.Contadores.FirstOrDefault(x => x.Variable == Variable);
                    if (Contador == null)
                    {
                        Contador = new Contadore();
                        Contador.Variable = Variable;
                        Contador.Valor = 1;
                        oEntidades.Contadores.Add(Contador);
                    }
                    else
                    {
                        Contador.Valor++;
                    }
                    oEntidades.SaveChanges();
                    return Contador.Valor.ToString().PadLeft(6, '0');
                }
            }
            catch (Exception ex)
            {
                string x = ex.Message;
            }
            return "";
        }
        public static string SQLGetLast(string Variable)
        {
            try
            {
                using (var oEntidades = new SQLDataContext(Program.cn))
                {
                    SQLContadores Contador = oEntidades.SQLContadores.FirstOrDefault(x => x.Variable == Variable);
                    if (Contador == null)
                    {
                        Contador = new SQLContadores();
                        Contador.Variable = Variable;
                        Contador.Valor = 1;
                        oEntidades.SQLContadores.InsertOnSubmit(Contador);
                    }
                    else
                    {
                        Contador.Valor++;
                    }
                    oEntidades.SubmitChanges();
                    return Contador.Valor.ToString().PadLeft(6, '0');
                }
            }
            catch (Exception ex)
            {
                string x = ex.Message;
            }
            return "";
        }
    }
    class FactoryProductos
    {
        public static List<Producto> CargarProductosProveedor(string IdProveedor)
        {
            using (var dc = new Entities())
            {
                return dc.Productos.Where(x => x.IdProveedor == IdProveedor).ToList();
            }
        }
        public static List<Producto> CargarProductosMinimo()
        {
            using (var dc = new Entities())
            {
                return dc.Productos.Where(x => x.Existencia <= x.Minimo).ToList();
            }
        }
        static public List<Producto> Buscar(string Texto)
        {
            using(var Entities = new Entities())
            {
                return Entities.Productos.Where(x => x.Descripcion.Contains(Texto)).Where(x=> x.Existencia>0).OrderBy(x=>x.Descripcion).ToList();
            }
        }
        static public Producto Item(string Id)
        {
            using (var Entities = new Entities())
            {
                return Entities.Productos.Where(x => x.IdProducto == Id).FirstOrDefault();
            }
        }

        public static List<Producto> BuscarP(string Texto)
        {
            using (var Entities = new Entities())
            {
                return Entities.Productos.Where(x => x.Descripcion.Contains(Texto) && x.Existencia > 0).OrderBy(x => x.Descripcion).ToList();
            }

        }
    }
    class FactoryTerceros
    {
        static public List<string> Rutas()
        {
            using (var Entities = new Entities())
            {
                var x = (from p in Entities.Clientes
                         orderby p.Ruta
                         where p.Ruta != null
                         select p.Ruta).Distinct();

                return x.ToList();
            }
        }
        static public List<string> Ciudades()
        {
            using( var Entities = new Entities())
            {
                var x = (from p in Entities.Clientes
                         orderby p.Ciudad
                         where p.Ciudad != null
                         select p.Ciudad).Distinct();

                return x.ToList();
            }
        }
        static public List<string> Zonas()
        {
            using (var Entities = new Entities())
            {
                var x = (from p in Entities.Clientes
                         orderby p.Zona
                         where p.Zona != null
                         select p.Zona).Distinct();
                return x.ToList();
            }
        }
        static public List<string> Condiciones()
        {
            using (var Entities = new Entities())
            {
                var x = (from p in Entities.Clientes
                         orderby p.Condiciones
                         where p.Zona != null
                         select p.Condiciones).Distinct();
                return x.ToList();
            }
        }
        public static bool IsNumeric(object Expression)
        {
            bool isNum;
            double retNum;

            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }
        public static string CedulaRif(string Texto)
        {
            if (string.IsNullOrEmpty(Texto))
            {
                return Texto;
            }
            Texto = Texto.ToUpper();
            Texto = Texto.Replace(":", "");
            Texto = Texto.Replace("-", "").Replace(".", "").Replace(" ", "").Replace(",", "").Replace("CI", "").Replace("RIF", "").Replace("C", "");
            if (Texto.Length > 0)
            {
                if (IsNumeric(Texto[0]))
                {
                    Texto = "V" + Texto;
                }
            }
            if (Texto[0] == 'J' || Texto[0] == 'G')
            {
                if (Texto.Length > 10)
                {
                    Texto.Substring(0, 10);
                }
            }
            return Texto;
        }
        public static bool VerificarDuplicidad(Cliente cliente )
        {
            using (var dc = new Entities())
            {
                var q = from p in dc.Clientes
                        where p.CedulaRif == cliente.CedulaRif 
                        select p;
                if(q.FirstOrDefault()!=null)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool Guardar(Cliente cliente)
        {
           using (var dc = new Entities())
            {
                cliente.CedulaRif = CedulaRif(cliente.CedulaRif);
                if (VerificarDuplicidad(cliente))
                {
                    throw new Exception("Error Datos del cliente no se pueden registrar, esta cedula esta registrada con otro nombre");
                }
                if ((cliente.CedulaRif[0] == 'J' || cliente.CedulaRif[0] == 'G') && cliente.CedulaRif.Length != 10)
                {
                    throw new Exception("Numero de Rif Invalido");
                }
                if (string.IsNullOrEmpty(cliente.RazonSocial))
                {
                    throw new Exception("Nombre o Razon Social no pueden estar en blanco");
                }
                if (String.IsNullOrEmpty(cliente.IdTercero))
                {
                    cliente.IdTercero ="T"+ FactoryContadores.GetLast("IdTercero");
                    cliente.Nuevo = true;
                    dc.Clientes.Add(cliente);
                }
                else
                {
                    dc.Clientes.Attach(cliente);
                }
                try
                {
                    dc.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return true;
            }
        }
        static public List<Cliente> Buscar(Entities Entities, string Texto)
        {
            {
                if (Texto.Length > 0)
                {
                    var x = from p in Entities.Clientes
                            where (p.RazonSocial.Contains(Texto) || p.CedulaRif.Contains(Texto)) && p.TipoPrecio!=""
                            orderby p.RazonSocial
                            select p;
                    return x.ToList();
                }
                else
                {
                    var x = from p in Entities.Clientes                            
                            orderby p.RazonSocial
                            select p;
                    return x.ToList();
                }                
            }
        }
        static public Cliente Item(string Id)
        {
            using (var Entities = new Entities())
            {
                return Entities.Clientes.Where(x => x.IdTercero==Id).FirstOrDefault();
            }
        }
        static public double SaldoPendiente(string Id)
        {
            using (var Entities = new Entities())
            {
                var TotalPendiente = (from p in Entities.Cuentas
                                      where p.IdTercero == Id && p.Tipo == "CXC" && p.Saldo > 0
                                      select p).Sum(p => p.Saldo);
                if (TotalPendiente.HasValue)
                    return TotalPendiente.Value;
                else
                    return 0;
            } 
        }
    }
    class FactoryPedidos
    {
        static public Pedido Item(string id, Entities Entities)
        {          
           return Entities.Pedidos.Where(x => x.IdPedido == id).FirstOrDefault();
        }
        static public List<PedidosDetalle> Detalles(string id, Entities Entities)
        {
            return Entities.PedidosDetalles.Where(x => x.IdPedido == id).ToList();
        }
        static public bool EliminarPedido(Pedido pedido, Entities Entities)
        {
            try
            {
                foreach (PedidosDetalle Detalle in Entities.PedidosDetalles.Where(x => x.IdPedido == pedido.IdPedido))
                {
                    Producto p = Entities.Productos.FirstOrDefault(x => x.IdProducto == Detalle.IdProducto);
                    if (p != null)
                    {
                        p.Existencia = p.Existencia + Detalle.Cantidad;
                    }
                }
                Entities.Pedidos.Remove(pedido);
                RegistroPago Pago = (from p in Entities.RegistroPagos
                                     where p.IdDocumento == pedido.IdPedido
                                     select p).FirstOrDefault();
                if (Pago != null)
                {
                    Entities.RegistroPagos.Remove(Pago);
                }
                Entities.SaveChanges();
                return true;
            }
            catch( Exception x )
            {
                throw x;
            }

        }
    }
    class FactoryParametros
    {
        static Parametro parametro = new Parametro();
        public static Parametro Item()
        {
            using (var Entities = new Entities())
            {
                parametro = (from q in Entities.Parametros
                                select q).FirstOrDefault();
                return parametro;
            }
        }
    }
    class FactoryCuentas
    {
        public static List<Cuenta> DocumentosPendientesCobrar(string IdTercero)
        {
            using(var dc = new Entities())
            {
                var Consulta = from p in dc.Cuentas
                               where (p.IdTercero == IdTercero) && (p.Saldo > 0) && (p.Tipo == "CXC")
                               orderby p.Fecha
                               select p;
                return Consulta.ToList();
            }
        }
        public static ResumenCuentas CuentasTercero(string IdTercero)
        {
            ResumenCuentas Nuevo = new ResumenCuentas();
            Entities dc = new Entities();;
            var TotalFacturado = (from p in dc.Cuentas
                                  where p.IdTercero == IdTercero && p.Tipo == "CXC"
                                  select p).Sum(p => p.Monto);
            if (TotalFacturado.HasValue) Nuevo.TotalFacturado = (double)TotalFacturado;
            var TotalPagado = (from p in dc.Cuentas
                               where p.IdTercero == IdTercero && p.Tipo == "CXC"
                               select p).Sum(p => p.Monto - p.Saldo);
            if (TotalPagado.HasValue) Nuevo.TotalPagado = (double)TotalPagado;
            var TotalVencido = (from p in dc.Cuentas
                                where p.IdTercero == IdTercero && p.Tipo == "CXC" && p.Saldo > 0 && p.Vence <= DateTime.Today
                                select p).Sum(p => p.Saldo);
            if (TotalVencido.HasValue) Nuevo.TotalVencido = (double)TotalVencido;
            var TotalPendiente = (from p in dc.Cuentas
                                  where p.IdTercero == IdTercero && p.Tipo == "CXC" && p.Saldo > 0
                                  select p).Sum(p => p.Saldo);
            if (TotalPendiente.HasValue) Nuevo.TotalPendiente = (double)TotalPendiente;
            return Nuevo;
        }
    }
    class ResumenCuentas
    {
        private double totalFacturado = 0;

        public double TotalFacturado
        {
            get { return totalFacturado; }
            set { totalFacturado = value; }
        }
        private double totalPagado = 0;

        public double TotalPagado
        {
            get { return totalPagado; }
            set { totalPagado = value; }
        }
        private double totalVencido = 0;

        public double TotalVencido
        {
            get { return totalVencido; }
            set { totalVencido = value; }
        }
        private double totalPendiente = 0;

        public double TotalPendiente
        {
            get { return totalPendiente; }
            set { totalPendiente = value; }
        }
    }
    class FactoryPagos
    {
        static public bool EliminarPago(RegistroPago pago, Entities Entities)
        {
            try
            {
                Entities.RegistroPagos.Remove(pago);
                Entities.SaveChanges();
                return true;
            }
            catch (Exception x)
            {
                throw x;
            }
        }
    }
    partial class Cuenta
    {
        double? pagarHoy = null;

        public double? PagarHoy
        {
            get { return pagarHoy; }
            set { pagarHoy = value; }
        }
        double? pendiente = 0;
        public double? Pendiente
        {
            get { return this.Saldo.Value - this.pagarHoy; }
            set { pendiente = value; }
        }
    }
    partial class Pedido
    {
        public void CalcularTotales()
        {
            this.MontoTotal = 0;
            var Total = (from p in this.PedidosDetalles
                         select p.Total).Sum();
            this.Peso = (from p in this.PedidosDetalles
                         select p.PesoTotal).Sum();
            if (Total.HasValue)
                this.MontoTotal = Total.Value;            
        }
    }
    partial class Producto
    {
        double? pedido = 0;

        public double? Pedido
        {
            get { return pedido; }
            set { pedido = value; }
        }
        //double? precioIva = 0;

        //public double? PrecioIva
        //{
        //    get {
        //        precioIva = Precio + (Precio * Iva / 100);
        //        return precioIva; 
        //    }
        //    set { precioIva = value; }
        //}
        //double? precioIva2 = 0;

        //public double? PrecioIva2
        //{
        //    get 
        //    {
        //        precioIva2 = Precio2 + (Precio2 * Iva / 100);
        //        return precioIva2; 
        //    }
        //    set { precioIva2 = value; }
        //}
        //double? precioIva3 = 0;

        //public double? PrecioIva3
        //{
        //    get {
        //        precioIva3 = Precio3 + (Precio3 * Iva / 100);
        //        return precioIva3; 
        //    }
        //    set { precioIva3 = value; }
        //}
        //double? precioIva4 = 0;

        //public double? PrecioIva4
        //{
        //    get {
        //        precioIva4 = Precio4 + (Precio4 * Iva / 100);
        //        return precioIva4; 
        //    }
        //    set { precioIva4 = value; }
        //}

    }
}
