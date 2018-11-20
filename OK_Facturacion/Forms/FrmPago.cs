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
    public partial class FrmPago : Form
    {
        public FrmPago()
        {
            InitializeComponent();
        }

        public HK.Terceros Tercero = new Terceros();
        public RegistroPagos Pago = new RegistroPagos();
        public List<RegistroPagosDetalles> Detalles = new List<RegistroPagosDetalles>();
        SoroEntities db = new SoroEntities();
        private void Calcular()
        {
            bs.EndEdit();
            Pago.MontoPagado = Convert.ToDouble( Pago.Cheque )+ Convert.ToDouble( Pago.Cheque2) + Convert.ToDouble( Pago.Deposito) + Convert.ToDouble( Pago.Deposito2 ) + Convert.ToDouble( Pago.Efectivo ) + Pago.RetencionIVA.GetValueOrDefault(0)+Pago.RetencionISLR.GetValueOrDefault(0);
            Pago.SaldoPendiente = Pago.MontoPagar - Pago.MontoPagado;
            MontoPagadoSpinEdit.Value = (decimal)Pago.MontoPagado;
            Pago.Cambio = Pago.SaldoPendiente < 0?Pago.SaldoPendiente*-1:0;
        }
        private void FrmPago_Load(object sender, EventArgs e)
        {
            this.bs.DataSource = Pago;
            this.bs.ResetBindings(true);
            this.BancoCheque2ComboBoxEdit.Properties.Items.AddRange(cBasicas.Bancos().ToArray());
            this.BancoChequeComboBoxEdit.Properties.Items.AddRange(cBasicas.Bancos().ToArray());
            this.BancoDeposito2ComboBoxEdit.Properties.Items.AddRange(cBasicas.Bancos().ToArray());
            this.BancoDepositoComboBoxEdit.Properties.Items.AddRange(cBasicas.Bancos().ToArray());
            if (Tercero != null)
            {
                LeerTitular();
            }
            Calcular();
        }

        private void Control_Enter(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SpinEdit Editor = (DevExpress.XtraEditors.SpinEdit)sender;
            Editor.Value = Convert.ToDecimal(Pago.SaldoPendiente);
        }

        private void Control_Validating(object sender, CancelEventArgs e)
        {
            Calcular();
        }

        private void Salir()
        {
            bs.EndEdit();
          //  Pago.Activo = true;
            if (Pago.Efectivo > 0)
            {
                if (!Pago.Cambio.HasValue)
                {
                    Pago.Cambio = 0;
                }
                Pago.Detalles += string.Format("Efectivo:{0}", (Pago.Efectivo - Pago.Cambio).Value.ToString("N2"));
            }
            if(Pago.Cheque>0)
            {
                Pago.Detalles += string.Format("CH:{0} BANCO:{1} NUM:{2}", Pago.Cheque.Value.ToString("N2"),Pago.BancoCheque,Pago.NumeroCheque);
            }
            if (Pago.Cheque2 > 0)
            {
                Pago.Detalles += string.Format("CH:{0} BANCO:{1} NUM:{2}", Pago.Cheque2.Value.ToString("N2"), Pago.BancoCheque2, Pago.NumeroCheque2);
            }
            if (Pago.Deposito > 0)
            {
                Pago.Detalles += string.Format("DP:{0} BANCO:{1} NUM:{2}", Pago.Deposito.Value.ToString("N2"), Pago.Deposito, Pago.NumeroDeposito);
            }
            if (Pago.Deposito2 > 0)
            {
                Pago.Detalles += string.Format("DP:{0} BANCO:{1} NUM:{2}", Pago.Deposito2.ToString(), Pago.Deposito2, Pago.NumeroDeposito2);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void Aceptar_Click(object sender, EventArgs e)
        {           
            if( string.IsNullOrEmpty( Tercero.IdTercero ) )
            {
                return;
            }
            if (Pago.SaldoPendiente > 0)
            {
                MessageBox.Show("El Monto de pagos no cubre lo que selecciono para pagar", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Pago.Cambio > 0)
            {
                MessageBox.Show(string.Format("Su Cambio son: {0}",Pago.Cambio.Value.ToString()), "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information);                    
            }
            Pago.IdTercero = Tercero.IdTercero;
            //Pago.Activo = true;
            //Pago.Enviado = false;
            Pago.RazonSocial = Tercero.RazonSocial;
            Pago.Fecha = DateTime.Today;
            Pago.Documento = this.txtFacturas.Text;
            Pago.IdRegistroPago = FactoryContadores.GetLast("IdRegistroPago");
            if(Pago.Efectivo>0)
            {
                Pago.Detalles =  string.Format("EF:{0}",Pago.Efectivo.Value.ToString("N2"));
            }
            if(Pago.Cheque>0)
            {
                Pago.Detalles = Pago.Detalles + " " +string.Format("CH:{0} {1} {2}", Pago.BancoCheque,Pago.NumeroCheque,Pago.Cheque.Value.ToString("N2"));
            }
            if (Pago.Cheque2 > 0)
            {
                Pago.Detalles = Pago.Detalles + " " + string.Format("CH:{0} {1} {2}", Pago.BancoCheque2, Pago.NumeroCheque2, Pago.Cheque2.Value.ToString("N2"));
            }
            if (Pago.Deposito > 0)
            {
                Pago.Detalles = Pago.Detalles + " " + string.Format("DP:{0} {1} {2}", Pago.BancoDeposito, Pago.NumeroDeposito, Pago.Deposito.Value.ToString("N2"));
            }
            if (Pago.Deposito2 > 0)
            {
                Pago.Detalles = Pago.Detalles + " " + string.Format("DP:{0} {1} {2}", Pago.BancoDeposito2, Pago.NumeroDeposito2, Pago.Deposito2.Value.ToString("N2"));
            }
            db.RegistroPagos.Add(Pago);
            db.SaveChanges();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        private void EscribirDatos()
        {
            bs.EndEdit();
        }
        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FrmPago_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void txtEfectivo_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtEfectivo_DoubleClick(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SpinEdit Editor = (DevExpress.XtraEditors.SpinEdit)sender;
            Editor.Value = Convert.ToDecimal(Pago.SaldoPendiente);
        }

        private void txtCedulaRif_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.TextEdit Editor = (DevExpress.XtraEditors.TextEdit)sender;
            if (!Editor.IsModified)
                return;
            string Texto = Editor.Text;
            List<Terceros> T = FactoryTerceros.Buscar(db,Texto);
            switch (T.Count)
            {
                case 0:
                    Tercero = new Terceros();
                    Tercero.CedulaRif = cBasicas.CedulaRif(Editor.Text);
                    break;
                case 1:
                    Tercero = T[0];
                    Tercero = FactoryTerceros.Item(Tercero.IdTercero);
                    break;
                default:
                    FrmBuscarEntidades F = new FrmBuscarEntidades();
                    F.Texto = Texto;
                    F.myLayout = "TERCEROS";
                    F.Filtro = "";
                    F.ShowDialog();
                    if (F.Registro != null)
                    {
                        Tercero = (Terceros)F.Registro;
                        Tercero = FactoryTerceros.Item(Tercero.IdTercero);
                    }
                    else
                    {
                        Tercero = new Terceros();
                    }
                    break;
            }
            LeerTitular();
            Editor.Text = Tercero.CedulaRif;

        }
        private void BuscarTitulares_Click(object sender, EventArgs e)
        {
            FrmBuscarEntidades F = new FrmBuscarEntidades();
            F.Texto = "";
            F.myLayout = "TERCEROS";
            F.Filtro = "";
            F.ShowDialog();
            if (F.Registro != null)
            {
                Tercero = (Terceros)F.Registro;
                Tercero = FactoryTerceros.Item(Tercero.IdTercero);
                LeerTitular();
            }
            else
            {
                Tercero = new Terceros();
                LeerTitular();
            }
        }
        private void LeerTitular()
        {
            if (Tercero == null)
            {
                Tercero = new Terceros();
            }
            this.txtCedulaRif.Text = Tercero.CedulaRif;
            this.txtRazonSocial.Text = Tercero.RazonSocial;            
            //if (Tercero.SaldoDeudor.HasValue)
            //{
            //    this.SaldoDeudorTextEdit.Text = Tercero.SaldoDeudor.Value.ToString("n2");
            //}

        }

        private void spinEdit1_Enter(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SpinEdit Editor = (DevExpress.XtraEditors.SpinEdit)sender;
            Editor.Value = Convert.ToDecimal(Pago.SaldoPendiente);
        }
    }
}
