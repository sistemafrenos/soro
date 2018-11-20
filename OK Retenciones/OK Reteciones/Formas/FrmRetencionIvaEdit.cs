using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HK.Formas
{
    public partial class FrmRetencionIvaEdit : Form
    {
        public Retenciones registro;
        public FrmRetencionIvaEdit()
        {
            InitializeComponent();
            this.BaseImponibleCalcEdit.Validating += new CancelEventHandler(BaseImponibleCalcEdit_Validating);
            this.AlicuotaCalcEdit.Validating += new CancelEventHandler(AlicuotaCalcEdit_Validating);
            this.MontoExentoIvaCalcEdit.Validating += new CancelEventHandler(MontoExentoIvaCalcEdit_Validating);
            this.MontoIvaCalcEdit.Validating += new CancelEventHandler(MontoIvaCalcEdit_Validating);
            this.MontoIvaRetenidoCalcEdit.Validating += new CancelEventHandler(MontoIvaRetenidoCalcEdit_Validating);
            this.Aceptar.Click+=new EventHandler(Aceptar_Click);
            this.Cancelar.Click+=new EventHandler(Cancelar_Click);
            this.TipoOperacionTextEdit.Properties.Items.AddRange(new object[] { "01", "02" });
        }

        void MontoIvaRetenidoCalcEdit_Validating(object sender, CancelEventArgs e)
        {
            CalcularTotal();
        }

        void  Cancelar_Click(object sender, EventArgs e)
        {
 	        this.DialogResult= System.Windows.Forms.DialogResult.Cancel;
        }

        void  Aceptar_Click(object sender, EventArgs e)
        {
 	        this.bs.EndEdit();
            this.Calcular();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        public void Editar(Retenciones r)
        {
            this.registro = r;
            if (r.Alicuota == null)
            {
                registro.BaseImponible = 0;
                registro.MontoExentoIva = 0;
                registro.MontoIvaRetenido = 0;
                registro.MontoDocumento = 0;
                registro.PorcentajeRetencion = Basicas.parametros().PorcentajeRetencion;
                r.Alicuota= Basicas.parametros().TasaIva;
            }

            this.bs.DataSource = this.registro;
            this.ShowDialog();
        }
        void MontoIvaCalcEdit_Validating(object sender, CancelEventArgs e)
        {
            CalcularTotal();
        }

        void MontoExentoIvaCalcEdit_Validating(object sender, CancelEventArgs e)
        {
            Calcular();
        }

        void AlicuotaCalcEdit_Validating(object sender, CancelEventArgs e)
        {
            Calcular();
        }

        void BaseImponibleCalcEdit_Validating(object sender, CancelEventArgs e)
        {
            Calcular();
        }

        private void Calcular()
        {
            bs.EndEdit();
            registro = (Retenciones)bs.Current;
            if (registro.Alicuota == null)
            {
                registro.Alicuota = Basicas.parametros().TasaIva;
            }
            registro.MontoIva = registro.BaseImponible.GetValueOrDefault(0) * registro.Alicuota.GetValueOrDefault(0) / 100;
            registro.MontoIvaRetenido = registro.MontoIva * registro.PorcentajeRetencion / 100;
            CalcularTotal();
        }
        private void CalcularTotal()
        { 
            registro.MontoDocumento = registro.BaseImponible.GetValueOrDefault(0) + registro.MontoExentoIva.GetValueOrDefault(0) + registro.MontoIva.GetValueOrDefault(0);
        }
    }
}
