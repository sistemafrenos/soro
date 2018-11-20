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
    public partial class FrmBancosMovimientosNotaCredito : Form
    {
        public FrmBancosMovimientosNotaCredito()
        {
            InitializeComponent();
        }
        public Bancos Banco;
        Terceros Titular = new Terceros();
        SoroEntities dc = new SoroEntities();
        public BancosMovimientos Registro = null;
        public int Año = 0;
        public int Mes = 0;
        private void Form_Load(object sender, EventArgs e)
        {
            if (Registro == null)
            {
                Registro = new BancosMovimientos
                {
                    Fecha = DateTime.Today,
                    Debe = 0,
                    Haber = 0
                };
            }
            else
            {
                this.Text = "Editar Nota Credito";
                Registro = FactoryBancos.ItemMovimiento(dc, Registro.IdMovimiento);
            }
            this.bancosMovimientosBindingSource.DataSource = Registro;
            this.bancosMovimientosBindingSource.ResetBindings(true);
            if (Banco != null)
            {
                this.bancosBindingSource.DataSource = Banco;
                this.bancosBindingSource.ResetBindings(true);
            }
        }
        private void SimpleButton1_Click(object sender, EventArgs e)
        {
            this.bancosMovimientosBindingSource.EndEdit();
            Registro.Tipo = "NC";
            Registro.IdBanco = Banco.IdBanco;
            if (string.IsNullOrEmpty(Registro.Numero))
            {
                MessageBox.Show("No puede estar vacio el numero", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (FactoryBancos.MovimientoDuplicado(Registro))
            {
                MessageBox.Show("Error esta Nota de credito ya esta registrada", "Verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Registro.Fecha.Value.Month != Mes || Registro.Fecha.Value.Year != Año)
            {
                MessageBox.Show("Esta fecha no corresponde al mes o el año con el que se esta trabajando", "Verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FactoryBancos.GuardarMovimiento(dc, Registro);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
