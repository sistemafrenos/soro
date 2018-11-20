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
        public Terceros Tercero;
        public string Status;
        public string Texto="";
        public string Filtro;
        public string myLayout = "";
        public object Registro = null;
        public SoroEntities dc;
        public object items;
        public FrmBuscarEntidades()

        {
            InitializeComponent();
        }
        private void FrmBuscar_Load(object sender, EventArgs e)
        {
            this.txtFiltro.Text = "TODOS";
            if (myLayout == "BUSCARVENTAS")
            {
                this.txtFiltro.Items.AddRange(new[] { "TODOS", "PRESUPUESTO", "PEDIDO", "FACTURA", "NOTA ENTREGA" });
                this.txtFiltro.Visible = true;                
            }
            if (myLayout == "BUSCARCOMPRAS")
            {
                this.txtFiltro.Items.AddRange(new[] { "TODOS", "COMPRA", "AJUSTE"});
                this.txtFiltro.Visible = true;
            }
            if(myLayout == "BUSCARDEVOLUCIONES")
            {
                this.txtFiltro.Items.AddRange(new[] { "TODOS", "FACTURA", "PEDIDO" });
                this.txtFiltro.Visible = true;
            }
            this.txtBuscar.Text = Texto;

            Busqueda();
        }
        private void Busqueda()
        {
            Texto = this.txtBuscar.Text;
            switch (myLayout.ToUpper())
            {
                case "BUSCARVENTAS":
                    if (txtFiltro.Text == "TODOS")
                    {
                        this.bindingSource.DataSource = FactoryDocumentos.BuscarVentas(Texto);
                    }
                    else
                    {
                        this.bindingSource.DataSource = FactoryDocumentos.BuscarVentas(Texto, txtFiltro.Text);
                    }
                    break;
                case "BUSCARCOMPRAS":
                    if (txtFiltro.Text == "TODOS")
                    {
                        this.bindingSource.DataSource = FactoryDocumentos.BuscarCompras(Texto);
                    }
                    else
                    {
                        this.bindingSource.DataSource = FactoryDocumentos.BuscarCompras(Texto, txtFiltro.Text);
                    }
                    break;
                case "BUSCARDEVOLUCIONES":
                    if (txtFiltro.Text == "TODOS")
                    {
                        this.bindingSource.DataSource = FactoryDocumentos.BuscarVentas2(Texto,"FACTURA,PEDIDO");
                    }
                    else
                    {
                        this.bindingSource.DataSource = FactoryDocumentos.BuscarVentas2(Texto, txtFiltro.Text);
                    }
                    break;
                case "COMPRAS":
                    this.bindingSource.DataSource = FactoryDocumentos.Buscar(dc, Texto,"COMPRA",true);
                    break;
                case "PRODUCTOS":
                    if (Filtro == "FACTURAS")
                    {
                        this.bindingSource.DataSource = FactoryProductos.BuscarConExistencia(Texto);
                    }
                    else
                    {
                        this.bindingSource.DataSource = FactoryProductos.Buscar(Texto);
                    }
                    break;
                case "TERCEROS":
                    this.bindingSource.DataSource = FactoryTerceros.Buscar(Texto);
                    break;
                case "PROVEEDORES":
                    this.bindingSource.DataSource = FactoryTerceros.Buscar(Texto,"PROVEEDOR");
                    break;
                case "VISTADOCUMENTO":
                    this.bindingSource.DataSource = FactoryDocumentos.Buscar(dc, Texto, Filtro, true);
                    break;
                case "PRESUPUESTOS":
                    this.bindingSource.DataSource = FactoryDocumentos.Buscar(dc, Texto, Filtro, true);
                    break;
                case "FACTURAS":
                    this.bindingSource.DataSource = FactoryDocumentos.Buscar(dc, Texto, Filtro, true);
                    break;
                case "DEVOLUCIONES":
                    this.bindingSource.DataSource = FactoryDocumentos.Buscar(dc, Texto, Filtro, true);
                    break;
                case "PEDIDOS":
                    this.bindingSource.DataSource = FactoryDocumentos.Buscar(dc,Texto,Filtro,true);
                    break;
                case "BANCOS":
                    this.bindingSource.DataSource = FactoryBancos.Buscar(dc,Texto);
                    break;
                case "RECIBOS":
                    this.bindingSource.DataSource = FactoryRecibos.BusquedaRecibos(dc, Texto);
                    break;
                case "PRODUCTOSNODEVUELTOS":
                    this.bindingSource.DataSource = FactoryProductos.BuscarNoDevueltosFactura(Texto,(List<VistaFactura>)items);
                    break;
            }
            this.gridControl1.DataSource = this.bindingSource;
            gridControl1.ForceInitialize();
            gridView1.OptionsLayout.Columns.Reset();
            this.gridControl1.DefaultView.RestoreLayoutFromXml(Application.StartupPath + "\\Layout\\" + myLayout + ".XML", DevExpress.Utils.OptionsLayoutGrid.FullLayout);
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
                case Keys.Return:
                    Seleccionar();
                    break;
                case Keys.Escape:
                    Cancelar();
                    break;
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

    }
}
