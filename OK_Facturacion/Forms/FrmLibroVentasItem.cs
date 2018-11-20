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
    public partial class FrmLibroVentasItem : Form
    {
        public FrmLibroVentasItem()
        {
            InitializeComponent();
        }
        SoroEntities dc = new SoroEntities();
        public LibroVentas Registro = null;
        private Terceros Tercero = null;
        private void SpinEdit6_Validating(object sender, CancelEventArgs e)
        {
            Calcular();
        }
        private void FrmLibroVentasItem_Load(object sender, EventArgs e)
        {
            if (Registro == null)
            {
                Registro = new LibroVentas
                {
                    Fecha = DateTime.Today,
                    Año = DateTime.Today.Year,
                    Mes = DateTime.Today.Month,
                    TasaIva = FactoryParametros.Item(dc).TasaIVA,
                    BaseImponible = 0,
                    IvaRetenido = 0,
                    MontoIncluyentoIva = 0,
                    VentasNoGravadas = 0,
                    ImpuestoIva = 0,
                    TipoTransaccion = "01"
                };
            }
            else
            {
                this.Text = "Editar Registro Libro Ventas";
                Registro = FactoryLibroVentas.Item(dc, Registro.IdLibroVentas);
            }
            this.libroVentasBindingSource.DataSource = Registro;
            this.libroVentasBindingSource.ResetBindings(true);
        }
        private void Calcular()
        {
            this.libroVentasBindingSource.EndEdit();
            Registro.ImpuestoIva = Registro.BaseImponible * (Registro.TasaIva / 100);
            Registro.MontoIncluyentoIva = Registro.BaseImponible + Registro.ImpuestoIva+ Registro.VentasNoGravadas;
        }
        private void SimpleButton1_Click(object sender, EventArgs e)
        {
            this.libroVentasBindingSource.EndEdit();
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
            if (Registro.IdLibroVentas == null)
            {
                Registro.IdLibroVentas = FactoryContadores.GetLast("IdLibroVentas");
                dc.LibroVentas.Add(Registro);
            }
            dc.SaveChanges();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void SimpleButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void BuscarTitulares_Click(object sender, EventArgs e)
        {
            SoroEntities dc = new SoroEntities();
            FrmBuscarEntidades F = new FrmBuscarEntidades
            {
                Texto = "",
                myLayout = "TERCEROS",
                Filtro = "",
                dc = dc
            };
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

        private void TxtTitular_Validating(object sender, CancelEventArgs e)
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
