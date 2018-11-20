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
    public partial class FrmPedidosItemSave : DevExpress.XtraEditors.XtraForm
    {
        private float tasaiva = 16;
        public FrmPedidosItemSave()
        {
            InitializeComponent();
            this.Cargar();
        }

        public float Tasaiva { get => tasaiva; set => tasaiva = value; }

        private void Cargar()
        {
            this.TxtTasaIva.Properties.Items.AddRange(new object[] { 16, 12, 9 });
            this.TxtTasaIva.EditValue = tasaiva;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.tasaiva = float.Parse(this.TxtTasaIva.Text);
            this.DialogResult = DialogResult.OK;
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {            
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
