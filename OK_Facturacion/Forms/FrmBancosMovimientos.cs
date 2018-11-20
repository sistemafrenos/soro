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
    public partial class FrmBancosMovimientos : Form
    {
        public FrmBancosMovimientos()
        {
            InitializeComponent();
        }

        SoroEntities dc = new SoroEntities();
        List<BancosMovimientos> Libro = new List<BancosMovimientos>();
        Bancos Banco = new Bancos();
        private void FrmBancos_Load(object sender, EventArgs e)
        {
            for (int mes = 1; mes <= 12; mes++)
            {
                this.txtMes.Items.Add(mes);
            }
            for (int año = DateTime.Today.Year - 5; año <= DateTime.Today.Year; año++)
            {
                this.txtAño.Items.Add(año);
            }
            this.txtAño.Text = DateTime.Today.Year.ToString();
            this.txtMes.Text = DateTime.Today.Month.ToString();
        }
        private void Busqueda()
        {
            dc = new SoroEntities();
            List<Bancos> Bancos = FactoryBancos.Buscar(txtBuscar.Text);
            List<BancosMovimientos> movimientos = dc.BancosMovimientos.ToList();
            foreach (var item in Bancos)
            {
                item.SaldoActual = item.SaldoInicial + 
                   movimientos.Where(x=>x.IdBanco==item.IdBanco).Sum(x => x.Haber).GetValueOrDefault(0)
                 - movimientos.Where(x =>x.IdBanco==item.IdBanco).Sum(x => x.Debe).GetValueOrDefault(0);
            }
            dc.SaveChanges();
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
                    F.Texto = txtBuscar.Text;
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
            Libro = FactoryBancos.BuscarMovimientos(dc, Banco,Convert.ToInt16( txtAño.Text),Convert.ToInt16(txtMes.Text));
            this.bancosMovimientosBindingSource.DataSource = Libro;
            Calcular();
            Cheque.Enabled = true;
            Deposito.Enabled = true;
            NotaCredito.Enabled = true;
            NotaDebito.Enabled = true;
            Imprimir.Enabled = true;
            btnEliminar.Enabled = true;
            this.txtBuscar.Text = Banco.Banco;
        }
        private void Calcular()
        {
            try
            {
                double? Saldo = FactoryBancos.SaldoInicioMes(Banco, Convert.ToInt16(this.txtMes.Text), Convert.ToInt16(txtAño.Text));
                this.txtSaldoInicial.EditValue = Saldo;
                this.txtSaldoFinal.EditValue = (double)txtSaldoInicial.EditValue
                                + Libro.Sum(x => x.Haber).GetValueOrDefault(0)
                                - Libro.Sum(x => x.Debe).GetValueOrDefault(0);
                foreach(var x in Libro)
                {
                    x.Saldo = Saldo + x.Haber.GetValueOrDefault() - x.Debe.GetValueOrDefault(0); 
                    Saldo = x.Saldo;
                }
                
            }
            catch (Exception x)
            {
                string s = x.Message;
            }
        }
        private void Buscar_Click(object sender, EventArgs e)
        {
            Busqueda();
        }


        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (gridView1.ActiveEditor == null)
            {
                if (e.KeyCode == Keys.Return)
                {
                    EditarRegistro();
                }
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Subtract)
                {
                    EliminarRegistro();
                }
                if (e.KeyCode == Keys.Insert)
                {
                    AgregarRegistro();
                }
            }

        }
        private void Imprimir_Click(object sender, EventArgs e)
        {
            if (Libro.Count > 0)
            {
                FrmReportes F = new FrmReportes();
                F.MovimientosBancarios(Banco, Libro);
            }
        }
        private void Editar_Click(object sender, EventArgs e)
        {
            EditarRegistro();
        }
        private void AgregarRegistro()
        {
            //FrmCuentasBancariasItem F = new FrmCuentasBancariasItem();
            //F.ShowDialog();
            //if (F.DialogResult == DialogResult.OK)
            //{
            //    this.bancosMovimientosBindingSource.Add(F.Registro);
            //    Calcular();
            //}
        }
        private void EditarRegistro()
        {
            BancosMovimientos Registro = (BancosMovimientos)this.bancosMovimientosBindingSource.Current;
            if (Registro == null)
                return;
            switch (Registro.Tipo)
            {
                case "CH":
                    EditarCheque(Registro);
                    break;
                case "DP":
                    EditarDeposito(Registro);
                    break;
                case "NC":
                    break;
                case "ND":
                    break;
            }
        }
        private void EliminarRegistro()
        {
            if (MessageBox.Show("Esta seguro de eliminar este registro", "Atencion", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            if (this.gridView1.IsFocusedView)
            {
                BancosMovimientos Registro = (BancosMovimientos)this.bancosMovimientosBindingSource.Current;
                if (Registro == null)
                    return;
                if (FactoryBancos.EliminarMovimiento(dc, Registro))
                {
                    this.bancosMovimientosBindingSource.Remove(Registro);
                    Busqueda();
                }
            }
        }
        private void Nuevo_Click(object sender, EventArgs e)
        {
            AgregarRegistro();
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }
        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Busqueda();
            }
        }
        private void Cheque_Click(object sender, EventArgs e)
        {
            EditarCheque(null);            
        }
        private void EditarCheque(BancosMovimientos Registro)
        {
            bool Nuevo = (Registro == null);
            FrmBancosMovimientosCheque F = new FrmBancosMovimientosCheque();
            F.Registro = Registro;
            F.Banco = Banco;
            F.Año = Convert.ToInt16(this.txtAño.Text);
            F.Mes = Convert.ToInt16(this.txtMes.Text);
            F.ShowDialog();
            if (F.DialogResult == DialogResult.OK)
            {
                if (Nuevo)
                {
                    this.bancosMovimientosBindingSource.Add(F.Registro);
                    Calcular();
                }
                Busqueda();              
            }
        }
        private void txtMes_Validated(object sender, EventArgs e)
        {
            try
            {
                int mes = Convert.ToInt16(this.txtMes.Text);
                if (mes < 1 || mes > 12)
                {
                    this.txtMes.Text = DateTime.Today.Month.ToString();
                }

            }
            catch 
            {
                this.txtMes.Text = DateTime.Today.Month.ToString();
            }
        }

        private void Deposito_Click(object sender, EventArgs e)
        {
            EditarDeposito(null);

        }
        private void EditarDeposito(BancosMovimientos Registro)
        {
            bool Nuevo = (Registro == null);
            FrmBancosMovimientosDeposito F = new FrmBancosMovimientosDeposito();
            F.Registro = Registro;
            F.Banco = Banco;
            F.Año = Convert.ToInt16(this.txtAño.Text);
            F.Mes = Convert.ToInt16(this.txtMes.Text);
            F.ShowDialog();
            if (F.DialogResult == DialogResult.OK)
            {
                if (Nuevo)
                {
                    this.bancosMovimientosBindingSource.Add(F.Registro);
                    Calcular();
                }
                Busqueda();
            }
        }

        private void NotaCredito_Click(object sender, EventArgs e)
        {
            EditarNotaCredito(null);
        }
        private void EditarNotaCredito(BancosMovimientos Registro)
        {
            bool Nuevo = (Registro == null);
            FrmBancosMovimientosNotaCredito F = new FrmBancosMovimientosNotaCredito();
            F.Registro = Registro;
            F.Banco = Banco;
            F.Año = Convert.ToInt16(this.txtAño.Text);
            F.Mes = Convert.ToInt16(this.txtMes.Text);
            F.ShowDialog();
            if (F.DialogResult == DialogResult.OK)
            {
                if (Nuevo)
                {
                    this.bancosMovimientosBindingSource.Add(F.Registro);
                    Calcular();
                }
                Busqueda();
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            EditarRegistro();
        }

        private void NotaDebito_Click(object sender, EventArgs e)
        {
            EditarNotaDebito(null);
        }
        private void EditarNotaDebito(BancosMovimientos Registro)
        {
            bool Nuevo = (Registro == null);
            FrmBancosMovimientosNotaDebito F = new FrmBancosMovimientosNotaDebito();
            F.Registro = Registro;
            F.Banco = Banco;
            F.Año = Convert.ToInt16(this.txtAño.Text);
            F.Mes = Convert.ToInt16(this.txtMes.Text);
            F.ShowDialog();
            if (F.DialogResult == DialogResult.OK)
            {
                if (Nuevo)
                {
                    this.bancosMovimientosBindingSource.Add(F.Registro);
                    Calcular();
                }
                Busqueda();
            }
        }


    }
}
