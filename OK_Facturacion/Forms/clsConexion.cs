using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Text.RegularExpressions;
using System.Data.Common;

namespace SincronizacionServDisp
{
    public partial class Configuraciones : Form
    {


        public Assembly assembly;
        public string path;

        public Configuration config;
        public string cadenaConexionCentral, cadenaConexionNotebook;
        public string Servidor;
        public string Catalogo;
        public string Usuario;
        public string Password;




        private void Cargar()
        {
            assembly = Assembly.GetExecutingAssembly();
            path = assembly.Location;

            config = ConfigurationManager.OpenExeConfiguration(path);
            
            cargaConf();
        } 
        


        private void cargaConf()
        {
            char[] div = {';'};
            // Carga los valores de la cadena de conexion de la netbook
            string[] cadena = global::HK.Properties.Settings.Default.SoroConnectionString.Split(div);
            Servidor = cadena[0].Replace("Data Source=","");
            Catalogo = cadena[1].Replace("Initial Catalog=", "");
            Usuario = cadena[2].Replace("User Id=", "");
            Password = cadena[3].Replace("Password=", ""); 
            
           
        }

        private void grabaConf()
        {
            
            
            cadenaConexionCentral="Data Source="+Servidor+";"+"Initial Catalog="+Catalogo+";";
            cadenaConexionCentral+="User Id="+Usuario+";"+"Password="+Password+";";
            config.ConnectionStrings.ConnectionStrings["Config.Properties.Settings.conSERVIDORSQL"].ConnectionString = cadenaConexionCentral;
            config.Save(ConfigurationSaveMode.Modified);
            config.ConnectionStrings.ConnectionStrings["Config.Properties.Settings.conServidor1"].ConnectionString = cadenaConexionCentral;
            config.Save(ConfigurationSaveMode.Modified);
            cadenaConexionNotebook = "Data Source=" + Servidor + ";" + "Initial Catalog=" + Catalogo + ";";
            cadenaConexionNotebook += "User Id=" + Usuario + ";" + "Password=" + Password + ";";
            config.ConnectionStrings.ConnectionStrings["Config.Properties.Settings.conNotebook"].ConnectionString = cadenaConexionNotebook;
            config.Save(ConfigurationSaveMode.Modified);
            global::HK.Properties.Settings.Default.Save();
            Application.Restart();
        }     
    }
} 
