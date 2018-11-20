using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HK
{
    public partial class FrmPersonas : Form
    {
        public FrmPersonas()
        {
            InitializeComponent();
        }
        SoroEntities dc = new SoroEntities();
        List<Terceros> Lista = new List<Terceros>();
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
            if (txtFiltro.Text != "TODOS")
            {
                Lista = FactoryTerceros.Buscar(dc,this.txtBuscar.Text, txtFiltro.Text);
            }
            else
            {
                Lista = FactoryTerceros.Buscar(dc,this.txtBuscar.Text);
            }
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

        private void Editar_Click(object sender, EventArgs e)
        {
            EditarRegistro();
            //  Busqueda();

        }
        private void AgregarRegistro()
        {
            FrmTercerosItem F = new FrmTercerosItem();
            F.ShowDialog();
            if (F.DialogResult == DialogResult.OK)
            {
                this.bs.Add(F.Registro);
            }
        }
        private void EditarRegistro()
        {
            FrmTercerosItem F = new FrmTercerosItem();
            Terceros Registro = (Terceros)this.bs.Current;
            if (Registro == null)
                return;
            F.Registro = Registro;
            F.ShowDialog();
            if (F.DialogResult == DialogResult.OK)
            {
                Lista[this.bs.Position] = F.Registro;
            }
        }
        private void EliminarRegistro()
        {
            if (this.gridView1.IsFocusedView)
            {
                Terceros Registro = (Terceros)this.bs.Current;
                if (Registro == null)
                    return;
                if (MessageBox.Show("Esta seguro de eliminar este registro", "Atencion", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return;
                }
                try
                {
                    FactoryTerceros.Delete(Registro.IdTercero);
                    this.bs.Remove(Registro);
                }
                catch (Exception x)
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

        private void Imprimir_Click(object sender, EventArgs e)
        {
            this.gridControl1.ShowPrintPreview();
        }
    }
}
