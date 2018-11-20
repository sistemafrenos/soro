 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HK
{
    class FactoryCaja
    {
        public static Double SaldoFinal()
        {
            using (var newDc = new SoroEntities())
            {
                var TotalDebe = (from i in newDc.Caja
                                 select i.Debe).Sum();
                var TotalHaber = (from i in newDc.Caja
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

    }
}
