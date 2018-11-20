using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HK
{
    class FactoryGrupos
    {
        public static Grupos GetItemxGrupo(string Texto)
        {
            using (var dc = new SoroEntities())
            {
                var Q = (from Grupo in dc.Grupos
                         where Grupo.Grupo == Texto
                         select Grupo).FirstOrDefault();
                return Q;
            }
        }
        public static Grupos Item( string Texto)
        {
            using (var dc = new SoroEntities())
            {
                var Q = (from Grupo in dc.Grupos
                         where Grupo.IdGrupo == Texto
                             select Grupo).FirstOrDefault();
                return Q;
            }
        }
        public static List<Grupos> Buscar(SoroEntities dc, string Texto)
        {
            var Q = from Grupo in dc.Grupos
                    where (Grupo.Grupo.Contains(Texto) || Grupo.Codigo.Contains(Texto) )  && Grupo.Activo == true
                    select Grupo;
            return Q.ToList();
        }
        public static bool VerificarIntegridad(string Id)
        {
            using (var oEntidades = new SoroEntities())
            {
                Productos Doc = oEntidades.Productos.FirstOrDefault(x => x.IdGrupo == Id);
                return (Doc != null);
            }
        }
        public static bool Delete(string Id)
        {
            using (var oEntidades = new SoroEntities())
            {
                Grupos Item = oEntidades.Grupos.FirstOrDefault(x => x.IdGrupo == Id);
                if (Item == null)
                    return false;
                if (!VerificarIntegridad(Id))
                {
                    try
                    {
                        oEntidades.Grupos.Remove(Item);
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
        public static bool Guardar(Grupos Grupo)
        {
            using (var dc = new SoroEntities())
            {
                if (string.IsNullOrEmpty(Grupo.Codigo))
                {
                    throw new Exception("Es Obligatorio el codigo");
                }
                if (string.IsNullOrEmpty(Grupo.Grupo))
                {
                    throw new Exception("Es Obligatoria la descripcion");
                }
                if (string.IsNullOrEmpty(Grupo.IdGrupo))
                {
                    Grupo.IdGrupo = FactoryContadores.GetLast("IdGrupo");
                    Grupo.Activo = true;
                    dc.Grupos.Add(Grupo);
                }
                else
                {
                    dc.Grupos.Attach(Grupo);
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


    }
}
