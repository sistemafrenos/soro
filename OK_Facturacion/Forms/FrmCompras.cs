using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HK.Clases;

namespace HK
{
    public partial class FrmCompras : Form
    {
        public FrmCompras()
        {
            InitializeComponent();
        }
        Terceros Tercero = null;
        Productos Producto = null;
        Documentos Documento = null;
        DocumentosProductos DocumentoProducto = null;
        SoroEntities dc = new SoroEntities();
        private void Forma_Load(object sender, EventArgs e)
        {
            DesHabilitarEdicion();
            this.txtCodigo.Validating += new CancelEventHandler(txtCodigo_Validating);
            this.txtCantidad.Validating += new CancelEventHandler(txtCantidad_Validating);
            this.txtCosto.Validating += new CancelEventHandler(txtCosto_Validating);
            this.txtCostoIva.Validating += new CancelEventHandler(txtCostoIVA_Validating);
            this.txtDescuentoBs.Validating += new CancelEventHandler(DescuentoBs_Validating);
            this.txtDescuentoPorcentaje.Validating += new CancelEventHandler(DescuentoPorcentaje_Validating);
            this.txtPrecio.Validating += new CancelEventHandler(txtPrecio_Validating);
            this.txtPrecioIVA.Validating += new CancelEventHandler(txtPrecioIVA_Validating);
            this.txtUtil.Validating += new CancelEventHandler(txtUtil_Validating);
            this.txtUtil2.Validating += new CancelEventHandler(txtUtil2_Validating);
            this.txtUtil3.Validating += new CancelEventHandler(txtUtil3_Validating);
            this.txtUtil4.Validating += new CancelEventHandler(txtUtil4_Validating);
        }

 
        private void Limpiar()
        {
            Documento = new Documentos();
            LeerTercero();
            this.bs.DataSource = Documento;
            this.bs.ResetBindings(true);
        }
        private void Incluir()
        {
            Documento = new Documentos();
            Documento.Status = "ABIERTA";
            Documento.Mes = DateTime.Today.Month;
            Documento.Año = DateTime.Today.Year;
            Documento.Fecha = DateTime.Today;
            Documento.Activo = true;
            Enlazar();
            HabilitarEdicion();
        }
        private void Enlazar()
        {
            if (Documento == null)
            {
                Documento = new Documentos();
            }
            Tercero = FactoryTerceros.Item(Documento.IdTercero);
            
            LeerTercero();
            this.bs.DataSource = Documento;
            this.bs.ResetBindings(true);
            this.bsDetalles.DataSource = Documento.DocumentosProductos;
            this.bsDetalles.ResetBindings(true);

        }
        private void Eliminar()
        {
            Documento = (Documentos)this.bs.Current;
            if (Documento == null) return;
            if (MessageBox.Show("Esta seguro de eliminar este Documento", "Atencion", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                return;
            FactoryDocumentos.Delete(Documento.IdDocumento);
            Documento = new Documentos();
            this.bs.Clear();
            this.bsDetalles.Clear();
            Enlazar();
        }
        private void HabilitarEdicion()
        {
            if (Documento.Status == "ABIERTA")
            {
                this.Aceptar.Enabled = true;
                this.Cancelar.Enabled = true;
                Encab1.Enabled = true;
                Encab2.Enabled = true;
                Encab3.Enabled = true;
                this.gridControl1.Enabled = true;
                Encab1.Focus();
                this.btnEliminar.Enabled = true;

            }
            else
            {
                Encab1.Enabled = false;
                Encab2.Enabled = false;
                Encab3.Enabled = false;
                this.gridControl1.Enabled = false;
                this.Aceptar.Enabled = false;
                this.Cancelar.Enabled = false;
                this.btnEliminar.Enabled = true;
            }
            this.txtBuscar.Enabled = false;
            this.Nuevo.Enabled = false;
            this.Editar.Enabled = false;
            this.Imprimir.Enabled = false;
            this.btnEliminar.Enabled = false;
            this.toolCargarDoc.Enabled = true;

        }
        private void DesHabilitarEdicion()
        {
            Encab1.Enabled = false;
            Encab2.Enabled = false;
            Encab3.Enabled = false;
            toolCargarDoc.Enabled = false;
            this.gridControl1.Enabled = false;
            this.Aceptar.Enabled = false;
            this.Cancelar.Enabled = false;
            this.txtBuscar.Enabled = true;
            txtBuscar.Focus();
            this.Nuevo.Enabled = true;

            if (Documento == null)
            {
                this.Editar.Enabled = false;
                this.Imprimir.Enabled = false;
                this.btnEliminar.Enabled = false;
            }
            else
            {
                this.Imprimir.Enabled = true;
                this.btnEliminar.Enabled = true;
                if (Documento.Status != "INVENTARIO")
                {
                    this.Editar.Enabled = true;
                    this.btnEliminar.Enabled = true;
                }
                else
                {
                    this.Editar.Enabled = false;
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
        private void EscribirTitular()
        {
            Tercero.CedulaRif = this.txtCedulaTitular.Text;
            Tercero.RazonSocial = this.razonSocialTextBox.Text;
            Tercero.Direccion = this.direccionTextBox.Text;
            Tercero.Email = this.emailTextBox.Text;
            Tercero.Telefonos = this.telefonosTextBox.Text;
        }
        private void LeerTercero()
        {
            if (Tercero == null)
            {
                Tercero = new Terceros();
            }
            this.txtCedulaTitular.Text = Tercero.CedulaRif;
            this.razonSocialTextBox.Text = Tercero.RazonSocial;
            this.direccionTextBox.Text = Tercero.Direccion;
            this.emailTextBox.Text = Tercero.Email;
            this.telefonosTextBox.Text = Tercero.Telefonos;
        }
        private void toolGuardar_Click(object sender, EventArgs e)
        {
            this.bs.EndEdit();
            this.bsDetalles.EndEdit();
            if (String.IsNullOrEmpty(Documento.Numero))
            {
                MessageBox.Show("Error debe llenar el numero de Factura", "Verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (String.IsNullOrEmpty(Documento.Control))
            {
                MessageBox.Show("Error debe llenar el numero de Control", "Verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("Esta opcion realiza los asientos en los libros de inventarios y compras \n luego de eso la factura no podra ser modificada de nuevo", "Esta seguro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            if (!Guardar())
                return;
            cCompras.CargarInventario(Documento);
            if (Documento.Control != "000000")
            {
                cCompras.LibroDeCompras(Documento);
            }
            cCompras.EscribirCuentaxPagar(Documento);
            Limpiar();
            Enlazar();
            DesHabilitarEdicion();
        }
        private bool Guardar()
        {
            this.bs.EndEdit();
            this.bsDetalles.EndEdit();
            if (String.IsNullOrEmpty(Documento.Numero))
            {
                MessageBox.Show("Error debe llenar el numero de Factura", "Verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (Documento.MontoTotal == 0)
            {
                MessageBox.Show("No puede estar vacio para guardar", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (this.txtCedulaTitular.Text.Length == 0)
            {
                MessageBox.Show("No puede estar vacia la Cedula", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (this.razonSocialTextBox.Text.Length == 0)
            {
                MessageBox.Show("No puede estar vacia la Razon Social", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            txtCedulaTitular.Text = cBasicas.CedulaRif(txtCedulaTitular.Text);
            if (!cBasicas.IsValidCIRIF(txtCedulaTitular.Text))
            {
                MessageBox.Show("Error en Cedula o Rif del Tercero debe comenzar en V/E/J/G \n y no debe llevar guiones ni puntos", "Verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            EscribirTitular();
            return cCompras.Guardar(dc, Documento, Tercero);            
        }
        private void toolBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
            if (Documento == null)
            {
                return;
            }
            if (Documento.Status == "ABIERTA")
            {
                HabilitarEdicion();
            }
            else
            {
                DesHabilitarEdicion();
            }
            
        }
        private void Buscar()
        {
            dc = new SoroEntities();
            List<VistaDocumento> T = FactoryDocumentos.Buscar(dc, this.txtBuscar.Text, "COMPRA",true);

            switch (T.Count)
            {
                case 0:
                    MessageBox.Show("Registro no encontrado", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.txtBuscar.Text = "";
                    return;
                case 1:
                    Documento = (Documentos)FactoryDocumentos.Item(dc, T[0].IdDocumento);
                    LeerDocumento(Documento);
                    return;
                default:
                    FrmBuscarEntidades F = new FrmBuscarEntidades();
                    F.Texto = this.txtBuscar.Text;
                    F.myLayout = "COMPRAS";
                    F.Filtro = "COMPRA";
                    F.dc = dc;
                    F.ShowDialog();
                    if (F.Registro == null)
                        return;
                    VistaDocumento VistaDocumento = (VistaDocumento)F.Registro;
                    Documento = (Documentos)FactoryDocumentos.Item(dc, VistaDocumento.IdDocumento);
                    LeerDocumento(Documento);
                    break;
            }
        }
        private void LeerDocumento(Documentos T)
        {
            Enlazar();
         //   Documento.DocumentosProductos.Load();

        }
        private void toolCancelar_Click(object sender, EventArgs e)
        {
            this.bs.CancelEdit();
            DesHabilitarEdicion();
        }
        private void CalcularPrecios()
        {
            Producto.Utilidad = DocumentoProducto.Utilidad;
            Producto.Utilidad2 = DocumentoProducto.Utilidad2;
            Producto.Utilidad3 = DocumentoProducto.Utilidad3;
            Producto.Utilidad4 = DocumentoProducto.Utilidad4;
            Producto.CostoActual = DocumentoProducto.MontoNeto;
            Producto.CalcularPrecio1();
            Producto.CalcularPrecio2();
            Producto.CalcularPrecio3();
            Producto.CalcularPrecio4();
            DocumentoProducto.Precio = Producto.Precio;            
            DocumentoProducto.Precio2 = Producto.Precio2;
            DocumentoProducto.Precio3 = Producto.Precio3;
            DocumentoProducto.Precio4 = Producto.Precio4;
            DocumentoProducto.PrecioIva = Producto.PrecioIva;
        }
        private void txtCostoIVA_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.SpinEdit Editor = (DevExpress.XtraEditors.SpinEdit)sender;
            if (!Editor.IsModified)
                return;
            this.bsDetalles.EndEdit();
            DocumentoProducto = (DocumentosProductos)this.bsDetalles.Current;
            DocumentoProducto.CostoIva = (double)Editor.Value;
            DocumentoProducto.Costo = DocumentoProducto.CostoIva / (1 + (DocumentoProducto.TasaIva) / 100);
            CalcularMontoItem();
            Documento.CalcularTotales();
        }        
        private void txtCosto_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.SpinEdit Editor = (DevExpress.XtraEditors.SpinEdit)sender;
            if (!Editor.IsModified)
                return;
            DocumentoProducto = (DocumentosProductos)this.bsDetalles.Current;
            DocumentoProducto.Costo = (double)Editor.Value;
            CalcularMontoItem();
            Documento.CalcularTotales();
        }
        private void DocumentosProductosBindingSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            //if (Documento == null)
            //    return;
            //if (bsDetalles.Current == null)
            //    return;
            //DocumentosProductos detalle = (DocumentosProductos)bsDetalles.Current;
            //if (detalle.IdProducto != null)
            //    this.bsDetalles.EndEdit();
            //Documento.CalcularTotales();
        }
        private void toolImprimir_Click(object sender, EventArgs e)
        {
            if (Documento == null)
                return;

            if (!string.IsNullOrEmpty(Documento.IdDocumento))
            {
                List<VistaCompras> Compra = new List<VistaCompras>();
                Compra = FactoryDocumentos.VistaCompras(new SoroEntities(), Documento.IdDocumento);
                FrmReportes f = new FrmReportes();
                f.ReporteCompras(Compra);
            }
        }
        private void toolImprimir_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (Documento == null)
                    return;
                //Cotizacion r = new Cotizacion();
                //r.LoadLayout(Application.StartupPath + "\\Reportes\\COTIZACION.REPX");
                //DSTableAdapters.ParametrosTableAdapter P = new HK.DSTableAdapters.ParametrosTableAdapter();
                //DSTableAdapters.VistaFacturaTableAdapter T = new HK.DSTableAdapters.VistaFacturaTableAdapter();
                //T.FillByIDDocumento(Ds.VistaFactura, (string)DocumentoActual["IdDocumento"]);
                //P.Fill(Ds.Parametros);
                //r.DataSource = Ds;
                //r.ShowDesignerDialog();
            }
        }
        private void gridView1_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ErrorText = "";
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.Ignore;
        }
        private void LeerTitular()
        {
            if (Tercero == null)
            {
                Tercero = new Terceros();
            }
            this.txtCedulaTitular.Text = Tercero.CedulaRif;
            this.razonSocialTextBox.Text = Tercero.RazonSocial;
            this.direccionTextBox.Text = Tercero.Direccion;
            this.emailTextBox.Text = Tercero.Email;
            this.telefonosTextBox.Text = Tercero.Telefonos;
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
                    Tercero = new Terceros();
                    Editor.Text = cBasicas.CedulaRif(Editor.Text);
                    Tercero.CedulaRif = Editor.Text;
                    break;
                case 1:
                    Tercero = T[0];
                    Tercero = FactoryTerceros.Item(Tercero.IdTercero);
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
                        Tercero = (Terceros)F.Registro;
                        Tercero = FactoryTerceros.Item(Tercero.IdTercero);
                    }
                    else
                    {
                        Tercero = new Terceros();
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
                Tercero = (Terceros)F.Registro;
                Tercero = FactoryTerceros.Item(Tercero.IdTercero);
                LeerTitular();
            }
            else
            {
                Tercero = new Terceros();
                LeerTitular();
            }
        }
        private void Editar_Click(object sender, EventArgs e)
        {
            if (Documento == null)
                return;
            HabilitarEdicion();
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {

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
                            dc.DocumentosProductos.Remove(Registro);
                        }
                        catch { }
                        this.bsDetalles.Remove(Registro);
                    }
                }
            }
        }
        private void dateEdit1_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.DateEdit Fecha = (DevExpress.XtraEditors.DateEdit)sender;
            if (!Fecha.IsModified)
                return;
            this.txtAño.Value = this.txtFecha.DateTime.Year;
            this.txtMes.Value = this.txtFecha.DateTime.Month;
            if (Tercero != null)
            {
                if (Tercero.DiasCredito.HasValue)
                {
                    Documento.Vence = Fecha.DateTime.AddDays( Tercero.DiasCredito.Value );
                }
            }            
        }
        //private void toolStripMenuItem1_Click(object sender, EventArgs e)
        //{

        //    DocumentosProductos Registro = (DocumentosProductos)this.bsDetalles.Current;
        //    this.bsDetalles.EndEdit();
        //    FrmAjustarPrecios f = new FrmAjustarPrecios();
        //    f.Registro = Registro;
        //    f.Producto = Producto;
        //    f.ShowDialog();
        //    if (f.DialogResult == DialogResult.OK)
        //    {
        //        Registro = f.Registro;
        //    }
        //}
        private void gridControl1_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                DocumentosProductos Registro = (DocumentosProductos)this.bsDetalles.Current;
                if (Registro == null) return;
                this.contextMenuStrip1.Show(this.gridControl1, e.Location);
            }
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DocumentosProductos Registro = (DocumentosProductos)this.bsDetalles.Current;
            if (Registro == null) return;
            this.bsDetalles.EndEdit();
            FrmAjustarExistenciaAnterior f = new FrmAjustarExistenciaAnterior();
            f.Registro = Registro;
            f.Producto = Producto;
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
            {
                Registro = f.Registro;
            }
        }
        private void DescuentoPorcentaje_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.CalcEdit Editor = (DevExpress.XtraEditors.CalcEdit)sender;
            if (!Editor.IsModified)
                return;
            DocumentoProducto = (DocumentosProductos)this.bsDetalles.Current;
            DocumentoProducto.DescuentoPorcentaje = (double)Editor.Value;
            CalcularMontoItem();
            Documento.CalcularTotales();
        }
        private void DescuentoBs_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.CalcEdit Editor = (DevExpress.XtraEditors.CalcEdit)sender;
            if (!Editor.IsModified)
                return;
            DocumentoProducto = (DocumentosProductos)this.bsDetalles.Current;
            DocumentoProducto.DescuentoBs = (double)Editor.Value;
            DocumentoProducto.DescuentoPorcentaje = 0;
            try
            {
                DocumentoProducto.DescuentoPorcentaje = (DocumentoProducto.DescuentoBs) / (DocumentoProducto.Costo / 100);
            }
            catch { }
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
            if (DocumentoProducto.IdProducto == null)
            {
                DocumentoProducto = new DocumentosProductos();
                return;
            }
            this.bsDetalles.EndEdit();
            DocumentoProducto.DescuentoBs = cBasicas.Round( DocumentoProducto.DescuentoPorcentaje * DocumentoProducto.Costo / 100);
            DocumentoProducto.MontoNeto = cBasicas.Round(DocumentoProducto.Costo - DocumentoProducto.DescuentoBs);
            DocumentoProducto.Iva = cBasicas.Round(DocumentoProducto.MontoNeto * DocumentoProducto.TasaIva / 100);
            Producto = FactoryProductos.Item(DocumentoProducto.IdProducto);
            try
            {
                Producto.Costo = DocumentoProducto.Costo;
            }
            catch 
            {
                Producto.Costo = 0;
            }
            Producto.CostoActual = DocumentoProducto.MontoNeto;
            Producto.Iva = DocumentoProducto.TasaIva;
            CalcularPrecios();
            DocumentoProducto.CostoIva = DocumentoProducto.Costo + (DocumentoProducto.TasaIva * DocumentoProducto.Costo/100);
            DocumentoProducto.PrecioIva = DocumentoProducto.Precio + (DocumentoProducto.TasaIva * DocumentoProducto.Precio/100);
            DocumentoProducto.Total = (DocumentoProducto.MontoNeto + DocumentoProducto.Iva) * DocumentoProducto.Cantidad;
        }
        private void LeerProducto()
        {
            DocumentoProducto = (DocumentosProductos)this.bsDetalles.Current;
            this.gridControl1.MainView.ActiveEditor.Text = Producto.Codigo;
            DocumentoProducto.Tipo = Producto.Tipo;
            DocumentoProducto.IdProducto = Producto.IdProducto;
            DocumentoProducto.Cantidad = 1;
            DocumentoProducto.DescuentoBs = 0;
            DocumentoProducto.DescuentoPorcentaje = 0;
            DocumentoProducto.TasaIva = Producto.Iva;
            DocumentoProducto.Precio = Producto.Precio;
            DocumentoProducto.Codigo = Producto.Codigo;
            DocumentoProducto.Descripcion = Producto.Descripcion;
            DocumentoProducto.PrecioIva = Producto.PrecioIva;
            DocumentoProducto.BloqueoPrecio = Producto.BloqueoPrecio;
            DocumentoProducto.SolicitaServidor = Producto.SolicitaServidor;
            DocumentoProducto.Costo = Producto.CostoActual;
            DocumentoProducto.CostoIva = Producto.CostoIva;
            DocumentoProducto.Tipo = Producto.Tipo;
            DocumentoProducto.ExistenciaAnterior = Producto.Existencia;
            DocumentoProducto.Precio2 = Producto.Precio2;
            DocumentoProducto.Precio3 = Producto.Precio3;
            DocumentoProducto.Precio4 = Producto.Precio4;
            DocumentoProducto.Utilidad = Producto.Utilidad;
            DocumentoProducto.Utilidad2 = Producto.Utilidad2;
            DocumentoProducto.Utilidad3 = Producto.Utilidad3;
            DocumentoProducto.Utilidad4 = Producto.Utilidad4;
            DocumentoProducto.Pvs = Producto.PVP;
            CalcularMontoItem();
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
        }
        private void txtUtil_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.TextEdit Editor = (DevExpress.XtraEditors.TextEdit)sender;
            if (!Editor.IsModified)
                return;
            Producto = FactoryProductos.Item(DocumentoProducto.IdProducto);
            if (Producto == null)
            {
                DocumentoProducto.Precio = 0;
            }
            else
            {
                Producto.CostoActual = DocumentoProducto.MontoNeto;
                Producto.Utilidad = Convert.ToDouble(Editor.EditValue);
                Producto.CalcularPrecio1();
                DocumentoProducto.Precio = Producto.Precio;
                DocumentoProducto.PrecioIva = Producto.PrecioIva;
            }
        }
        private void txtUtil2_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.TextEdit Editor = (DevExpress.XtraEditors.TextEdit)sender;
            if (!Editor.IsModified)
                return;
            Producto = FactoryProductos.Item(DocumentoProducto.IdProducto);
            if (Producto == null)
            {
                DocumentoProducto.Precio2 = 0;
            }
            else
            {
                Producto.CostoActual = DocumentoProducto.MontoNeto;
                Producto.Utilidad2 = Convert.ToDouble(Editor.EditValue);
                Producto.CalcularPrecio2();
                DocumentoProducto.Precio2 = Producto.Precio2;
            }
        }
        private void txtUtil3_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.TextEdit Editor = (DevExpress.XtraEditors.TextEdit)sender;
            if (!Editor.IsModified)
                return;
            Producto = FactoryProductos.Item(DocumentoProducto.IdProducto);
            if (Producto == null)
            {
                DocumentoProducto.Precio3 = 0;
            }
            else
            {
                Producto.CostoActual = DocumentoProducto.MontoNeto;
                Producto.Utilidad3 = Convert.ToDouble(Editor.EditValue);
                Producto.CalcularPrecio3();
                DocumentoProducto.Precio3 = Producto.Precio3;
            }
        }
        private void txtUtil4_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.TextEdit Editor = (DevExpress.XtraEditors.TextEdit)sender;
            if (!Editor.IsModified)
                return;
            Producto = FactoryProductos.Item(DocumentoProducto.IdProducto);
            if (Producto == null)
            {
                DocumentoProducto.Precio = 0;
            }
            else
            {
                Producto.CostoActual = DocumentoProducto.MontoNeto;
                Producto.Utilidad4= Convert.ToDouble(Editor.EditValue);
                Producto.CalcularPrecio4();
                DocumentoProducto.Precio4 = Producto.Precio4;
            }
        }
        private void txtCodigo_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.TextEdit Editor = (DevExpress.XtraEditors.TextEdit)sender;
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

        private void txtPrecio_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.SpinEdit Editor = (DevExpress.XtraEditors.SpinEdit)sender;
            if (!Editor.IsModified)
                return;
            DocumentoProducto = (DocumentosProductos)this.bsDetalles.Current;
            DocumentoProducto.Precio = cBasicas.Round((double)Editor.Value);
            Producto.CostoActual = DocumentoProducto.MontoNeto;
            Producto.Iva = DocumentoProducto.TasaIva;
            Producto.Precio = DocumentoProducto.Precio;
            DocumentoProducto.PrecioIva = Producto.PrecioIva;
            DocumentoProducto.Utilidad = Producto.Utilidad;
           // CalcularMontoItem();
            Documento.CalcularTotales();

        }
        private void txtPrecioIVA_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.SpinEdit Editor = (DevExpress.XtraEditors.SpinEdit)this.gridControl1.MainView.ActiveEditor;
            if (!Editor.IsModified)
                return;
            DocumentoProducto = (DocumentosProductos)this.bsDetalles.Current;
            DocumentoProducto.PrecioIva = cBasicas.Round((double)Editor.Value);
            Producto.CostoActual = DocumentoProducto.MontoNeto;
            Producto.Iva = DocumentoProducto.TasaIva;
            Producto.PrecioIva = DocumentoProducto.PrecioIva;
            DocumentoProducto.Precio = Producto.Precio;
            DocumentoProducto.Utilidad = Producto.Utilidad;
            Documento.CalcularTotales();
        }
        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            ImprimirCodigoBarras F = new ImprimirCodigoBarras();
            F.Documento = Documento;
            F.ShowDialog();
        }

        private void CargarDocumento_Click(object sender, EventArgs e)
        {
            FrmBuscarEntidades F = new FrmBuscarEntidades();
            F.myLayout = "BUSCARCOMPRAS";
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
            Documento.Tipo = "COMPRA";
            Documento.Activo = true;
            Documento.Año = DateTime.Today.Year;
            Documento.Fecha = DateTime.Today;
            Documento.Vence = DateTime.Today.AddDays(30);
            Documento.DescuentoBs = Doc.DescuentoBs;
            Documento.DescuentoPorcentaje = Doc.DescuentoPorcentaje;
            Tercero = FactoryTerceros.Item(Documento.IdTercero);            
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
                Documento.DocumentosProductos.Add(newItem);
            }
            LeerTercero();
            Documento.CalcularTotales();
            this.bs.DataSource = Documento;
            this.bs.ResetBindings(true);
            this.bsDetalles.DataSource = Documento.DocumentosProductos;
            this.bsDetalles.ResetBindings(true);
        }

        private void spinEdit1_Validating(object sender, CancelEventArgs e)
        {
            this.bs.EndEdit();
            Documento.CalcularTotales();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}
