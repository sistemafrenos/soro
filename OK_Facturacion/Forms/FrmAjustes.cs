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
    public partial class FrmAjustes : Form
    {
        public FrmAjustes()
        {
            InitializeComponent();
        }
        SoroEntities dc = new SoroEntities();
        Productos Producto = null;
        Documentos Documento = null;
        DocumentosProductos DocumentoProducto = null;

        private void Forma_Load(object sender, EventArgs e)
        {
            DesHabilitarEdicion();
            this.txtCodigo.Validating += new CancelEventHandler(txtCodigo_Validating);
            this.txtCantidad.Validating += new CancelEventHandler(txtCantidad_Validating);
        }
        private void Limpiar()
        {

            this.bs.DataSource = Documento;
            this.bs.ResetBindings(true);
        }
        private void Incluir()
        {
            dc = new SoroEntities();
            this.bs.Clear();
            Documento = (Documentos)this.bs.AddNew();
            Documento.Activo = true;
            Documento.Fecha = DateTime.Today;
            Documento.Tipo = "AJUSTE";
            dc.Documentos.Add(Documento);
            Enlazar();
            HabilitarEdicion();
        }
        private void Enlazar()
        {
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
            Encab1.Enabled = true;
            this.gridControl1.Enabled = true;
            Encab1.Focus();
            this.Aceptar.Enabled = true;
            this.Cancelar.Enabled = true;
            this.txtBuscar.Enabled = false;
            this.Nuevo.Enabled = false;
            this.Editar.Enabled = false;
            this.Imprimir.Enabled = false;
            this.btnEliminar.Enabled = false;
        }
        private void DesHabilitarEdicion()
        {
            Encab1.Enabled = false;
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
                this.Editar.Enabled = true;
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
        private void toolGuardar_Click(object sender, EventArgs e)
        {
            this.bs.EndEdit();
            this.bsDetalles.EndEdit();
            if (txtComentarios.Text.Length == 0)
            {
                MessageBox.Show("No puede estar vacio el comentario", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!cAjustes.Guardar(dc, Documento))
            {
                MessageBox.Show("Los cambios no se pudieron guardar");
            }
            Enlazar();
            DesHabilitarEdicion();
        }

        private void toolBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
            DesHabilitarEdicion();
        }
        private void Buscar()
        {
            List<VistaDocumento> T = FactoryDocumentos.Buscar(dc, this.txtBuscar.Text, "AJUSTE", true);

            switch (T.Count)
            {
                case 0:
                    MessageBox.Show("Registro no encontrado", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.txtBuscar.Text = "";
                    return;
                case 1:
                    Documento = FactoryDocumentos.Item(dc, T[0].IdDocumento);
                    LeerDocumento(Documento);
                    return;
                default:
                    FrmBuscarEntidades F = new FrmBuscarEntidades();
                    F.Texto = this.txtBuscar.Text;
                    F.myLayout = "FACTURAS";
                    F.Filtro = "AJUSTE";
                    F.dc = this.dc;
                    F.ShowDialog();
                    if (F.Registro == null)
                        return;
                    VistaDocumento VistaDocumento = (VistaDocumento)F.Registro;
                    Documento = FactoryDocumentos.Item(dc, VistaDocumento.IdDocumento);
                    LeerDocumento(Documento);
                    break;
            }
        }
        private void LeerDocumento(Documentos T)
        {
            Documento = FactoryDocumentos.Item(dc, T.IdDocumento);
            Enlazar();
        }
        private void toolCancelar_Click(object sender, EventArgs e)
        {
            this.bs.CancelEdit();
            DesHabilitarEdicion();
        }
        #region Servicios
        private void txtCodigo_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.TextEdit Editor = (DevExpress.XtraEditors.TextEdit)sender;
            if (!Editor.IsModified)
                return;
            string Texto = Editor.Text;
            List<VistaProductos> T = FactoryProductos.Buscar(dc, Texto);
            switch (T.Count)
            {
                case 0:
                    MessageBox.Show("Producto o Servicio no Encontrado", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    {
                        return;
                    }
                case 1:                    
                    Producto = FactoryProductos.Item(T[0].IdProducto); 
                    break;
                default:
                    FrmBuscarEntidades F = new FrmBuscarEntidades();
                    F.Texto = Texto;
                    F.myLayout = "PRODUCTOS";
                    F.Filtro = "";
                    F.dc = dc;
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
        private void txtCantidad_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.SpinEdit Editor = (DevExpress.XtraEditors.SpinEdit)sender;
            if (!Editor.IsModified)
                return; ;
            DocumentoProducto = (DocumentosProductos)this.bsDetalles.Current;
            DocumentoProducto.Cantidad = (double)Editor.Value;
            DocumentoProducto.TasaIva = Producto.Iva;
            DocumentoProducto.Iva = DocumentoProducto.Costo * DocumentoProducto.TasaIva / 100;
            DocumentoProducto.Total = DocumentoProducto.CostoIva * DocumentoProducto.Cantidad;
            DocumentoProducto.MontoNeto = DocumentoProducto.Costo;
            Documento.CalcularTotales();
        }
        private void LeerProducto()
        {
            DocumentoProducto = (DocumentosProductos)this.bsDetalles.Current;
            this.gridControl1.MainView.ActiveEditor.Text = Producto.Codigo;            
            DocumentoProducto.Tipo = "SERVICIOS";
            DocumentoProducto.IdProducto = Producto.IdProducto;
            DocumentoProducto.Cantidad = 1;
            DocumentoProducto.TasaIva = Producto.Iva;
            DocumentoProducto.Precio = Producto.Precio;
            DocumentoProducto.Codigo = Producto.Codigo;
            DocumentoProducto.Costo = Producto.Costo;
            DocumentoProducto.Descripcion = Producto.Descripcion;
            DocumentoProducto.CostoIva = Producto.Costo + (Producto.Costo * (DocumentoProducto.TasaIva / 100));
            DocumentoProducto.ExistenciaAnterior = Producto.Existencia;
            DocumentoProducto.MontoNeto = DocumentoProducto.Costo;
            DocumentoProducto.Total = DocumentoProducto.CostoIva * DocumentoProducto.Cantidad;
        }
        private void DocumentosProductosBindingSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (Documento == null)
                return;
            bsDetalles.EndEdit();
            Documento.CalcularTotales();
       }
        #endregion
        private void toolImprimir_Click(object sender, EventArgs e)
        {
            if (Documento == null)
                return;
            if (string.IsNullOrEmpty(Documento.Numero))
            {
                List<VistaFactura> Factura = new List<VistaFactura>();
                Factura = FactoryDocumentos.VistaFacturas(dc, Documento.IdDocumento);
                FrmReportes f = new FrmReportes();
                // Pendiente
                // f.ReporteFactura(Factura);
            }
        }
        private void gridView1_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ErrorText = "";
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.Ignore;
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
                        Documento.CalcularTotales();
                    }
                    e.Handled = true;
                }
            }

        }

        private void Editar_Click(object sender, EventArgs e)
        {
            if (Documento == null)
                return;
            HabilitarEdicion();
        }
    }
}
