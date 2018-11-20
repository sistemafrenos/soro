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
    public partial class FrmAjustarExistenciaAnterior : Form
    {
        public FrmAjustarExistenciaAnterior()
        {
            InitializeComponent();
        }
        public DocumentosProductos Registro = new DocumentosProductos();
        public Productos Producto = new Productos();


        private void FrmAjustarPrecios_Load(object sender, EventArgs e)
        {
            this.documentosProductosBindingSource.DataSource = Registro;
            this.documentosProductosBindingSource.ResetBindings(true);
            txtExistenciaNueva.Value = (decimal)Registro.ExistenciaAnterior;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.documentosProductosBindingSource.EndEdit();
            if (Producto.Existencia != (float)this.txtExistenciaNueva.Value  )
            {
                FactoryLibroInventarios.EscribirLibroInventario(DateTime.Today, Producto, (float)txtExistenciaNueva.Value);
                using (var dc = new SoroEntities())
                {
                    Producto.Existencia = (float)txtExistenciaNueva.Value;
                    Productos oProducto = FactoryProductos.Item(Producto.IdProducto);                    
                    oProducto.Existencia = Producto.Existencia;
                    dc.SaveChanges();
                    Registro.ExistenciaAnterior = Producto.Existencia;
                    this.DialogResult = DialogResult.OK;
                }                
            }            
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
