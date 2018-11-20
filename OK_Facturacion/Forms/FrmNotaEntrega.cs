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
    public partial class FrmNotaEntrega : Form
    {
        SoroEntities dc = new SoroEntities();
        Terceros Titular = null;
        Terceros Vendedor = null;
        Productos Producto = null;
        Documentos Documento = null;
        DocumentosProductos DocumentoProducto = null;
        RegistroPagos Recibo = new RegistroPagos();
        public FrmNotaEntrega()
        {
            InitializeComponent();
            this.Load += new EventHandler(FrmNotaEntrega_Load);
        }

        void FrmNotaEntrega_Load(object sender, EventArgs e)
        {
            DesHabilitarEdicion();
            this.txtCodigo.Validating += new CancelEventHandler(txtCodigo_Validating);
            this.txtCantidad.Validating += new CancelEventHandler(txtCantidad_Validating);
            this.txtPrecio.Validating += new CancelEventHandler(txtPrecio_Validating);
            this.txtPrecioIVA.Validating += new CancelEventHandler(txtPrecioIVA_Validating);
            this.txtDescuentoPorcentaje.Validating += new CancelEventHandler(txtDescuentoPorcentaje_Validating);
            this.colRealizadoPor.Visible = false;
            this.colUnidadMedida.Visible = true;
            this.colPeso.Visible = false;
            this.colExistencia.Visible = true;
            this.colRealizadoPor.Visible = false;
            this.colExistencia.OptionsColumn.AllowEdit = false;
            this.colDescuentoBs.OptionsColumn.AllowEdit = false;
            this.colPrecio.OptionsColumn.AllowEdit = true;
            this.colPrecioIva.OptionsColumn.AllowEdit = false;
            this.colTotal.OptionsColumn.AllowEdit = false;
            this.colExistencia.OptionsColumn.AllowFocus = false;
            this.colDescuentoBs.OptionsColumn.AllowFocus = false;
            this.colPrecio.OptionsColumn.AllowFocus = false;
            this.colPrecioIva.OptionsColumn.AllowFocus = false;
            this.colTotal.OptionsColumn.AllowFocus = false;
            this.colDescripcion.OptionsColumn.AllowFocus = false;
            this.Nuevo.Click += new EventHandler(Nuevo_Click);
            this.toolCargarDocumento.Click += new EventHandler(toolCargarDocumento_Click);
            this.btnEliminar.Click += new EventHandler(btnEliminar_Click);
            this.Aceptar.Click += new EventHandler(Aceptar_Click);
            this.Cancelar.Click += new EventHandler(Cancelar_Click);
            this.Imprimir.Click += new EventHandler(Imprimir_Click);
            this.btnBuscar.Click += new EventHandler(btnBuscar_Click);
            this.txtCedulaTitular.Validating += new CancelEventHandler(txtCedulaTitular_Validating);
            this.BuscarTitulares.Click+=new EventHandler(BuscarTitulares_Click);
            this.simpleButton1.Click+=new EventHandler(simpleButton1_Click);
            this.gridControl1.KeyDown+=new KeyEventHandler(gridControl1_KeyDown);
            this.bsDetalles.ListChanged += new ListChangedEventHandler(bsDetalles_ListChanged);
        }

        void txtDescuentoPorcentaje_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.SpinEdit x=(DevExpress.XtraEditors.SpinEdit)sender;
            DocumentoProducto.DescuentoPorcentaje = (double)x.Value;
            this.bsDetalles.EndEdit();
            
            CalcularMontoItem();
        }

        void bsDetalles_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (Documento == null)
                return;
            if (bsDetalles.Current == null)
                return;
            DocumentosProductos detalle = (DocumentosProductos)bsDetalles.Current;
            if (detalle.IdProducto != null)
            {
                this.bsDetalles.EndEdit();
                //   Documento.CalcularTotales();
            }
        }

        void txtCedulaTitular_Validating(object sender, CancelEventArgs e)
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
                    Titular.CedulaRif = cBasicas.CedulaRif(Editor.Text);
                    Titular.Direccion = FactoryParametros.Item().EmpresaCiudad;
                    Titular.DescuentoPorcentaje = 0;
                    Titular.TipoPrecio = "PRECIO 1";
                    Titular.Condiciones = "CONTADO";
                    Titular.DiasCredito = 0;
                    Titular.LimiteCredito = 0;
                    break;
                case 1:
                    Titular = T[0];
                    Titular = FactoryTerceros.Item(Titular.IdTercero);
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
                        Titular = FactoryTerceros.Item(Titular.IdTercero);
                    }
                    else
                    {
                        Titular = new Terceros();
                    }
                    break;
            }
            LeerTitular();
            Documento.Comentarios = Titular.Condiciones;
            if (string.IsNullOrWhiteSpace(Titular.RazonSocial))
                this.razonSocialTextBox.Focus();
            else
                this.gridControl1.Focus();
        }
        void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
            Enlazar();
            DesHabilitarEdicion();
        }
        void Imprimir_Click(object sender, EventArgs e)
        {
            if (Documento == null)
                return;
            cBasicas.ImprimirNotaEntrega(Documento);
        }
        void Cancelar_Click(object sender, EventArgs e)
        {
            this.bs.CancelEdit();
            Limpiar();
            Enlazar();
            DesHabilitarEdicion();
        }
        void Aceptar_Click(object sender, EventArgs e)
        {
            try
            {

                if (!DoGuardar())
                {
                    return;
                }
                Limpiar();
                Enlazar();
                DesHabilitarEdicion();
            }
            catch (Exception x)
            {
                string s = x.Message;
            }
            
        }
        void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
            DesHabilitarEdicion();
        }
        void toolCargarDocumento_Click(object sender, EventArgs e)
        {
            FrmBuscarEntidades F = new FrmBuscarEntidades();
            F.myLayout = "BUSCARVENTAS";
            F.ShowDialog();
            if (F.DialogResult == DialogResult.OK)
            {
                CargarDocumento(FactoryDocumentos.Item(dc, ((VistaDocumento)F.Registro).IdDocumento));
            }
        }
        void Nuevo_Click(object sender, EventArgs e)
        {
            Incluir();
        }
        private void Limpiar()
        {
            dc = new SoroEntities();
            Titular = new Terceros();
            Titular.DescuentoPorcentaje = 0;
            Titular.TipoPrecio = "PRECIO 1";
            Titular.DiasCredito = 0;
            Vendedor = new Terceros();
            Documento = new Documentos();
            Recibo = new RegistroPagos();
        }
        private void Incluir()
        {
            Limpiar();
            Documento.Activo = true;
            Documento.Fecha = DateTime.Today;
            Documento.Tipo = "NOTA ENTREGA";
            Documento.TasaIVA = FactoryParametros.Item().TasaIVA;
            Titular.Direccion = FactoryParametros.Item().EmpresaCiudad;
            Vendedor = FactoryTerceros.ItemxNombre(FactoryUsuarios.UsuarioActivo.Nombre);
            if (Vendedor != null)
            {
                Documento.IdVendedor = Vendedor.IdTercero;
            }
            Enlazar();
            HabilitarEdicion();
            this.txtCedulaTitular.Focus();
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
            if (string.IsNullOrEmpty(Documento.Numero))
            {
                FactoryDocumentos.Delete(Documento.IdDocumento);
            }
            else
            {
                MessageBox.Show("Esta Nota ya fue impresa y no se puede eliminar debe realizar una devolucion", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Limpiar();
            Enlazar();
        }
        private void HabilitarEdicion()
        {
            Encab1.Enabled = true;
            Encab2.Enabled = true;
            this.toolCargarDocumento.Enabled = true;
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
            this.toolCargarDocumento.Enabled = false;
            this.gridControl1.Enabled = false;
            this.Aceptar.Enabled = false;
            this.Cancelar.Enabled = false;
            this.txtBuscar.Enabled = true;
            txtBuscar.Focus();
            this.Nuevo.Enabled = true;
            this.btnBuscar.Enabled = true;
            this.Imprimir.Enabled = true;
            if (Documento == null)
            {
                this.Imprimir.Enabled = false;
                this.btnEliminar.Enabled = false;
            }
            else
            {
                if (string.IsNullOrEmpty(Documento.Numero))
                {
                    this.btnEliminar.Enabled = true;
                }
                else
                {
                    this.btnEliminar.Enabled = false;
                }
            }
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
            FactoryTerceros.Guardar(Titular);
        }
        private bool DoGuardar()
        {
            this.bs.EndEdit();
            this.bsDetalles.EndEdit();
            if (Documento.MontoTotal == 0)
            {
                MessageBox.Show("No puede estar vacio para guardar", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Guardar Titular
            txtCedulaTitular.Text = cBasicas.CedulaRif(txtCedulaTitular.Text);
            if (string.IsNullOrEmpty(txtCedulaTitular.Text))
            {
                MessageBox.Show("No puede dejar cliente estar vacio para guardar", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            try
            {
                EscribirTitular();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            Documento.Tipo = "NOTA ENTREGA";
            Documento.IdVendedor = Vendedor.IdTercero;
            Documento.MontoTotal = cBasicas.Round(Documento.MontoTotal);
            Documento.DeudaAnterior = Convert.ToDouble(this.txtMontoPendiente.Value);
            Documento.PesoFactura = Documento.DocumentosProductos.Sum(x => x.PesoTotal);
            Recibo.MontoPagar = cBasicas.Round(Documento.MontoTotal);
            FrmPago f = new FrmPago();
            f.Pago = Recibo;
            f.Tercero = Titular;
            f.ShowDialog();
            Documento.Saldo = Documento.MontoTotal-f.Pago.MontoPagado.GetValueOrDefault(0);
            if(Documento.Saldo>0)
            {
                Documento.Condiciones = string.Format("CREDITO {0} DIAS", Titular.DiasCredito.GetValueOrDefault(0));
                Documento.Vence = Documento.Fecha.Value.AddDays((double)Titular.DiasCredito.GetValueOrDefault(0));
            }
            else
            {
                Documento.Condiciones = "CONTADO";
            }
            if (Recibo != null)
            {
                Recibo.Fecha = DateTime.Today;
                Recibo.Momento = DateTime.Now;
                Recibo.Modulo = "FACTURACION";
            }
            try
            {
                FactoryDocumentos.Save(dc, Documento, Recibo, Titular);
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "Error al guardar Documento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            try
            {
                ImprimirDocumento();
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "Error al imprimir Documento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
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
                    Documento = FactoryDocumentos.Item(dc, T[0].IdDocumento);
                    break;
                default:
                    FrmBuscarEntidades F = new FrmBuscarEntidades();
                    F.Texto = this.txtBuscar.Text;
                    F.myLayout = "FACTURAS";
                    F.Filtro = "NOTA ENTREGA";
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
        private void ImprimirDocumento()
        {
            cBasicas.ImprimirNotaEntrega(Documento);
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
            this.Text = "NOTA ENTREGA-" + Titular.RazonSocial;
            if (String.IsNullOrEmpty(Titular.TipoPrecio))
            {
                Titular.TipoPrecio = "PRECIO 1";
            }
            this.txtTipoPrecio.Text = Titular.TipoPrecio;
            ResumenCuentas Resumen = FactoryCuentas.CuentasTercero(Titular.IdTercero);
            this.txtMontoPendiente.Value = Convert.ToDecimal(Resumen.TotalPendiente);
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
                        Vendedor = FactoryTerceros.Item(((Terceros)F.Registro).IdTercero);
                    }
                    break;
            }
            LeerVendedor();
        }
        private bool CargarPagos()
        {
            FrmPago f = new FrmPago();
            Recibo.Modulo = "NOTA ENTREGA";
            f.Pago = Recibo;
            f.ShowDialog();
            if (f.DialogResult != DialogResult.OK)
                return false;
            Documento.Efectivo = Recibo.Efectivo;
            Documento.Cheque = Recibo.Cheque;
            Documento.TarjetaCredito = Recibo.TCredito;
            Documento.TarjetaDebito = Recibo.TDebito;
            Documento.Deposito = Recibo.Deposito;
            Documento.Cambio = Recibo.Cambio;
            return true;
        }

        #region ManejoDocumentoProductos
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
                        Documento.CalcularTotales();
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
            List<VistaProductos> T = FactoryProductos.BuscarConExistencia(Texto);
            switch (T.Count)
            {
                case 0:
                    if (MessageBox.Show("Producto o Servicio no Encontrado, Desea crear uno nuevo", "Atencion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        Editor.Undo();
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
                    if (FactoryParametros.Item().FacturarSinExistencia == false)
                    {
                        F.Filtro = "FACTURAS";
                    }
                    F.dc = new SoroEntities();
                    F.ShowDialog();
                    Application.DoEvents();
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
        private void txtPrecioIVA_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.SpinEdit Editor = (DevExpress.XtraEditors.SpinEdit)this.gridControl1.MainView.ActiveEditor;
            if (!Editor.IsModified)
                return;
            DocumentoProducto = (DocumentosProductos)this.bsDetalles.Current;
            DocumentoProducto.PrecioIva = cBasicas.Round((double)Editor.Value);
            DocumentoProducto.Precio = cBasicas.Round(DocumentoProducto.PrecioIva / (1 + (DocumentoProducto.TasaIva) / 100));
            CalcularMontoItem();
        }
        private void txtPrecio_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.SpinEdit Editor = (DevExpress.XtraEditors.SpinEdit)this.gridControl1.MainView.ActiveEditor;
            if (!Editor.IsModified)
                return;
            DocumentoProducto = (DocumentosProductos)this.bsDetalles.Current;
            DocumentoProducto.Precio = cBasicas.Round((double)Editor.Value);
            CalcularMontoItem();
        }
        private void txtCantidad_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.SpinEdit Editor = (DevExpress.XtraEditors.SpinEdit)this.gridControl1.MainView.ActiveEditor;
            if (!Editor.IsModified)
                return;
            if (FactoryParametros.Item().FacturarSinExistencia == false)
            {
                if ((double)Editor.Value > DocumentoProducto.ExistenciaAnterior)
                {
                    Editor.Value = (decimal)DocumentoProducto.ExistenciaAnterior;
                }

            }
            DocumentoProducto = (DocumentosProductos)this.bsDetalles.Current;
            DocumentoProducto.Cantidad = (double)Editor.Value;
            DocumentoProducto.PesoTotal = DocumentoProducto.PesoUnitario * DocumentoProducto.Cantidad;
            CalcularMontoItem();
        }
        private void CalcularMontoItem()
        {
            this.bsDetalles.EndEdit();            
            DocumentoProducto.DescuentoBs = DocumentoProducto.DescuentoPorcentaje * DocumentoProducto.Precio / 100;
            DocumentoProducto.MontoNeto = DocumentoProducto.Precio - DocumentoProducto.DescuentoBs;
            DocumentoProducto.Iva = DocumentoProducto.MontoNeto * DocumentoProducto.TasaIva / 100;
            DocumentoProducto.PrecioIva = DocumentoProducto.MontoNeto + DocumentoProducto.Iva;
            DocumentoProducto.Total = (DocumentoProducto.MontoNeto+DocumentoProducto.Iva) * DocumentoProducto.Cantidad;
            Documento.CalcularTotales();
        }
        private void LeerProducto()
        {
            DocumentoProducto = (DocumentosProductos)this.bsDetalles.Current;
            this.gridControl1.MainView.ActiveEditor.Text = Producto.Codigo;
            DocumentoProducto.Tipo = Producto.Tipo;
            DocumentoProducto.IdProducto = Producto.IdProducto;
            DocumentoProducto.Cantidad = Producto.CantidadVentaPorDefecto;
            DocumentoProducto.DescuentoBs = 0;
            DocumentoProducto.Pvs = Producto.PVP;
            if (!Titular.DescuentoPorcentaje.HasValue)
            {
                Titular.DescuentoPorcentaje = 0;
            }
            DocumentoProducto.DescuentoPorcentaje = Titular.DescuentoPorcentaje;
            DocumentoProducto.TasaIva = Producto.Iva;
            switch (Titular.TipoPrecio)
            {
                case "PRECIO 1":
                    DocumentoProducto.Precio = cBasicas.Round(Producto.Precio);
                    break;
                case "PRECIO 2":
                    DocumentoProducto.Precio = cBasicas.Round(Producto.Precio2);
                    break;
                case "PRECIO 3":
                    DocumentoProducto.Precio = cBasicas.Round(Producto.Precio3);
                    break;
                case "PRECIO 4":
                    DocumentoProducto.Precio = cBasicas.Round(Producto.Precio4);
                    break;
            }
            DocumentoProducto.Codigo = Producto.Codigo;
            DocumentoProducto.Descripcion = Producto.Descripcion;
            DocumentoProducto.Iva = cBasicas.Round(DocumentoProducto.Precio * (DocumentoProducto.TasaIva / 100));
            DocumentoProducto.PrecioIva = DocumentoProducto.Precio + DocumentoProducto.Iva;
            DocumentoProducto.BloqueoPrecio = Producto.BloqueoPrecio;
            DocumentoProducto.SolicitaServidor = Producto.SolicitaServidor;
            DocumentoProducto.Costo = Producto.Costo;
            DocumentoProducto.SolicitaServidor = Producto.SolicitaServidor;
            DocumentoProducto.MontoNeto = Producto.Precio;
            DocumentoProducto.Tipo = Producto.Tipo;
            DocumentoProducto.ExistenciaAnterior = Producto.Existencia;
            DocumentoProducto.PesoUnitario = Producto.Peso;
            DocumentoProducto.PesoTotal = DocumentoProducto.PesoUnitario * DocumentoProducto.Cantidad;
            DocumentoProducto.UnidadMedida = Producto.UnidadMedida;
            CalcularMontoItem();
        }
        private void DocumentosProductosBindingSource_ListChanged(object sender, ListChangedEventArgs e)
        {
        }
        private void gridView1_InvalidRowException_1(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
            this.bsDetalles.CancelEdit();
        }
        private void gridView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DocumentosProductos p = (DocumentosProductos)e.Row;
            if (p == null)
                return;
            if (string.IsNullOrEmpty(p.IdProducto))
            {
                p = new DocumentosProductos();
                e.Valid = false;
                e.ErrorText = "Error en codigo del producto, Esc para cancelar";
            }
            if (p.Cantidad == null)
            {
                p.Cantidad = 0;
            }
            //if (Documento.Tipo == "FACTURA" || Documento.Tipo == "PEDIDO")
            //{
            //    if (p.Cantidad > p.ExistenciaAnterior)
            //    {
            //        if (MessageBox.Show("Esta facturando mas de la existencia, es correcto", "Atencion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            //        {
            //            e.Valid = false;
            //            p.Cantidad = 0;
            //        }
            //    }
            //}
        }
        #endregion
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
            Documento.Tipo = "NOTA ENTREGA";
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
                newItem.Pvs = Item.Pvs;
                newItem.UnidadMedida = Item.UnidadMedida;
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
        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                //if (Documento.MontoTotal > 0)
                //{
                //    DoGuardar();
                //}
            }
        }
        private void gridView1_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {

        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            FrmBuscarProductos f = new FrmBuscarProductos();
            f.TipoPrecio = Titular.TipoPrecio;
            f.ShowDialog();
            if (f.DialogResult != System.Windows.Forms.DialogResult.OK)
                return;
            if (f.Productos.Where(x => x.Pedido > 0).Count() > 1)
            {
                foreach (Productos p in f.Productos.Where(x => x.Pedido > 0))
                {
                    LeerProducto(p);
                }
                Documento.CalcularTotales();
                Producto = new Productos();
                //this.bsDetalles.DataSource = Documento;
                //this.bsDetalles.ResetBindings(true);
                //this.gridControl1.DataSource = bsDetalles;
            }


        }
        private void LeerProducto(Productos producto)
        {
            DocumentoProducto = new DocumentosProductos();
            DocumentoProducto.Codigo = producto.Codigo;
            DocumentoProducto.IdProducto = producto.IdProducto;
            DocumentoProducto.Cantidad = producto.Pedido;
            if (Titular == null)
            {
                Titular = new Terceros();
                Titular.DescuentoPorcentaje = 0;
                Titular.TipoPrecio = "PRECIO 4";
            }
            if (!Titular.DescuentoPorcentaje.HasValue)
            {
                Titular.DescuentoPorcentaje = 0;
            }
            DocumentoProducto.DescuentoPorcentaje = Titular.DescuentoPorcentaje;
            DocumentoProducto.TasaIva = producto.Iva;
            switch (Titular.TipoPrecio)
            {
                case "PRECIO 1":
                    DocumentoProducto.Precio = cBasicas.Round(producto.Precio);
                    break;
                case "PRECIO 2":
                    DocumentoProducto.Precio = cBasicas.Round(producto.Precio2);
                    break;
                case "PRECIO 3":
                    DocumentoProducto.Precio = cBasicas.Round(producto.Precio3);
                    break;
                case "PRECIO 4":
                    DocumentoProducto.Precio = cBasicas.Round(producto.Precio4);
                    break;
            }
            DocumentoProducto.Codigo = producto.Codigo;
            DocumentoProducto.Descripcion = producto.Descripcion;
            DocumentoProducto.Iva = cBasicas.Round(DocumentoProducto.Precio * (DocumentoProducto.TasaIva / 100));
            DocumentoProducto.PesoUnitario = producto.Peso;
            DocumentoProducto.PesoTotal = DocumentoProducto.PesoUnitario * DocumentoProducto.Cantidad;
            DocumentoProducto.UnidadMedida = producto.UnidadMedida;
            DocumentoProducto.ExistenciaAnterior = producto.Existencia;
            DocumentoProducto.Pvs = producto.PVP;
            CalcularMontoItem();
            this.bsDetalles.List.Add(DocumentoProducto);
            // Documento.PedidosDetalles.Add(DocumentoProducto);
        }
    }
}
