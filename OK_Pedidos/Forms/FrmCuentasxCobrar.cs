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
        }        
        
        public Cliente Titular = new Cliente();
        Entities Db = new Entities();     
        List<Cuenta> Cuentas = new List<Cuenta>();

        private void CargarCuentas()
        {
            this.tercerosBindingSource.DataSource = Titular;
            this.tercerosBindingSource.ResetBindings(true);
            Cuentas = FactoryCuentas.DocumentosPendientesCobrar(Titular.IdTercero);
            this.vistaDocumentoBindingSource.DataSource = Cuentas;
            this.vistaDocumentoBindingSource.ResetBindings(true);
            ResumenCuentas Resumen = new ResumenCuentas();
            ResumenCuentas CuentasTercero = FactoryCuentas.CuentasTercero(Titular.IdTercero);
            this.txtTotalFacturado.Value = (decimal)CuentasTercero.TotalFacturado;
            this.txtTotalPagado.Value = (decimal)CuentasTercero.TotalPagado;
            this.txtTotalVencido.Value = (decimal)CuentasTercero.TotalVencido;
            this.txtTotalPendiente.Value = (decimal)CuentasTercero.TotalPendiente;
            txtPagarHoy.Enter += new EventHandler(txtPagarHoy_Enter);
        }
        
        void txtPagarHoy_Enter(object sender, EventArgs e)
        {
           
        }

        private void Pagar()
        {
            RegistroPago Pago = new RegistroPago();
            this.gridView1.CloseEditor();
            this.vistaDocumentoBindingSource.EndEdit();
            Pago.MontoPagar =0;
            Pago.Modulo = "CxC";
            Pago.Documento ="";
            Pago.IdDocumento = "";
            Pago.Tipo = "";
            Pago.Momento = DateTime.Now;
            Pago.Fecha = DateTime.Today;
            Pago.IdTecero = Titular.IdTercero;
            Pago.Documento = "";            
            foreach (Cuenta Item in Cuentas.Where(x=> x.PagarHoy>0))                
            {
                PagosDetalle Detalle = new PagosDetalle();
                Detalle.IdDocumento = Item.IdDocumento;
                Detalle.IdPagoDetalle = FactoryContadores.GetLast("IdPagoDetalle");
                Detalle.Monto = Item.PagarHoy;
                Detalle.Numero = Item.Numero;
                Pago.PagosDetalles.Add(Detalle);
                Pago.Documento += Item.Numero + " ";                
            }
            Pago.MontoPagar = Cuentas.Sum(x => x.PagarHoy);
            if (!Pago.MontoPagar.HasValue)
            {
                return;
            }
            if (Pago.MontoPagar > 0)
            {
                FrmPago f = new FrmPago();
                f.Tercero = Titular;
                f.Pago = Pago;
                f.ShowDialog();
                if (f.DialogResult != System.Windows.Forms.DialogResult.OK)
                {
                    return;
                }                
            }
            //if (Pago.SaldoPendiente > 0)
            //{
            //    MessageBox.Show("El Monto de pagos no cubre lo que selecciono para pagar", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //if (string.IsNullOrEmpty(Pago.IdRegistroPago))
            //{
            //    Pago.IdRegistroPago = FactoryContadores.GetLast("IdRegistroPago");
            //    Pago.RazonSocial = Titular.RazonSocial;
            //    Pago.Modulo = "CxC";
            //    Pago.Momento = DateTime.Now;
            //    Pago.Fecha = DateTime.Today;
            //    Pago.IdTecero = Titular.IdTercero;
            //    Db.RegistroPagos.Add(Pago);
            //}
            //Db.SaveChanges();
            foreach (Cuenta Item in Cuentas.Where(x=> x.PagarHoy>0))
            {
                //PagosDetalle Detalle = new PagosDetalle();
                //Detalle.IdDocumento = Item.IdDocumento;
                //Detalle.IdPagoDetalle = FactoryContadores.GetLast("IdPagoDetalle");
                //Detalle.Monto = Item.PagarHoy;
                //Detalle.Numero = Item.Numero;
                //Detalle.Tipo ="CXC";
                //Detalle.IdRegistroPago = Pago.IdRegistroPago;
                //Db.PagosDetalles.Add(Detalle);
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
            Cuenta Registro = (Cuenta)this.vistaDocumentoBindingSource.Current;
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
                Cuenta Registro = (Cuenta)this.vistaDocumentoBindingSource.Current;
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
            
            Cuenta Registro = (Cuenta)this.vistaDocumentoBindingSource.Current;
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

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}
