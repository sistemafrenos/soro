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
    public partial class FrmLibroInventarios : Form
    {
        List<LibroInventarios> Libro = new List<LibroInventarios>();
        public FrmLibroInventarios()
        {
            InitializeComponent();
        }

        SoroEntities dc = new SoroEntities();
        private void Frm_Load(object sender, EventArgs e)
        {
            for (int mes = 1; mes <= 12; mes++)
            {
                this.txtMes.Items.Add(mes);
            }
            for (int año = DateTime.Today.Year - 5; año <= DateTime.Today.Year; año++)
            {
                this.txtAño.Items.Add(año);
            }
            this.txtAño.Text = DateTime.Today.Year.ToString();
            this.txtMes.Text = DateTime.Today.Month.ToString();
        }

        private void Buscar_Click(object sender, EventArgs e)
        {
            dc = new SoroEntities();
            Libro = FactoryLibroInventarios.LibroInventarios(dc, Convert.ToInt16(this.txtMes.Text), Convert.ToInt16(this.txtAño.Text));
            this.libroBindingSource.DataSource = Libro;
            this.Buscar.Enabled = false;
            this.Aceptar.Enabled = true;
            this.Cancelar.Enabled = true;
        }

        private void Aceptar_Click(object sender, EventArgs e)
        {
            this.libroBindingSource.EndEdit();
            dc.SaveChanges();
            this.Buscar.Enabled = true;
            this.Aceptar.Enabled = false;
            this.Cancelar.Enabled = false;
        }

        private void Imprimir_Click(object sender, EventArgs e)
        {
            var Consulta = from p in Libro
                           where (p.Final> 0)
                           select p;
            FrmReportes F = new FrmReportes();
            if( this.checkBox1.Checked )
               F.LibroDeInventarios(Libro);
            else
               F.LibroDeInventarios(Consulta.ToList());
        }

        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (gridView1.ActiveEditor == null)
            {
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Subtract)
                {
                    if (this.gridView1.IsFocusedView)
                    {
                        LibroInventarios Registro = (LibroInventarios)this.libroBindingSource.Current;
                        try
                        {
                            dc.LibroInventarios.Remove(Registro);
                        }
                        catch { }
                        this.libroBindingSource.Remove(Registro);
                    }
                }
            }

        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            dc = new SoroEntities();
            Libro = FactoryLibroInventarios.LibroInventarios(dc, Convert.ToInt16(this.txtMes.Text), Convert.ToInt16(this.txtAño.Text));
            this.libroBindingSource.DataSource = Libro;
            this.Buscar.Enabled = true;
            this.Aceptar.Enabled = false;
            this.Cancelar.Enabled = false;
        }

        private void txtMes_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                int mes = Convert.ToInt16(this.txtMes.Text);
                if (mes < 1 || mes > 12)
                {
                    this.txtMes.Text = DateTime.Today.Month.ToString();
                }

            }
            catch
            {
                this.txtMes.Text = DateTime.Today.Month.ToString();
            }
        }


    }
}
