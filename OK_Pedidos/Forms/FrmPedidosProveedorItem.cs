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
    public partial class FrmPedidosProveedorItem : Form
    {
        public FrmPedidosProveedorItem()
        {
            InitializeComponent();
        }

        Tercero Tercero = null;
        Producto Producto = null;
        Documento Documento = null;
        DocumentosProducto DocumentoProducto = null;
        Entities dc = new Entities();
        List<DocumentosProducto> myPedido = new List<DocumentosProducto>();
        private void LeerTitular()
        {
            if (Tercero == null)
            {
                Tercero = new Tercero();
            }
            this.txtCedulaTitular.Text = Tercero.CedulaRif;
            this.razonSocialTextBox.Text = Tercero.RazonSocial;
            this.direccionTextBox.Text = Tercero.Direccion;
            this.emailTextBox.Text = Tercero.Email;
            this.telefonosTextBox.Text = Tercero.Telefonos;
        }

        private void btnCargarProductos_Click(object sender, EventArgs e)
        {
            List<Producto> ListaProductos = FactoryProductos.CargarProductosProveedor(Tercero.IdTercero);
            Limpiar();
            foreach (Producto p in ListaProductos)
            {
                DocumentosProducto newItem = new DocumentosProducto();
                newItem.Cantidad = 0;
                newItem.Codigo = p.Codigo;
                newItem.Activo = true;
                newItem.Costo = p.Costo;
                newItem.Descripcion = p.Descripcion;
                newItem.DescuentoBs = 0;
                newItem.ExistenciaAnterior = p.Existencia;
                newItem.Total = 0;
                newItem.IdProducto = p.IdProducto;
                newItem.TasaIva = p.Iva;
                newItem.Minimo = p.Minimo;
                newItem.Maximo = p.Maximo;
                newItem.Linea = p.Grupo;
                CalcularMontoItem(newItem);
                Documento.DocumentosProductos.Add(newItem);
            }
            Documento.CalcularTotales();
            this.bs.DataSource = Documento;
            this.bs.ResetBindings(true);
            this.bsDetalles.DataSource = Documento.DocumentosProductos;
            this.bsDetalles.ResetBindings(true);
        }

        private void FrmPedidoProveedorItem_Load(object sender, EventArgs e)
        {
            this.txtCodigo.Validating += new CancelEventHandler(txtCodigo_Validating);
            this.txtCedulaTitular.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(txtCedulaTitular_ButtonClick);
            this.txtCedulaTitular.Validating += new CancelEventHandler(txtCedulaTitular_Validating);
        }

        void txtCedulaTitular_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.TextEdit Editor = (DevExpress.XtraEditors.TextEdit)sender;
            if (!Editor.IsModified)
                return;
            string Texto = Editor.Text;
            List<Tercero> T = FactoryProveedores.Buscar(Texto);
            switch (T.Count)
            {
                case 0:
                    Tercero = new Tercero();
                    Editor.Text = cBasicas.CedulaRif(Editor.Text);
                    Tercero.CedulaRif = Editor.Text;
                    break;
                case 1:
                    Tercero = T[0];
                    Tercero = FactoryProveedores.Item(Tercero.IdTercero);
                    break;
                default:
                    FrmBuscarEntidades F = new FrmBuscarEntidades();
                    F.Texto = Texto;
                    F.myLayout = "PROVEEDORES";
                    F.Filtro = "";
                    F.ShowDialog();
                    if (F.Registro != null)
                    {
                        Tercero = (Tercero)F.Registro;
                        Tercero = FactoryProveedores.Item(Tercero.IdTercero);
                    }
                    else
                    {
                        Tercero = new Tercero();
                    }
                    break;
            }
            LeerTitular();
            
        }

        void txtCedulaTitular_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmBuscarEntidades F = new FrmBuscarEntidades();
            F.Texto = "";
            F.myLayout = "PROVEEDORES";
            F.Filtro = "";
            F.ShowDialog();
            if (F.Registro != null)
            {
                Tercero = (Tercero)F.Registro;
                Tercero = FactoryProveedores.Item(Tercero.IdTercero);
                LeerTitular();
            }
            else
            {
                Tercero = new Tercero();
                LeerTitular();
            }

        }
        private void Limpiar()
        {
            Documento = new Documento();
            Documento.Status = "ABIERTA";
            Documento.Mes = DateTime.Today.Month;
            Documento.Año = DateTime.Today.Year;
            Documento.Fecha = DateTime.Today;
            Documento.Activo = true;
            Documento.Tipo = "PEDIDO PROVEEDOR";
        }
        public void Incluir()
        {
            Limpiar();
            Enlazar();
            this.ShowDialog();
        }
        private void Enlazar()
        {
            if (Documento == null)
            {
                Documento = new Documento();
            }
            Tercero = FactoryProveedores.Item(Documento.IdTercero);
            LeerTitular();
            this.bs.DataSource = Documento;
            this.bs.ResetBindings(true);
            this.bsDetalles.DataSource = Documento.DocumentosProductos;
            this.bsDetalles.ResetBindings(true);

        }

        private void txtCodigo_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.TextEdit Editor = (DevExpress.XtraEditors.TextEdit)sender;
            if (!Editor.IsModified)
                return;
            string Texto = Editor.Text;
            List<Producto> T = FactoryProductos.Buscar(Texto);
            switch (T.Count)
            {
                case 0:
                    MessageBox.Show("Producto o Servicio no Encontrado", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Producto = new Producto();
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
                    F.ShowDialog();
                    if (F.Registro != null)
                    {
                        Producto = FactoryProductos.Item(((Producto)F.Registro).IdProducto);
                        Editor.Text = Producto.Codigo;
                    }
                    else
                    {
                        Producto = new Producto();
                        Editor.Text = Producto.Codigo;
                    }
                    break;
            }
            LeerProducto();
        }
        private void LeerProducto()
        {
            DocumentoProducto = (DocumentosProducto)this.bsDetalles.Current;
            this.gridControl1.MainView.ActiveEditor.Text = Producto.Codigo;
            DocumentoProducto.IdProducto = Producto.IdProducto;
            DocumentoProducto.Cantidad = 1;
            DocumentoProducto.DescuentoBs = 0;
            DocumentoProducto.DescuentoPorcentaje = 0;
            DocumentoProducto.TasaIva = Producto.Iva;
            DocumentoProducto.Precio = Producto.Precio;
            DocumentoProducto.Codigo = Producto.Codigo;
            DocumentoProducto.Descripcion = Producto.Descripcion;
            DocumentoProducto.PrecioIva = Producto.PrecioIVA;
            DocumentoProducto.Costo = Producto.Costo;
            DocumentoProducto.ExistenciaAnterior = Producto.Existencia;
            CalcularMontoItem(DocumentoProducto);
            Documento.CalcularTotales();
        }

        private void CalcularMontoItem(DocumentosProducto DocumentoProducto)
        {
            if (DocumentoProducto.IdProducto == null)
            {
                DocumentoProducto = new DocumentosProducto();
                return;
            }
            this.bsDetalles.EndEdit();
            DocumentoProducto.DescuentoBs = cBasicas.Round(DocumentoProducto.DescuentoPorcentaje * DocumentoProducto.Costo / 100);
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
            Producto.Costo= DocumentoProducto.MontoNeto;
            Producto.Iva = DocumentoProducto.TasaIva;
            DocumentoProducto.CostoIva = DocumentoProducto.Costo + (DocumentoProducto.TasaIva * DocumentoProducto.Costo / 100);
            DocumentoProducto.PrecioIva = DocumentoProducto.Precio + (DocumentoProducto.TasaIva * DocumentoProducto.Precio / 100);
            DocumentoProducto.Total = (DocumentoProducto.MontoNeto + DocumentoProducto.Iva) * DocumentoProducto.Cantidad;
        }
        private void documentosProductosBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void gridView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DocumentoProducto = (DocumentosProducto)e.Row;
            CalcularMontoItem(DocumentoProducto);
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            Documento.CalcularTotales();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tercero == null)
                {
                    return;
                }
                Documento.IdTercero = Tercero.IdTercero;
                Documento.Razonsocial = Tercero.RazonSocial;
                Documento.CedulaRif = Tercero.CedulaRif;
                Documento doc = new Documento();
                if (Documento.IdDocumento == null)
                {
                    
                    doc.IdDocumento = FactoryContadores.GetLast("IdDocumento");
                    doc.Numero = FactoryContadores.GetLast("PedidoProveedor");
                    doc.Status = "ABIERTA";
                    doc.Mes = DateTime.Today.Month;
                    doc.Año= DateTime.Today.Year;
                    doc.Fecha = DateTime.Today;
                    doc.Activo = true;
                    doc.Tipo = "PEDIDO PROVEEDOR";
                    doc.IdTercero = Tercero.IdTercero;
                    doc.Razonsocial = Tercero.RazonSocial;
                    doc.CedulaRif = Tercero.CedulaRif;
                    foreach (DocumentosProducto d in Documento.DocumentosProductos)
                    {
                        if (d.Cantidad != 0)
                        {
                            DocumentosProducto newItem = new DocumentosProducto();
                            newItem.Cantidad = d.Cantidad;
                            newItem.Codigo = d.Codigo;
                            newItem.Activo = true;
                            newItem.Costo = d.Costo;
                            newItem.Descripcion = d.Descripcion;
                            newItem.DescuentoBs = 0;
                            newItem.ExistenciaAnterior = d.ExistenciaAnterior;
                            newItem.TasaIva = d.TasaIva;
                            newItem.IdProducto = d.IdProducto;
                            CalcularMontoItem(newItem);
                            newItem.IdDetalleDocumento = FactoryContadores.GetLast("IdDetalleDocumento");                          
                            doc.DocumentosProductos.Add(newItem);                            
                        }
                    }
                    doc.CalcularTotales();
                    dc.Documentos.Add(doc);
                }               
                dc.SaveChanges();
                FrmReportes f = new FrmReportes();
                f.PedidoProveedor(doc);
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "Error al guardar pedido", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void CargarFallas_Click(object sender, EventArgs e)
        {
            List<Producto> ListaProductos = FactoryProductos.CargarProductosMinimo();
            Limpiar();
            foreach (Producto p in ListaProductos)
            {
                DocumentosProducto newItem = new DocumentosProducto();
                newItem.Cantidad = 0;
                newItem.Codigo = p.Codigo;
                newItem.Activo = true;
                newItem.Costo = p.Costo;
                newItem.Descripcion = p.Descripcion;
                newItem.DescuentoBs = 0;
                newItem.ExistenciaAnterior = p.Existencia;
                newItem.Total = 0;
                newItem.IdProducto = p.IdProducto;
                newItem.TasaIva = p.Iva;
                newItem.Minimo = p.Minimo;
                newItem.Maximo = p.Maximo;
                newItem.Linea = p.Grupo;
                CalcularMontoItem(newItem);
                Documento.DocumentosProductos.Add(newItem);
            }
            Documento.CalcularTotales();
            this.bs.DataSource = Documento;
            this.bs.ResetBindings(true);
            this.bsDetalles.DataSource = Documento.DocumentosProductos;
            this.bsDetalles.ResetBindings(true);
        }
    }
}
