using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HK
{
    class FactoryRecibos
    {
        public static int Count()
        {
            using (var oEntidades = new SoroEntities())
            {
                return oEntidades.Recibos.Count();
            }
        }
        public static bool Delete(string Id)
        {
            using (var oEntidades = new SoroEntities())
            {
                Recibos Item = oEntidades.Recibos.FirstOrDefault(x => x.IdRecibo == Id);
                if (Item == null)
                    return false;
                try
                {
                    try
                    {
                        oEntidades.SaveChanges();
                    }
                    catch (Exception x)
                    {
                        string s = x.Message;
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                    return false;
                }
            }
        }
        public static List<RecibosDetalles> ReciboDetalles(SoroEntities dc, string IdRecibo)
        {
            var Consulta = from p in dc.RecibosDetalles
                           where (p.IdRecibo == IdRecibo)
                           select p;
            return Consulta.ToList();
        }
        public static IEnumerable<RecibosPagos> ReciboPagos(SoroEntities dc, string IdRecibo, string _filtro)
        {
            var Consulta = from p in dc.RecibosPagos
                           where (p.IdRecibo == IdRecibo) 
                           select p;
            return Consulta;
        }
        public static List<VistaRecibo> VistaRecibos(SoroEntities dc, string IdRecibo)
        {
            var Consulta = from p in dc.VistaRecibo
                           where (p.IdRecibo == IdRecibo)
                           select p;
            return Consulta.ToList();
        }
        public static List<VistaRecibo> SelectAllVista(SoroEntities dc, string Texto, string _filtro, string Status)
        {
            var Q = dc.VistaRecibo.Where(x =>
                (x.CedulaRif == Texto ||
                x.RazonSocial.Contains(Texto) ||
                x.Numero == Texto) && (x.Tipo == _filtro) 
                );
            return Q.ToList();
        }
        public static List<VistaRecibo> SelectAllVista(SoroEntities dc, string Texto, string _filtro, bool Activo)
        {
            var Q = dc.VistaRecibo.Where(x =>
                (x.CedulaRif == Texto ||
                x.RazonSocial.Contains(Texto) ||
                x.Numero == Texto) && x.Tipo == _filtro
                );

            return Q.ToList();
        }
        public static IEnumerable<VistaRecibo> SelectAll(SoroEntities dc, string Texto, string _filtro, bool Activo)
        {
            var Consulta = from p in dc.VistaRecibo
                           where (p.Tipo == _filtro) && (p.Activo == true) &&
                           (p.RazonSocial.Contains(Texto) ||
                             p.CedulaRif == Texto)
                           select p;
            return Consulta;
        }
        public static Recibos ItemxId(SoroEntities dc, string IdRecibo)
        {
            return dc.Recibos.FirstOrDefault(x => x.IdRecibo == IdRecibo);
        }
        public static bool Save(SoroEntities dc, Recibos Doc, Terceros Tercero, IEnumerable<RecibosDetalles> Detalles)
        {

            //if (string.IsNullOrEmpty(Doc.IdRecibo))
            //{
            //    Doc.IdRecibo = FactoryContadores.GetLast("IdRecibo");
            //    dc.Recibos.Add(Doc);
            //}
            try
            {
                Doc.IdSesion = FactorySesiones.SesionActiva.IdSesion;
                Doc.Momento = DateTime.Now;
                dc.SaveChanges();
            }
            catch (Exception x)
            {
                string s = x.Message;
            }
            foreach (RecibosDetalles Detalle in Detalles)
            {
                if (string.IsNullOrEmpty(Detalle.IdRecibo))
                {
                    Detalle.IdRecibo = Doc.IdRecibo;
                    dc.RecibosDetalles.Add(Detalle);
                }
                // Detalle.IdSesion = FactorySesiones.SesionActiva.IdSesion;
                // Detalle.Momento = DateTime.Now;
            }
            try
            {
                dc.SaveChanges();
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return false;
            }
            return true;
        }
        public static IQueryable BusquedaRecibos( SoroEntities dc, string Texto )
        {
            var Q = from r in dc.Recibos
                    join t in dc.Terceros on r.IdTercero equals t.IdTercero
                    where t.RazonSocial.Contains(Texto) 
                    select new VistaRecibo
                    {
                         IdRecibo=   r.IdRecibo,
                         Monto=   r.Monto,
                         Tipo=   r.TipoRecibo,
                         NumeroRecibo=   r.NumeroRecibo,
                         Concepto =   r.Concepto,
                         Fecha=   r.Fecha,
                         RazonSocial=   t.RazonSocial,
                         CedulaRif =   t.CedulaRif
                        };
            return Q;
        }
    }
}
