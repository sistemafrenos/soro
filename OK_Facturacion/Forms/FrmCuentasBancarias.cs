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
    public partial class FrmCuentasBancarias : Form
    {
        public FrmCuentasBancarias()
        {
            InitializeComponent();
        }
        SoroEntities dc = new SoroEntities();
        List<Bancos> Libro = new List<Bancos>();
        private void FrmBancos_Load(object sender, EventArgs e)
        {
            Busqueda();
        }
        private void Busqueda()
        {
            dc = new SoroEntities();
            Libro = FactoryBancos.Buscar(dc,txtBuscar.Text);
            List<BancosMovimientos> movimientos = dc.BancosMovimientos.ToList();
            foreach (var item in Libro)
            {
                item.SaldoActual = item.SaldoInicial +
                   movimientos.Where(x => x.IdBanco == item.IdBanco).Sum(x => x.Haber).GetValueOrDefault(0)
                 - movimientos.Where(x => x.IdBanco == item.IdBanco).Sum(x => x.Debe).GetValueOrDefault(0);
            }
            dc.SaveChanges();
            this.bancosBindingSource.DataSource = Libro;

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
            FrmReportes F = new FrmReportes();
            F.CuentasBancarias(Libro);
        }

        private void Editar_Click(object sender, EventArgs e)
        {
            EditarRegistro();
        }
        private void AgregarRegistro()
        {
            FrmCuentasBancariasItem F = new FrmCuentasBancariasItem();
            F.ShowDialog();
            if (F.DialogResult == DialogResult.OK)
            {
                this.bancosBindingSource.Add(F.Registro);
            }
        }
        private void EditarRegistro()
        {
            FrmCuentasBancariasItem F = new FrmCuentasBancariasItem();
            Bancos Registro = (Bancos)this.bancosBindingSource.Current;
            if (Registro == null)
                return;
            F.Registro = Registro;
            F.ShowDialog();
            if (F.DialogResult == DialogResult.OK)
            {
                Registro = F.Registro;
                Busqueda();
            }
        }
        private void EliminarRegistro()
        {
            if (this.gridView1.IsFocusedView)
            {
                Bancos Registro = (Bancos)this.bancosBindingSource.Current;
                if (FactoryBancos.Eliminar(Registro))
                {
                    this.bancosBindingSource.Remove(Registro);
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
    }
}
