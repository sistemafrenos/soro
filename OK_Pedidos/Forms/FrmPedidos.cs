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
    public partial class FrmPedidos : Form
    {
        public FrmPedidos()
        {
            InitializeComponent();
        }
        Entities Entities = new Entities();
        List<Pedido> Lista = new List<Pedido>();
        private void FrmPedidos_Load(object sender, EventArgs e)
        {
            Busqueda();
        }
        private void Busqueda()
        {
            Entities  = new Entities();
            Lista = Entities .Pedidos.OrderByDescending(x => x.Numero).ToList();
            this.bs.DataSource = Lista;
        }

        private void bntNuevo_Click(object sender, EventArgs e)
        {
            FrmPedidosItem f = new FrmPedidosItem();
            f.Incluir();
            Busqueda();
        }

        private void bntEliminar_Click(object sender, EventArgs e)
        {
            Pedido Pedido = (Pedido)this.bs.Current;
            if (Pedido == null)
                return;
            if (MessageBox.Show("Esta seguro de eliminar este pedido(s)", "Atencion", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
            {
                return;
            }
            if (Pedido.Enviado == true)
            {
                MessageBox.Show("Este pedido no se puede eliminar por ya estar enviado");
                return;
            }
            FactoryPedidos.EliminarPedido(Pedido, Entities );
            Busqueda();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Busqueda();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Pedido P = (Pedido)this.bs.Current;
            if (P == null)
                return;
            FrmPedidosItem f = new FrmPedidosItem();
            if (P.Enviado == true)
            {
                MessageBox.Show("Este pedido ya fue enviado, se creara una copia del mismo");
                f.Copiar(P.IdPedido);
            }
            else
            {
                f.Modificar(P.IdPedido);
            }
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                Busqueda();
            }
        }
    }
}