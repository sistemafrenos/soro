using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Linq;

namespace HK
{
    public partial class FrmCobranzas : Form
    {
        public FrmCobranzas()
        {
            InitializeComponent();
        }
        SoroEntities db = new SoroEntities();
        List<TercerosConSaldos> Lista = new List<TercerosConSaldos>();
        
        private void FrmCobranzas_Load(object sender, EventArgs e)
        {
            CargarDatos();
            this.txtBuscar.KeyDown += new KeyEventHandler(txtBuscar_KeyDown);
            this.txtBuscar.Validating += new CancelEventHandler(txtBuscar_Validating);
        //    this.toolStripActualizarSaldos.Click += new EventHandler(toolStripActualizarSaldos_Click);
            this.Activated += new EventHandler(FrmCobranzas_Activated);
        }

        //void toolStripActualizarSaldos_Click(object sender, EventArgs e)
        ////{
        ////    foreach(var item in db.Terceros)

        ////    double? CargosaLaFecha = (from x in db.VistaDocumentos
        ////                              where x.IdTercero == cliente.IdTercero && x.Fecha < txtDesde.DateTime
        ////                              select x.MontoTotal).Sum();
        ////    double? PagosaLaFecha = (from x in db.RegistroPagos
        ////                             where x.IdTercero == cliente.IdTercero && x.Fecha < txtDesde.DateTime
        ////                             select x.MontoPagado).Sum();

        //}

        void FrmCobranzas_Activated(object sender, EventArgs e)
        {
            this.txtBuscar.Text = "";
            this.txtBuscar.Focus();
        }

        void txtBuscar_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.TextEdit Editor = (DevExpress.XtraEditors.TextEdit)sender;
            if (!Editor.IsModified)
                return;
            
        }

        void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                CargarDatos();
            }
        }
        private void CargarDatos()
        {
            
            if (this.txtFiltro.Text == "CLIENTES CON SALDO PENDIENTE")           
            {
                Lista = db.TercerosConSaldos.Where(x => x.TipoCuenta == "CXC" && x.MontoPendiente>0).OrderBy(x => x.RazonSocial).ToList();
                
            }
            else
            {
                Lista = db.TercerosConSaldos.Where(x=>x.Tipo=="CLIENTE").OrderBy(x => x.RazonSocial).ToList();
            }
            if (!string.IsNullOrEmpty(txtBuscar.Text))
            {
                Lista = (from p in Lista
                         where p.RazonSocial.Contains(txtBuscar.Text)
                         select p).ToList();
            }
            if (Lista.Count() > 0)
            {
                this.bs.DataSource = Lista;
                this.bs.ResetBindings(true);
                this.gridControl1.Focus();
            }
            else
            {
                txtBuscar.Text = "";
            }
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
            TercerosConSaldos cliente = (TercerosConSaldos)this.bs.Current;
            if (cliente == null)
                return;
                f.Titular = FactoryTerceros.Item(cliente.IdTercero);
            f.ShowDialog();
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
            {              
               CargarDatos();
            }
            this.txtBuscar.Focus();
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
                f.EstadoDeCuentaxCobrar(Titular);
            }

        }
        private void listadoDeCuentasXCobrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmReportes f = new FrmReportes();
            f.ListadoCuentasCxC();
        }
        private void listadoDeTotalesXCobrarXClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmReportes f = new FrmReportes();
            f.ResumenCxC();
        }

        private void movimientosCuentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmReportes f = new FrmReportes();
            f.cliente=(TercerosConSaldos)this.bs.Current;
            f.ReporteTercerosMovimientos();
        }

        private void txtBuscar_Enter(object sender, EventArgs e)
        {
            this.txtBuscar.SelectAll();
        }
    }
}
