using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace HK
{
    class FactoryUsuariosDerechos
    {
        private static List<UsuariosDerechos> DerechosActivos = null;
        public static UsuariosDerechos CrearDerecho(Usuarios Usuario, string Opcion, string SubOpcion, bool Habilitado)
        {
            using (var oEntidades = new SoroEntities())
            {
                UsuariosDerechos Derecho = new UsuariosDerechos();
                Derecho.Usuarios = Usuario;
                Derecho.Opcion = Opcion;
                Derecho.SubOpcion = SubOpcion;
                Derecho.Habilitado = Habilitado;
                try
                {
                    oEntidades.UsuariosDerechos.Add(Derecho);
                    oEntidades.SaveChanges();
                    return Derecho;
                }
                catch
                {
                    return null;
                }

            }
        }
        public static bool Delete(int Id)
        {
            using (var oEntidades = new SoroEntities())
            {
                UsuariosDerechos Item = oEntidades.UsuariosDerechos.FirstOrDefault(x => x.IDUsuarioDerecho == Id);
                if (Item == null)
                    return false;
                try
                {
                    oEntidades.UsuariosDerechos.Remove(Item);
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
        public static void LimpiarDerechos()
        {
            DerechosActivos = null;
        }
        public static List<UsuariosDerechos> UsuarioDerechos(Usuarios Usuario)
        {
            if (DerechosActivos != null)
            {
                return DerechosActivos;
            }
            using (var oEntidades = new SoroEntities())
            {
                var Q = from usuarioderecho in oEntidades.UsuariosDerechos
                        where usuarioderecho.IdUsuario == Usuario.IdUsuario
                        select usuarioderecho;                
                return Q.ToList();
            }
        }
    }
}

