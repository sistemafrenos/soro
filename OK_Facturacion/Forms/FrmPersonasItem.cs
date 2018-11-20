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
    public partial class FrmTercerosItem : Form
    {
        public FrmTercerosItem()
        {
            InitializeComponent();
        }
        SoroEntities dc = new SoroEntities();
        public Terceros Registro = null;

        private void Frm_Load(object sender, EventArgs e)
        {
            if (Registro == null)
            {
                Registro = new Terceros();
                Registro.Activo = true;
                Registro.LimiteCredito = 0;
                Registro.DiasCredito = 0;
                Registro.TipoContribuyente = "CONTRIBUYENTE";
                Registro.Condiciones = "CONTADO"; 
            }
            else
            {
                if (Registro.IdTercero != null)
                {
                    this.Text = "Editar Persona";
                    Registro = FactoryTerceros.Item(Registro.IdTercero);
                    if ((Registro.CedulaRif.StartsWith("V") || Registro.CedulaRif.StartsWith("E") ) && string.IsNullOrEmpty(Registro.TipoContribuyente))
                    {
                        Registro.TipoContribuyente = "NO CONTRIBUYENTE";
                    }
                    else
                    {
                        Registro.TipoContribuyente = "CONTRIBUYENTE";
                    }
                }
            }
            CargarCondiciones();
            this.bs.DataSource = Registro;
            this.bs.ResetBindings(true);
        }
        private void CargarCondiciones()
        {
            foreach (string s in FactoryTerceros.Condiciones())
            {
                if (s != null)
                {
                    cmbCondiciones.Properties.Items.Add(s);
                }
            }
        }
        private void Aceptar_Click(object sender, EventArgs e)
        {
            try
            {                
                bs.EndEdit();
                Registro = (Terceros)bs.Current;
                FactoryTerceros.Guardar(Registro);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar los datos \n" + ex.Source + "\n" + ex.Message, "Atencion", MessageBoxButtons.OK);
            }
        }

        private void txtCedulaRif_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.TextEdit Editor = new DevExpress.XtraEditors.TextEdit();
            if (!Editor.IsModified)
                return;
            Editor.Text = cBasicas.CedulaRif(Editor.Text);
        }

        private void textEdit6_EditValueChanged(object sender, EventArgs e)
        {

        }

    }
}
