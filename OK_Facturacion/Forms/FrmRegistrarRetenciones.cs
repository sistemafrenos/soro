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
    public partial class FrmRegistrarRetenciones : Form
    {
        public Retenciones Retencion;
        public Terceros Titular;
        SoroEntities dc = new SoroEntities();
        public FrmRegistrarRetenciones()
        {
            InitializeComponent();
        }

        private void FrmRegistrarPagos_Load(object sender, EventArgs e)
        {
            this.retencionesBindingSource.DataSource = this.Retencion;
            this.retencionesBindingSource.ResetBindings(true);
            this.retencionesDetallesBindingSource.DataSource = this.Retencion.RetencionesDetalles;
            this.retencionesDetallesBindingSource.ResetBindings(true);
            Titular = FactoryTerceros.Item(Retencion.IdTercero);
            this.txtRazonSocial.Text = Titular.RazonSocial;
            Calcular();
        }

        private void Calcular()
        {
            this.retencionesDetallesBindingSource.EndEdit();
            if (Retencion == null)
            {
                return;
            }
            double? Total = 0;
            foreach (RetencionesDetalles Item in Retencion.RetencionesDetalles)
            {
                Total += Item.MontoRetenido;
            }
            Retencion.Monto = Total;
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.retencionesBindingSource.EndEdit();
            this.retencionesDetallesBindingSource.EndEdit();
            this.Hide();
        }

        private void recibosPagosBindingSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            Calcular();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Retencion = null;
            this.Hide();
        }

    }
}
