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
    public partial class FrmRegistrarCobros : Form
    {
        public Recibos Recibo;
        public bool PuedeAbonar = false;
        public double? MontoPagar = 0;
        public bool CuentasxCobrar = false;
        public FrmRegistrarCobros()
        {
            InitializeComponent();
        }

        private void FrmRegistrarPagos_Load(object sender, EventArgs e)
        {
            this.recibosBindingSource.DataSource = this.Recibo;
            this.recibosBindingSource.ResetBindings(true);
            this.recibosDetallesBindingSource.DataSource = this.Recibo.RecibosDetalles;
            this.recibosDetallesBindingSource.ResetBindings(true);
            this.recibosPagosBindingSource.DataSource = this.Recibo.RecibosPagos;
            this.recibosPagosBindingSource.ResetBindings(true);
            if (PuedeAbonar)
            {
                this.gridControl1.Enabled = true;
            }
            if (CuentasxCobrar)
            {
                Retenciones.Enabled = true;
            }
            else
            {
                Retenciones.Enabled = false;
            }
            Calcular();
        }

        private void Calcular()
        {
            this.recibosPagosBindingSource.EndEdit();
            if (Recibo == null)
            {
                return;
            }
            this.MontoPagar =  Convert.ToDouble(Recibo.RecibosDetalles.Sum(x => x.MontoAbono));
            double? TotalPagado = 0;
            Recibo.Efectivo = 0;
            Recibo.Cheques =0;
            Recibo.Tarjetas = 0;
            Recibo.Depositos = 0;            
            foreach (RecibosPagos Item in Recibo.RecibosPagos)
            {
                if (Item.Monto.HasValue)
                {
                    TotalPagado += Item.Monto;
                    switch (Item.FormaPago)
                    {
                        case "EFECTIVO":

                            Recibo.Efectivo += Item.Monto;
                            break;
                        case "CHEQUE":
                            Recibo.Cheques += Item.Monto;
                            break;
                        case "TARJETA":
                            Recibo.Tarjetas += Item.Monto;
                            break;
                        case "DEPOSITO":
                            Recibo.Depositos += Item.Monto;
                            break;
                    }
                }
                else
                {
                    Item.Monto = 0;
                }
            }
            Recibo.Cambio = (double)Recibo.Efectivo - (double)this.MontoPagar;
            if (Recibo.Cambio < 0)
            {
                Recibo.Cambio = 0;
            }
            Recibo.Monto = Recibo.RecibosPagos.Sum(X => X.Monto) - Recibo.Cambio
                + (double)txtRetencionISLR.Value + (double)txtRetencionIVA.Value;
            txtTotalPagar.Value = Convert.ToDecimal(this.MontoPagar);
            txtTotalPagos.Value = Convert.ToDecimal(TotalPagado);
            txtTotalPagos.Value = Convert.ToDecimal(Recibo.Monto);
            txtCambio.Value = Convert.ToDecimal(Recibo.Cambio);
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.gridView3.CloseEditor();
            this.gridView1.UpdateCurrentRow();            
            this.recibosBindingSource.EndEdit();
            this.recibosDetallesBindingSource.EndEdit();
            this.recibosPagosBindingSource.EndEdit();           
            this.Hide();
        }

        private void recibosPagosBindingSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            Calcular();
        }

        private void gridView3_ShownEditor(object sender, EventArgs e)
        {
            if (this.gridView3.FocusedColumn.FieldName.ToUpper() == "MONTO")
            {
                Calcular();
                this.gridView3.ActiveEditor.EditValue = (double)txtTotalPagar.Value - (double)txtTotalPagos.Value;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Calcular();
            this.gridView3.AddNewRow();
            RecibosPagos Pago = (RecibosPagos)this.recibosPagosBindingSource.Current;
            Pago.FormaPago = "EFECTIVO";
            Pago.Monto = (double)txtTotalPagar.Value - (double)txtTotalPagos.Value;
            this.recibosPagosBindingSource.EndEdit();
            Recibo.Efectivo = (double)this.txtTotalPagar.Value;
            Calcular();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Calcular();
            this.gridView3.AddNewRow();
            RecibosPagos Pago = (RecibosPagos)this.recibosPagosBindingSource.Current;
            Pago.FormaPago = "CHEQUE";
            Pago.Monto = (double)txtTotalPagar.Value - (double)txtTotalPagos.Value; 
            this.recibosPagosBindingSource.EndEdit();
            Recibo.Cheques = (double)this.txtTotalPagar.Value;
            Calcular();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Calcular();
            this.gridView3.AddNewRow();
            RecibosPagos Pago = (RecibosPagos)this.recibosPagosBindingSource.Current;
            Pago.FormaPago = "TARJETAS";
            Pago.Monto = (double)txtTotalPagar.Value - (double)txtTotalPagos.Value;
            this.recibosPagosBindingSource.EndEdit();
            Recibo.Tarjetas = (double)this.txtTotalPagar.Value;
            Calcular();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void txtRetencionISLR_Validating(object sender, CancelEventArgs e)
        {
            Calcular();
        }

        private void gridControl2_KeyDown(object sender, KeyEventArgs e)
        {
            if (gridView1.ActiveEditor == null)
            {
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Subtract)
                {
                    if (this.gridView1.IsFocusedView)
                    {
                        RecibosPagos Pago = (RecibosPagos)this.recibosPagosBindingSource.Current;
                        //try
                        //{
                        //    dc.DocumentosProductos.DeleteOnSubmit(Registro);
                        //}
                        //catch { }
                        this.recibosPagosBindingSource.Remove(Pago);
                    }
                }
            }

        }

    }
}
