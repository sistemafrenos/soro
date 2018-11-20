using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;

namespace HK
{
    class FactoryContadores
    {
        public static string GetLast(string Variable)
        {
            try
            {
                using (var oEntidades = new SoroEntities())
                {
                     Contadores Contador = oEntidades.Contadores.FirstOrDefault(x => x.Variable == Variable);
                    if (Contador == null)
                    {
                        Contador = new Contadores();
                        Contador.Variable = Variable;
                        Contador.Valor = 1;
                        oEntidades.Contadores.Add(Contador);                        
                    }
                    else
                    {
                        Contador.Valor++;
                    }
                    oEntidades.SaveChanges();
                    return Contador.Valor.ToString().PadLeft(6, '0');    

                }                
            }
            catch (Exception ex)
            {
                string x = ex.Message;
            }
            return "";
        }

    }
}
