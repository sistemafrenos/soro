using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Linq;

namespace HK
{
    public partial class FrmPresupuestos : Form
    {
        public FrmPresupuestos()
        {
            InitializeComponent();
        }
        SoroEntities dc = new SoroEntities();
        Terceros Titular = null;
        Terceros Vendedor = null;
        Productos Producto = null;
        Documentos Documento = null;
        DocumentosProductos DocumentoProducto = null;
        Recibos Recibo = null;
        List<Terceros> Tecnicos = FactoryTerceros.ItemsxTipo("TECNICO");

        private void Forma_Load(object sender, EventArgs e)
        {
            DesHabilitarEdicion();
            this.txtCodigo.Validating += new CancelEventHandler(txtCodigo_Validating);
            this.txtCantidad.Validating += new CancelEventHandler(txtCantidad_Validating);
            this.txtPrecio.Validating += new CancelEventHandler(txtPrecio_Validating);
            this.txtPrecioIVA.Validating += new CancelEventHandler(txtPrecioIVA_Validating);
            this.txtDescuentoPorcentaje.Validating += new CancelEventHandler(DescuentoPorcentaje_Validating);
            this.txtDescuentoBs.Validating += new CancelEventHandler(DescuentoBolivares_Validating);
            this.cmdRealizadoPor.Validating += new CancelEventHandler(cmbRealizadoPor_Validating);
            foreach( Terceros Tecnico in Tecnicos)
            {
                this.cmdRealizadoPor.Items.Add(Tecnico.RazonSocial);
            }
            #region Custom
            switch (FactoryParametros.Item().Empresa)
            {
                case "FARMACIA CHUPARIN,C.A.":
                    {
                        this.colDescuentoBs.Visible = false;
                        this.colDescuentoPorcentaje.Visible = false;
                        this.colRealizadoPor.Visible = false;
                        break;
                    }
            }
            #endregion
        }
        private void Limpiar()
        {
            dc = new SoroEntities();
            Titular = new Terceros();
            Vendedor = new Terceros();
            Documento = new Documentos();
        }
        private void Incluir()
        {
            Limpiar();
            Documento.Activo = true;
            Documento.Fecha = DateTime.Today;
            Documento.TasaIVA = FactoryParametros.Item().TasaIVA;
            Titular.Direccion = FactoryParametros.Item().EmpresaCiudad;
            Vendedor = FactoryTerceros.ItemxNombre(FactoryParametros.Item().Vendedor);
            if (Vendedor != null)
            {
                Documento.IdVendedor = Vendedor.IdTercero;
            }
            Enlazar();
            HabilitarEdicion();
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
        private void Eliminar()
        {
            if (Documento == null) return;
            if (MessageBox.Show("Esta seguro de eliminar este Documento", "Atencion", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                return;
            FactoryDocumentos.Delete(Documento.IdDocumento);
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
            this.btnEliminar.Enabled = false;
            this.Nuevo.Enabled = false;
            this.Imprimir.Enabled = false;
            this.btnEliminar.Enabled = false;
            this.toolCargarDocumento.Enabled = true;
            toolActualizarPrecios.Enabled = true;

        }
        private void DesHabilitarEdicion()
        {
            Encab1.Enabled = false;
            Encab2.Enabled = false;
            toolActualizarPrecios.Enabled = false;
            this.gridControl1.Enabled = false;
            this.Aceptar.Enabled = false;
            this.Cancelar.Enabled = false;
            this.txtBuscar.Enabled = true;
            this.btnEliminar.Enabled = true;
            this.toolCargarDocumento.Enabled = false;
            txtBuscar.Focus();
            this.Nuevo.Enabled = true;
            if (Documento == null)
            {
                this.Imprimir.Enabled = false;
                this.btnEliminar.Enabled = false;
            }
            if (Documento != null)
            {
                this.Imprimir.Enabled = true;
                this.btnEliminar.Enabled = true;
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
        private void EscribirTitular()
        {
            Titular.CedulaRif = this.txtCedulaTitular.Text;
            Titular.RazonSocial = this.razonSocialTextBox.Text;
            Titular.Direccion = this.direccionTextBox.Text;
            Titular.Email = this.emailTextBox.Text;
            Titular.Telefonos = this.telefonosTextBox.Text;
            if (string.IsNullOrEmpty(Titular.IdTercero))
            {
                Titular.Tipo = "CLIENTE";
            }
        }
        private void toolGuardar_Click(object sender, EventArgs e)
        {
            this.bs.EndEdit();
            this.bsDetalles.EndEdit();
            if (Documento.MontoTotal == 0)
            {
                MessageBox.Show("No puede estar vacio para guardar", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Guardar Titular
            txtCedulaTitular.Text = cBasicas.CedulaRif(txtCedulaTitular.Text);
            if (string.IsNullOrEmpty(txtCedulaTitular.Text))
            {
                MessageBox.Show("No puede dejar cliente estar vacio para guardar", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                EscribirTitular();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Guardar
            Documento.Tipo = "PRESUPUESTO";
            Documento.Vence = Documento.Fecha.Value.AddDays(30);
            Documento.IdVendedor = Vendedor.IdTercero;
            try
            {
                FactoryDocumentos.Save(dc, Documento,  Titular);
                ImprimirDo();
                Enlazar();
                DesHabilitarEdicion();
            }
            catch(Exception x )
            {
                MessageBox.Show(x.Message, "Error al guardar PRESUPUESTO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void ValidarDescuentoPorcentaje(object sender, CancelEventArgs e)
        {
            Documentos d = new Documentos();
            d.Fecha = DateTime.Today;
            DocumentosProductos Detalle = new DocumentosProductos();
            d.DocumentosProductos.Add(Detalle);

            DevExpress.XtraEditors.SpinEdit Editor = (DevExpress.XtraEditors.SpinEdit)sender;
            if (!Editor.IsModified)
                return;
            this.bs.EndEdit();
            if (Documento.MontoTotal == 0)
            {
                Documento.DescuentoBs = 0;
                Documento.DescuentoPorcentaje = 0;
                return;
            }
            Documento.CalcularTotales();
            Documento.DescuentoBs = (double)Editor.Value * Documento.SubTotal / 100;
            Documento.CalcularTotales();
        }
        private void ValidarDescuentoBolivares(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.SpinEdit Editor = (DevExpress.XtraEditors.SpinEdit)sender;
            if (!Editor.IsModified)
                return;
            this.bs.EndEdit();
            if (Documento.MontoTotal == 0)
            {
                Documento.DescuentoBs = 0;
                Documento.DescuentoPorcentaje = 0;
                return;
            }
            if (Documento.DescuentoBs > Documento.SubTotal)
            {
                Editor.Value = 0;
            }

            Documento.CalcularTotales();
            Documento.DescuentoPorcentaje = (Documento.DescuentoBs) / (Documento.SubTotal / 100);
            Documento.CalcularTotales();
        }
        private void toolBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
            Enlazar();
            HabilitarEdicion();
        }
        private void Buscar()
        {
            List<VistaDocumento> T = FactoryDocumentos.Buscar(dc, this.txtBuscar.Text, "PRESUPUESTO", true);

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
                    F.myLayout = "PRESUPUESTOS";
                    F.Filtro = "PRESUPUESTO ";
                    F.dc = this.dc;
                    F.ShowDialog();
                    if (F.Registro == null)
                        return;
                    VistaDocumento VistaDocumento = (VistaDocumento)F.Registro;
                    Documento = FactoryDocumentos.Item(dc, VistaDocumento.IdDocumento);
                    break;
            }
            Enlazar();
        }
        private void toolCancelar_Click(object sender, EventArgs e)
        {
            this.bs.CancelEdit();
            Limpiar();
            Enlazar();
            DesHabilitarEdicion();
        }
        private void ImprimirDo()
        {
            if (Documento == null)
                return;

            List<VistaFactura> Presupuesto = new List<VistaFactura>();
            Presupuesto = FactoryDocumentos.VistaFacturas(dc, Documento.IdDocumento);
            FrmReportes f = new FrmReportes();
            f.ReportePresupuesto(Presupuesto);
        }
        private void toolImprimir_Click(object sender, EventArgs e)
        {
            ImprimirDo();
        }
        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            DocumentosProductos Fila = (DocumentosProductos)bsDetalles.Current;
            if (Fila == null)
                return;
            if (this.gridView1.FocusedColumn.Name == "colPrecio" || this.gridView1.FocusedColumn.Name == "colPrecioIva")
            {
                if (Fila.BloqueoPrecio.HasValue)
                {
                    e.Cancel = Fila.BloqueoPrecio.Value;
                }
            }
            if (this.gridView1.FocusedColumn.Name == "colRealizadoPor" || this.gridView1.FocusedColumn.Name == "colPrecioIva")
            {
                if (Fila.SolicitaServidor.HasValue)
                {
                    e.Cancel = Fila.BloqueoPrecio.Value;
                }
            }
        }
        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {
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
        private void txtTitular_Validating(object sender, CancelEventArgs e)
        {            
            DevExpress.XtraEditors.TextEdit Editor = (DevExpress.XtraEditors.TextEdit)sender;
            if (!Editor.IsModified)
                return;            
            string Texto = Editor.Text;
            List<Terceros> T = FactoryTerceros.Buscar(Texto);
            switch (T.Count)
            {
                case 0:
                    if ((Editor.Text[0] == 'J' || Editor.Text[0] == 'G') && cBasicas.CedulaRif(Editor.Text).Length != 10)
                    {
                        MessageBox.Show("Numero de Rif Invalido");
                        Editor.Text = "";
                        e.Cancel = true;
                        return;
                    }
                    Titular = new Terceros();
                    Editor.Text = cBasicas.CedulaRif(Editor.Text);
                    Titular.CedulaRif = Editor.Text;
                    Titular.Direccion = FactoryParametros.Item().EmpresaCiudad;
                    Titular.DescuentoPorcentaje = 0;
                    break;
                case 1:
                    Titular = T[0];
                    Titular = FactoryTerceros.Item( Titular.IdTercero);
                    break;
                default:
                    FrmBuscarEntidades F = new FrmBuscarEntidades();
                    F.Texto = Texto;
                    F.myLayout = "TERCEROS";
                    F.Filtro = "";
                    F.dc = dc;
                    F.ShowDialog();
                    if (F.Registro != null)
                    {
                        Titular = (Terceros)F.Registro;
                        Titular = FactoryTerceros.Item( Titular.IdTercero);
                    }
                    else
                    {
                        Titular = new Terceros();
                    }
                    break;
            }
            LeerTitular();
        }
        private void BuscarTitulares_Click(object sender, EventArgs e)
        {
            FrmBuscarEntidades F = new FrmBuscarEntidades();
            F.Texto = "";
            F.myLayout = "TERCEROS";
            F.Filtro = "";
            F.dc = dc;
            F.ShowDialog();
            if (F.Registro != null)
            {
                Titular = (Terceros)F.Registro;
                Titular = FactoryTerceros.Item(Titular.IdTercero);
                LeerTitular();
            }
            else
            {
                Titular = new Terceros();
                LeerTitular();
            }
        }
        private void Editar_Click(object sender, EventArgs e)
        {
            if (Documento == null)
                return;
            HabilitarEdicion();
        }
        private void LeerVendedor()
        {
            if (Vendedor == null)
            {
                Vendedor = new Terceros();
            }
            txtVendedor.Text = Vendedor.RazonSocial;
        }
        private void txtVendedor_Validating(object sender, CancelEventArgs e)
        {
            if (!this.txtVendedor.IsModified) return;

            if (this.txtVendedor.Text.Length == 0)
            {
                Vendedor = new Terceros();
                return;
            }
            List<Terceros> T = FactoryTerceros.Buscar(this.txtVendedor.Text, "VENDEDOR");
            switch (T.Count)
            {
                case 0:
                    MessageBox.Show("Vendedor no encontrado", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Vendedor = new Terceros();
                    break;
                case 1:
                    Vendedor = FactoryTerceros.Item(T[0].IdTercero);
                    break;
                default:
                    FrmBuscarEntidades F = new FrmBuscarEntidades();
                    F.Texto = txtVendedor.Text;
                    F.myLayout = "TERCEROS";
                    F.Filtro = "VENDEDOR";
                    F.dc = dc;
                    F.ShowDialog();
                    if (F.Registro != null)
                    {
                        Vendedor = FactoryTerceros.Item( ((Terceros)F.Registro).IdTercero);
                    }
                    break;
            }
            LeerVendedor();
        }
        private bool CargarPagos()
        {
            FrmRegistrarCobros F = new FrmRegistrarCobros();
            Recibo = new Recibos();
            Recibo.Activo = true;
            Recibo.Concepto = "PAGO PRESUPUESTO";
            Recibo.Fecha = DateTime.Today;
            Recibo.Monto = Documento.MontoTotal;
            RecibosDetalles ReciboDetalles = new RecibosDetalles();
            ReciboDetalles.MontoAbono = Documento.MontoTotal;
            ReciboDetalles.Tipo = "PAGO";
            Recibo.RecibosDetalles.Add(ReciboDetalles);
            F.Recibo = Recibo;
            F.ShowDialog();
            if (F.DialogResult != DialogResult.OK)
            {
                return false;
            }
            if (F.Recibo.Monto > 0)
            {
                dc.Recibos.Add(Recibo);
            }
            return true;

        }
        #region Servicios
        private void txtCodigo_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.TextEdit Editor = (DevExpress.XtraEditors.TextEdit)this.gridControl1.MainView.ActiveEditor;
            if (!Editor.IsModified)
                return;
            string Texto = Editor.Text;
            List<VistaProductos> T = FactoryProductos.Buscar(new SoroEntities(), Texto);
            switch (T.Count)
            {
                case 0:
                    if (MessageBox.Show("Producto o Servicio no Encontrado, Desea crear uno nuevo", "Atencion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        return;
                    }
                    FrmProductosItem Nuevo = new FrmProductosItem();
                    Nuevo.codigo = Texto;
                    Nuevo.CargarDatos.Visible = true;
                    Nuevo.ShowDialog();
                    if (Nuevo.DialogResult == DialogResult.OK)
                    {
                        Producto = Nuevo.Producto;
                    }
                    else
                    {
                        Producto = new Productos();
                    }
                    Editor.Text = Producto.Codigo;
                    break;
                case 1:
                    Producto = FactoryProductos.Item(T[0].IdProducto);
                    Editor.Text = Producto.Codigo;
                    break;
                default:
                    FrmBuscarEntidades F = new FrmBuscarEntidades();
                    F.Texto = Texto;
                    F.myLayout = "PRODUCTOS";
                    F.Filtro = "";
                    F.dc = new SoroEntities();
                    F.ShowDialog();
                    if (F.Registro != null)
                    {
                        Producto = FactoryProductos.Item(((VistaProductos)F.Registro).IdProducto);
                        Editor.Text = Producto.Codigo;
                    }
                    else
                    {
                        Producto = new Productos();
                        Editor.Text = Producto.Codigo;
                    }
                    break;
            }
            LeerProducto();
        }
        private void cmbRealizadoPor_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.ComboBoxEdit Editor = (DevExpress.XtraEditors.ComboBoxEdit)this.gridControl1.MainView.ActiveEditor;
            if (!Editor.IsModified)
                return;
            DocumentoProducto = (DocumentosProductos)this.bsDetalles.Current;
            Terceros T = FactoryTerceros.ItemxNombre(Editor.Text);
            if (T != null)
            {
                DocumentoProducto.IdServidor = T.IdTercero;
            }
        }
        private void DescuentoPorcentaje_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.SpinEdit Editor = (DevExpress.XtraEditors.SpinEdit)this.gridControl1.MainView.ActiveEditor;
            if (!Editor.IsModified)
                return;
            DocumentoProducto = (DocumentosProductos)this.bsDetalles.Current;
            DocumentoProducto.DescuentoPorcentaje = (double)Editor.Value;
            CalcularMontoItem();
            Documento.CalcularTotales();
        }
        private void DescuentoBolivares_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.SpinEdit Editor = (DevExpress.XtraEditors.SpinEdit)this.gridControl1.MainView.ActiveEditor;
            if (!Editor.IsModified)
                return;
            DocumentoProducto = (DocumentosProductos)this.bsDetalles.Current;
            DocumentoProducto.DescuentoBs = (double)Editor.Value;
            DocumentoProducto.DescuentoPorcentaje = 0;
            try
            {
                DocumentoProducto.DescuentoPorcentaje = (DocumentoProducto.DescuentoBs) / (DocumentoProducto.Precio / 100);
            }
            catch { }
            CalcularMontoItem();
            Documento.CalcularTotales();
        }
        private void txtPrecioIVA_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.SpinEdit Editor = (DevExpress.XtraEditors.SpinEdit)this.gridControl1.MainView.ActiveEditor;
            if (!Editor.IsModified)
                return;
            DocumentoProducto = (DocumentosProductos)this.bsDetalles.Current;
            DocumentoProducto.PrecioIva = cBasicas.Round((double)Editor.Value);
            DocumentoProducto.Precio = cBasicas.Round(DocumentoProducto.PrecioIva / (1 + (DocumentoProducto.TasaIva) / 100));
            CalcularMontoItem();
            Documento.CalcularTotales();
        }
        private void txtPrecio_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.SpinEdit Editor = (DevExpress.XtraEditors.SpinEdit)this.gridControl1.MainView.ActiveEditor;
            if (!Editor.IsModified)
                return;
            DocumentoProducto = (DocumentosProductos)this.bsDetalles.Current;
            DocumentoProducto.Precio = cBasicas.Round((double)Editor.Value);
            CalcularMontoItem();
            Documento.CalcularTotales();
        }
        private void txtCantidad_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.SpinEdit Editor = (DevExpress.XtraEditors.SpinEdit)this.gridControl1.MainView.ActiveEditor;
            if (!Editor.IsModified)
                return;
            DocumentoProducto = (DocumentosProductos)this.bsDetalles.Current;
            DocumentoProducto.Cantidad = (double)Editor.Value;
            CalcularMontoItem();
            Documento.CalcularTotales();
        }
        private void CalcularMontoItem()
        {
            this.bsDetalles.EndEdit();
            DocumentoProducto.DescuentoBs = DocumentoProducto.DescuentoPorcentaje * DocumentoProducto.Precio / 100;
            DocumentoProducto.Iva = cBasicas.Round(DocumentoProducto.Precio * DocumentoProducto.TasaIva / 100);
            DocumentoProducto.PrecioIva = cBasicas.Round(DocumentoProducto.Precio + DocumentoProducto.Iva);
            DocumentoProducto.MontoNeto = cBasicas.Round(DocumentoProducto.Precio - DocumentoProducto.DescuentoBs);
            DocumentoProducto.Iva = cBasicas.Round(DocumentoProducto.MontoNeto * DocumentoProducto.TasaIva / 100);
            DocumentoProducto.CostoIva = cBasicas.Round( DocumentoProducto.Costo + (DocumentoProducto.TasaIva * DocumentoProducto.Costo / 100));
            DocumentoProducto.Total = (DocumentoProducto.MontoNeto + DocumentoProducto.Iva) * DocumentoProducto.Cantidad;
        }
        private void LeerProducto()
        {
            DocumentoProducto = (DocumentosProductos)this.bsDetalles.Current;
            this.gridControl1.MainView.ActiveEditor.Text = Producto.Codigo;
            DocumentoProducto.Tipo = Producto.Tipo;
            DocumentoProducto.IdProducto = Producto.IdProducto;
            if (Producto.CantidadVentaPorDefecto.HasValue)
            {
                DocumentoProducto.Cantidad = Producto.CantidadVentaPorDefecto;
            }
            else
            {
                DocumentoProducto.Cantidad = 1;
            }
            double IvaUnitario = cBasicas.Round(Producto.Precio * (DocumentoProducto.TasaIva / 100));
            if (!Titular.DescuentoPorcentaje.HasValue)
            {
                Titular.DescuentoPorcentaje = 0;
            }
            DocumentoProducto.DescuentoPorcentaje = Titular.DescuentoPorcentaje;
            DocumentoProducto.DescuentoBs = 0;
            DocumentoProducto.TasaIva = Producto.Iva;
            DocumentoProducto.Precio = cBasicas.Round(Producto.Precio);
            DocumentoProducto.Codigo = Producto.Codigo;
            DocumentoProducto.Descripcion = Producto.Descripcion;
            DocumentoProducto.Iva = DocumentoProducto.Cantidad * IvaUnitario;
            DocumentoProducto.PrecioIva = cBasicas.Round( Producto.Precio + IvaUnitario);
            DocumentoProducto.BloqueoPrecio = Producto.BloqueoPrecio;
            DocumentoProducto.SolicitaServidor = Producto.SolicitaServidor;
            DocumentoProducto.Costo = Producto.Costo;
            DocumentoProducto.CostoIva = cBasicas.Round(DocumentoProducto.Costo + (DocumentoProducto.TasaIva * DocumentoProducto.Costo / 100));
            DocumentoProducto.SolicitaServidor = Producto.SolicitaServidor;
            DocumentoProducto.MontoNeto = Producto.Precio;
            DocumentoProducto.Tipo = Producto.Tipo;
            DocumentoProducto.ExistenciaAnterior = Producto.Existencia;
            CalcularMontoItem();
            Documento.CalcularTotales();
        }
        private void DocumentosProductosBindingSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (Documento == null)
                return;
            if (bsDetalles.Current == null)
                return;
            DocumentosProductos detalle = (DocumentosProductos)bsDetalles.Current;
            if (detalle.IdProducto != null)
                this.bsDetalles.EndEdit();
            Documento.CalcularTotales();
        }
        private void gridView1_InvalidRowException_1(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
            this.bsDetalles.CancelEdit();
        }
        private void gridView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DocumentosProductos p = (DocumentosProductos)e.Row;
            if (string.IsNullOrEmpty(p.IdProducto))
            {
                p = new DocumentosProductos();
                e.Valid = false;
                e.ErrorText = "Error en codigo del producto, Esc para cancelar";
            }
            if (Documento.Tipo == "FACTURA" || Documento.Tipo == "PEDIDO")
            {
                if (p.Cantidad > p.ExistenciaAnterior)
                {
                    if (MessageBox.Show("Esta facturando mas de la existencia, es correcto", "Atencion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        e.Valid = false;
                        p.Cantidad = 0;
                    }
                }
            }
        }
        #endregion

        private void CargarDocumento_Click(object sender, EventArgs e)
        {
            FrmBuscarEntidades F = new FrmBuscarEntidades();
            F.myLayout = "BUSCARVENTAS";
            F.ShowDialog();
            if (F.DialogResult == DialogResult.OK)
            {
                CargarDocumento(FactoryDocumentos.Item(dc, ((VistaDocumento)F.Registro).IdDocumento));
            }
        }
        private void CargarDocumento(Documentos Doc)
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
            Documento.Tipo = "PRESUPUESTO";
            Documento.Activo = true;
            Documento.Año = DateTime.Today.Year;
            Documento.Fecha = DateTime.Today;
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
                newItem.Precio4 = Item.Precio4;
                newItem.Precio5 = Item.Precio5;
                newItem.PrecioIva = Item.PrecioIva;
                newItem.SolicitaServidor = Item.SolicitaServidor;
                newItem.TasaIva = Item.TasaIva;
                newItem.Tipo = Item.Tipo;
                newItem.Total = Item.Total;
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

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            foreach (DocumentosProductos d in Documento.DocumentosProductos)
            {
                Producto = FactoryProductos.Item( d.IdProducto );
                d.Precio = Producto.Precio;
                DocumentoProducto = d;
                CalcularMontoItem();
                Documento.CalcularTotales();
            }
        }

        private void txtCedulaTitular_Validated(object sender, EventArgs e)
        {

        }
    }
}