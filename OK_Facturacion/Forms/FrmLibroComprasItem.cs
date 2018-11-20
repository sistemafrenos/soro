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
    public partial class FrmLibroComprasItem : Form
    {        
        public FrmLibroComprasItem()
        {
            InitializeComponent();
        }
        Terceros Tercero = new Terceros();
        SoroEntities dc = new SoroEntities();
        public LibroCompras Registro = null;

        private void spinEdit6_Validating(object sender, CancelEventArgs e)
        {
            Calcular();
        }

        private void FrmLibroComprasItem_Load(object sender, EventArgs e)
        {
            if (Registro == null)
            {
                Registro = new LibroCompras();
                Registro.Fecha = DateTime.Today;
                Registro.Año = DateTime.Today.Year;
                Registro.Mes = DateTime.Today.Month;
                Registro.TasaIva = FactoryParametros.Item(dc).TasaIVA;
                Registro.BaseImponible = 0;
                Registro.ComprasNoSujetas = 0;
                Registro.ComprasSinCreditoIVA = 0;
                Registro.ImpuestoIVA = 0;
                Registro.TotalIncluyendoIva = 0;
                
            }
            else
            {
                this.Text = "Editar Registro Libro Compras";
                Registro = FactoryLibroCompras.Item(dc, Registro.IdLibroCompras);
            }
            this.libroComprasBindingSource.DataSource = Registro;
            this.libroComprasBindingSource.ResetBindings(true);
        }
        private void Calcular()
        {
            this.libroComprasBindingSource.EndEdit();
            Registro.ImpuestoIVA = Registro.BaseImponible * (Registro.TasaIva / 100);
            Registro.TotalIncluyendoIva = Registro.BaseImponible + Registro.ComprasNoSujetas + Registro.ComprasSinCreditoIVA+Registro.ImpuestoIVA;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.libroComprasBindingSource.EndEdit();
            if ( string.IsNullOrEmpty(Registro.CedulaRif))
            {
                MessageBox.Show("No puede estar vacia la Cedula", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);                
                return;
            }
            if (string.IsNullOrEmpty(Registro.RazonSocial))
            {
                MessageBox.Show("No puede estar vacia la Razon Social", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!cBasicas.IsValidCIRIF(Registro.CedulaRif))
            {
                MessageBox.Show("Error en Cedula o Rif del Titular debe comenzar en V/E/J/G \n y no debe llevar guiones ni puntos", "Verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Registro.IdLibroCompras == null)
            {
                Registro.IdLibroCompras = FactoryContadores.GetLast("IdLibroCompras");
                dc.LibroCompras.Add(Registro);
            }
            dc.SaveChanges();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BuscarTitulares_Click(object sender, EventArgs e)
        {
            SoroEntities dc = new SoroEntities();
            FrmBuscarEntidades F = new FrmBuscarEntidades();
            F.Texto = "";
            F.myLayout = "TERCEROS";
            F.Filtro = "";
            F.dc = dc;
            F.ShowDialog();
            if (F.Registro != null)
            {
                Tercero = (Terceros)F.Registro;
            }
            else
            {
                Tercero = new Terceros();
            }
            Registro.CedulaRif = Tercero.CedulaRif;
            Registro.RazonSocial = Tercero.RazonSocial;

        }

        private void txtTitular_Validating(object sender, CancelEventArgs e)
        {
            SoroEntities dc = new SoroEntities();
            if (!this.txtTitular.IsModified) return;
            Terceros mTitular = FactoryTerceros.ItemxCedula(this.txtTitular.Text);
            if (mTitular != null)
            {
                Tercero = mTitular;
            }
            else
            {
                Tercero = new Terceros();
            }
            Registro.CedulaRif = Tercero.CedulaRif;
            Registro.RazonSocial = Tercero.RazonSocial;
        }
        
    }
}