using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HK
{
    public partial class FrmBuscarEntidades : Form
    {
        public string Status;
        public string Texto="";
        public string Filtro;
        public string myLayout = "";
        public object Registro = null;
        Entities Entities  = new Entities();        
        List<Cliente> Clientes = new List<Cliente>();
        List<Producto> Productos = new List<Producto>();
        List<Tercero> Proveedores = new List<Tercero>();
        public string TipoPrecio = "PRECIO 4";
        public FrmBuscarEntidades()
        {
            InitializeComponent();
        }
        private void Busqueda()
        {
            try
            {
                //Texto = this.txtBuscar.Text;

                switch (myLayout.ToUpper())
                {
                    case "PRODUCTOS":
                        Texto = txtBuscar.Text;
                        Productos = FactoryProductos.BuscarP(Texto);
                        foreach (Producto p in Productos)
                        {
                            switch (TipoPrecio)
                            {
                                case "PRECIO 2":
                                    p.Precio = p.Precio2;
                                    p.PrecioIVA = p.PrecioIVA2;
                                    break;
                                case "PRECIO 3":
                                    p.Precio = p.Precio3;
                                    p.PrecioIVA = p.PrecioIVA3;
                                    break;
                                case "PRECIO 4":
                                    p.Precio = p.Precio4;
                                    p.PrecioIVA = p.PrecioIVA4;
                                    break;
                                case "PRECIO 5":
                                    p.Precio = p.Precio5;
                                    p.PrecioIVA = p.PrecioIVA5;
                                    break;
                            }
                        }
                        this.bindingSource.DataSource = Productos;
                        this.bindingSource.ResetBindings(true);
                        break;
                    case "TERCEROS":
                        this.gridControl1.DefaultView.RestoreLayoutFromXml(Application.StartupPath + "\\Layout\\" + myLayout + ".XML", DevExpress.Utils.OptionsLayoutGrid.FullLayout);

                        Clientes = FactoryTerceros.Buscar(Entities , Texto);
                        this.bindingSource.DataSource = Clientes;
                        break;
                    case "PROVEEDORES":
                        this.gridControl1.DefaultView.RestoreLayoutFromXml(Application.StartupPath + "\\Layout\\" + myLayout + ".XML", DevExpress.Utils.OptionsLayoutGrid.FullLayout);
                        Proveedores = FactoryProveedores.Buscar(Entities , Texto);
                        this.bindingSource.DataSource = Proveedores;
                        break;

                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message + "\n" + x.StackTrace);
            }
            this.gridControl1.DataSource = this.bindingSource;

        }
        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            Seleccionar();
        }
        private void Seleccionar()
        {
            if (this.bindingSource.Current != null)
            {
                this.DialogResult = DialogResult.OK;
                Registro = this.bindingSource.Current;
                this.Close();
            }
        }
        private void Cancelar()
        {
            this.DialogResult = DialogResult.Cancel;
            Registro =null;
            this.Close();
        }
        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F10:
                    mostrarCosto();
                    break;
                case Keys.Return:
                    Seleccionar();
                    break;
                case Keys.Escape:
                    Cancelar();
                    break;
            }
        }

        private void mostrarCosto()
        {
            if (this.bindingSource.Current != null)
            {
                Producto r = (Producto)this.bindingSource.Current;
                MessageBox.Show(r.Costo.Value.ToString("n2"));
            }
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

        private void txtBuscar_Validating(object sender, CancelEventArgs e)
        {

        }

        private void FrmBuscarEntidades_Load(object sender, EventArgs e)
        {
            txtBuscar.Text = Texto;
            Busqueda();            
        }

    }
}
