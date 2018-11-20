using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HK.Forms
{
    public partial class FrmAplicarPagosExternos : Form
    {
        SoroEntities db  = new SoroEntities();
        List<RegistroPagosExternos> Lista = new List<RegistroPagosExternos>();
        public FrmAplicarPagosExternos()
        {
            InitializeComponent();
        }

        private void FrmAplicarPagosExternos_Load(object sender, EventArgs e)
        {
            Busqueda();
        }
        private void Busqueda()
        {
            var Consulta = from Item in db.RegistroPagosExternos
                           where Item.Aplicado == null
                           orderby Item.Fecha,Item.Equipo
                           select Item;
            Lista = Consulta.ToList();
            this.bs.DataSource = Lista;
            this.bs.ResetBindings(true);
        }
        private void BuscarTodos()
        {
            db = new SoroEntities();
            var Consulta = from Item in db.RegistroPagosExternos
                           orderby Item.Fecha, Item.Equipo
                           select Item;
            Lista = Consulta.ToList();
            this.bs.DataSource = Lista;
            this.bs.ResetBindings(true);
        }
        private void bntNuevo_Click(object sender, EventArgs e)
        {
            this.gridView1.EndInit();
            foreach (int id in this.gridView1.GetSelectedRows())            
            {
                RegistroPagosExternos Item = (RegistroPagosExternos)this.bs.List[id];
                try
                {
                AplicarPago(Item);
                Item.Aplicado = true;
                db.SaveChanges();
                }
                catch(Exception x )
                {
                    MessageBox.Show(x.Message,"Error al aplicar pagos", MessageBoxButtons.OK, MessageBoxIcon.Error);  
                }
            }
            Busqueda();
        }
        private void AplicarPago(RegistroPagosExternos Item)
        {
            if (Item.Aplicado == true)
                return;
            foreach(RegistroPagosExternosDetalles Detalle in Item.RegistroPagosExternosDetalles)
            {
                Cuentas Cuenta = FactoryCuentas.Item(db, Detalle.IDDocumento);
                if (Cuenta != null)
                {
                    Cuenta.PagarHoy = Detalle.Monto;
                    Cuenta.Saldo = Cuenta.Saldo - Detalle.Monto;                
                }
                RegistroPagosDetalles newDetalle = new RegistroPagosDetalles();
                newDetalle.IdDocumento = Detalle.IDDocumento;
                newDetalle.Monto = Detalle.Monto;
                newDetalle.Numero = Detalle.Numero;
                newDetalle.Tipo = "FACTURA";
                newDetalle.IdRegistroPagosDetalle = FactoryContadores.GetLast("IdRegistroPagosDetalle");
                db.RegistroPagosDetalles.Add(newDetalle);
            }
        }

        private void Imprimir_Click(object sender, EventArgs e)
        {
            FrmReportes f = new FrmReportes();
            f.RegistroPagosExternos();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            RegistroPagosExternos Item = (RegistroPagosExternos)this.bs.Current;
            FrmReportes f = new FrmReportes();
            f.ReciboPagoExterno(Item);
          
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            BuscarTodos();
        }
    }
}
