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
    public partial class FrmGuiaCarga : Form
    {
        Entities dc = new Entities();
        Pedido Pedido = new Pedido();
        List<Pedido> Lista = new List<Pedido>();
        List<Pedido> Facturas = new List<Pedido>();
        public FrmGuiaCarga()
        {
            InitializeComponent();
        }
        private void FrmGuiaCarga_Load(object sender, EventArgs e)
        {
            var q = from p in dc.Pedidos
                    orderby p.Numero descending
                    select p;
            Facturas = q.ToList();
            this.bs.DataSource = Facturas ;
            this.bs.ResetBindings(true);
            this.PedidoBindingSource.DataSource = Lista;
            this.PedidoBindingSource.ResetBindings(true);
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FrmReportes F = new FrmReportes();
            F.GuiaDeCarga(Lista);
            this.Close();
        }
        private void CargarFactura()
        {
            Pedido = (Pedido)this.bs.Current;
            Pedido doc = new Pedido();            
            if (Pedido != null)
            {
                if (Lista.FirstOrDefault(x => x.IdPedido == Pedido.IdPedido) == null)
                { 
                    Lista.Add(Pedido);
                    this.PedidoBindingSource.ResetBindings(true);
                }
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridControl2_DoubleClick(object sender, EventArgs e)
        {
            CargarFactura();
        }

        private void gridControl2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                CargarFactura();
            }
        }

        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                gridView1.MoveBy(0);
            }
            if (gridView1.ActiveEditor == null)
            {
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Subtract)
                {
                    if (this.gridView1.IsFocusedView)
                    {
                        Pedido Registro = (Pedido)this.PedidoBindingSource.Current;
                        this.PedidoBindingSource.Remove(Registro);
                    }
                    e.Handled = true;
                }
            }
        }
    }
}
