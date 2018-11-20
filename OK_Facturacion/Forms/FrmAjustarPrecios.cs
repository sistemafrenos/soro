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
    public partial class FrmAjustarPrecios : Form
    {
        public FrmAjustarPrecios()
        {
            InitializeComponent();
        }
        public DocumentosProductos Registro = new DocumentosProductos();
        public Productos Producto = new Productos();


        private void FrmAjustarPrecios_Load(object sender, EventArgs e)
        {
            this.documentosProductosBindingSource.DataSource = Registro;
            this.documentosProductosBindingSource.ResetBindings(true);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.documentosProductosBindingSource.EndEdit();
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void calcEdit1_Validating(object sender, CancelEventArgs e)
        {
            this.documentosProductosBindingSource.EndEdit();
            Registro.CostoIva = Registro.Costo + Registro.Costo * (Registro.TasaIva / 100);
            Producto.CostoActual = Registro.Costo;
            CalcularPrecios();
        }
        private void calcEdit2_Validating(object sender, CancelEventArgs e)
        {
            this.documentosProductosBindingSource.EndEdit();
            Registro.Costo = Registro.CostoIva / (1 + (Registro.TasaIva) / 100);
            Producto.CostoActual = Registro.Costo;
            CalcularPrecios();
        }
        private void CalcularPrecios()
        {
            Producto.parametros = FactoryParametros.Item(new DbDataContext());
            Producto.CalcularPrecio1();
            Producto.CalcularPrecio2();
            Producto.CalcularPrecio3();
            Registro.Precio = Producto.Precio;
            Registro.Precio2 = Producto.Precio2;
            Registro.Precio3 = Producto.Precio3;
            Registro.PrecioIva = Registro.Precio + (Registro.Precio * (Registro.TasaIva / 100));

        }
    }
}
