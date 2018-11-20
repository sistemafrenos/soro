using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

using DevExpress.XtraEditors;

namespace HK
{
    public partial class FrmDevoluciones : DevExpress.XtraEditors.XtraForm
    {
        Terceros Titular = new Terceros();
        Terceros Vendedor = new Terceros();
        Documentos Documento = new Documentos();
        SoroEntities dc = new SoroEntities();
        DocumentosProductos DocumentoProducto = null;
        Documentos Doc = new Documentos();

        public FrmDevoluciones()
        {
            InitializeComponent();
        }
        private void HabilitarEdicion()
        {
            this.toolCargarDocumento.Enabled = false;
            this.gridControl1.Enabled = true;
            Encab1.Focus();
            this.Aceptar.Enabled = true;
            this.Cancelar.Enabled = true;
            this.txtBuscar.Enabled = false;
            this.Imprimir.Enabled = false;
        }
        private void DesHabilitarEdicion()
        {
            //Encab1.Enabled = false;
            //Encab2.Enabled = false;
            this.toolCargarDocumento.Enabled = true;
            this.gridControl1.Enabled = false;
            this.Aceptar.Enabled = false;
            this.Cancelar.Enabled = false;
            this.txtBuscar.Enabled = true;
            txtBuscar.Focus();
            if (Documento == null)
            {
                this.Imprimir.Enabled = false;
            }
            if (Documento != null)
            {
                //   if (Documento.Status == "INVENTARIO" || Documento.Status == "ABIERTA")
                //{
                this.Imprimir.Enabled = true;
                //}
                //else
                //{
                //    this.Imprimir.Enabled = false;
                //    this.btnEliminar.Enabled = false;
                //}
            }
        }
        private void FrmDevoluciones_Load(object sender, EventArgs e)
        {
            this.txtCantidad.Validating += new CancelEventHandler(txtCantidad_Validating);
            DesHabilitarEdicion();

        }
        private void CargarDocumento_Click(object sender, EventArgs e)
        {
            FrmBuscarEntidades F = new FrmBuscarEntidades();
            F.myLayout = "BUSCARDEVOLUCIONES";
            F.ShowDialog();
            if (F.DialogResult == DialogResult.OK)
            {
                Doc = FactoryDocumentos.Item(dc, ((VistaDocumento)F.Registro).IdDocumento);

                if (Doc.Status == "DEVUELTO")
                {
                    MessageBox.Show("Esta operacion ya fue Devuelta", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);
               //     return;
                }
                CargarDocumento();
                HabilitarEdicion();
            }
        }
        private void CargarDocumento()
        {
            if (Doc == null)
            {
                return;
            }
            Documento = new Documentos();
            Documento.IdTercero = Doc.IdTercero;
            Documento.IdVendedor = Doc.IdVendedor;
            Documento.Mes = DateTime.Today.Month;
            Documento.Status = "ABIERTA";
            Documento.TasaIVA = FactoryParametros.Item().TasaIVA;
            Documento.Tipo = "DEVOLUCION";
            Documento.Activo = true;
            Documento.Año = DateTime.Today.Year;
            Documento.Fecha = DateTime.Today;
            Documento.Vence = Documento.Fecha.Value.AddDays(30);
            Documento.DescuentoBs = Doc.DescuentoBs;
            Documento.DescuentoPorcentaje = Doc.DescuentoPorcentaje;
            Titular = FactoryTerceros.Item(Documento.IdTercero);
            Vendedor = FactoryTerceros.Item(Documento.IdVendedor);
            foreach (DocumentosProductos Item in Doc.DocumentosProductos)
            {
                DocumentosProductos newItem = new DocumentosProductos();
                newItem.Activo = Item.Activo;
                newItem.BloqueoPrecio = Item.BloqueoPrecio;
                newItem.Cantidad = Item.Cantidad;
                newItem.Codigo = Item.Codigo;
                newItem.Comentarios = Item.Comentarios;
                newItem.Costo = Item.Costo;
                newItem.CostoIva = Item.CostoIva;
                newItem.Descripcion = Item.Descripcion;
                newItem.DescuentoBs = Item.DescuentoBs;
                newItem.DescuentoPorcentaje = Item.DescuentoPorcentaje;
                newItem.IdProducto = Item.IdProducto;
                newItem.IdServidor = Item.IdServidor;
                newItem.Iva = Item.Iva;
                newItem.MontoNeto = Item.MontoNeto;
                newItem.Precio = Item.Precio;
                newItem.Precio2 = Item.Precio2;
                newItem.Precio3 = Item.Precio3;
                newItem.PrecioIva = Item.PrecioIva;
                newItem.SolicitaServidor = Item.SolicitaServidor;
                newItem.TasaIva = Item.TasaIva;
                newItem.Tipo = Item.Tipo;
                newItem.Total = Item.Total;
                newItem.IdDetalleDocumento = Item.IdDetalleDocumento;
                Documento.DocumentosProductos.Add(newItem);
            }
            LeerTitular();
            LeerVendedor();
            Documento.CalcularTotales();
            this.bs.DataSource = Documento;
            this.bs.ResetBindings(true);
            this.bsDetalles.DataSource = Documento.DocumentosProductos;
            this.bsDetalles.ResetBindings(true);
        }
        private void LeerTitular()
        {
            if (Titular == null)
            {
                Titular = new Terceros();
            }
            Titular.CedulaRif = cBasicas.CedulaRif(Titular.CedulaRif);
            this.txtCedulaTitular.Text = Titular.CedulaRif;
            this.razonSocialTextBox.Text = Titular.RazonSocial;
            this.direccionTextBox.Text = Titular.Direccion;
            this.emailTextBox.Text = Titular.Email;
            this.telefonosTextBox.Text = Titular.Telefonos;
        }
        private void LeerVendedor()
        {
            if (Vendedor == null)
            {
                Vendedor = new Terceros();
            }
            txtVendedor.Text = Vendedor.RazonSocial;
        }
        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                gridView1.MoveBy(0);
            }
            if (gridView1.ActiveEditor == null)
            {
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Subtract)
                {
                    if (this.gridView1.IsFocusedView)
                    {
                        DocumentosProductos Registro = (DocumentosProductos)this.bsDetalles.Current;
                        try
                        {
                            Registro.Activo = false;
                            dc.DocumentosProductos.Remove(Registro);
                        }
                        catch { }
                        this.bsDetalles.Remove(Registro);
                    }
                    e.Handled = true;
                }
            }
        }
        private void txtCantidad_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.SpinEdit Editor = (DevExpress.XtraEditors.SpinEdit)this.gridControl1.MainView.ActiveEditor;
            if (!Editor.IsModified)
                return;
            DocumentoProducto = (DocumentosProductos)this.bsDetalles.Current;
            using( var dc = new SoroEntities() )
            {
                var p = (from i in dc.DocumentosProductos
                         where i.IdDetalleDocumento == DocumentoProducto.IdDetalleDocumento
                         select i).FirstOrDefault() ;
                if (p != null)
                {
                    if (p.Cantidad < (double)Editor.Value || (double)Editor.Value<=0 )
                    {
                        Editor.Value = (decimal)p.Cantidad;
                    }
                }
            }
            DocumentoProducto.Cantidad = (double)Editor.Value;
            CalcularMontoItem();
            Documento.CalcularTotales();
        }
        private void CalcularMontoItem()
        {
            this.bsDetalles.EndEdit();
            if (DocumentoProducto.DescuentoPorcentaje == null)
                DocumentoProducto.DescuentoPorcentaje = 0;
            DocumentoProducto.DescuentoBs = DocumentoProducto.DescuentoPorcentaje * DocumentoProducto.Precio / 100;
            DocumentoProducto.Iva = cBasicas.Round(DocumentoProducto.Precio * DocumentoProducto.TasaIva / 100);
            DocumentoProducto.PrecioIva = cBasicas.Round(DocumentoProducto.Precio + DocumentoProducto.Iva);
            DocumentoProducto.MontoNeto = cBasicas.Round(DocumentoProducto.Precio - DocumentoProducto.DescuentoBs);
            DocumentoProducto.Iva = cBasicas.Round(DocumentoProducto.MontoNeto * DocumentoProducto.TasaIva / 100);
            DocumentoProducto.Total = (DocumentoProducto.MontoNeto + DocumentoProducto.Iva) * DocumentoProducto.Cantidad;
        }

        private void Aceptar_Click(object sender, EventArgs e)
        {
            if (FactoryParametros.Item().TipoImpresora.Contains("FISCAL"))
            {
                if (cBasicas.ImpresoraOcupada && Doc.Tipo != "PEDIDO")
                {
                    MessageBox.Show("Impresora Fiscal Ocupada, Por favor espere", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
            }
            try
            {
                double MontoPagos = 0;
                Doc.Status = "DEVUELTO";
                Documento.Efectivo = Doc.Efectivo;
                Documento.Cheque = Doc.Cheque;
                Documento.TarjetaCredito = Doc.TarjetaCredito;
                Documento.TarjetaDebito = Doc.TarjetaDebito;
                Documento.Deposito = Doc.Deposito;
                Documento.DescuentoBs = Doc.DescuentoBs;
                Documento.Cambio = Doc.Cambio;
                Documento.Transferencias = Doc.Transferencias;
                Documento.Comentarios = Doc.Numero;
                if (Documento.Efectivo.HasValue)
                    MontoPagos += Documento.Efectivo.Value;
                if (Documento.Cheque.HasValue)
                    MontoPagos += Documento.Cheque.Value;
                if (Documento.TarjetaCredito.HasValue)
                    MontoPagos += Documento.TarjetaCredito.Value;
                if (Documento.TarjetaDebito.HasValue)
                    MontoPagos += Documento.TarjetaDebito.Value;
                if (Documento.Deposito.HasValue)
                    MontoPagos += Documento.Deposito.Value;
                if (Documento.Transferencias.HasValue)
                    MontoPagos += Documento.Transferencias.Value;
                if (MontoPagos < Documento.MontoTotal)
                {
                    Documento.Saldo = Documento.MontoTotal - MontoPagos;
                }
                FactoryDocumentos.Save(dc, Documento, Titular);
               // ImprimirDo();
                CFacturas.Devolucion(Documento,Doc);                
            }
            catch(Exception x )
            {
                MessageBox.Show(x.Message);
                return;
            }
            Limpiar();
            Enlazar();
            DesHabilitarEdicion();
        }
        private void Enlazar()
        {
            if (Documento == null)
            {
                Documento = new Documentos();
            }
            Titular = FactoryTerceros.Item(Documento.IdTercero);
            Vendedor = FactoryTerceros.Item(Documento.IdVendedor);
            LeerTitular();
            LeerVendedor();
            this.bs.DataSource = Documento;
            this.bs.ResetBindings(true);
            this.bsDetalles.DataSource = Documento.DocumentosProductos;
            this.bsDetalles.ResetBindings(true);
        }
        private void Limpiar()
        {
            dc = new SoroEntities();
            Titular = new Terceros();
            Vendedor = new Terceros();
            Documento = new Documentos();
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
            Enlazar();
            DesHabilitarEdicion();
        }

        private void toolBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
            Enlazar();
            DesHabilitarEdicion();

        }
        private void Buscar()
        {
            List<VistaDocumento> T = FactoryDocumentos.Buscar(dc, this.txtBuscar.Text, "DEVOLUCION", true);

            switch (T.Count)
            {
                case 0:
                    MessageBox.Show("Registro no encontrado", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    Limpiar();
                    break;
                case 1:
                    Documento = FactoryDocumentos.Item(dc, T[0].IdDocumento);
                    break;
                default:
                    FrmBuscarEntidades F = new FrmBuscarEntidades();
                    F.Texto = this.txtBuscar.Text;
                    F.myLayout = "DEVOLUCIONES";
                    F.Filtro = "DEVOLUCION";
                    F.dc = this.dc;
                    F.ShowDialog();
                    if (F.Registro == null)
                        return;
                    VistaDocumento VistaDocumento = (VistaDocumento)F.Registro;
                    Documento = FactoryDocumentos.Item(dc, VistaDocumento.IdDocumento);
                    break;
            }
        }

        private void ImprimirDo()
        {
            if (Documento == null)
                return;
            List<VistaFactura> Presupuesto = new List<VistaFactura>();
            Presupuesto = FactoryDocumentos.VistaFacturas(dc, Documento.IdDocumento);
            FrmReportes f = new FrmReportes();
            f.ReporteDevolucion(Presupuesto);
        }
        private void toolImprimir_Click(object sender, EventArgs e)
        {
            ImprimirDo();
        }
    }
}