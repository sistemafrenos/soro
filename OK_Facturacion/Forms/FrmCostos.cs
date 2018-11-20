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
    public partial class FrmCostos : Form
    {
        public FrmCostos()
        {
            InitializeComponent();
        }
        SoroEntities dc = new SoroEntities();
        List<VistaProductos> ListaProductos = null;
        Productos Producto = null; 
        private void Buscar_Click(object sender, EventArgs e)
        {
            Busqueda();
        }
        private void Busqueda()
        {
            try
            {
                dc = new SoroEntities();
                ListaProductos = FactoryProductos.Buscar(dc, this.txtBuscar.Text); 
                this.VistaProductos.DataSource = ListaProductos;
                this.VistaProductos.ResetBindings(true);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        //private void txtCostoDolares_Validating(object sender, CancelEventArgs e)
        //{
        //    DevExpress.XtraEditors.SpinEdit Editor = (DevExpress.XtraEditors.SpinEdit)sender;
        //    Productos Producto = (Productos)this.productosBindingSource.Current;
        //    Producto.CostoActual = (double)Editor.Value * Convert.ToDouble( this.txtCostoDolar.Text ) ;
        //    Producto.parametros = parametros;
        //    Producto.CalcularPrecio1();
        //    Producto.CalcularPrecio2();
        //    Producto.CalcularPrecio3();
        //}
        private void txtCosto_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.SpinEdit Editor = (DevExpress.XtraEditors.SpinEdit)sender;
            Productos Producto = (Productos)this.productosBindingSource.Current;
            Producto.CostoActual = (double)Editor.Value;          
            Producto.CalcularPrecio1();
            Producto.CalcularPrecio2();
            Producto.CalcularPrecio3();
        }
        private void txtCostoActual_Validating(object sender, CancelEventArgs e)
        {
            VistaProductos Item = (VistaProductos)this.VistaProductos.Current;            
            DevExpress.XtraEditors.SpinEdit Editor = (DevExpress.XtraEditors.SpinEdit)sender;
            Productos Producto = (Productos)this.productosBindingSource.Current;
            Producto.CostoActual = Convert.ToDouble( Editor.Value);
            Producto.CalcularPrecio1();
            Producto.CalcularPrecio2();
            Producto.CalcularPrecio3();
            Item.Precio = Producto.Precio;
            Item.Precio2 = Producto.Precio2;
            Item.Precio3 = Producto.Precio3;            
            this.VistaProductos.ResetCurrentItem();
        }
        private void FrmCostos_Load(object sender, EventArgs e)
        {
        //   this.txtCostoDolar.Text = parametros.CostoDolar.ToString();
        //    this.txtEditCostoDolares.Validating += new CancelEventHandler(txtCostoDolares_Validating);
            this.txtCosto.Validating += new CancelEventHandler(txtCosto_Validating);
            this.txtCostoActual.Validating += new CancelEventHandler(txtCostoActual_Validating);
            this.txtCosto.Enter += new EventHandler(Editor_Activate);
            this.txtPrecio.Enter += new EventHandler(Editor_Activate);
            this.txtCostoActual.Enter += new EventHandler(Editor_Activate);
        }
        private void Editor_Activate(object sender, EventArgs e)
        {
            ((DevExpress.XtraEditors.SpinEdit)sender).SelectAll();
        }
        //private void simpleButton1_Click(object sender, EventArgs e)
        //{
        //    foreach (VistaProductos Item in ListaProductos)
        //    {
        //        if( Item.CostoDolares.HasValue && Item.CostoDolares.Value!=0)
        //        {
        //            Producto = FactoryProductos.Item(Item.IdProducto);
        //            Producto.CostoActual = Item.CostoDolares * Convert.ToDouble(this.txtCostoDolar.Text);
        //            Producto.parametros = parametros;
        //            Producto.CalcularPrecio1();
        //            Producto.CalcularPrecio2();
        //            Producto.CalcularPrecio3();
        //            dc.SaveChanges();
        //        }
        //    }
        //    Busqueda();
        //}

        //private void simpleButton2_Click(object sender, EventArgs e)
        //{
        //    foreach (VistaProductos Item in ListaProductos)
        //    {
        //        if (Item.Costo.HasValue && Item.Costo.Value != 0)
        //        {
        //            Producto = FactoryProductos.Item(Item.IdProducto);
        //            Producto.parametros = parametros;
        //            Producto.CostoActual = Item.Costo * (1 + Convert.ToDouble(this.txtPorcentaje.Text) / 100);
        //            Producto.CalcularPrecio1();
        //            Producto.CalcularPrecio2();
        //            Producto.CalcularPrecio3();

        //        }
        //    }
        //    dc.SaveChanges();
        //    Busqueda();
        //}

        //private void simpleButton3_Click(object sender, EventArgs e)
        //{
        //    foreach (VistaProductos Item in ListaProductos)
        //    {
        //        Producto = FactoryProductos.Item(Item.IdProducto);
        //        if (!Producto.CostoActual.HasValue )
        //        {
        //            Producto.CostoActual = Item.Costo;
        //        }
        //        if (Producto.CostoActual.HasValue)
        //        {
        //            Producto.CostoActual = Item.CostoActual * (1 + Convert.ToDouble(this.txtPorcentaje.Text) / 100);
        //            Producto.parametros = parametros;
        //            Producto.CalcularPrecio1();
        //            Producto.CalcularPrecio2();
        //            Producto.CalcularPrecio3();                    
        //        }
        //    }
        //    dc.SaveChanges();
        //    Busqueda();
        //}

        private void productosBindingSource_CurrentChanged(object sender, EventArgs e)
        {  
            VistaProductos Item = (VistaProductos)this.VistaProductos.Current;
            if (Item == null)
            {
                Item = new VistaProductos();
            }
            this.productosBindingSource.EndEdit();
            dc.SaveChanges();
            Producto = FactoryProductos.Item(Item.IdProducto);
            try
            {
                txtNuevaExistencia.Value = Convert.ToDecimal(Producto.Existencia);
            }
            catch
            {
                txtNuevaExistencia.Value = 0;
            }
            if (Producto == null)
            {
                Producto = new Productos();
            }
            this.productosBindingSource.DataSource = Producto;
            LeerUtilidad();
            LeerUtilidad2();
            LeerUtilidad3();
            this.productosBindingSource.ResetBindings(true);
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            this.productosBindingSource.EndEdit();
            if (Producto == null)
                return;
            if (Producto.Existencia != (float)txtNuevaExistencia.Value)
            {
                FactoryLibroInventarios.EscribirLibroInventario(DateTime.Today, Producto, (float)txtNuevaExistencia.Value);
                Producto.Existencia = (float)txtNuevaExistencia.Value;
            }
            FactoryProductos.Guardar(Producto);            
            Busqueda();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Busqueda();
            }
        }
        private void LeerUtilidad()
        {
            Producto = (Productos)this.productosBindingSource.Current;
            if (Producto == null) Producto = new Productos();
            this.txtUtilidad.Value = Convert.ToDecimal(Producto.Utilidad);
        }
        private void LeerUtilidad2()
        {
            Producto = (Productos)this.productosBindingSource.Current;
            if (Producto == null) Producto = new Productos();
            this.txtUtilidad2.Value = Convert.ToDecimal(Producto.Utilidad2);
        }
        private void LeerUtilidad3()
        {
            Producto = (Productos)this.productosBindingSource.Current;
            if (Producto == null) Producto = new Productos();
            this.txtUtilidad3.Value = Convert.ToDecimal(Producto.Utilidad3);
        }
        private void spinEdit1_Validated(object sender, EventArgs e)
        {
            if (!this.txtPrecio1.IsModified)
                return;
            this.productosBindingSource.EndEdit();
            LeerUtilidad();
        }
        private void txtPrecio2_Validated(object sender, EventArgs e)
        {
            if (!this.txtPrecio1.IsModified)
                return;
            this.productosBindingSource.EndEdit();
            LeerUtilidad2();
        }
        private void txtPrecio3_Validated(object sender, EventArgs e)
        {
            if (!this.txtPrecio3.IsModified)
                return;
            this.productosBindingSource.EndEdit();
            LeerUtilidad3();
        }
        private void Imprimir_Click(object sender, EventArgs e)
        {
            this.gridControl1.ShowPrintPreview();
        }

        private void txtUtilidad_Validating(object sender, CancelEventArgs e)
        {
            if (!this.txtUtilidad.IsModified)
                return;
            this.productosBindingSource.EndEdit();
            Producto.Utilidad = (double)this.txtUtilidad.Value;
            Producto.CalcularPrecio1();
            this.txtPrecio1.Value = (decimal)Producto.Precio;
        }

        private void txtUtilidad2_Validating(object sender, CancelEventArgs e)
        {
            if (!this.txtUtilidad2.IsModified)
                return;
            this.productosBindingSource.EndEdit();
            Producto.Utilidad2 = (double)this.txtUtilidad2.Value;
            Producto.CalcularPrecio2();
            this.txtPrecio2.Value = (decimal)Producto.Precio2;
        }

        private void txtUtilidad3_Validating(object sender, CancelEventArgs e)
        {
            if (!this.txtUtilidad3.IsModified)
                return;
            this.productosBindingSource.EndEdit();
            Producto.Utilidad3 = (double)this.txtUtilidad3.Value;
            Producto.CalcularPrecio3();
            this.txtPrecio3.Value = (decimal)Producto.Precio3;
        }

    }
}
