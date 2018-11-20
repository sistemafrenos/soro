using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HK.Clases;

namespace HK
{
    public partial class FrmBuscarEntidades : Form
    {
        public string Status;
        public object registro = null;
        Data d = new Data();
        public object items;        
        private string Texto = "";
        private string myLayout = "";
        public FrmBuscarEntidades()
        {
            InitializeComponent();
        }
        private void FrmBuscar_Load(object sender, EventArgs e)
        {
            this.txtFiltro.Text = "TODOS";
            this.txtBuscar.Text = Texto;
            this.gridControl1.KeyDown +=new KeyEventHandler(gridControl1_KeyDown);
            this.gridControl1.DoubleClick +=new EventHandler(gridControl1_DoubleClick);
            this.txtBuscar.Click += new EventHandler(txtBuscar_Click);
            this.Imprimir.Click +=new EventHandler(Imprimir_Click);
            Busqueda();
        }
        void txtBuscar_Click(object sender, EventArgs e)
        {
            Busqueda();
        }
        internal void BuscarTerceros(string s)
        {
            myLayout = "PROVEEDORES";
            this.ShowDialog();
        }

        private void Busqueda()
        {
            d = new Data();
            Texto = this.txtBuscar.Text;            
            switch (myLayout.ToUpper())
            {
                case "PROVEEDORES":
                    this.bindingSource.DataSource = FactoryProveedores.getItems(Texto);
                    break;
            }
            this.gridControl1.DataSource = this.bindingSource;
            gridControl1.ForceInitialize();
            gridView1.OptionsLayout.Columns.Reset();
            this.gridControl1.DefaultView.RestoreLayoutFromXml(Application.StartupPath + "\\Layouts\\" + myLayout + ".XML", DevExpress.Utils.OptionsLayoutGrid.FullLayout);
        }
        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            Seleccionar();
        }
        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    Seleccionar();
                    break;
                case Keys.Escape:
                    Cancelar();
                    break;
            }
        }
        private void Seleccionar()
        {
            if (this.bindingSource.Current != null)
            {
                this.DialogResult = DialogResult.OK;
                registro = this.bindingSource.Current;
                this.Close();
            }
        }
        private void Cancelar()
        {
            this.DialogResult = DialogResult.Cancel;
            registro =null;
            this.Close();
        }
        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Busqueda();
            }
            if (e.KeyCode == Keys.Escape)
            {
                Cancelar();
            }
        }
        private void Buscar_Click(object sender, EventArgs e)
        {
            Busqueda();
        }
        private void Imprimir_Click(object sender, EventArgs e)
        {
            this.gridControl1.ShowPrintPreview();
        }
    }
}
