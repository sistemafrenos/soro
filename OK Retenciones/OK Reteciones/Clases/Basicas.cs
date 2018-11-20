using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Text.RegularExpressions;
using System.Data.Common;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using HK.Formas;



namespace HK
{
    #region Pantallas
    static class Pantallas
    {
        internal static FrmRetenciones RetencionesLista = null;
        internal static FrmRetencionesISLR RetencionesISLRLista = null;
        internal static FrmProveedores ProveedoresLista = null;
    }
    #endregion
    static class Basicas
    {
        public static Parametros parametros()
        {
            using (var db = new Data())
            {
                return db.Parametros.FirstOrDefault();
            }
        }
        public static DateTime FirstDayOfMonthFromDateTime(DateTime dateTime)
        {
           return new DateTime(dateTime.Year, dateTime.Month, 1);
        }
        public static DateTime LastDayOfMonthFromDateTime(DateTime dateTime)
        {
           DateTime firstDayOfTheMonth = new DateTime(dateTime.Year, dateTime.Month, 1);
           return firstDayOfTheMonth.AddMonths(1).AddDays(-1);
        }
        public static bool ImpresoraOcupada = false;
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern long BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);
       
        public static double Round(double? valor)
        {
            try
            {
                decimal myValor = (decimal)valor;
                myValor = decimal.Round(myValor, 2);
                return (double)myValor;
            }
            catch
            {
                return 0;
            }
        }
        public static string CedulaRif(string Texto)
        {
            if (string.IsNullOrEmpty(Texto))
            {
                return Texto;
            }
            Texto = Texto.ToUpper();
            Texto = Texto.Replace(":", "");
            Texto = Texto.Replace("-", "").Replace(".", "").Replace(" ", "").Replace(",", "").Replace("CI", "").Replace("RIF", "").Replace("C", "");
            if (Texto.Length > 0)
            {
                if (IsNumeric(Texto[0]))
                {
                    Texto = "V" + Texto;
                }
            }
            if (Texto[0] == 'J' || Texto[0] == 'G')
            {
                if (Texto.Length > 10)
                {
                    Texto.Substring(0, 10);
                }
            }
            return Texto;
        }
        public static bool IsNumeric(object Expression)
        {
            bool isNum;
            double retNum;

            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }
        public static void textboxNumericFormat(object sender, ConvertEventArgs e)
        {
            // We only deal with converting to strings from double
            if (e.DesiredType != typeof(string)) return;
            if (e.Value.GetType() != typeof(double)) return;
            Double dt = (Double)e.Value;
            e.Value = dt.ToString("N2");
        }
        #region funciones regular expression
        public static bool IsValidEmail(string strIn)
        {
            // Return true if strIn is in valid e-mail format. 
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
        public static bool IsValidRIF(string strIn)
        {
            // Return true if strIn is in valid RIF format. 
            return Regex.IsMatch(strIn, @"^[JVI][-]\d{9}[-]\d$?");
        }
        public static bool IsValidCI(string strIn)
        {
            string NumberDecimalSeparator = System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
            // Return true if strIn is in valid CI format. 
            return Regex.IsMatch(strIn, @"^[VE][-]\d{1,3}\" + NumberDecimalSeparator + @"?\d{3}" + NumberDecimalSeparator + @"?\d{3}");
        }
        public static bool IsValidCIRIF(string strIn)
        {
            // Return true if strIn is in valid RIF format. 
            if (string.IsNullOrEmpty(strIn))
                return false;
            strIn = strIn.ToUpper();
            return Regex.IsMatch(strIn, @"[JVEG]([0-9])");
        }
        #endregion
        public static string PrintFix(string Texto, int Largo, int Alineacion)
        {
            string x = "                                        ";
            Texto = Texto.Trim();
            if (Texto.Length > Largo)
            {
                return Texto.Substring(0, Texto.Length);
            }
            switch (Alineacion)
            {
                case 1:
                    return Texto.PadRight(Largo);

                case 2:
                    return Texto.PadLeft(Largo);

                case 3:
                    int Suplemento = (Largo - Texto.Length) / 2;
                    return x.Substring(0, Suplemento) + Texto + x.Substring(0, Suplemento);

            }
            return Texto;
        }
        public static string PrintNumero(double? d, int len)
        {
            if (!d.HasValue)
            {
                d = 0;
            }
            return d.Value.ToString("n2").PadLeft(len);
        }
    }    
}
