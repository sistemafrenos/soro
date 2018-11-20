using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Linq;

namespace HK.Forms
{
    public partial class FrmRevisarPagos : Form
    {
        Entities Entities = new Entities();
        List<RegistroPago> Lista = new List<RegistroPago>();
        public FrmRevisarPagos()
        {
            InitializeComponent();
        }

        private void FrmRevisarPagos_Load(object sender, EventArgs e)
        {
            Busqueda();
        }

        private void Busqueda()
        {
            var q = from p in Entities.RegistroPagos
                    where p.Activo == true
                    orderby p.Fecha descending
                    select p;
            Lista = q.ToList();
            this.bs.DataSource = Lista;
            this.bs.ResetBindings(true);
        }

        private void bntEliminar_Click(object sender, EventArgs e)
        {
            RegistroPago registro = (RegistroPago)this.bs.Current;
            if (registro == null)
            {
                return;
            }
            Entities.Database.ExecuteSqlCommand(string.Format("Delete RegistroPagos Where IdRegistroPago='{0}'", registro.IdRegistroPago));
   //         Entities.ExecuteCommand(string.Format("Delete PagosDetalles Where IdRegistroPago='{0}'", registro.IdRegistroPago));
            if (registro.Modulo == "CXC")
            {
                foreach (PagosDetalle pagodetalle in Entities.PagosDetalles.Where(x => x.IdRegistroPago == registro.IdRegistroPago))
                {
                    var cuenta = (from p in Entities.Cuentas
                                  where p.IdDocumento == pagodetalle.IdDocumento
                                  select p).FirstOrDefault();
                    if (cuenta != null)
                    {
                        cuenta.Saldo = cuenta.Saldo + pagodetalle.Monto;
                        if (cuenta.Monto > cuenta.Saldo)
                        {
                            cuenta.Saldo = cuenta.Monto;
                        }
                    }
                }
            }
            if (registro.Modulo == "PEDIDO")
            {
                var pedido = (from p in Entities.Pedidos
                              where p.IdPedido == registro.IdDocumento
                              select p).FirstOrDefault();
                if (pedido != null)
                {
                    pedido.Saldo = pedido.Saldo + registro.MontoPagado;                   

                }
            }            
            var cliente = (from p in Entities.Clientes
                           where p.IdTercero == registro.IdTecero
                           select p).FirstOrDefault();
            cliente.SaldoDeudor = FactoryTerceros.SaldoPendiente(cliente.IdTercero);
            Entities.SaveChanges();
            Busqueda();
        }

        private void Imprimir_Click(object sender, EventArgs e)
        {
            this.gridControl1.ShowPrintPreview();
        }

        private void bntNuevo_Click(object sender, EventArgs e)
        {
            FrmPago F = new FrmPago();
            F.ShowDialog();
            if (F.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                Busqueda();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

        }

        private void bntEliminar_Click_1(object sender, EventArgs e)
        {
            RegistroPago Pago = (RegistroPago)this.bs.Current;
            if (Pago == null)
                return;
            if (MessageBox.Show("Esta seguro de eliminar este pago", "Atencion", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
            {
                return;
            }
            if (Pago.Enviado == true)
            {
                MessageBox.Show("Este pedido no se puede eliminar por ya estar enviado");
                return;
            }
            FactoryPagos.EliminarPago(Pago, Entities);
            Busqueda();
        }

        private void Imprimir_Click_1(object sender, EventArgs e)
        {
            this.gridControl1.ShowPrintPreview();
        }
    }
}
