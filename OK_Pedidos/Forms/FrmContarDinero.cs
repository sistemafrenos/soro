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
    public partial class FrmContarDinero : Form
    {
        public FrmContarDinero()
        {
            InitializeComponent();
        }

        private void spinEdit1_Validating(object sender, CancelEventArgs e)
        {
            txt500.EditValue = this.s500.Value * 500;
            txt100.EditValue = this.s100.Value * 100;
            txt50.EditValue = this.s50.Value * 50;
            txt20.EditValue = this.s20.Value * 20;
            txt10.EditValue = this.s10.Value * 10;
            txt5.EditValue = this.s5.Value * 5;
            txt2.EditValue = this.s2.Value * 2;
            txt1.EditValue = this.s1.Value;
            txtTotal.EditValue = 0;
            try
            {
                txtTotal.EditValue = (decimal)txt500.EditValue;
                txtTotal.EditValue = (decimal)txtTotal.EditValue + (decimal)txt100.EditValue;
                txtTotal.EditValue = (decimal)txtTotal.EditValue + (decimal)txt50.EditValue;
                txtTotal.EditValue = (decimal)txtTotal.EditValue + (decimal)txt20.EditValue;
                txtTotal.EditValue = (decimal)txtTotal.EditValue + (decimal)txt10.EditValue;
                txtTotal.EditValue = (decimal)txtTotal.EditValue + (decimal)txt5.EditValue;
                txtTotal.EditValue = (decimal)txtTotal.EditValue + (decimal)txt2.EditValue;
                txtTotal.EditValue = (decimal)txtTotal.EditValue + (decimal)txt1.EditValue;
            }
            catch { }
        }

        private void txt500_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}