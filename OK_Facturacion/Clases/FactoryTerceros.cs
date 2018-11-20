using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;

namespace HK
{
    partial class Terceros
    {
        public double? MontoPendienteActual
        {
            get { 
                using( var dc = new SoroEntities())
                {
                    var q = (from p in dc.Cuentas
                            where IdTercero == p.IdTercero
                            && p.Saldo>0
                            select p).Sum(x=>x.Saldo);
                    if (q.HasValue)
                        return q;
                    else
                        return 0;
                }
            }
            set { }
        }
    }
    public class TercerosMovimientos
    {
        string tipo;

        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        DateTime? vence;

        public DateTime? Vence
        {
            get { return vence; }
            set { vence = value; }
        }
        string idTercero;

        public string IdTercero
        {
            get { return idTercero; }
            set { idTercero = value; }
        }
        string detalles;

        public string Detalles
        {
            get { return detalles; }
            set { detalles = value; }
        }
        DateTime? fecha;
        
        public DateTime? Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
        string numero;

        public string Numero
        {
            get { return numero; }
            set { numero = value; }
        }
        string concepto;

        public string Concepto
        {
            get { return concepto; }
            set { concepto = value; }
        }
        double? credito = 0;

        public double? Credito
        {
            get { return credito; }
            set { credito = value; }
        }
        double? debito = 0;

        public double? Debito
        {
            get { return debito; }
            set { debito = value; }
        }
        double? saldo = 0;

        public double? Saldo
        {
            get { return saldo; }
            set { saldo = value; }
        }
    }
    class FactoryTerceros
    {
        public static List<Terceros> Buscar(string Texto, string Tipo)
        {
            using (var dc = new SoroEntities())
            {
                var Consulta = from p in dc.Terceros
                               where (p.CedulaRif.Contains( Texto ) || p.RazonSocial.Contains(Texto)) && (p.Tipo == Tipo) && (p.Activo == true)
                               orderby p.RazonSocial
                               select p;
                return Consulta.ToList();
            }
        }
        public static List<Terceros> Buscar(string Texto)
        {
            using (var dc = new SoroEntities())
            {
                var Consulta = from p in dc.Terceros
                               where (p.CedulaRif.Contains( Texto) || p.RazonSocial.Contains(Texto)) && (p.Activo == true)
                               orderby p.RazonSocial
                               select p;
                return Consulta.ToList();
            }
        }
        public static List<Terceros> Buscar(SoroEntities dc, string Texto, string Tipo)
        {
                var Consulta = from p in dc.Terceros
                               where (p.CedulaRif.Contains( Texto )|| p.RazonSocial.Contains(Texto)) && (p.Tipo == Tipo) && (p.Activo == true)
                               orderby p.RazonSocial
                               select p;
                return Consulta.ToList();
        }
        public static List<Terceros> Buscar(SoroEntities dc, string Texto)
        {

                var Consulta = from p in dc.Terceros
                               where (p.CedulaRif.Contains( Texto ) || p.RazonSocial.Contains(Texto)) && (p.Activo == true)
                               orderby p.RazonSocial
                               select p;
                return Consulta.ToList();
        }
        public static int Count(String _Filtro)
        {
            using (var dc = new SoroEntities())
            {
                if (string.IsNullOrEmpty(_Filtro))
                {
                    return dc.Terceros.Count();
                }
                else
                {
                    return dc.Terceros.Count(x => x.Tipo == _Filtro);
                }
            }
        }
        public static List<String> Condiciones()
        {
            using (var dc = new SoroEntities())
            {
                var q = (from p in dc.Terceros
                         orderby p.Condiciones
                         select p.Condiciones).Distinct();
                return q.ToList();
            }
        }
        public static bool VerificarDuplicidad(string _idtercero,string _cedula, string _razonsocial)
        {
            return false;
            //using (var dc = new SoroEntities())
            //{
            //    {
            //        Terceros Tercero = dc.Terceros.FirstOrDefault(x => x.CedulaRif == _cedula);
            //        if (Tercero != null)
            //        {
            //            if (Tercero.IdTercero != _idtercero)
            //                return true;
            //        }

            //        return false;
            //    }
            //}

        }
        public static bool VerificarIntegridad(string Id)
        {
            using (var dc = new SoroEntities())
            {
                Documentos Doc = dc.Documentos.FirstOrDefault(x => x.IdTercero == Id);
                return (Doc != null);
            }
        }
        public static bool Delete( string Id )
        {
            using (var dc = new SoroEntities())
            {
                Terceros Item = dc.Terceros.FirstOrDefault(x => x.IdTercero == Id);
                if (Item == null)
                    return false;
                if (!VerificarIntegridad(Id))
                {
                    try
                    {
                        dc.Terceros.Remove(Item);
                        dc.SaveChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    try
                    {
                        Item.Activo = false;
                        dc.SaveChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
        public static Terceros Item(string IdTercero)      
        {
            using (var dc = new SoroEntities())
            {
                return dc.Terceros.FirstOrDefault(x => x.IdTercero == IdTercero);
            }

        }        
        public static Terceros ItemxCedula(string Cedula)
        {
            using (var dc = new SoroEntities())
            {
                return dc.Terceros.FirstOrDefault(x => x.CedulaRif == Cedula);
            }
        }
        public static Terceros ItemxNombre(string Nombre)
        {
            using (var dc = new SoroEntities())
            {
                return dc.Terceros.FirstOrDefault(x => x.RazonSocial == Nombre);
            }
        }
        public static bool Guardar(Terceros Tercero)
        {
           using (var dc = new SoroEntities())
            {
                Tercero.CedulaRif = cBasicas.CedulaRif(Tercero.CedulaRif);
                if (VerificarDuplicidad(Tercero.IdTercero, Tercero.CedulaRif, Tercero.RazonSocial))
                {
                    throw new Exception("Error Datos del cliente no se pueden registrar, ese nombre ya esta registrado con otra cedula");
                }
                if (!cBasicas.IsValidCIRIF(Tercero.CedulaRif))
                {
                    throw new Exception("Error en Cedula o Rif del Titular debe comenzar en V/E/J/G \n y no debe llevar guiones ni puntos");
                }
                if ((Tercero.CedulaRif[0] == 'J' || Tercero.CedulaRif[0] == 'G') && cBasicas.CedulaRif(Tercero.CedulaRif).Length != 10)
                {
                    throw new Exception("Numero de Rif Invalido");
                }
                if (string.IsNullOrEmpty(Tercero.RazonSocial))
                {
                    throw new Exception("Nombre o Razon Social no pueden estar en blanco");
                }
                if ((Tercero.CedulaRif.StartsWith("V") || Tercero.CedulaRif.StartsWith("E") ) && string.IsNullOrEmpty(Tercero.TipoContribuyente))
                {
                    Tercero.TipoContribuyente = "NO CONTRIBUYENTE";
                }
                else
                {
                    Tercero.TipoContribuyente = "CONTRIBUYENTE";
                }
                if (String.IsNullOrEmpty(Tercero.IdTercero))
                {
                    Tercero.IdTercero = FactoryContadores.GetLast("IdTercero");
                    Tercero.Activo = true;
                    dc.Terceros.Add(Tercero);
                }
                else
                {
                    dc.Terceros.Attach(Tercero);
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

        public static List<Terceros> ItemsxTipo(string Tipo)
        {
            using (var dc = new SoroEntities())
            {
                var q = (from p in dc.Terceros
                         where p.Tipo == Tipo
                         orderby p.RazonSocial
                         select p).ToList();
                return q;
            }
        }
    }
}
