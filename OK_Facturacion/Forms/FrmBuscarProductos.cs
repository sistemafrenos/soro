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
    public partial class FrmBuscarProductos : Form
    {
        public string Status;
        public string Texto = "";
        public string Filtro;
        public string myLayout = "";
        public object Registro = null;
        SoroEntities Db = new SoroEntities();
        public List<Productos> Productos = new List<Productos>();
        public string TipoPrecio = "PRECIO 4";
        public Productos producto = new Productos();
        List<Productos> Respaldo = new List<Productos>();
        int linea = 0;
        public FrmBuscarProductos()
        {
            InitializeComponent();
        }
        private void txtCant_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.CalcEdit Editor = (DevExpress.XtraEditors.CalcEdit)this.gridControl1.MainView.ActiveEditor;
            if (!Editor.IsModified)
            {
                return;
            }
            //try
            //{
            //    if ((double)Editor.Value > producto.Existencia)
            //    {
            //        Editor.Value = (decimal)producto.Existencia;
            //    }
            //}
            //catch
            //{
            //}
            producto = (Productos)this.bindingSource.Current;
            producto.Pedido = (double)Editor.Value;
        }

        private void FrmBuscarProductos_Load(object sender, EventArgs e)
        {
            txtBuscar.Text = Texto;
            Busqueda();
            this.txtCant.Validating += new CancelEventHandler(txtCant_Validating);
            this.gridView1.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
            this.gridView1.FocusedColumn = colDescripcion;
            this.gridView1.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(gridView1_BeforeLeaveRow);
            this.gridView1.ColumnFilterChanged += new EventHandler(gridView1_ColumnFilterChanged);

        }

        void gridView1_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            if (this.gridView1.FocusedRowHandle != -999997)
            {
                linea = this.gridView1.FocusedRowHandle;
            }
        }

        void gridView1_ColumnFilterChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(gridView1.FilterPanelText))
            {
               this.gridView1.FocusedRowHandle = linea;
               this.gridView1.SelectRow(linea);
            }
        }
        private void Busqueda()
        {
            this.bindingSource.EndEdit();            
            Texto = this.txtBuscar.Text;
            foreach (Productos p in Productos.Where(x => x.Pedido > 0))
            {
                Respaldo.Add(p);
            }
            Productos = FactoryProductos.BuscarP(Texto);
            foreach (Productos p in Productos)
            {
                switch (TipoPrecio)
                {
                    case "PRECIO 2":
                        p.Precio = p.Precio2;
                        p.PrecioIva= p.PrecioIva2;
                        break;
                    case "PRECIO 3":
                        p.Precio = p.Precio3;
                        p.PrecioIva = p.PrecioIva3;
                        break;
                    case "PRECIO 4":
                        p.Precio = p.Precio4;
                        p.PrecioIva= p.PrecioIva4;
                        break;
                    case "PRECIO 5":
                        p.Precio = p.Precio5;
                        p.PrecioIva = p.PrecioIva5;
                        break;
                }                
            }
            
            foreach (Productos p in Respaldo)
            {
                Productos Item = Productos.FirstOrDefault(x => x.IdProducto == p.IdProducto);
                if (Item != null)
                    Item.Pedido = p.Pedido;
            }
            this.bindingSource.DataSource = Productos;
            
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
            Registro = null;
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

        private void gridControl1_Click(object sender, EventArgs e)
        {
        }

        private void FrmBuscarProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.gridView1.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
                this.gridView1.FocusedColumn = colDescripcion;                
            }
        }
    }
}
