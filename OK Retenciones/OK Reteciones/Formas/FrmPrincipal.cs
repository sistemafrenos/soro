using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace HK.Formas
{
    public partial class FrmPrincipal : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public FrmPrincipal()
        {
            InitializeComponent();
            this.Load += new EventHandler(FrmPrincipal_Load);
        }

        void FrmPrincipal_Load(object sender, EventArgs e)
        {
            this.barButtonRespaldo.ItemClick += new ItemClickEventHandler(barButtonRespaldo_ItemClick);
            this.barButtonRecuperacion.ItemClick += new ItemClickEventHandler(barButtonRecuperacion_ItemClick);
            this.barButtonRetencionesISLR.ItemClick += new ItemClickEventHandler(barButtonRetencionesISLR_ItemClick);
            this.barButtonRetencionesIVA.ItemClick += new ItemClickEventHandler(barButtonRetencionesIVA_ItemClick);
            this.barButtonParametros.ItemClick += new ItemClickEventHandler(barButtonParametros_ItemClick);
            this.barButtonProveedores.ItemClick += new ItemClickEventHandler(barButtonProveedores_ItemClick);
        }

        void barButtonProveedores_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Pantallas.ProveedoresLista == null)
            {
                Pantallas.ProveedoresLista = new FrmProveedores();
                Pantallas.ProveedoresLista.MdiParent = this;
                Pantallas.ProveedoresLista.Dock = DockStyle.Fill;
                Pantallas.ProveedoresLista.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                Pantallas.ProveedoresLista.Show();
            }
            else
            {
                Pantallas.ProveedoresLista.Activate();
            }
        }

        void barButtonParametros_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmParametros f = new FrmParametros();
            f.ShowDialog();
        }

        void barButtonRetencionesIVA_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Pantallas.RetencionesLista == null)
            {
                Pantallas.RetencionesLista = new FrmRetenciones();
                Pantallas.RetencionesLista.MdiParent = this;
                Pantallas.RetencionesLista.Dock = DockStyle.Fill;
                Pantallas.RetencionesLista.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                Pantallas.RetencionesLista.Show();
            }
            else
            {
                Pantallas.RetencionesLista.Activate();
            }
       }
        void barButtonRetencionesISLR_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Pantallas.RetencionesISLRLista == null)
            {
                Pantallas.RetencionesISLRLista = new FrmRetencionesISLR();
                Pantallas.RetencionesISLRLista.MdiParent = this;
                Pantallas.RetencionesISLRLista.Dock = DockStyle.Fill;
                Pantallas.RetencionesISLRLista.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                Pantallas.RetencionesISLRLista.Show();
            }
            else
            {
                Pantallas.RetencionesISLRLista.Activate();
            }
        }
        void barButtonRecuperacion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            f.RootFolder = Environment.SpecialFolder.MyComputer;
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (f.SelectedPath == string.Empty)
                    return;
                if (!System.IO.File.Exists(f.SelectedPath + "respaldo.bak"))
                {
                    MessageBox.Show("No se encontro un respaldo en ese sitio");
                    return;
                }
                try
                {
                    System.IO.File.Copy(Application.StartupPath + "\\Retenciones.sdf", Application.StartupPath + "\\Retenciones.old", true);
                    System.IO.File.Copy(f.SelectedPath + "\\respaldo.bak", Application.StartupPath + "\\Retenciones.sdf", true);
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message);
                }
            }
        }
        void barButtonRespaldo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            f.RootFolder = Environment.SpecialFolder.MyComputer;
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (f.SelectedPath == string.Empty)
                    return;
                if (System.IO.File.Exists(f.SelectedPath + "respaldo.bak"))
                {
                    System.IO.File.Copy(f.SelectedPath + "respaldo.bak", f.SelectedPath + "respaldo.old", true);
                }
                try
                {
                    System.IO.File.Copy(Application.StartupPath + "\\Retenciones.sdf", f.SelectedPath + "respaldo.bak", true);
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message);
                }
            }
        }
    }
}