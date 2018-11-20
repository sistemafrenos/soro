using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HK
{
    class FactoryBancos
    {
        public static bool ItemDuplicado(string Id,string Cuenta)
        {
            using (var dc = new SoroEntities())
            {
                var Consulta = (from p in dc.Bancos
                                where (p.IdBanco != Id) && (p.Cuenta==Cuenta)
                                select p).Count();
                return (Consulta>0);
            }
        }
        public static Bancos Item(SoroEntities dc, string Id)
        {
            var Consulta = (from p in dc.Bancos
                            where (p.IdBanco == Id)
                            select p).FirstOrDefault();
            return Consulta;
        }
        public static List<Bancos> Buscar(string Texto)
        {
            using (var dc = new SoroEntities())
            {
                var Consulta = from p in dc.Bancos
                                where (p.Cuenta.Contains(Texto) || p.Banco.Contains(Texto)) && p.Activo.Value 
                                select p;
                return Consulta.ToList();
            }
        }
        public static List<Bancos> Buscar(SoroEntities dc, string Texto)
        {
                var Consulta = from p in dc.Bancos
                               where (p.Cuenta.Contains(Texto) || p.Banco.Contains(Texto)) && p.Activo.Value
                               select p;
                return Consulta.ToList();
        }
        public static bool Eliminar(Bancos Registro)
        {
            using (var dc = new SoroEntities())
            {
                Bancos myRegistro = Item(dc,Registro.IdBanco);
                try
                {
                    myRegistro.Activo = false;
                    dc.SaveChanges();
                    return true;
                }
                catch 
                {
                    return false;
                }
            }
        }
        public static bool Guardar(SoroEntities dc, Bancos Cuenta)
        {
            if (string.IsNullOrEmpty(Cuenta.IdBanco))
            {
                Cuenta.SaldoActual = Cuenta.SaldoInicial;
                Cuenta.IdBanco = FactoryContadores.GetLast("IdBanco");
                dc.Bancos.Add(Cuenta);
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
        public static BancosMovimientos ItemMovimiento(SoroEntities dc, string Id)
        {
            var Consulta = (from p in dc.BancosMovimientos
                            where (p.IdMovimiento == Id)
                            select p).FirstOrDefault();
            return Consulta;
        }
        public static bool MovimientoDuplicado(BancosMovimientos Registro)
        {
            using (var dc = new SoroEntities())
            {
                var Consulta = (from p in dc.BancosMovimientos
                                where (p.IdBanco == Registro.IdBanco) && (p.Numero == Registro.Numero) && (p.Tipo == Registro.Tipo)
                                select p).FirstOrDefault();
                if (Consulta == null)
                {
                    return false;
                }
                if (Consulta.IdMovimiento != Registro.IdMovimiento)
                    return true;
                else
                    return false;
            }
        }
        public static Double SaldoFecha(Bancos Banco, DateTime Fecha)
        {
            using (var newDc = new SoroEntities())
            {
                var TotalDebe = (from i in newDc.BancosMovimientos
                                 where i.Fecha < Fecha && i.IdBanco == Banco.IdBanco
                                 select i.Debe).Sum();
                var TotalHaber = (from i in newDc.BancosMovimientos
                                  where i.Fecha < Fecha && i.IdBanco == Banco.IdBanco
                                  select i.Haber).Sum();
                if (!TotalDebe.HasValue)
                {
                    TotalDebe = 0;
                }
                if (!TotalHaber.HasValue)
                {
                    TotalHaber = 0;
                }
                return TotalHaber.Value - TotalDebe.Value;
            }
        }
        public static Double SaldoInicioMes(Bancos Banco, int Mes, int Año)
        {
            using (var newDc = new SoroEntities())
            {
                    DateTime PrimeroMes = Convert.ToDateTime("01/" + Mes.ToString() + "/" + Año.ToString());
                    var BancosMovimientos = from i in newDc.BancosMovimientos
                                             where i.Fecha<PrimeroMes && i.IdBanco == Banco.IdBanco
                                             select new 
                                             {
                                                 debe = i.Debe,
                                                 haber = i.Haber
                                             };
                    return Banco.SaldoInicial.GetValueOrDefault(0)
                                + BancosMovimientos.Sum(x=>x.haber).GetValueOrDefault(0)
                                - BancosMovimientos.Sum(x=>x.debe).GetValueOrDefault(0);
           }
        }
        public static Double SaldoInicioMesOld(Bancos Banco, int Mes, int Año)
        {
            using (var newDc = new SoroEntities())
            {
                DateTime PrimeroMes = Convert.ToDateTime("01/" + Mes.ToString() + "/" + Año.ToString());
                if (Mes > 1)
                {
                    var TotalDebe = (from i in newDc.BancosMovimientos
                                     where i.Fecha.Value.Month < Mes && i.Fecha.Value.Year == Año && i.IdBanco == Banco.IdBanco
                                     select i.Debe).Sum();
                    var TotalHaber = (from i in newDc.BancosMovimientos
                                      where i.Fecha.Value.Month < Mes && i.Fecha.Value.Year == Año && i.IdBanco == Banco.IdBanco
                                      select i.Haber).Sum();
                    if (!TotalDebe.HasValue)
                    {
                        TotalDebe = 0;
                    }
                    if (!TotalHaber.HasValue)
                    {
                        TotalHaber = 0;
                    }
                    return Banco.SaldoInicial.Value + TotalHaber.Value - TotalDebe.Value;
                }
                else
                {
                    var TotalDebe = (from i in newDc.BancosMovimientos
                                     where i.Fecha.Value.Year < Año && i.IdBanco == Banco.IdBanco
                                     select i.Debe).Sum();
                    var TotalHaber = (from i in newDc.BancosMovimientos
                                      where i.Fecha.Value.Year < Año && i.IdBanco == Banco.IdBanco
                                      select i.Haber).Sum();
                    if (!TotalDebe.HasValue)
                    {
                        TotalDebe = 0;
                    }
                    if (!TotalHaber.HasValue)
                    {
                        TotalHaber = 0;
                    }
                    return Banco.SaldoInicial.Value + TotalHaber.Value - TotalDebe.Value;
                }

            }
        }
        public static Double SaldoFinal(Bancos Banco)
        {
            using (var newDc = new SoroEntities())
            {
                var TotalDebe = (from i in newDc.BancosMovimientos
                                 where i.IdBanco == Banco.IdBanco
                                 select i.Debe).Sum();
                var TotalHaber = (from i in newDc.BancosMovimientos
                                  where i.IdBanco == Banco.IdBanco
                                  select i.Haber).Sum();
                if (!TotalDebe.HasValue)
                {
                    TotalDebe = 0;
                }
                if (!TotalHaber.HasValue)
                {
                    TotalHaber = 0;
                }
                return Banco.SaldoInicial.Value+ TotalHaber.Value - TotalDebe.Value;
            }
        }
        public static Double SaldoFinal()
        {
            using (var newDc = new SoroEntities())
            {
                var SaldoInicial = (from i in newDc.Bancos
                                 select i.SaldoInicial).Sum();
                var TotalDebe = (from i in newDc.BancosMovimientos
                                 select i.Debe).Sum();
                var TotalHaber = (from i in newDc.BancosMovimientos
                                  select i.Haber).Sum();
                if (!TotalDebe.HasValue)
                {
                    TotalDebe = 0;
                }
                if (!TotalHaber.HasValue)
                {
                    TotalHaber = 0;                
                }
                if (!TotalHaber.HasValue)
                {
                    TotalHaber = 0;
                }
                if (!SaldoInicial.HasValue)
                {
                    SaldoInicial = 0;
                }
                return SaldoInicial.Value + TotalHaber.Value - TotalDebe.Value;
            }
        }

        public static bool GuardarMovimiento(SoroEntities dc, BancosMovimientos Registro)
        {
            if (string.IsNullOrEmpty(Registro.IdMovimiento))
            {
                Registro.IdSesion = FactorySesiones.SesionActiva.IdSesion;
                Registro.Momento = DateTime.Now;
                Registro.IdMovimiento = FactoryContadores.GetLast("IdMovimiento");
                dc.BancosMovimientos.Add(Registro);
            }
            try
            {

                dc.SaveChanges();
                Bancos Banco = Item(dc, Registro.IdBanco);
                Banco.SaldoActual = SaldoFinal(Banco);
                dc.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public static List<BancosMovimientos> BuscarMovimientos(SoroEntities dc, Bancos Banco, int Año, int Mes )
        {
            //if (Mes > 1)
            {
                var Consulta = from p in dc.BancosMovimientos
                               where (p.IdBanco == Banco.IdBanco) && (p.Fecha.Value.Month == Mes) && (p.Fecha.Value.Year == Año)
                               orderby p.Fecha
                               select p;
                return Consulta.ToList();
            }

        }
        public static bool EliminarMovimiento(SoroEntities dc,  BancosMovimientos Movimiento)
        {
            using (var newdc = new SoroEntities())
            {
                Bancos Banco = Item(dc, Movimiento.IdBanco);
                
                dc.BancosMovimientos.Remove(Movimiento);
                dc.SaveChanges();
                Banco.SaldoActual = SaldoFinal(Banco);
                dc.SaveChanges();
                return true;
            }
        }
    }
}
