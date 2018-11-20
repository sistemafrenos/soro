using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Configuration;
using System.Windows.Forms;

namespace HK
{
    partial class Parametros
    {
        string impresoraFacturas = "";

        public string ImpresoraFacturas
        {
            get 
            {
                object x = null;
                try
                {
                    x = (string)Application.CommonAppDataRegistry.GetValue("impresoraFacturas");
                }
                catch
                {
                     
                }
                if (x != null)
                {
                    ImpresoraFacturas = (string)x;
                }
                if (string.IsNullOrEmpty(impresoraFacturas))
                {
                    try 
                    {
                        ImpresoraFacturas = System.Drawing.Printing.PrinterSettings.InstalledPrinters[0];
                    }
                    catch {}
                }
                return impresoraFacturas; 
            }
            set 
            { 
                impresoraFacturas = value;
                Application.CommonAppDataRegistry.SetValue("impresoraFacturas", value);
            }
        }
    }
    class FactoryParametros
    {
        static Parametros _parametros = null;
        public static List<Parametros> SelectAll()
        {
            using (var oEntidades = new SoroEntities())
            {
                return oEntidades.Parametros.ToList();
            }
        }
        public static Parametros Item(SoroEntities dc)
        {
            _parametros = dc.Parametros.FirstOrDefault();
            return _parametros;
        }
        public static Parametros Item()
        {
            if (_parametros == null)
            {
                using (var dc = new SoroEntities())
                {
                    _parametros= dc.Parametros.FirstOrDefault();
                }
            }
            return _parametros;
        } 
    }
}
