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
    public partial class FrmGrupos : Form
    {
        public FrmGrupos()
        {
            InitializeComponent();
        }

        SoroEntities dc = new SoroEntities();
        List<Grupos> Lista = new List<Grupos>();
        private void Frm_Load(object sender, EventArgs e)
        {
            Busqueda();
        }

        private void Buscar_Click(object sender, EventArgs e)
        {
            Busqueda();
        }

        private void Busqueda()
        {
            dc = new SoroEntities();
            Lista = FactoryGrupos.Buscar(dc,this.txtBuscar.Text);
            this.bs.DataSource = Lista;
        }
        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (gridView1.ActiveEditor == null)
            {
                if (e.KeyCode == Keys.Return)
                {
                    EditarRegistro();
                }
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Subtract)
                {
                    EliminarRegistro();
                }
                if (e.KeyCode == Keys.Insert)
                {
                    AgregarRegistro();
                }
            }

        }

        private void AgregarRegistro()
        {
            FrmGruposItem F = new FrmGruposItem();
            F.ShowDialog();
            if (F.DialogResult == DialogResult.OK)
            {
                this.bs.Add(FactoryGrupos.Item(F.Grupo.IdGrupo));
            }
        }
        private void EditarRegistro()
        {
            FrmGruposItem F = new FrmGruposItem();
            Grupos mRegistro = (Grupos)this.bs.Current;
            if (mRegistro == null)
                return;
            F.Grupo = FactoryGrupos.Item(mRegistro.IdGrupo);
            F.ShowDialog();
            if (F.DialogResult == DialogResult.OK)
            {
                Busqueda();
            }
        }
        private void EliminarRegistro()
        {
            if (this.gridView1.IsFocusedView)
            {
                Grupos Vista = (Grupos)this.bs.Current;
                if (Vista == null)
                    return;
                Grupos Registro = FactoryGrupos.Item(Vista.IdGrupo);                
                try
                {
                    FactoryGrupos.Delete(Vista.IdGrupo);
                    this.bs.Remove(Vista);
                }
                catch(Exception x) 
                {
                    MessageBox.Show(x.Message);
                }                
            }
        }
        private void Nuevo_Click(object sender, EventArgs e)
        {
            AgregarRegistro();
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }
        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Busqueda();
            }
        }
        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            EditarRegistro();
        }

        private void Editar_Click(object sender, EventArgs e)
        {
            EditarRegistro();
        }
    }
}

