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
    public partial class FrmProductosItemSimple : Form
    {
        public FrmProductosItemSimple()
        {
            InitializeComponent();
        }

        public string codigo;
        SoroEntities dc = new SoroEntities();
        public Productos Producto = null;
        Grupos Grupo = new Grupos();
        public Productos OldProducto = null;
        private void Frm_Load(object sender, EventArgs e)
        {
            var Ubicaciones = (from p in dc.Productos
                               select p.Ubicacion).OrderBy(x => x).Distinct();
                              
            foreach (Grupos Item in dc.Grupos)
            {
                cmbEditGrupos.Properties.Items.Add(Item.Grupo);
            }
            foreach (string Ubicacion in Ubicaciones)
            {
                try
                {
                    UbicacionComboBoxEdit.Properties.Items.Add(Ubicacion);
                }
                catch { }
            }
            if (Producto == null)
            {
                Producto = new Productos();
                Producto.Activo = true;
                Producto.BloqueoPrecio = false;
                Producto.LlevaInventario = true;
                Producto.SolicitaServidor = false;
                Producto.parametros = FactoryParametros.Item(dc);
                Producto.Existencia = 0;
                Producto.Codigo = codigo;
                Producto.Costo = 0;
                Producto.CostoActual = 0;
                Producto.CostoDolares = 0;
                Producto.Maximo = 0;
                Producto.Minimo = 0;
                Producto.Precio = 0;
                Producto.Precio2 = 0;
                Producto.Precio3 = 0;
                Producto.Precio4 = 0;
                Producto.CostoDolares = 0;
                Producto.CantidadVentaPorDefecto = 1;
                Producto.PVP = 0;
                Producto.Iva = Producto.parametros.TasaIVA;
            }
            else
            {
                this.Text = "Editar Producto";
                Producto = FactoryProductos.Item(Producto.IdProducto);
                Grupo = FactoryGrupos.Item(Producto.IdGrupo);
                if (Grupo != null)
                {
                    this.cmbEditGrupos.Text = Grupo.Grupo;
                }
            }
            if (OldProducto != null)
            {
                CargarOldRegistro(OldProducto);
            }
            LeerUtilidad();
            LeerUtilidad2();
            LeerUtilidad3();
            this.txtNuevaExistencia.Value = (decimal)Producto.Existencia;
            this.productosBindingSource.DataSource = Producto;
            this.productosBindingSource.ResetBindings(true);
        }

        private void cmbEditGrupos_Validating(object sender, CancelEventArgs e)
        {
            if (cmbEditGrupos.IsModified != true)
            {
                return;
            }
            Grupo = FactoryGrupos.GetItemxGrupo(this.cmbEditGrupos.Text);
            if (Grupo == null)
            {
                Grupo = new Grupos();
            }
            Producto.IdGrupo = Grupo.IdGrupo;
            Producto.parametros.Utilidad1 = Grupo.Utilidad1;
            Producto.parametros.Utilidad2 = Grupo.Utilidad2;
            Producto.parametros.Utilidad3 = Grupo.Utilidad3;
            // Producto.parametros.Utilidad4 = Grupo.Utilidad4;
            Producto.Iva = Grupo.TasaIVA;
            Producto.parametros.TasaIVA = Grupo.TasaIVA;
            Producto.CalcularPrecio1();
            Producto.CalcularPrecio2();
            Producto.CalcularPrecio3();
            Producto.CalcularPrecio4();
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void CargarOldRegistro(Productos OldRegistro)
        {
            Producto = new Productos();
            Producto.Activo = true;
            Producto.BloqueoPrecio = OldRegistro.BloqueoPrecio;
            Producto.Descripcion = OldRegistro.Descripcion;
            Producto.Existencia = 0;
            Producto.IdGrupo = OldRegistro.IdGrupo;
            Producto.Iva = OldRegistro.Iva;
            Producto.LlevaInventario = OldRegistro.LlevaInventario;
            Producto.Marca = OldRegistro.Marca;
            Producto.Modelo = OldRegistro.Modelo;
            Producto.Referencia = OldRegistro.Referencia;
            Producto.SolicitaServidor = OldRegistro.SolicitaServidor;
            Producto.Tipo = OldRegistro.Tipo;
            Producto.Minimo = OldRegistro.Minimo;
            Producto.Maximo = OldRegistro.Maximo;
            Producto.Costo = 0;
            Producto.CostoActual = 0;
            Producto.CostoDolares = 0;
            Producto.Maximo = 0;
            Producto.Minimo = 0;
            Producto.Precio = 0;
            Producto.Precio2 = 0;
            Producto.Precio3 = 0;
            Producto.CostoDolares = 0;
            Producto.CantidadVentaPorDefecto = 1;
            Producto.Existencia = 0;
            Grupo = FactoryGrupos.Item(Producto.IdGrupo);
        }
        private void Aceptar_Click(object sender, EventArgs e)
        {
            try
            {
                productosBindingSource.EndEdit();
                Producto = (Productos)productosBindingSource.Current;
                if (Producto.Iva != Grupo.TasaIVA)
                {
                    if (MessageBox.Show("El iva de este producto es diferente al resto de la linea\n es esto correcto", "Atencion", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                        return;
                }
                if (Producto.Existencia != (float)txtNuevaExistencia.Value && !string.IsNullOrEmpty(Producto.IdProducto))
                {
                    FactoryLibroInventarios.EscribirLibroInventario(DateTime.Today, Producto, (float)txtNuevaExistencia.Value);
                    Producto.Existencia = (float)txtNuevaExistencia.Value;
                }
                Producto.Precio = (double)decimal.Round((decimal)Producto.Precio.Value, 2);
                Producto.Precio2 = (double)decimal.Round((decimal)Producto.Precio2.Value, 2);
                Producto.Precio3 = (double)decimal.Round((decimal)Producto.Precio3.Value, 2);
               // Producto.Precio4 = (double)decimal.Round((decimal)Producto.Precio4.Value, 2);
                Producto.IdGrupo = Grupo.IdGrupo;
                FactoryProductos.Guardar(Producto);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar los datos \n" + ex.Source + "\n" + ex.Message, "Atencion", MessageBoxButtons.OK);
            }
        }
        private void txtCostoActual_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.SpinEdit Editor = (DevExpress.XtraEditors.SpinEdit)sender;
            if (!Editor.IsModified)
                return;
            CalcularPrecios(Editor.Value);
        }
        private void CalcularPrecios(decimal Valor)
        {
            Productos Producto = (Productos)this.productosBindingSource.Current;
            Producto.CostoActual = Convert.ToDouble(Valor);
            Producto.parametros = FactoryParametros.Item();
            Producto.CalcularPrecio1();
            Producto.CalcularPrecio2();
            Producto.CalcularPrecio3();
            Producto.CalcularPrecio4();
        }
        private void LeerUtilidad()
        {
            this.txtUtilidad.Value = Convert.ToDecimal(Producto.Utilidad);
            this.txtPrecioIva.Value = Convert.ToDecimal(Producto.Precio + (Producto.Precio * Producto.Iva / 100));
        }
        private void LeerUtilidad2()
        {
            this.txtUtilidad2.Value = Convert.ToDecimal(Producto.Utilidad2);
            this.txtPrecio2Iva.Value = Convert.ToDecimal(Producto.Precio2 + (Producto.Precio2 * Producto.Iva / 100));

        }
        private void LeerUtilidad3()
        {
            this.txtUtilidad3.Value = Convert.ToDecimal(Producto.Utilidad3);
            this.txtPrecio3IVA.Value = Convert.ToDecimal(Producto.Precio3 + (Producto.Precio3 * Producto.Iva / 100));

        }
        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Producto.Codigo = Grupo.Codigo + "-" + FactoryContadores.GetLast("CodigoLinea" + Producto.IdGrupo);
        }
        private void calcEdit1_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.CalcEdit c = (DevExpress.XtraEditors.CalcEdit)sender;
            if (!c.IsModified)
            {
                return;
            }
            if (c.Value == 0)
            {
                return;
            }
            this.productosBindingSource.EndEdit();
            Producto.CostoActual = (double)c.Value * FactoryParametros.Item(new SoroEntities()).TasaIVA;
            CalcularPrecios((decimal)Producto.CostoActual);
        }
        private void spinEdit5_Properties_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.SpinEdit c = (DevExpress.XtraEditors.SpinEdit)sender;
            if (!c.IsModified)
            {
                return;
            }
            Producto.CostoActual = (double)c.Value;
        }
        private void txtPrecio1_Validating(object sender, CancelEventArgs e)
        {
            if (!this.txtPrecio1.IsModified)
                return;
            this.productosBindingSource.EndEdit();
            
            LeerUtilidad();
        }
        private void txtPrecio2_Validating(object sender, CancelEventArgs e)
        {
            if (!this.txtPrecio2.IsModified)
                return;
            this.productosBindingSource.EndEdit();
            LeerUtilidad2();
        }
        private void txtPrecio3_Validating(object sender, CancelEventArgs e)
        {
            if (!this.txtPrecio3.IsModified)
                return;
            this.productosBindingSource.EndEdit();
            LeerUtilidad3();
        }


        private void txtUtilidad_Validating(object sender, CancelEventArgs e)
        {
            if (!this.txtUtilidad.IsModified)
                return;
            this.productosBindingSource.EndEdit();
            Producto.Utilidad = (double)this.txtUtilidad.Value;
            Producto.CalcularPrecio1();
            this.txtPrecio1.Value = (decimal)Producto.Precio;
            try
            {
                this.txtPrecioIva.Value = (decimal)Producto.PrecioIva;
            }
            catch { }
        }

        private void txtUtilidad2_Validating(object sender, CancelEventArgs e)
        {
            if (!this.txtUtilidad2.IsModified)
                return;
            this.productosBindingSource.EndEdit();
            Producto.Utilidad2 = (double)this.txtUtilidad2.Value;
            Producto.CalcularPrecio2();
            this.txtPrecio2.Value = (decimal)Producto.Precio2;
            try
            {
                this.txtPrecio2Iva.Value = (decimal)Producto.PrecioIva2;
            }
            catch { }
        }

        private void txtUtilidad3_Validating(object sender, CancelEventArgs e)
        {
            if (!this.txtUtilidad3.IsModified)
                return;
            this.productosBindingSource.EndEdit();
            Producto.Utilidad3 = (double)this.txtUtilidad3.Value;
            Producto.CalcularPrecio3();
            this.txtPrecio3.Value = (decimal)Producto.Precio3;
            try
            {
                this.txtPrecio3IVA.Value = (decimal)Producto.PrecioIva3;
            }
            catch { }
        }

        private void txtPrecioIva_Validating(object sender, CancelEventArgs e)
        {
            if (!this.txtPrecioIva.IsModified)
                return;
            Producto.Precio = Convert.ToDouble(txtPrecioIva.Value) / (1 + (Producto.Iva / 100));
        }

        private void txtPrecio2Iva_Validating(object sender, CancelEventArgs e)
        {
            if (!this.txtPrecio2Iva.IsModified)
                return;
            Producto.Precio2 = Convert.ToDouble(txtPrecio2Iva.Value) / (1 + (Producto.Iva / 100));

        }

        private void txtPrecio3IVA_Validating(object sender, CancelEventArgs e)
        {
            if (!this.txtPrecio3IVA.IsModified)
                return;
            Producto.Precio3 = Convert.ToDouble(txtPrecio3IVA.Value) / (1 + (Producto.Iva / 100));
        }
    }
}
