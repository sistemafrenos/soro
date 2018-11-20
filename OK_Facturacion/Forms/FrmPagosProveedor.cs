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
    public partial class FrmPagosProveedor : Form
    {
        public FrmPagosProveedor()
        {
            InitializeComponent();
        }

        SoroEntities db = new SoroEntities();
        List<TercerosConSaldos> Lista = new List<TercerosConSaldos>();

        private void FrmCobranzas_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }
        private void CargarDatos()
        {

            if (this.txtFiltro.Text == "PROVEEDORES CON SALDO PENDIENTE")
            {
                Lista = db.TercerosConSaldos.Where(x => x.TipoCuenta == "CXP" && x.MontoPendiente > 0).OrderBy(x => x.RazonSocial).ToList();

            }
            else
            {
                Lista = db.TercerosConSaldos.Where(x=> x.Tipo=="PROVEEDOR").OrderBy(x => x.RazonSocial ).ToList();
            }
            this.bs.DataSource = Lista;
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
            AbrirProveedor();
        }

        private void AbrirProveedor()
        {
            FrmCuentasxPagar f = new FrmCuentasxPagar();
            TercerosConSaldos cliente = (TercerosConSaldos)this.bs.Current;
            f.Titular = FactoryTerceros.Item(cliente.IdTercero);
            f.ShowDialog();
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                CargarDatos();
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            RegistrarPagos();
        }

        private void botonBuscar_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void estadoCuentaClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TercerosConSaldos Registro = (TercerosConSaldos)this.bs.Current;
            Terceros Titular = FactoryTerceros.Item(Registro.IdTercero);
            if (Titular != null)
            {
                FrmReportes f = new FrmReportes();
                f.EstadoDeCuentaxPagar(Titular);
            }
        }
        private void listadoDeCuentasXCobrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmReportes f = new FrmReportes();
            f.ListadoCuentasCxP();
        }
        private void listadoDeTotalesXCobrarXClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmReportes f = new FrmReportes();
            f.ResumenCxP();
        }

        private void botonBuscar_Click_1(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void gridControl1_DoubleClick_1(object sender, EventArgs e)
        {
            AbrirProveedor();
        }

        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                AbrirProveedor();
            }
        }

    }
}
