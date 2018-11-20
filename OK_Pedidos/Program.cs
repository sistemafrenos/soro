using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HK
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static string s = "Data Source=|DataDirectory|\\OK_Pedidos.sdf";
        public static string cn = "Data Source=.\\SQLEXPRESS;Initial Catalog=Soro;Integrated Security=True;";
     //   public static string cn = global::HK.Properties.Settings.Default.SoroConnectionString;
        
            
        [STAThread]        
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmPrincipal());
        }
    }
}
