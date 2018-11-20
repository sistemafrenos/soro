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
    public partial class FrmRetencionsISLRitemEdit : Form
    {
        public RetencionesISLR registro;
        public FrmRetencionsISLRitemEdit()
        {
            InitializeComponent();
            this.Load += new EventHandler(FrmRetencionsIslrItemEdit_Load);
        }

        void FrmRetencionsIslrItemEdit_Load(object sender, EventArgs e)
        {
            this.txtTasaIva.EditValue = 12;
            this.Aceptar.Click += new EventHandler(Aceptar_Click);
            this.Cancelar.Click += new EventHandler(Cancelar_Click);
            this.MontoDocumentoTextEdit.Validating += new CancelEventHandler(MontoDocumentoTextEdit_Validating);
            this.BaseImponibleTextEdit.Validating += new CancelEventHandler(BaseImponibleTextEdit_Validating);
            this.PorcentajeRetencionTextEdit.Validating += new CancelEventHandler(PorcentajeRetencionTextEdit_Validating);
            this.txtTasaIva.Validating += TxtTasaIva_Validating;
            this.txtSustraendo.Validating += new CancelEventHandler(txtSustraendo_Validating);
        }

        private void TxtTasaIva_Validating(object sender, CancelEventArgs e)
        {
            registro.BaseImponible = registro.MontoDocumento / (1 + (double.Parse(txtTasaIva.Text) / 100));
            Calcular();
        }

        void txtSustraendo_Validating(object sender, CancelEventArgs e)
        {
            Calcular();
        }

        void MontoDocumentoTextEdit_Validating(object sender, CancelEventArgs e)
        {
            registro.BaseImponible = registro.MontoDocumento / (1 + (double.Parse(txtTasaIva.Text) / 100));
            Calcular();
        }
        public void Editar(RetencionesISLR r)
        {
            this.registro = r;
            if (r.BaseImponible == null)
            {
                registro.BaseImponible = 0;
                registro.ImpuestoRetenido = 0;
                registro.MontoDocumento = 0;
                registro.PorcentajeRetencion =3;
                registro.Sustraendo = 0;
            }
            this.retencionesISLRBindingSource.DataSource = this.registro;
            this.ShowDialog();
        }
        void PorcentajeRetencionTextEdit_Validating(object sender, CancelEventArgs e)
        {
            Calcular();
        }

        void BaseImponibleTextEdit_Validating(object sender, CancelEventArgs e)
        {
            Calcular();
        }
        private void Calcular()
        {
            registro.ImpuestoRetenido = 
                ( registro.BaseImponible * registro.PorcentajeRetencion / 100) 
                - registro.Sustraendo;
        }
        void Cancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        void Aceptar_Click(object sender, EventArgs e)
        {
            retencionesISLRBindingSource.EndEdit();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

        }

        private void Aceptar_Click_1(object sender, EventArgs e)
        {

        }
    }
}
