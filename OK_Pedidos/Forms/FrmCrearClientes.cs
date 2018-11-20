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
    public partial class FrmCrearClientes : Form
    {
        public FrmCrearClientes()
        {
            InitializeComponent();
        }
        Cliente cliente = new Cliente();
        private void FrmCrearClientes_Load(object sender, EventArgs e)
        {
            this.CiudadComboBoxEdit.Properties.Items.AddRange(FactoryTerceros.Ciudades().ToArray());
            this.ZonaComboBoxEdit.Properties.Items.AddRange(FactoryTerceros.Zonas().ToArray());
        //    this.CondicionesComboBoxEdit.Properties.Items.AddRange(FactoryTerceros.Condiciones().ToArray());
            this.clienteBindingSource.DataSource = cliente;
            this.clienteBindingSource.ResetBindings(true);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {            
            try
            {
                FactoryTerceros.Guardar(cliente);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch( Exception x)
            {
                MessageBox.Show(x.Message,"Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
