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
    public partial class FrmCuentasBancariasItem : Form
    {
        public FrmCuentasBancariasItem()
        {
            InitializeComponent();
        }
        SoroEntities dc = new SoroEntities();
        public Bancos Registro = null;
        private void Form_Load(object sender, EventArgs e)
        {
            if (Registro == null)
            {
                Registro = new Bancos();
                Registro.FechaApertura = DateTime.Today;
                Registro.SaldoActual = 0;
                Registro.SaldoInicial = 0;
                Registro.Activo = true;
            }
            else
            {
                this.Text = "Editar Cuenta Bancaria";
                Registro = FactoryBancos.Item(dc, Registro.IdBanco);
            }
            this.bancosBindingSource.DataSource = Registro;
            this.bancosBindingSource.ResetBindings(true);
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.bancosBindingSource.EndEdit();
            if (string.IsNullOrEmpty(Registro.Banco))
            {
                MessageBox.Show("No puede estar vacio el banco", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(Registro.Cuenta))
            {
                MessageBox.Show("No puede estar vacia la cuenta", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (FactoryBancos.ItemDuplicado(Registro.IdBanco, Registro.Cuenta))
            {
                MessageBox.Show("Error esta cuenta ya esta registrada", "Verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FactoryBancos.Guardar(dc, Registro);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();                
        }          
    }
}
