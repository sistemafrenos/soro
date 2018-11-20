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
    public partial class FrmPagoProveedor : Form
    {
        public FrmPagoProveedor()
        {
            InitializeComponent();
        }
        SoroEntities dc= new SoroEntities();
        private Bancos banco = new Bancos();
        public RegistroPagoProveedor Pago = new RegistroPagoProveedor();
        private void Calcular()
        {
            bs.EndEdit();
            Pago.MontoPagado = Pago.Efectivo.GetValueOrDefault(0) + Pago.Cheque.GetValueOrDefault(0) + Pago.RetencionISLR.GetValueOrDefault(0) + Pago.RetencionIVA.GetValueOrDefault(0);
            Pago.SaldoPendiente = Pago.MontoPagar - Pago.MontoPagado;
            txtSaldo.Value = (decimal)Pago.SaldoPendiente;
            if (Pago.SaldoPendiente == 0)
            {
                this.Aceptar.Enabled = true;
            }

        }
        private void FrmPago_Load(object sender, EventArgs e)
        {
            this.txtBancoCheque.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(txtBancoCheque_ButtonClick);
            this.bs.DataSource = Pago;
            this.bs.ResetBindings(true);
            this.txtMontoPagar.Value = (decimal)Pago.MontoPagar;
            Calcular();
            banco = FactoryBancos.Item(dc, Pago.IdBanco);
            if (banco == null)
            {
                banco = new Bancos();
                this.txtBancoCheque.Text = "";
            }
            else
            {
                this.txtBancoCheque.Text = banco.Banco;
            }
        }

        void txtBancoCheque_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmBuscarEntidades F = new FrmBuscarEntidades();
            F.myLayout = "BANCOS";
            F.Filtro = "";
            F.dc = dc;
            F.ShowDialog();
            if (F.Registro != null)
            {
                banco = (Bancos)F.Registro;
                Pago.IdBanco = banco.IdBanco;
                txtBancoCheque.Text = banco.Banco;
            }
            else
            {
                Pago.IdBanco = null;
                txtBancoCheque.Text = "";
            }
        }
        public void Mostrar()
        {

            this.ShowDialog();
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
        private void Aceptar_Click(object sender, EventArgs e)
        {
            if (banco != null)
            {
                Pago.IdBanco = banco.IdBanco;
                Pago.BancoCheque = banco.Banco;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
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
        private void txtEfectivo_DoubleClick(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SpinEdit Editor = (DevExpress.XtraEditors.SpinEdit)sender;
            Editor.Value = Convert.ToDecimal(Pago.SaldoPendiente);
        }
        private void txtBancoCheque_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.TextEdit Editor = (DevExpress.XtraEditors.TextEdit)sender;
            if (!Editor.IsModified)
                return;
            string Texto = Editor.Text;
            List<Bancos> T = FactoryBancos.Buscar(Texto);           
            switch (T.Count)
            {
                case 0:
                    Pago.IdBanco = null;
                    Editor.Text = "";
                    break;
                case 1:
                    banco = T[0];
                    Pago.IdBanco = banco.IdBanco;
                    Editor.Text = banco.Banco;
                    break;
                default:
                    FrmBuscarEntidades F = new FrmBuscarEntidades();
                    F.Texto = Texto;
                    F.myLayout = "BANCOS";
                    F.Filtro = "";
                    F.dc = dc;
                    F.ShowDialog();
                    if (F.Registro != null)
                    {
                        banco = (Bancos)F.Registro;
                        Pago.IdBanco = banco.IdBanco;
                        Editor.Text = banco.Banco;
                    }
                    else
                    {
                        Pago.IdBanco = null;
                        Editor.Text = "";
                    }
                    break;
            }
        }
    }
}
