using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HK
{
    class FactoryLibroCompras
    {
        public static List<LibroCompras> LibroCompras(SoroEntities dc, int mes, int año)
        {
            var consulta = (from p in dc.LibroCompras
                           where p.Mes == mes && p.Año == año
                           orderby p.Fecha
                           select p);
            return consulta.ToList();
        }
        public static LibroCompras Item(SoroEntities dc,string Id)
        {
            LibroCompras _item = (from q in dc.LibroCompras
                                  where q.IdLibroCompras == Id
                                  select q).FirstOrDefault();
            return _item;
        }
    }
}
