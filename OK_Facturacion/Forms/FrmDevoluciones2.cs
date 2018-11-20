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
    public partial class FrmDevoluciones2 : Form
    {
        public FrmDevoluciones2()
        {
            InitializeComponent();
        }
        SoroEntities dc = new SoroEntities();
        Terceros titular = null;
        Productos producto = null;
        Documentos documento = null;
        Documentos documentoOriginal = null;
        DocumentosProductos documentoProducto = null;
        List<VistaFactura> items = new List<VistaFactura>();

        private void Forma_Load(object sender, EventArgs e)
        {
            DesHabilitarEdicion();
            this.txtCodigo.Validating += new CancelEventHandler(txtCodigo_Validating);
            this.txtCantidad.Validating += new CancelEventHandler(txtCantidad_Validating);
            this.comboBoxEditTipo.Properties.Items.AddRange(new Object[] { "FACTURA", "NOTA ENTREGA" });
            this.comboBoxEditTipo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEditTipo.Text = "FACTURA";
        }
        private void Limpiar()
        {
            dc = new SoroEntities();
            titular = new Terceros();
            documento = new Documentos();
            documentoOriginal = new Documentos();
            items = new List<VistaFactura>();
            txtFacturaDevolver.Text = "";
        }
        private void Incluir()
        {
            Limpiar();
            documento.Activo = true;
            documento.Fecha = DateTime.Today;
            documento.Tipo = "DEVOLUCION";
            documento.TasaIVA = FactoryParametros.Item().TasaIVA; 
            Enlazar();
            HabilitarEdicion();
            this.txtFacturaDevolver.Focus();
        }
        private void Enlazar()
        {
            if (documento == null)
            {
                documento = new Documentos();
            }
            titular = FactoryTerceros.Item(documento.IdTercero);
            if (titular == null)
            { 
                titular = new Terceros();
            }
            this.bs.DataSource = documento;
            this.bs.ResetBindings(true);
            this.bsDetalles.DataSource = documento.DocumentosProductos;
            this.bsDetalles.ResetBindings(true);
            this.bsTitular.DataSource = titular;
            this.bsTitular.ResetBindings(true);

        }
        private void Eliminar()
        {
            if (documento == null) return;
            if (MessageBox.Show("Esta seguro de eliminar este documento", "Atencion", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                return;
            Limpiar();
            Enlazar();
        }
        private void HabilitarEdicion()
        {
            Encab1.Enabled = true;
            Encab2.Enabled = true;
            this.gridControl1.Enabled = true;
            Encab1.Focus();
            this.Aceptar.Enabled = true;
            this.Cancelar.Enabled = true;
            this.txtBuscar.Enabled = false;
            this.Nuevo.Enabled = false;
            this.Imprimir.Enabled = false;
            this.btnEliminar.Enabled = false;
            this.btnBuscar.Enabled = false;
        }
        private void DesHabilitarEdicion()
        {
            Encab1.Enabled = false;
            Encab2.Enabled = false;
            this.gridControl1.Enabled = false;
            this.Aceptar.Enabled = false;
            this.Cancelar.Enabled = false;
            this.txtBuscar.Enabled = true;
            txtBuscar.Focus();
            this.Nuevo.Enabled = true;
            this.btnBuscar.Enabled = true;
            this.Imprimir.Enabled = true;
            if (documento == null)
            {
                this.Imprimir.Enabled = false;
                this.btnEliminar.Enabled = false;
            }
            else
            {
                if (string.IsNullOrEmpty(documento.Numero))
                {
                    this.btnEliminar.Enabled = true;
                }
                else
                {
                    this.btnEliminar.Enabled = false;
                }
            }
        }
        private void Incluir_Click(object sender, EventArgs e)
        {            
            Incluir();
        }
        private void Eliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
            DesHabilitarEdicion();
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
                        documento.CalcularTotales();
                    }
                    e.Handled = true;
                }
            }
        }
        private void txtCodigo_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.TextEdit Editor = (DevExpress.XtraEditors.TextEdit)this.gridControl1.MainView.ActiveEditor;
            if (!Editor.IsModified)
                return;
            string Texto = Editor.Text;
            List<VistaFactura> T = FactoryProductos.BuscarNoDevueltosFactura(Texto,items);
            switch (T.Count)
            {
                case 0:
                    Editor.Undo();
                    return;
                case 1:
                    producto = FactoryProductos.Item(T[0].IdProducto);
                    Editor.Text = producto.Codigo;
                    break;
                default:
                    FrmBuscarEntidades F = new FrmBuscarEntidades();
                    F.Texto = Texto;
                    F.myLayout = "PRODUCTOSNODEVUELTOS";
                    F.Filtro = "PRODUCTOSNODEVUELTOS";
                    F.dc = new SoroEntities();
                    F.items = items;
                    F.ShowDialog();
                    Application.DoEvents();
                    if (F.Registro != null)
                    {
                        producto = FactoryProductos.Item(((VistaFactura)F.Registro).IdProducto);
                        Editor.Text = producto.Codigo;
                    }
                    else
                    {
                        producto = new Productos();
                        Editor.Text = producto.Codigo;
                    }
                    break;
            }
            
            LeerProducto();
        }
        private void txtCantidad_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.SpinEdit Editor = (DevExpress.XtraEditors.SpinEdit)this.gridControl1.MainView.ActiveEditor;
            if (!Editor.IsModified)
                return;
            //if (FactoryParametros.Item().FacturarSinExistencia == false)
            //{
            //    if ((double)Editor.Value > documentoProducto.ExistenciaAnterior)
            //    {
            //        Editor.Value = (decimal)documentoProducto.ExistenciaAnterior;
            //    }
            //}
            documentoProducto = (DocumentosProductos)this.bsDetalles.Current;
            documentoProducto.Cantidad = (double)Editor.Value;
            documentoProducto.PesoTotal = documentoProducto.PesoUnitario * documentoProducto.Cantidad;
            CalcularMontoItem();
        }
        private void CalcularMontoItem()
        {
            this.bsDetalles.EndEdit();
            documentoProducto.DescuentoBs = documentoProducto.DescuentoPorcentaje.GetValueOrDefault(0) * documentoProducto.Precio / 100;
            documentoProducto.Iva = cBasicas.Round(documentoProducto.Precio * documentoProducto.TasaIva / 100);
            documentoProducto.PrecioIva = cBasicas.Round(documentoProducto.Precio + documentoProducto.Iva);
            documentoProducto.MontoNeto = cBasicas.Round(documentoProducto.Precio - documentoProducto.DescuentoBs);
            documentoProducto.Iva = cBasicas.Round(documentoProducto.MontoNeto * documentoProducto.TasaIva / 100);
            documentoProducto.Total = (documentoProducto.MontoNeto + documentoProducto.Iva) * documentoProducto.Cantidad;
            documento.CalcularTotales();
        }
        private void LeerProducto()
        {
            documentoProducto = (DocumentosProductos)this.bsDetalles.Current;
            this.gridControl1.MainView.ActiveEditor.Text = producto.Codigo;
            documentoProducto.Tipo = producto.Tipo;
            documentoProducto.IdProducto = producto.IdProducto;
            documentoProducto.Cantidad = producto.CantidadVentaPorDefecto;
            documentoProducto.DescuentoBs = 0;
            documentoProducto.Pvs = producto.PVP;

            if (!titular.DescuentoPorcentaje.HasValue)
            {
                titular.DescuentoPorcentaje = 0;
            }
            documentoProducto.DescuentoPorcentaje = titular.DescuentoPorcentaje;
            documentoProducto.TasaIva = producto.Iva;
            switch (titular.TipoPrecio)
            {
                case "PRECIO 1":
                    documentoProducto.Precio = cBasicas.Round(producto.Precio);
                    break;
                case "PRECIO 2":
                    documentoProducto.Precio = cBasicas.Round(producto.Precio2);
                    break;
                case "PRECIO 3":
                    documentoProducto.Precio = cBasicas.Round(producto.Precio3);
                    break;
                case "PRECIO 4":
                    documentoProducto.Precio = cBasicas.Round(producto.Precio4);
                    break;
            }
            documentoProducto.Codigo = producto.Codigo;
            documentoProducto.Descripcion = producto.Descripcion;
            documentoProducto.Iva = cBasicas.Round(documentoProducto.Precio * (documentoProducto.TasaIva / 100));
            documentoProducto.PrecioIva = documentoProducto.Precio + documentoProducto.Iva;
            documentoProducto.BloqueoPrecio = producto.BloqueoPrecio;
            documentoProducto.SolicitaServidor = producto.SolicitaServidor;
            documentoProducto.Costo = producto.Costo;
            documentoProducto.SolicitaServidor = producto.SolicitaServidor;
            documentoProducto.MontoNeto = producto.Precio;
            documentoProducto.Tipo = producto.Tipo;
            documentoProducto.ExistenciaAnterior = producto.Existencia;
            documentoProducto.PesoUnitario = producto.Peso;
            documentoProducto.PesoTotal = documentoProducto.PesoUnitario * documentoProducto.Cantidad;
            documentoProducto.UnidadMedida = producto.UnidadMedida;
            documentoProducto.IdProducto = producto.IdProducto;
            CalcularMontoItem();
        }
        private void txtFacturaDevolver_Validating(object sender, CancelEventArgs e)
        {
            if (!txtFacturaDevolver.IsModified)
            {
                return;
            }
            switch (comboBoxEditTipo.Text)
            {
                case "FACTURA":
                    BuscarFacturaDevolver();
                    break;
                case "NOTA ENTREGA":
                    BuscarNotaEntregaDevolver();
                    break;
            }
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
            var q = from p in dc.VistaFactura
                    where p.Numero == txtFacturaDevolver.Text
                    select p;
            if (q.FirstOrDefault() != null)
            {
                items = q.ToList();
                CargarFactura();
            }
        }
        private void BuscarNotaEntregaDevolver()
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
                    F.Filtro = "NOTA ENTREGA";
                    F.dc = this.dc;
                    F.ShowDialog();
                    if (F.Registro == null)
                        return;
                    VistaDocumento VistaDocumento = (VistaDocumento)F.Registro;
                    txtFacturaDevolver.Text = VistaDocumento.Numero;
                    break;
            }
            var q = from p in dc.VistaFactura
                    where p.Numero == txtFacturaDevolver.Text
                    select p;
            if (q.FirstOrDefault() != null)
            {
                items = q.ToList();
                CargarFactura();
            }
        }
        private void CargarFactura()
        {
            documentoOriginal = FactoryDocumentos.Item(dc, items[0].IdDocumento);
            var item = dc.Documentos.Where(x=>x.Comentarios==documentoOriginal.Numero).FirstOrDefault();
            if(item!=null)
            {
                MessageBox.Show("Esta factura ya tiene una anulacion anterior","Atencion", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            documento = new Documentos();
            documento.Activo = true;
            documento.IdTercero = items[0].IdTercero;
            documento.IdVendedor = items[0].IdTercero;
            documento.Tipo = "DEVOLUCION";
            documento.Fecha = DateTime.Today;
            documento.Efectivo = items[0].Efectivo;
            documento.Cheque = items[0].Cheque;
            documento.TarjetaCredito= items[0].TarjetaCredito;
            documento.TarjetaDebito= items[0].TarjetaDebito;            
            if (MessageBox.Show("Desea cargar todos los productos de esta factura para devolver", "Atencion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                CargarProductos();
            }
            titular = FactoryTerceros.Item(items[0].IdTercero);
            documentoOriginal = FactoryDocumentos.Item(dc,items[0].IdDocumento);
            this.bsTitular.DataSource = titular;
            this.bsTitular.ResetBindings(true);
            this.bs.DataSource = documento;
            this.bs.ResetBindings(true);
            this.bsDetalles.DataSource = documento.DocumentosProductos;
            this.bsDetalles.ResetBindings(true);
        }
        private void CargarProductos()
        {
            using (var dc = new SoroEntities())
            {
                var q = from p in dc.VistaFactura
                        where p.Numero == txtFacturaDevolver.Text
                        select p;
                foreach (VistaFactura Item in q)
                {
                    documentoProducto = new DocumentosProductos();
                    documentoProducto.Cantidad = Item.Cantidad;
                    documentoProducto.Codigo = Item.Codigo;
                    documentoProducto.Costo = Item.Costo;
                    documentoProducto.CostoIva = Item.CostoIva;
                    documentoProducto.Descripcion = Item.Descripcion;
                    documentoProducto.Iva = Item.Iva;
                    documentoProducto.MontoNeto = Item.MontoNeto;
                    documentoProducto.Precio = Item.Precio;
                    documentoProducto.PrecioIva = Item.PrecioIva;
                    documentoProducto.Total = Item.Total;
                    documentoProducto.TasaIva = Item.TasaIva;
                    documentoProducto.DescuentoBs = Item.DescuentoBs;
                    documentoProducto.IdProducto = Item.IdProducto;
                    documentoProducto.UnidadMedida = Item.UnidadMedida;
                    documento.DocumentosProductos.Add(documentoProducto);
                    CalcularMontoItem();
                }                
            }
        }
        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.bs.CancelEdit();
            Limpiar();
            Enlazar();
            DesHabilitarEdicion();
        }
        private void txtFacturaDevolver_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch (comboBoxEditTipo.Text)
            {
                case "FACTURA":
                    BuscarFacturaDevolver();
                    break;
                case "NOTA ENTREGA":
                    BuscarNotaEntregaDevolver();
                    break;
            }
        }
        private void Aceptar_Click(object sender, EventArgs e)
        {
            try
            {
                documento.Comentarios = documentoOriginal.Numero;
                if (!checkImprimirDevolucion.Checked)
                {
                    documento.Numero = "S/N";
                }
                FactoryDocumentos.Save(dc, documento, titular);
                if (checkDevolverInventario.Checked)
                {
                    DevolverInventario();
                }
                if (checkGenerarNotaCredito.Checked)
                {
                    GenerarNotaCredito();
                }
                if (checkImprimirDevolucion.Checked)
                {
                   ImprimirDevolucion();
                }
                Limpiar();
                Enlazar();
                DesHabilitarEdicion();
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void ImprimirDevolucion()
        {
            FrmReportes Reporte = new FrmReportes();
            Reporte.ReporteDevolucion(documento);

        }

        private void GenerarNotaCredito()
        {
            using(var dc = new SoroEntities())
            {
                Documentos doc = FactoryDocumentos.Item(dc,documentoOriginal.IdDocumento);
                doc.Saldo = doc.Saldo - documento.MontoTotal;
                Cuentas cuenta = FactoryCuentas.Item(dc, documentoOriginal.IdDocumento);
                if(cuenta!=null)
                   cuenta.Saldo = cuenta.Saldo - documento.MontoTotal;
                RegistroPagos pago = new RegistroPagos();
                pago.IdRegistroPago = FactoryContadores.GetLast("IdRegistroPago");
                pago.Modulo = "DEVOLUCION";
                pago.Documento = FactoryContadores.GetLast("NumeroNotaCreditoPago");
                pago.IdDocumento = documentoOriginal.IdDocumento;
                pago.Fecha = DateTime.Today;
                pago.MontoPagar = documentoOriginal.Saldo;
                
                pago.MontoPagado = documento.MontoTotal;
                pago.IdTercero = documentoOriginal.IdTercero;
                pago.RazonSocial = titular.RazonSocial;
                pago.SaldoPendiente = documentoOriginal.Saldo - documento.MontoTotal;
                pago.Documento = documentoOriginal.Numero;
                pago.Tipo = "NC";
                RegistroPagosDetalles detalle = new RegistroPagosDetalles();
                detalle.IdRegistroPagosDetalle = FactoryContadores.GetLast("IdRegistroPagosDetalle");
                detalle.IdDocumento = documentoOriginal.IdDocumento;
                detalle.Monto = documento.MontoTotal;
                detalle.Numero = documentoOriginal.Numero;
                detalle.Tipo = "CXC";
                dc.RegistroPagos.Add(pago);
                dc.RegistroPagosDetalles.Add(detalle);
                dc.SaveChanges();
                ///
                //var ndocumento = new Documentos();
                //ndocumento.Tipo = "NOTA CREDITO";
                //ndocumento.IdDocumento = FactoryContadores.GetLast("IdDocumento");
                //ndocumento.Numero = doc.Numero;
                //ndocumento.Comentarios = string.Format("DOCUMENTO AFECTADO {0}", documentoOriginal.Numero);
                CFacturas.DevolucionLibroDeVentas(documento, documentoOriginal.Numero);
                ////

            }
        }

        private void DevolverInventario()
        {
            CFacturas.DevolverInventario(documento);
        }

        private void Imprimir_Click(object sender, EventArgs e)
        {
            FrmReportes f = new FrmReportes();
            f.ReporteDevolucion(documento);
        }

        private void toolBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
            Enlazar();
            DesHabilitarEdicion();
        }
        private void Buscar()
        {
            List<VistaDocumento> T = FactoryDocumentos.Buscar(dc, this.txtBuscar.Text, "FACTURA", true);

            switch (T.Count)
            {
                case 0:
                    MessageBox.Show("Registro no encontrado", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    Limpiar();
                    break;
                case 1:
                    documento = FactoryDocumentos.Item(dc, T[0].IdDocumento);
                    break;
                default:
                    FrmBuscarEntidades F = new FrmBuscarEntidades();
                    F.Texto = this.txtBuscar.Text;
                    F.myLayout = "FACTURAS";
                    F.Filtro = "DEVOLUCION";

                    F.dc = this.dc;
                    F.ShowDialog();
                    if (F.Registro == null)
                        return;
                    VistaDocumento VistaDocumento = (VistaDocumento)F.Registro;
                    documento = FactoryDocumentos.Item(dc, VistaDocumento.IdDocumento);
                    break;
            }
            Enlazar();
        }
    }
}
