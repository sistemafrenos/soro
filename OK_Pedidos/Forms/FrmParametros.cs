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
    public partial class FrmParametros : Form
    {
        Entities Entities = new Entities();
        Parametro parametro = new Parametro();
        public FrmParametros()
        {
            InitializeComponent();
        }

        private void Aceptar_Click(object sender, EventArgs e)
        {
            this.parametroBindingSource.EndEdit();
            Entities.SaveChanges();
            cBasicas.ConectarDB();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void FrmParametros_Load(object sender, EventArgs e)
        {
            parametro = (from p in Entities.Parametros
                         select p).FirstOrDefault();
            this.parametroBindingSource.DataSource = parametro;
            this.parametroBindingSource.ResetBindings(true);
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
