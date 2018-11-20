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
    public partial class FrmCobranzas : Form
    {
        public FrmCobranzas()
        {
            InitializeComponent();
        }       
        Entities Entities = new Entities();
        private void FrmCobranzas_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }
        private void CargarDatos()
        {
            List<Cliente> Clientes = FactoryTerceros.Buscar(Entities, "");
            this.bs.DataSource = Clientes;
            this.bs.ResetBindings(true);
        }
        private void bntNuevo_Click(object sender, EventArgs e)
        {
            RegistrarPagos();

        }

        private void gridControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
            RegistrarPagos();
        }
        private void RegistrarPagos()
        {
            FrmCuentasxCobrar f = new FrmCuentasxCobrar();
            Cliente cliente = (Cliente)this.bs.Current;
            f.Titular = cliente;
            f.ShowDialog();
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    cliente.SaldoDeudor = FactoryTerceros.SaldoPendiente(cliente.IdTercero);
                    Entities.SaveChanges();
                }
                catch
                { }
               // CargarDatos();

            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
         //   RegistrarPagos();
        }
    }
}
