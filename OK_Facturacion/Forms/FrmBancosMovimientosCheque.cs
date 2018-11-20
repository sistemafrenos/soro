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
    public partial class FrmBancosMovimientosCheque : Form
    {
        public FrmBancosMovimientosCheque()
        {
            InitializeComponent();
        }
        public Bancos Banco;
        public Terceros Titular = new Terceros();
        SoroEntities dc = new SoroEntities();
        public BancosMovimientos Registro = null;
        public int Año = 0;
        public int Mes = 0;
        public bool EstaPagando = false;
        private void Form_Load(object sender, EventArgs e)
        {
            if (Registro == null)
            {
                Registro = new BancosMovimientos();
                Registro.Fecha = DateTime.Today;
                Registro.Debe = 0;
                Registro.Haber = 0;
            }
            else
            {
                if (Banco != null)
                {
                    this.Text = "Editar Cheque";
                    Registro = FactoryBancos.ItemMovimiento(dc, Registro.IdMovimiento);
                    Titular = FactoryTerceros.Item(Registro.IdBeneficiario);
                    if (Titular != null)
                    {
                        txtTitular.Text = Titular.CedulaRif;
                    }
                }
                else
                {
 
                }
            }
            this.bancosMovimientosBindingSource.DataSource = Registro;
            this.bancosMovimientosBindingSource.ResetBindings(true);
            if (Banco != null)
            {
                this.bancosBindingSource.DataSource = Banco;
                this.bancosBindingSource.ResetBindings(true);
            }
            else
            {
                this.txtBanco.TabStop = true;
                this.txtBanco.Properties.ReadOnly = false;
                this.txtBanco.BackColor = this.spinEdit1.BackColor;
                this.txtBanco.TabIndex = 0;
            }
            if (Titular != null)
            {
               // Pruebas
              //  string s = "x";
            }
        }
        private void Busqueda()
        {
            Banco = new Bancos();
            dc = new SoroEntities();
            List<Bancos> Bancos = FactoryBancos.Buscar(txtBanco.Text);
            switch (Bancos.Count)
            {
                case 0:
                    MessageBox.Show("Banco o numero de cuenta no encontrado", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case 1:
                    Banco = Bancos[0];
                    break;
                default:
                    FrmBuscarEntidades F = new FrmBuscarEntidades();
                    F.Texto = txtBanco.Text;
                    F.myLayout = "BANCOS";
                    F.dc = dc;
                    F.ShowDialog();
                    if (F.DialogResult == DialogResult.OK)
                    {
                        Banco = (Bancos)F.Registro;
                    }
                    break;
            }
            this.bancosBindingSource.DataSource = Banco;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {

            this.bancosMovimientosBindingSource.EndEdit();
            Registro.Tipo = "CH";
            if (string.IsNullOrEmpty(Registro.Numero))
            {
                MessageBox.Show("No puede estar vacio el numero", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Banco == null)
            {
                MessageBox.Show("Debe elejir el banco para registrar el cheque", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }
            Registro.IdBanco = Banco.IdBanco;

            if (string.IsNullOrEmpty(Banco.IdBanco))
            {
                MessageBox.Show("No puede estar vacio el banco y la cuenta", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(Registro.Beneficiario))
            {
                MessageBox.Show("No puede estar vacio el beneficiario", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Titular != null)
            {
                if (string.IsNullOrEmpty(Titular.IdTercero) && !string.IsNullOrEmpty(txtTitular.Text))
                {
                    FactoryTerceros.Guardar(Titular);
                }
                Registro.IdBeneficiario = Titular.IdTercero;
            }
            if (FactoryBancos.MovimientoDuplicado(Registro))
            {
                MessageBox.Show("Error este cheque ya esta registrado", "Verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (!EstaPagando)
                {
                    return;
                }

            }
            if (!EstaPagando)
            {
                if (Registro.Fecha.Value.Month != Mes || Registro.Fecha.Value.Year != Año)
                {
                    MessageBox.Show("Esta fecha no corresponde al mes o el año con el que se esta trabajando", "Verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            FactoryBancos.GuardarMovimiento(dc, Registro);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void LeerTitular()
        {
            Registro.IdBeneficiario = Titular.IdTercero;
            Registro.Beneficiario = Titular.RazonSocial;
            this.txtTitular.Text = Titular.CedulaRif;
        }

        private void txtBanco_Validating(object sender, CancelEventArgs e)
        {
            if (!txtBanco.IsModified)
                return;
            Busqueda();
        }

        private void txtTitular_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.TextEdit Editor = (DevExpress.XtraEditors.TextEdit)sender;
            if (!Editor.IsModified)
                return;            
            string Texto = Editor.Text;
            List<Terceros> T = FactoryTerceros.Buscar(Texto);
            switch (T.Count)
            {
                case 0:
                    Titular = new Terceros();
                    Editor.Text = cBasicas.CedulaRif(Editor.Text);
                    Titular.CedulaRif = Editor.Text;
                    break;
                case 1:
                    Titular = T[0];
                    Titular = FactoryTerceros.Item(Titular.IdTercero);
                    break;
                default:
                    FrmBuscarEntidades F = new FrmBuscarEntidades();
                    F.Texto = Texto;
                    F.myLayout = "TERCEROS";
                    F.Filtro = "";
                    F.dc = dc;
                    F.ShowDialog();
                    if (F.Registro != null)
                    {
                        Titular = (Terceros)F.Registro;
                        Titular = FactoryTerceros.Item(Titular.IdTercero);
                    }
                    else
                    {
                        Titular = new Terceros();
                    }
                    break;
            }
            LeerTitular();
        }
        private void BuscarTitulares_Click(object sender, EventArgs e)
        {
            FrmBuscarEntidades F = new FrmBuscarEntidades();
            F.Texto = "";
            F.myLayout = "TERCEROS";
            F.Filtro = "";
            F.dc = dc;
            F.ShowDialog();
            if (F.Registro != null)
            {
                Titular = (Terceros)F.Registro;
                Titular = FactoryTerceros.Item(Titular.IdTercero);
                LeerTitular();
            }
            else
            {
                Titular = new Terceros();
                LeerTitular();
            }
        }


    }
}
