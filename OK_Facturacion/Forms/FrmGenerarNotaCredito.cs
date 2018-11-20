using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HK.Forms
{
    public partial class FrmGenerarNotaCredito : Form
    {
        SoroEntities dc;
        Terceros persona;
        Documentos documento;
        VistaFactura DocumentoAnterior;
        public FrmGenerarNotaCredito()
        {
            InitializeComponent();
            dc = new SoroEntities();
            documento = new Documentos();
        }


        private void txtFacturaDevolver_Validating(object sender, CancelEventArgs e)
        {
            BuscarFacturaDevolver();
        }

        private void CargarFactura(VistaFactura d)
        {
            documento = new Documentos();
            documento.Activo = true;
            documento.IdTercero = d.IdTercero;
            documento.IdVendedor = d.IdTercero;
            documento.Tipo = "NOTA CREDITO";
            documento.Fecha = DateTime.Today;
            documento.MontoExento = d.MontoExento;
            documento.MontoIva = d.MontoIva;
            documento.MontoTotal = d.MontoTotal;
            documento.BaseImponible = d.BaseImponible;
            documento.Año = d.Año;
            documento.Saldo =0; 
            persona = FactoryTerceros.Item(d.IdTercero);
            this.bsDocumento.DataSource = documento;
            this.bsDocumento.ResetBindings(true);
            this.bsPersona.DataSource = persona;
            this.bsPersona.ResetBindings(true);
        }

        private void Aceptar_Click(object sender, EventArgs e)
        {
            ///
            ///
            ///           
            string NumeroNotaCredito = FactoryContadores.GetLast("NumeroNotaCredito");
            documento = (Documentos)this.bsDocumento.Current;
            VistaFactura d = DocumentoAnterior;
            d.Saldo = d.Saldo - documento.MontoTotal;
            Cuentas cuenta = FactoryCuentas.Item(dc, d.IdDocumento);
            if (cuenta != null)
                cuenta.Saldo = cuenta.Saldo - documento.MontoTotal;
            RegistroPagos pago = new RegistroPagos();
            pago.IdRegistroPago = FactoryContadores.GetLast("IdRegistroPago");
            pago.Modulo = "NOTA CREDITO";
            pago.Documento = FactoryContadores.GetLast("NumeroNotaCreditoPago");
            pago.IdDocumento = d.IdDocumento;
            pago.Fecha = DateTime.Today;
            pago.MontoPagar = documento.Saldo;
            pago.MontoPagado = d.MontoTotal;
            pago.IdTercero = d.IdTercero;
            pago.RazonSocial = persona.RazonSocial;
            pago.SaldoPendiente = d.Saldo - documento.MontoTotal;
            pago.Documento = d.Numero;
            pago.Tipo = "NC";
            RegistroPagosDetalles detalle = new RegistroPagosDetalles();
            detalle.IdRegistroPagosDetalle = FactoryContadores.GetLast("IdRegistroPagosDetalle");
            detalle.IdDocumento = d.IdDocumento;
            detalle.Monto = documento.MontoTotal;
            detalle.Numero = d.Numero;
            detalle.Tipo = "CXC";
            documento.Tipo = "NOTA CREDITO";
            documento.IdDocumento = FactoryContadores.GetLast("IdDocumento");
            documento.Numero = NumeroNotaCredito;
            documento.Comentarios = string.Format("DOCUMENTO AFECTADO {0}", DocumentoAnterior.Numero);
            dc.RegistroPagos.Add(pago);
            dc.RegistroPagosDetalles.Add(detalle);
            dc.Documentos.Add(documento);
            Documentos doc = dc.Documentos.FirstOrDefault(x => x.IdDocumento == DocumentoAnterior.IdDocumento);
            doc.Saldo = doc.Saldo - documento.MontoTotal;
            dc.SaveChanges();
            FrmReportes f = new FrmReportes();
            f.ReporteNotaCredito(documento);
            CFacturas.DevolucionLibroDeVentas(doc, DocumentoAnterior.Numero);
            this.Close();
        }

        private void txtFacturaDevolver_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtFacturaDevolver_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            BuscarFacturaDevolver();
        }

        private void BuscarFacturaDevolver()
        {
            List<VistaDocumento> T = FactoryDocumentos.Buscar(dc, txtFacturaDevolver.Text, "FACTURA", true);
            switch (T.Count)
            {
                case 0:
                    txtFacturaDevolver.Text = "";
                    break;
                case 1:
                    txtFacturaDevolver.Text = FactoryDocumentos.Item(dc, T[0].IdDocumento).Numero;
                    break;
                default:
                    FrmBuscarEntidades F = new FrmBuscarEntidades();
                    F.Texto = this.txtFacturaDevolver.Text;
                    F.myLayout = "FACTURAS";
                    F.Filtro = "FACTURA";
                    F.dc = this.dc;
                    F.ShowDialog();
                    if (F.Registro == null)
                        return;
                    VistaDocumento VistaDocumento = (VistaDocumento)F.Registro;
                    txtFacturaDevolver.Text = VistaDocumento.Numero;
                    break;
            }
            DocumentoAnterior = (from p in dc.VistaFactura
                     where p.Numero == txtFacturaDevolver.Text
                     select p).FirstOrDefault();
            if (DocumentoAnterior != null)
            {
                CargarFactura(DocumentoAnterior);
            }
        }

        private void textEdit18_Validated(object sender, EventArgs e)
        {
            documento.MontoIva = documento.BaseImponible * ( FactoryParametros.Item().TasaIVA /100) ;
            documento.MontoTotal = documento.MontoExento.GetValueOrDefault(0) + documento.MontoIva.GetValueOrDefault(0) + documento.BaseImponible;
        }
    }
}

