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
        SoroEntities dc = new SoroEntities();
        List<VistaProductos> Lista = new List<VistaProductos>();
        private void Frm_Load(object sender, EventArgs e)
        {
            Busqueda();
            #region Custom
            switch (FactoryParametros.Item().Empresa)
            {
                case "HK SOLUCIONES,C.A.":
                    break;
                case "FARMACIA CHUPARIN,C.A.":
                    this.colMarca.Visible = false;
                    this.colModelo.Visible = false;
                    this.colReferencia.Visible = false;
                    this.colUbicacion.Visible = false;
                    break;
                case "COMERCIAL PADRE E HIJO,C.A.":
                    {
                        break;
                    }
                case "FARMASHOP 2000,c.a.":
                    {
                        break;
                    }
                case "REPUESTOS TALLERES LATINOS,C.A.":
                    {
                        this.colMarca.Visible = true;
                        this.colModelo.Visible = true;
                        break;
                    }
                case "REPUESTOS SAN MIGUEL,C.A.":
                    this.colMarca.Visible = true;
                    this.colModelo.Visible = true;
                    break;
                case "MERCANTIL EL ROSARIO,C.A.":
                    {
                        break;
                    }
            }
            #endregion
        }

        private void Buscar_Click(object sender, EventArgs e)
        {
            Busqueda();
        }

        private void Busqueda()
        {
            dc = new SoroEntities();
            Lista = FactoryProductos.Buscar(dc, this.txtBuscar.Text);
            this.bs.DataSource = Lista;
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Busqueda();
            }
        }

        private void Imprimir_Click(object sender, EventArgs e)
        {
            this.gridControl1.ShowPrintPreview();
        }
    }
}
