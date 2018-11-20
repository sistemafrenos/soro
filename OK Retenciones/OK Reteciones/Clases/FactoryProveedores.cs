using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HK;

namespace HK.Clases
{
    class FactoryProveedores
    {
        public static List<Proveedore> getItems(string texto)
        {
            using (var db = new Data())
            {
                var q = from p in db.Proveedores
                        orderby p.RazonSocial
                        where (p.CedulaRif.Contains(texto) || p.RazonSocial.Contains(texto) || texto.Length == 0) && p.Activo == true
                        select p;
                return q.ToList();
            }
        }
        public static List<Proveedore> getItems(Data db, string texto)
        {
            var q = from p in db.Proveedores
                    orderby p.RazonSocial
                    where (p.CedulaRif.Contains(texto) || p.RazonSocial.Contains(texto) || texto.Length == 0) && p.Activo==true
                    select p;
            return q.ToList();
        }
        public static Proveedore Item(string id)
        {
            using (var db = new Data())
            {
                var q = from p in db.Proveedores
                        orderby p.RazonSocial
                        where p.CedulaRif == id
                        select p;
                return q.FirstOrDefault();
            }
        }
        public static Proveedore Item(Data db, string id)
        {
            var q = from p in db.Proveedores
                    orderby p.RazonSocial
                    where p.IdProveedor == id
                    select p;
            return q.FirstOrDefault();
        }
        public static Proveedore ItemCedulaRif(Data db, string id)
        {
            var q = from p in db.Proveedores
                    orderby p.RazonSocial
                    where p.CedulaRif == id &&  p.Activo==true
                    select p;
            return q.FirstOrDefault();
        }
        public static void Validar(Proveedore registro)
        {
            if (string.IsNullOrEmpty(registro.RazonSocial))
                throw new Exception("Error el Nombre no puede estar vacio");
            if (registro.RazonSocial.Length > 100)
                throw new Exception("Error RazonSocial no puede tener mas de 100 caracteres");
            if (string.IsNullOrEmpty(registro.CedulaRif))
                throw new Exception("Error CedulaRif no puede estar vacia");
            if (registro.CedulaRif.Length > 20)
                throw new Exception("Error CedulaRif no puede tener mas de 20 caracteres");
        }
    }
}
