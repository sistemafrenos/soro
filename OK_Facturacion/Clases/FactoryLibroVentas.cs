using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HK
{
    class FactoryLibroVentas
    {
        public static List<LibroVentas> LibroVentas(SoroEntities dc, int mes, int año)
        {
            var consulta = from p in dc.LibroVentas
                           where p.Mes == mes && p.Año == año
                           orderby p.Fecha,p.Factura
                           select p;
            return consulta.ToList();
        }
        public static LibroVentas Item(SoroEntities dc, string Id)
        {
            LibroVentas _item = (from q in dc.LibroVentas
                                 where q.IdLibroVentas == Id
                                  select q).FirstOrDefault();
            return _item;
        }
    }
}