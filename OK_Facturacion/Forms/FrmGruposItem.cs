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
    public partial class FrmGruposItem : Form
    {
        public FrmGruposItem()
        {
            InitializeComponent();
        }
        public string codigo;
        SoroEntities dc = new SoroEntities();
        public Grupos Grupo = null;

        private void Frm_Load(object sender, EventArgs e)
        {

            if (Grupo == null)
            {
                Grupo = new Grupos();
                Grupo.Activo = true;
                Grupo.TasaIVA = FactoryParametros.Item(dc).TasaIVA;
                Grupo.Codigo = codigo;
                Grupo.Utilidad1= FactoryParametros.Item(dc).Utilidad1;
                Grupo.Utilidad2 =FactoryParametros.Item(dc).Utilidad2;
                Grupo.Utilidad3 = FactoryParametros.Item(dc).Utilidad3;
                Grupo.Codigo = FactoryContadores.GetLast("CodigoGrupo");

            }
            else
            {
                if (Grupo.IdGrupo != null)
                {
                    this.Text = "Editar Grupo";
                    Grupo = FactoryGrupos.Item(Grupo.IdGrupo);
                }
            }
            this.gruposBindingSource.DataSource = Grupo;
            this.gruposBindingSource.ResetBindings(true);
        }
        private void Aceptar_Click(object sender, EventArgs e)
        {
            gruposBindingSource.EndEdit();
            Grupos Original = FactoryGrupos.Item(Grupo.IdGrupo);
            if (Original != null)
            {
                if (Original.Utilidad1 != Grupo.Utilidad1 || Original.Utilidad2 != Grupo.Utilidad2 || Original.Utilidad3 != Grupo.Utilidad3)
                {
                    if (MessageBox.Show("Desea aplicar esta nueva utilidad a los los productos de esta linea", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        AplicarUtilidad();
                    }
                }
            }
            try
            {
                FactoryGrupos.Guardar(Grupo);

            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void AplicarUtilidad()
        {
            using ( var dc = new SoroEntities() )
            {
                var Productos = from p in dc.Productos
                                where p.IdGrupo == Grupo.IdGrupo
                                select p;
                foreach (Productos Producto in Productos)
                {
                    Producto.parametros.Utilidad1 = Grupo.Utilidad1;
                    Producto.parametros.Utilidad2 = Grupo.Utilidad2;
                    Producto.parametros.Utilidad3 = Grupo.Utilidad3;
                    Producto.parametros.TasaIVA = Grupo.TasaIVA;
                    Producto.CalcularPrecio1();
                    Producto.CalcularPrecio2();
                    Producto.CalcularPrecio3();
                    FactoryProductos.Guardar(Producto);
                }
            }
        }
    }
}
