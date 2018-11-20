using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HK;

namespace HK.Clases
{
    class FactoryContadores
    {
        public static string GetMax(string Variable, bool actualizar = true)
        {
            try
            {
                using (var oEntidades = new Data())
                {
                    Contadore Contador = oEntidades.Contadores.FirstOrDefault(x => x.Variable == Variable);
                    if (Contador == null)
                    {
                        Contador = new Contadore();
                        Contador.Variable = Variable;
                        Contador.Valor = 1;
                        oEntidades.Contadores.AddObject(Contador);
                    }
                    else
                    {
                        Contador.Valor++;
                    }
                    if (actualizar)
                    {
                        oEntidades.SaveChanges();
                    }
                    return ((int)Contador.Valor).ToString("000000");
                }
            }
            catch (Exception ex)
            {
                string x = ex.Message;
            }
            return "";
        }
        public static string GetMaxII(string Variable)
        {
            try
            {
                using (var oEntidades = new Data())
                {
                    Contadore Contador = oEntidades.Contadores.FirstOrDefault(x => x.Variable == Variable);
                    if (Contador == null)
                    {
                        Contador = new Contadore();
                        Contador.Variable = Variable;
                        Contador.Valor = 1;
                        oEntidades.Contadores.AddObject(Contador);
                        oEntidades.SaveChanges();
                    }
                    return ((int)Contador.Valor).ToString("000000");
                }
            }
            catch (Exception ex)
            {
                string x = ex.Message;
            }
            return "";
        }
        public static void SetMaxII(string Variable,int Valor)
        {
            try
            {
                using (var oEntidades = new Data())
                {
                    Contadore Contador = oEntidades.Contadores.FirstOrDefault(x => x.Variable == Variable);
                    if (Contador == null)
                    {
                        Contador = new Contadore();
                        Contador.Variable = Variable;
                    }
                    else
                    {
                        Contador.Valor++;

                    }
                    Contador.Valor = Valor;
                    oEntidades.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                string x = ex.Message;
            }
        }
        public static string GetMaxComprobante(string mes,string año)
        {
            return año + mes + "00" + GetMax("COMPROBANTE_IVA", false);
        }
        public static string GetMaxComprobanteISLR(string mes,string año)
        {
            return año + mes + "00" + GetMax("COMPROBANTE_ISLR", false);
        }
        public static void SetContador(string variable, int valor)
        { 
            using (var oEntidades = new Data())
            {
                Contadore Contador = oEntidades.Contadores.FirstOrDefault(x => x.Variable == variable);
                if (Contador == null)
                {
                    Contador = new Contadore();
                    Contador.Variable = variable;
                    Contador.Valor = 1;
                    oEntidades.Contadores.AddObject(Contador);
                }
                else
                {
                    Contador.Valor = valor;

                }
                oEntidades.SaveChanges();
            }
            
        }
        public static void SetMax(string variable)
        {
            try
            {
                using (var oEntidades = new Data())
                {
                    Contadore Contador = oEntidades.Contadores.FirstOrDefault(x => x.Variable == variable);
                    if (Contador == null)
                    {
                        Contador = new Contadore();
                        Contador.Variable = variable;
                        Contador.Valor = 1;
                        oEntidades.Contadores.AddObject(Contador);
                    }
                    else
                    {
                        Contador.Valor++;

                    }
                    oEntidades.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                string x = ex.Message;
            }
        }
        }

    }
