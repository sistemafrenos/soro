using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;


namespace HK
{
    class FactoryUsuarios
    {
        public static Usuarios UsuarioActivo = new Usuarios();
        public static Usuarios CrearUsuario(string Nombre, string Contraseña, string Tipo)
        {
            using (var oEntidades = new SoroEntities())
            {
                Usuarios Usuario = new Usuarios();
                Usuario.Nombre = Nombre;
                Usuario.Tipo = Tipo;
                Usuario.Clave= Contraseña;
                Usuario.Activo = true;
                try
                {
                    oEntidades.Usuarios.Add(Usuario);
                    oEntidades.SaveChanges();
                    return Usuario;
                }
                catch
                {
                    return null;
                }

            }
        }
        public static Usuarios Buscar(string Texto)
        {
            using (var dc = new SoroEntities())
            {
                var Q = from usuario in dc.Usuarios
                        where usuario.Nombre.Contains(Texto)
                        select usuario;
                    return Q.FirstOrDefault();
                }

            }      
        public static int Count(String _Filtro)
        {
              using (var dc = new SoroEntities())
            {
                if (string.IsNullOrEmpty(_Filtro))
                {
                    return dc.Usuarios.Count();
                }
                else
                {
                    return dc.Usuarios.Count(x => x.Tipo == _Filtro);
                }
            }
        }
        public static bool VerificarDuplicidad(string _idusuario, string _nombre)
        {
            using (var oEntidades = new SoroEntities())
            {
                Usuarios Usuario = oEntidades.Usuarios.FirstOrDefault(x => x.Nombre == _nombre );
                if (Usuario != null)
                {
                    if (Usuario.IdUsuario != _idusuario)
                        return true;
                }

                return false;
            }
        }
        public static bool VerificarIntegridad(string Id)
        {
            using (var oEntidades = new SoroEntities())
            {
                Sesiones Doc = oEntidades.Sesiones.FirstOrDefault(x => x.IdUsuario == Id);
                return (Doc != null);
            }
        }
        public static bool Delete(string Id)
        {
            using (var oEntidades = new SoroEntities())
            {
                Usuarios Item = oEntidades.Usuarios.FirstOrDefault(x => x.IdUsuario == Id);
                if (Item == null)
                    return false;
                if (!VerificarIntegridad(Id))
                {
                    try
                    {
                        oEntidades.Usuarios.Remove(Item);
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
        public static List<Usuarios> SelectAll(SoroEntities dc, string Texto, string _filtro, bool Activo)
        {
            var Q = from usuario in dc.Usuarios
                    where usuario.Nombre.Contains(Texto) && usuario.Activo == true
                    select usuario;
                return Q.ToList();
        }
        public static Usuarios ItemxId(SoroEntities dc, string IdUsuario)
        {
                return dc.Usuarios.FirstOrDefault(x => x.IdUsuario == IdUsuario);
        }
        public static Usuarios ItemxNombre(SoroEntities dc, string Nombre)
        {
            return dc.Usuarios.FirstOrDefault(x => x.Nombre == Nombre);
        }
        public static Usuarios Item(SoroEntities dc, string Nombre, string Contraseña)
        {
            UsuarioActivo = dc.Usuarios.FirstOrDefault(x => x.Nombre == Nombre && x.Clave == Contraseña);
            return UsuarioActivo;
        }        
    }
}

