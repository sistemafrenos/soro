using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HK
{
    public partial class FrmInformacion : Form
    {
        public FrmInformacion()
        {
            InitializeComponent();
        }

        private void FrmInformacion_Load(object sender, EventArgs e)
        {

        }
        public void SetTextoModal(string Texto)
        {                       
            
            this.Width = 600;
            this.Opacity = 100;
            this.button1.Visible = true;
            this.txtTexto.Text = Texto;
            this.Hide();
            this.ShowDialog();
        }
        public void SetTexto( string Texto)
        {            
            this.Width = 511;
            this.Opacity = 80;
            this.txtTexto.Text = Texto;
            this.button1.Visible = false;
        }
        private void txtTexto_TextChanged(object sender, EventArgs e)
        {            
            if (txtTexto.Text == "")
                this.Hide();
            else
                this.Show();                
                this.Update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
