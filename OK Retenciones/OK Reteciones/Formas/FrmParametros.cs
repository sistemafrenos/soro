using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HK;
using HK.Clases;

namespace HK.Formas
{
    public partial class FrmParametros : Form
    {
        Data db = new Data();
        public Parametros registro = new Parametros();

        public FrmParametros()
        {
            InitializeComponent();
            this.Load += new EventHandler(FrmParametros_Load);
        }

        void FrmParametros_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Frm_KeyDown);
            this.Aceptar.Click += new EventHandler(Aceptar_Click);
            this.Cancelar.Click += new EventHandler(Cancelar_Click);
            this.CedulaRifTextEdit.Validating += new CancelEventHandler(CedulaRifTextEdit_Validating);
            registro = (from p in db.Parametros
                        select p).FirstOrDefault();
            Enlazar();
        }


        void CedulaRifTextEdit_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.TextEdit Editor = (DevExpress.XtraEditors.TextEdit)sender;
            if (!Editor.IsModified)
                return;
            Editor.Text = Basicas.CedulaRif(Editor.Text);
            registro.CedulaRif = Editor.Text;
        }
        private void Limpiar()
        {
            registro = new Parametros();
        }
        private void Enlazar()
        {
            if (registro == null)
            {
                Limpiar();
            }
            this.parametrosBindingSource.DataSource = registro;
            this.txtNumeroIva.Value = Decimal.Parse(FactoryContadores.GetMax("COMPROBANTE_IVA", false)) - 1;
            this.txtNumeroISLR.Value = Decimal.Parse(FactoryContadores.GetMax("COMPROBANTE_ISLR", false)) - 1;
            this.parametrosBindingSource.ResetBindings(true);
        }
        private void Aceptar_Click(object sender, EventArgs e)
        {
            try
            {
                parametrosBindingSource.EndEdit();
                registro = (Parametros)parametrosBindingSource.Current;
                db.SaveChanges();
                FactoryContadores.SetContador("COMPROBANTE_ISLR", (int)txtNumeroISLR.Value);
                FactoryContadores.SetContador("COMPROBANTE_IVA", (int)txtNumeroIva.Value);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar los datos \n" + ex.Source + "\n" + ex.Message, "Atencion", MessageBoxButtons.OK);
            }
        }
        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.parametrosBindingSource.ResetCurrentItem();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void Frm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Cancelar.PerformClick();
                    e.Handled = true;
                    break;
                case Keys.F12:
                    this.Aceptar.PerformClick();
                    e.Handled = true;
                    break;
            }
        }

        private void Aceptar_Click_1(object sender, EventArgs e)
        {

        }
    }
}
