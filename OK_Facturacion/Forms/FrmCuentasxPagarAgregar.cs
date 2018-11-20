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
    public partial class FrmCuentasxPagarAgregar : Form
    {
        public FrmCuentasxPagarAgregar()
        {
            InitializeComponent();
        }
        SoroEntities dc = new SoroEntities();
        public Documentos Registro= null;
        public string IdTercero;

        private void FrmLibro_Load(object sender, EventArgs e)
        {
            if (Registro == null)
            {
                Registro = new Documentos();
                Registro.Fecha = DateTime.Today;
                Registro.Activo = true;
                Registro.BaseImponible = 0;
                Registro.Fecha = DateTime.Today;
                Registro.IdSesion = FactorySesiones.SesionActiva.IdSesion;
                Registro.IdTercero = IdTercero;
                Registro.MontoExento = 0;
                Registro.MontoIva = 0;
                Registro.MontoTotal = 0;
                Registro.Saldo = 0;
                Registro.Status = "INVENTARIO";
                Registro.TasaIVA = FactoryParametros.Item(dc).TasaIVA;
                Registro.Tipo = "COMPRA";
                Registro.TipoCuenta = "CxP";
                Registro.Vence = Registro.Fecha;
                this.Text = "Agregar Factura x Pagar";
            }
            else
            {
                this.Text = "Editar Factura";
              //  Registro = FactoryLibroCompras.Item(dc, Registro.IdDocumento);
            }
            this.documentosBindingSource.DataSource = Registro;
            this.documentosBindingSource.ResetBindings(true);
            this.txtAño.Value = txtFecha.DateTime.Year;
            this.txtMes.Value = txtFecha.DateTime.Month;

        }
        private void Calcular(object sender, CancelEventArgs e)
        {
            if (!((DevExpress.XtraEditors.TextEdit)sender).IsModified)
                return;
            this.documentosBindingSource.EndEdit();
            if (((DevExpress.XtraEditors.TextEdit)sender).Name != "MontoIva")
            {
                Registro.MontoIva = Registro.BaseImponible * (Registro.TasaIVA / 100);
            }
            Registro.MontoTotal = Registro.BaseImponible +  + Registro.MontoIva + Registro.MontoExento;
            Registro.Saldo = Registro.MontoTotal;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.documentosBindingSource.EndEdit();
            if (Registro.IdDocumento == null)
            {
                Registro.IdDocumento = FactoryContadores.GetLast("IdDocumento");
                dc.Documentos.Add(Registro);
            }
            dc.SaveChanges();
            cCompras.EscribirCuentaxPagar( Registro );
            if (this.EscribirLibroCompras.Checked)
            {
                cCompras.LibroDeCompras(Registro);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtFecha_Validating(object sender, CancelEventArgs e)
        {
            if (!txtFecha.IsModified)
                return;
            this.txtAño.Text = txtFecha.DateTime.Year.ToString();
            this.txtMes.Text = txtFecha.DateTime.Month.ToString();
        } 
    }
}
