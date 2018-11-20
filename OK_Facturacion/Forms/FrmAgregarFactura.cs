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
    public partial class FrmAgregarFactura : Form
    {
        SoroEntities dc = new SoroEntities();
        public Documentos Registro= null;
        public string IdTercero;
        public string Tipo = "CXC";
        public FrmAgregarFactura()
        {
            InitializeComponent();
        }
        private void FrmLibroComprasItem_Load(object sender, EventArgs e)
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
                ;
                switch (Tipo)
                {
                    case "CXC":
                        Registro.TipoCuenta = "CXC";
                        Registro.Tipo = "FACTURA";
                        this.Text = "Agregar Factura por Cobrar";
                        this.EscribirLibroVentas.Text = "Agregar al Libro de ventas";
                        break;
                    case "CXP":
                        Registro.TipoCuenta = "CXP";
                        Registro.Tipo = "COMPRA";
                        this.Text = "Agregar Factura por Pagar";
                        this.EscribirLibroVentas.Text = "Agregar al Libro de compras";
                        break;
                }                
                Registro.Vence = Registro.Fecha;
            }
            else
            {
                this.Text = "Editar Factura";              
            }
            this.documentosBindingSource.DataSource = Registro;
            this.documentosBindingSource.ResetBindings(true);
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
            switch (Tipo)
            {
                case "CXC":
                    CFacturas.EscribirCuentaxCobrar( Registro );
                    if (this.EscribirLibroVentas.Checked)
                    {
                        CFacturas.LibroDeVentas(Registro);
                    }
                    break;
                case "CXP":
                    cCompras.EscribirCuentaxPagar( Registro );
                    if (this.EscribirLibroVentas.Checked)
                    {
                        cCompras.LibroDeCompras(Registro);
                    }
                    break;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        } 
    }
}
