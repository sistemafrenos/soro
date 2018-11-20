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
    public partial class FrmPedidosProveedor : Form
    {
        public FrmPedidosProveedor()
        {
            InitializeComponent();
        }
        List<VistaDocumento> Documentos = new List<VistaDocumento>();

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Busqueda();
        }

        private void Busqueda()
        {
            Documentos = FactoryDocumentos.Buscar(new SoroEntities(), this.txtBuscar.Text, "PEDIDO PROVEEDOR", true);
            this.bs.DataSource = Documentos;
            this.bs.ResetBindings(true);
        }

        private void bntNuevo_Click(object sender, EventArgs e)
        {
            FrmPedidosProveedorItem f = new FrmPedidosProveedorItem();
            f.Incluir();
            Busqueda();
        }

        private void FrmPedidosProveedor_Load(object sender, EventArgs e)
        {
            Busqueda();
        }

        private void Imprimir_Click(object sender, EventArgs e)
        {
             VistaDocumento Item = (VistaDocumento)bs.Current;
            if (Item == null)
            {
                return;
            }
            using (var dc = new SoroEntities())
            {
                Documentos Documento = FactoryDocumentos.Item(dc, Item.IdDocumento);
                FrmReportes f = new FrmReportes();
                f.PedidoProveedor(Documento);
            }
        }

        private void bntEliminar_Click(object sender, EventArgs e)
        {

            VistaDocumento Item = (VistaDocumento)bs.Current;
            if (Item == null)
            {
                return;
            }
            using (var dc = new SoroEntities())
            {
                Documentos Documento = FactoryDocumentos.Item(dc, Item.IdDocumento);
                Documento.Activo = false;
                dc.SaveChanges();
            }
            Busqueda();
        }
    }
}
