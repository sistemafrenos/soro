using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HK
{

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
    partial class Cuentas
    {
        double? pagarHoy = null;

        public double? PagarHoy
        {
            get { return pagarHoy; }
            set { pagarHoy = value; }
        }
    }
    class FactoryCuentas
    {
        public static Cuentas Item(SoroEntities dc, string Id)
        {
            var Consulta = (from p in dc.Cuentas
                           where  (p.IdDocumento == Id)                           
                           select p).FirstOrDefault();
            return Consulta;
        }
        public static bool Guardar(SoroEntities dc,  Cuentas Cuenta)
        {
            if (string.IsNullOrEmpty(Cuenta.IdDocumento))
            {
                Cuenta.IdCuenta = FactoryContadores.GetLast("IdCuenta");
                dc.Cuentas.Add(Cuenta);
            }
            try
            {
                dc.SaveChanges();
            }
            catch 
            {
                return false;
            }
            return true;
        }
        public static double CuentasxCobrarSaldo()
        {
            SoroEntities dc = new SoroEntities();

            var TotalPendiente = (from p in dc.Cuentas
                                  where p.Tipo == "CXC" && p.Saldo > 0
                                  select p).Sum(p => p.Saldo);
            if (TotalPendiente.HasValue)
            {
                return TotalPendiente.Value;
            }
            else
            {
                return 0;
            }
        }
        public static double CuentasxPagarSaldo()
        {
            SoroEntities dc = new SoroEntities();

            var TotalPendiente = (from p in dc.TercerosConSaldos
                                  where p.Tipo == "CXP" 
                                  select p).Sum(p => p.MontoPendiente);
            if (!TotalPendiente.HasValue)
            {
                TotalPendiente = 0;
            }
            return TotalPendiente.Value;
        }
        public static ResumenCuentas CuentasTercero(string IdTercero)
        {
            ResumenCuentas Nuevo = new ResumenCuentas();
            SoroEntities dc = new SoroEntities();
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
        public static double SaldoPendienteCliente(string IdTercero)
        {
            using (SoroEntities dc = new SoroEntities())
            {
                var TotalxCobrar = (from p in dc.Cuentas
                                      where p.IdTercero == IdTercero && p.Tipo == "CXC"
                                      select p).Sum(p => p.Saldo);
                return TotalxCobrar.Value;
            }
        }
        public static ResumenCuentas CuentasPagarTercero(string IdTercero)
        {
            ResumenCuentas Nuevo = new ResumenCuentas();
            SoroEntities dc = new SoroEntities();
            var TotalFacturado = (from p in dc.Cuentas
                                  where p.IdTercero == IdTercero && p.Tipo == "CXP"
                                  select p).Sum(p => p.Monto);
            if (TotalFacturado.HasValue) Nuevo.TotalFacturado = (double)TotalFacturado;
            var TotalPagado = (from p in dc.Cuentas
                               where p.IdTercero == IdTercero && p.Tipo == "CXP"
                               select p).Sum(p => p.Monto - p.Saldo);
            if (TotalPagado.HasValue) Nuevo.TotalPagado = (double)TotalPagado;
            var TotalVencido = (from p in dc.Cuentas
                                where p.IdTercero == IdTercero && p.Tipo == "CXP" && p.Saldo > 0 && p.Vence <= DateTime.Today
                                select p).Sum(p => p.Saldo);
            if (TotalVencido.HasValue) Nuevo.TotalVencido = (double)TotalVencido;
            var TotalPendiente = (from p in dc.Cuentas
                                  where p.IdTercero == IdTercero && p.Tipo == "CXP" && p.Saldo > 0
                                  select p).Sum(p => p.Saldo);
            if (TotalPendiente.HasValue) Nuevo.TotalPendiente = (double)TotalPendiente;
            return Nuevo;
        }
        public static List<Cuentas> DocumentosPendientesCobrar(SoroEntities dc, string IdTercero)
        {
            var Consulta = from p in dc.Cuentas
                           where (p.IdTercero == IdTercero) && (p.Saldo > 0) && (p.Tipo == "CXC")
                           orderby p.Fecha
                           select p;
            return Consulta.ToList();
        }
        public static List<Cuentas> DocumentosPendientesCobrar(SoroEntities dc, string IdTercero,DateTime Fecha)
        {
            var Consulta = from p in dc.Cuentas
                           where (p.IdTercero == IdTercero) && (p.Saldo > 0) && (p.Tipo == "CXC") && (p.Fecha <= Fecha)
                           orderby p.Fecha
                           select p;
            return Consulta.ToList();
        }
        public static List<Cuentas> DocumentosPendientesPagar(SoroEntities dc, string IdTercero)
        {
            var Consulta = from p in dc.Cuentas
                           where (p.IdTercero == IdTercero) && (p.Saldo > 0) && (p.Tipo == "CXP")
                           orderby p.Fecha
                           select p;
            return Consulta.ToList();
        }
        public static List<Cuentas> DocumentosPendientesPagar(SoroEntities dc, string IdTercero,DateTime Fecha)
        {
            var Consulta = from p in dc.Cuentas
                           where (p.IdTercero == IdTercero) && (p.Saldo > 0) && (p.Tipo == "CXP") && (p.Fecha <= Fecha)
                           orderby p.Fecha
                           select p;
            return Consulta.ToList();
        }
        public static List<VistaCuentas> VistaCuentas(string Tipo)
        {
            using( var dc = new SoroEntities() )
            {
                var Consulta = from p in dc.VistaCuentas
                               where p.Saldo > 0 && p.Tipo == Tipo
                               orderby p.RazonSocial, p.Fecha
                               select p;
                return Consulta.ToList();
            }
        }
        public static List<VistaCuentas> VistaCuentas(string Tipo, DateTime Fecha)
        {
            using (var dc = new SoroEntities())
            {
                var Consulta = from p in dc.VistaCuentas
                               where p.Saldo > 0 && p.Tipo == Tipo && p.Fecha<=Fecha
                               orderby p.RazonSocial, p.Fecha
                               select p;
                return Consulta.ToList();
            }
        }
    }
}
