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
    public partial class FrmCuentasxCobrar : Form
    {        
        public FrmCuentasxCobrar()
        {
            InitializeComponent();
            this.Load+=new EventHandler(FrmCuentasxCobrar_Load);
        }

        public Terceros Titular = new Terceros();
        SoroEntities Db = new SoroEntities();    
        List<Cuentas> Cuentas = new List<Cuentas>();

        private void CargarCuentas()
        {
            this.tercerosBindingSource.DataSource = Titular;
            this.tercerosBindingSource.ResetBindings(true);
            Cuentas = FactoryCuentas.DocumentosPendientesCobrar(Db, Titular.IdTercero);
            this.vistaDocumentoBindingSource.DataSource = Cuentas;
            this.vistaDocumentoBindingSource.ResetBindings(true);
            ResumenCuentas Resumen = new ResumenCuentas();
            ResumenCuentas CuentasTercero = FactoryCuentas.CuentasTercero(Titular.IdTercero);
            this.txtTotalFacturado.Value = (decimal)CuentasTercero.TotalFacturado;
            this.txtTotalPagado.Value = (decimal)CuentasTercero.TotalPagado;
            this.txtTotalVencido.Value = (decimal)CuentasTercero.TotalVencido;
            this.txtTotalPendiente.Value = (decimal)CuentasTercero.TotalPendiente;

        }

        private void Pagar()
        {
            if (Cuentas.Count < 1)
            {
                return;
            }
            RegistroPagos Pago = new RegistroPagos();
            this.gridView1.CloseEditor();
            this.vistaDocumentoBindingSource.EndEdit();
            Pago.MontoPagar =0;
            Pago.Modulo = "CxC";
            Pago.Documento ="";
            Pago.IdDocumento = "";
            Pago.Tipo = "";
            Pago.Momento = DateTime.Now;
            Pago.Fecha = DateTime.Today;            
            Pago.Tipo += Cuentas.FirstOrDefault().TipoDocumento+" ";
            Pago.MontoPagar = 0;
            Pago.IdTercero = Titular.IdTercero;
            foreach (Cuentas cItem in Cuentas.Where(x => x.PagarHoy > 0))
            {
                Pago.IdDocumento += cItem.IdDocumento;
                Pago.Documento += cItem.Numero + " ";
                Pago.MontoPagar += cItem.PagarHoy;
            }
            if (!Pago.MontoPagar.HasValue)
            {
                return;
            }
            if (Pago.MontoPagar > 0)
            {
                FrmPago f = new FrmPago();
                f.Pago = Pago;
                f.Tercero = Titular;
                f.ShowDialog();
                if (f.DialogResult != System.Windows.Forms.DialogResult.OK)
                {
                    return;
                }
            }
            if (Pago.SaldoPendiente > 0)
            {
                MessageBox.Show("El Monto de pagos no cubre lo que selecciono para pagar", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(Pago.IdRegistroPago))
            {
                Pago.IdRegistroPago = FactoryContadores.GetLast("IdRegistroPago");
                Pago.RazonSocial = Titular.RazonSocial;
                Pago.Modulo = "CxC";
                Pago.Momento = DateTime.Now;
                Pago.Fecha = DateTime.Today;
                Pago.IdTercero = Titular.IdTercero;
                Db.RegistroPagos.Add(Pago);
            }
            foreach (Cuentas Item in Cuentas.Where(x=> x.PagarHoy>0))
            {
                RegistroPagosDetalles Detalle = new RegistroPagosDetalles();
                Detalle.IdDocumento = Item.IdDocumento;
                Detalle.IdRegistroPagosDetalle = FactoryContadores.GetLast("IdRegistroPagosDetalle");
                Detalle.Monto = Item.PagarHoy;
                Detalle.Numero = Item.Numero;
                Detalle.Tipo ="CXC";
                Detalle.IdRegistroPago = Pago.IdRegistroPago;
                Detalle.Saldo = Item.Saldo - Item.PagarHoy;
                Db.RegistroPagosDetalles.Add(Detalle);
                Item.Saldo = Item.Saldo - Item.PagarHoy;
            }
            Db.SaveChanges();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
            
        }
        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {


            
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Cuentas Registro = (Cuentas)this.vistaDocumentoBindingSource.Current;
            if (Registro == null)
            {
                return;
            }
            Registro.PagarHoy = Registro.Saldo.Value;
            this.vistaDocumentoBindingSource.EndEdit();
            
        }

        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {

            }
        }

        private void txtPagarHoy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Cuentas Registro = (Cuentas)this.vistaDocumentoBindingSource.Current;
                //this.vistaDocumentoBindingSource.CancelEdit();
                if (Registro == null)
                {
                    return;
                }
                //Registro.PagarHoy = Registro.Saldo.Value;
                //this.vistaDocumentoBindingSource.ResetCurrentItem();
                this.gridView1.ActiveEditor.Text = Registro.Saldo.Value.ToString("n2");
            }
        }

        private void txtPagarHoy_DoubleClick(object sender, EventArgs e)
        {
            
            Cuentas Registro = (Cuentas)this.vistaDocumentoBindingSource.Current;
            this.vistaDocumentoBindingSource.CancelEdit();
            if (Registro == null)
            {
                return;
            }
            this.gridView1.ActiveEditor.Text = Registro.Saldo.Value.ToString("n2");
            //Registro.PagarHoy = Registro.Saldo.Value;
            //this.vistaDocumentoBindingSource.ResetCurrentItem();
        }

        private void FrmCuentasxCobrar_Load(object sender, EventArgs e)
        {
            CargarCuentas();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(FrmCuentasxCobrar_KeyDown);
        }

        void FrmCuentasxCobrar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Cancelar.PerformClick();
                e.Handled = true;
            }
        }

        private void Aceptar_Click(object sender, EventArgs e)
        {
            Pagar();
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmAgregarFactura f = new FrmAgregarFactura();
            f.IdTercero = Titular.IdTercero;
            f.Tipo = "CXC";
            f.ShowDialog();
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                CargarCuentas();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cuentas cuenta = (Cuentas)this.vistaDocumentoBindingSource.Current;
            if (cuenta == null)
                return;
            if(MessageBox.Show("Esta seguro de eliminar esta cuenta","Atencion",  MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)== System.Windows.Forms.DialogResult.Yes)
            {
                Db.Cuentas.Remove(cuenta);
                Db.SaveChanges();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}
