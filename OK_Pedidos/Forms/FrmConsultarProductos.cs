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
    public partial class FrmConsultarProductos : Form
    {
        public FrmConsultarProductos()
        {
            InitializeComponent();
        }
        
        List<Producto> Lista = new List<Producto>();
        private void Frm_Load(object sender, EventArgs e)
        {
            Busqueda();
        }

        private void Busqueda()
        {
            Entities db = new Entities();
            Lista = db.Productos.OrderBy(x=>x.Descripcion).ToList();
            this.bs.DataSource = Lista;
        }

        private void Imprimir_Click(object sender, EventArgs e)
        {
            this.gridControl1.ShowPrintPreview();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}
