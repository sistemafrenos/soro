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
        SoroEntities db = new SoroEntities();
        List<RegistroPagos> Lista = new List<RegistroPagos>();
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
            var q = from p in db.RegistroPagos
                    orderby p.Fecha descending
                    where p.MontoPagado>0
                    select p;
            Lista = q.ToList();
            this.bs.DataSource = Lista;
            this.bs.ResetBindings(true);
        }

        private void bntEliminar_Click(object sender, EventArgs e)
        {
            RegistroPagos registro = (RegistroPagos)this.bs.Current;
            if (registro == null)
            {
                return;
            }
            db.Database.ExecuteSqlCommand(string.Format("Delete RegistroPago Where IdRegistroPago='{0}'", registro.IdRegistroPago));
   //         db.ExecuteCommand(string.Format("Delete PagosDetalles Where IdRegistroPago='{0}'", registro.IdRegistroPago));
            if (registro.Modulo.ToUpper().Contains("CXC"))
            {
                    var cuenta = (from p in db.Cuentas
                                  where p.IdDocumento == registro.IdDocumento
                                  select p).FirstOrDefault();
                    if (cuenta != null)
                    {
                        cuenta.Saldo = cuenta.Saldo + registro.MontoPagado;
                        if (cuenta.Monto < cuenta.Saldo)
                        {
                            cuenta.Saldo = cuenta.Monto;
                        }
                    }
            }
            if (registro.Modulo == "PEDIDO")
            {
                var pedido = (from p in db.Documentos
                              where p.IdDocumento == registro.IdDocumento
                              select p).FirstOrDefault();
                if (pedido != null)
                {
                    pedido.Saldo = pedido.Saldo + registro.MontoPagado;                   

                }
            }            
            var cliente = (from p in db.Terceros
                           where p.IdTercero == registro.IdTercero
                           select p).FirstOrDefault();
            cliente.MontoPendiente = FactoryCuentas.SaldoPendienteCliente(cliente.IdTercero);
            db.SaveChanges();
            Busqueda();
        }

        private void Imprimir_Click(object sender, EventArgs e)
        {
            RegistroPagos Item = (RegistroPagos)this.bs.Current;
            FrmReportes f = new FrmReportes();
            f.ReciboPago(Item);
        }
    }
}
