using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace HK
{
    class FactorySesiones
    {
        public static Sesiones SesionActiva = new Sesiones();
        public static Sesiones Item(string Id)
        {
            using (var oEntidades = new SoroEntities())
            {
                return oEntidades.Sesiones.FirstOrDefault(x => x.IdSesion == Id);
            }
        }        
        public static List<Sesiones> SesionesAbiertas()
        {
            using (var oEntidades = new SoroEntities())
            {
                var Q = from sesiones in oEntidades.Sesiones
                        where !sesiones.Cierre.HasValue
                        select sesiones;
                return Q.ToList();
            }
        }
        public static Sesiones AbrirSesion(Usuarios Usuario)
        {

            SesionActiva = new Sesiones();
            SesionActiva.Apertura = DateTime.Now;
            SesionActiva.Fecha = DateTime.Today;
            SesionActiva.IdUsuario = Usuario.IdUsuario;
            using (var oEntidades = new SoroEntities())
            {
                SesionActiva.IdSesion = FactoryContadores.GetLast("idSesion");
                oEntidades.Sesiones.Add(SesionActiva);
                oEntidades.SaveChanges();
                return SesionActiva;
            }            
        }
        public static void ContinuarSesion(Sesiones mSesion)
        {
            SesionActiva = Item(mSesion.IdSesion);
        }
        public static void CerrarSesion()
        {            
            if (SesionActiva != null)
            {                
                using (var oEntidades = new SoroEntities())
                {
                    Sesiones Sesion = oEntidades.Sesiones.First(x => x.IdSesion == SesionActiva.IdSesion);                    
                    Sesion.Cierre = DateTime.Now;                    
                    oEntidades.SaveChanges();
                }
            }
        }
    }
}
