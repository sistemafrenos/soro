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
using System.Linq;
using System.Data.Linq;
using DevExpress.XtraReports.UI;


namespace HK
{
    static class cBasicas
    {
        public static bool ImpresoraOcupada = false;
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern long BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);
        public static List<string> Bancos()
        {
            using (var db = new SoroEntities())
            {
                var q = from p in db.AuxiliarBancos
                        orderby p.Banco
                        select p.Banco;
                return q.ToList();
            }
        }
        public static string GetConnectionString(string strConnection)
        {
            //Declare a string to hold the connection string
            string sReturn = null;
            //Check to see if they provided a connection string name
            if (!string.IsNullOrEmpty(strConnection))
            {
                //Retrieve the connection string fromt he app.config
                sReturn = ConfigurationManager.ConnectionStrings[0].ConnectionString;
            }
            else
            {
                //Since they didnt provide the name of the connection string
                //just grab the default on from app.config
                sReturn = ConfigurationManager.ConnectionStrings[strConnection].ConnectionString;
            }
            //Return the connection string to the calling method
            return sReturn;
        }
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
        public static bool ProbarConexion(string s)
        {
            System.Data.SqlClient.SqlConnection S = new System.Data.SqlClient.SqlConnection();
            S.ConnectionString = s;
            try
            {
                S.Open();
                S.Close();
                return true;
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Error en la conexion con el servidor\n" + x.Message, "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public static bool ProbarConexion()
        {
            System.Data.SqlClient.SqlConnection S = new System.Data.SqlClient.SqlConnection();
            S.ConnectionString = global::HK.Properties.Settings.Default.SoroConnectionString;
            try
            {
                S.Open();
                S.Close();
                return true;
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Error en la conexion con el servidor\n" + x.Message, "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public static Bitmap CaptureScreen(Form T)
        {
            Bitmap memoryImage;
            Graphics mygraphics = T.CreateGraphics();
            Size s = T.Size;
            memoryImage = new Bitmap(s.Width, s.Height, mygraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            IntPtr dc1 = mygraphics.GetHdc();
            IntPtr dc2 = memoryGraphics.GetHdc();
            BitBlt(dc2, 0, 0, T.ClientRectangle.Width, T.ClientRectangle.Height, dc1, 0, 0, 13369376);
            mygraphics.ReleaseHdc(dc1);
            memoryGraphics.ReleaseHdc(dc2);
            return memoryImage;
        }
        public static bool EsFinDeSemana()
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday || DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
                return true;
            else
                return false;
        }
        public static bool EsNocturno()
        {
            if (DateTime.Now.Hour > 18)
                return true;
            else
                return false;
        }
        public static bool IsNumeric(object Expression)
        {
            bool isNum;
            double retNum;

            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }
        public static List<string> Sexos = new List<string>();
        public static List<string> Parentescos = new List<string>();
        public static void textboxNumericFormat(object sender, ConvertEventArgs e)
        {
            // We only deal with converting to strings from double
            if (e.DesiredType != typeof(string)) return;
            if (e.Value.GetType() != typeof(double)) return;
            Double dt = (Double)e.Value;
            e.Value = dt.ToString("N2");
        }
        /*
        public static void ImprirmeRecibo(int IdRecibo, string IdTitular)
        {
            SoroEntities dc = new SoroEntities();
            PrintDocument pd = new PrintDocument();
            String OldPrinter = pd.PrinterSettings.PrinterName;
            int Lineas = 0;
            SetDefaultPrinter("RECIBOS");
            Terceros Tercero = EntityTerceros.ItemxId(dc,IdTitular);            
            try
            {
                LPrintWriter l = new LPrintWriter();
                l.WriteLine("       RECIBO DE CAJA");
                Lineas++;
                l.WriteLine("       ==============");
                Lineas++;
                l.WriteLine(" ");
                Lineas++;
                l.WriteLine(String.Format("FECHA:{0}     NUMERO:{1}", ds.Recibos[0].Fecha.ToString("d"), ds.Recibos[0].NumeroRecibo));
                Lineas++;
                l.WriteLine(" ");
                Lineas++;                
                l.WriteLine(String.Format("HEMOS RECIBIDO DE :"));
                Lineas++;                
                l.WriteLine(" ");
                Lineas++;                
                l.WriteLine(String.Format("  CEDULA/RIF:{0}", Tercero.CedulaRif));
                Lineas++;
                l.WriteLine(String.Format("RAZON SOCIAL:{0}" ,Tercero.RazonSocial));
                Lineas++;
                l.WriteLine(" ");
                Lineas++;                
                l.WriteLine(String.Format("LA CANTIDAD DE Bs.:{0}",((double)ds.Recibos[0].Total).ToString("N2")));
                Lineas++;
                l.WriteLine(" ");
                Lineas++;                
                l.WriteLine(String.Format("POR CONCEPTO DE :" ));
                Lineas++;
                l.WriteLine(String.Format(ds.Recibos[0].Concepto ));
                Lineas++;
                l.WriteLine(" ");
                Lineas++;
                foreach( Myds.RecibosPagosRow Pago in ds.RecibosPagos)
                {
                    l.WriteLine(Pago.FormaPago);
                    Lineas++;
                    l.WriteLine("      Bs.:"  + Pago.Monto.ToString("N2"));
                    Lineas++;
                    if (!Pago.IsBancoNull())
                    {
                        l.WriteLine(Pago.Banco);
                        Lineas++;
                    }
                    if (!Pago.IsDetallesNull())
                    {
                        l.WriteLine(Pago.Detalles);
                        Lineas++;
                    }
                }
                for (; Lineas < 26; Lineas++)
                    l.WriteLine(".");
                l.WriteLine(String.Format("Realizado Por : {0}...", FactoryUsuarios.UsuarioActivo.Nombre));
                l.WriteLine(DateTime.Now);
                l.WriteLine(".");
                l.WriteLine(".");
                l.WriteLine(".");
                l.WriteLine(".");
                l.WriteLine(".");
                l.WriteLine(".");                
                l.Flush();                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            SetDefaultPrinter(OldPrinter);
        }        
         */

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
        static public bool ProbarImpresoraFiscal()
        {
            FiscalBixolon Fiscal = new FiscalBixolon();
            {
                try
                {
                    Fiscal.ConectarImpresora();
                    Fiscal.LiberarImpresora();
                    return true;
                }
                catch
                {
                    MessageBox.Show("Error de coneccion con la impresora fiscal", "Atencion", MessageBoxButtons.OK);
                    return false;
                }
            }
        }
        static void ImprimirFormaLibre(Documentos Documento)
        {
            //if (Documento == null)
            //    return;
            //FrmReportes f = new FrmReportes();
            //f.ReporteFactura(Documento);
            try
            {
                foreach (string temporal in System.IO.Directory.GetFiles(".", "*.EMF"))
                {
                    System.IO.File.Delete(temporal);
                }
            }
            catch { }
            FacturaPrint f = new FacturaPrint();
            f.Run(Documento);
            f = null;
            return;
        }
        public static void ImprimirPedido(Documentos Documento)
        {
            //if (Documento == null)
            //    return;
            //FrmReportes f = new FrmReportes();
            //f.ReporteFactura(Documento);
            foreach (string temporal in System.IO.Directory.GetFiles(".", "*.EMF"))
            {
                System.IO.File.Delete(temporal);
            }
            using( FacturaPrint f = new FacturaPrint())
            {          
               f.Run(Documento);
            }
            return;
        }
        static void ImprimirFiscal(Documentos Documento)
        {
            FiscalBixolon Fiscal = new FiscalBixolon();
            {
                try
                {
                    try
                    {
                        if (!Fiscal.ImprimeFactura(Documento.IdDocumento))
                            return;
                    }
                    catch (Exception x)
                    {
                        ImpresoraOcupada = false;
                        throw x;
                    }
                    using (var dc = new SoroEntities())
                    {
                        Documentos Doc = FactoryDocumentos.Item(dc, Documento.IdDocumento);
                        Doc.Numero = Fiscal.GetNumeroFactura();
                        dc.SaveChanges();
                    }
                    Fiscal.LiberarImpresora();
                }
                catch (Exception x)
                {
                    throw new Exception(x.Message);
                }
            }

        }
        static public void ImprimirFactura(Documentos Documento)
        {
            Parametros ItemParametros = FactoryParametros.Item();
            try
            {
                if (!ItemParametros.TipoImpresora.Contains("FISCAL"))
                {
                    ImprimirFormaLibre(Documento);                    
                }
                else
                {
                    if (Documento.Numero == null)
                    {
                        ImprimirFiscal(Documento);
                    }
                    else
                    {
                        FrmReportes f = new FrmReportes();
                        f.ReporteFactura(Documento);

                    }
                }
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
            if (Documento.Status != "INVENTARIO")
            {
                CFacturas.CargarInventario(Documento);
                Documento = FactoryDocumentos.Item(new SoroEntities(), Documento.IdDocumento);
                if (Documento.Saldo > 0)
                {
                    CFacturas.EscribirCuentaxCobrar(Documento);
                }
                CFacturas.LibroDeVentas(Documento);
            }
        }
        static public void ImprimirNotaEntrega(Documentos Documento)
        {
            FrmReportes f = new FrmReportes();
            f.ReporteNotaEntrega(Documento);
            if (Documento.Status != "INVENTARIO")
            {
                CFacturas.CargarInventario(Documento);
                Documento = FactoryDocumentos.Item(new SoroEntities(), Documento.IdDocumento);
                if (Documento.Saldo > 0)
                {
                    CFacturas.EscribirCuentaxCobrar(Documento);
                }
            }
        }
        static void ImprimirDevolucionFormaLibre(Documentos Documento)
        {
            using (var dc = new SoroEntities())
            {
                Parametros ItemParametros = FactoryParametros.Item();
                Documentos Doc = FactoryDocumentos.Item(dc, Documento.IdDocumento);
                ItemParametros.CorrelativoDevolucion++;
                Doc.Numero = ItemParametros.CorrelativoDevolucion.ToString().PadLeft(6, '0');
                dc.SaveChanges();
            }
            // Pendiente Imprimir devolucion
            return;
        }
        static void ImprimirDevolucionFiscal(Documentos Documento)
        {
            FiscalBixolon Fiscal = new FiscalBixolon();
            {
                try
                {
                    Fiscal.ConectarImpresora();
                    if (!Fiscal.ImprimeDevolucion(Documento.IdDocumento))
                        throw new Exception("Error en impresion de factura");
                    using (var dc = new SoroEntities())
                    {
                        Parametros ItemParametros = FactoryParametros.Item();
                        Documentos Doc = FactoryDocumentos.Item(dc, Documento.IdDocumento);
                        ItemParametros.CorrelativoDevolucion++;
                        Doc.Numero = ItemParametros.CorrelativoDevolucion.ToString().PadLeft(6, '0');
                        dc.SaveChanges();
                    }
                    Fiscal.LiberarImpresora();
                }
                catch (Exception x)
                {
                    throw new Exception(x.Message);
                }
            }

        }
        static public void ImprimirDevolucion(Documentos Documento)
        {
            Parametros ItemParametros = FactoryParametros.Item();
            try
            {
                if (!ItemParametros.TipoImpresora.Contains("FISCAL"))
                {
                    ImprimirDevolucionFormaLibre(Documento);
                }
                else
                {
                    
                    ImprimirDevolucionFiscal(Documento);
                }
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
            //cFacturas.CargarInventario(Documento);
            //Documento = FactoryDocumentos.Item(new SoroEntities(), Documento.IdDocumento);
            //if (Documento.Saldo > 0)
            //{
            //    cFacturas.EscribirCuentaxCobrar(Documento);
            //}
            //cFacturas.LibroDeVentas(Documento);
        }
        public static void ImprimirCodigoBarras()
        {
            CodigosBarra r = new CodigosBarra();
            //r.LoadLayout(Application.StartupPath + "\\Reportes\\ReporteDocumento.REPX");  
            r.DataSource = FactoryProductos.Buscar("");
            using (ReportPrintTool printTool = new ReportPrintTool(r))
            {
                printTool.ShowRibbonPreviewDialog();
            }
        }
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
