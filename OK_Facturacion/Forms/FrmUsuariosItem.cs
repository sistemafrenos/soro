using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HK.Forms
{
    public partial class FrmUsuariosItem : Form
    {
        public FrmUsuariosItem()
        {
            InitializeComponent();
        }
        Usuarios Usuario = new Usuarios();
        private void CrearDerechos()
        {
            FrmPrincipal f = new FrmPrincipal();
            //         f.ribbonControl1.PageCategories.TotalCategory.Pages[].Groups[].ItemLinks[].Caption
            foreach (object Item in f.ribbonControl1.Items)
            {
                if (Item.GetType().Name == "BarButtonItem")
                    CrearDerecho((DevExpress.XtraBars.BarButtonItem)Item);
            }
            //this.usuariosDerechosBindingSource.DataSource = Usuario.UsuariosDerechos;
            //this.usuariosDerechosBindingSource.ResetBindings(false);
        }
        private void CrearDerecho(DevExpress.XtraBars.BarButtonItem Item)
        {
            UsuariosDerechos NuevoDerecho = new UsuariosDerechos();
            NuevoDerecho.Opcion = "";
            NuevoDerecho.SubOpcion = Item.Caption;
            NuevoDerecho.Habilitado = true;
            Usuario.UsuariosDerechos.Add(NuevoDerecho);
        }
        private void CargarDetalles()
        {
            if (Usuario == null)
            {
                //this.usuariosDerechosBindingSource.Clear();
                return;
            }
            if (Usuario.UsuariosDerechos.Count < 1)
                CrearDerechos();
            //this.usuariosDerechosBindingSource.DataSource = Usuario.UsuariosDerechos;
            //this.usuariosDerechosBindingSource.Sort = "Opcion,SubOpcion";
            //this.usuariosDerechosBindingSource.ResetBindings(false);
        }
    }
}
