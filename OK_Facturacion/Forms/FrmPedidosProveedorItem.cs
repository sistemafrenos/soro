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

        Terceros Tercero = null;
        Productos Producto = null;
        Documentos Documento = null;
        DocumentosProductos DocumentoProducto = null;
        SoroEntities dc = new SoroEntities();
        List<DocumentosProductos> myPedido = new List<DocumentosProductos>();
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

        private void btnCargarProductos_Click(object sender, EventArgs e)
        {
            List<VistaProductos> ListaProductos = FactoryProductos.CargarProductosProveedor(Tercero.IdTercero);
            Limpiar();
            foreach (VistaProductos p in ListaProductos)
            {
                DocumentosProductos newItem = new DocumentosProductos();
                newItem.Cantidad = 0;
                newItem.Codigo = p.Codigo;
                newItem.Activo = true;
                newItem.Costo = p.CostoActual;
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
        }
        private void Limpiar()
        {
            Documento = new Documentos();
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
                Documento = new Documentos();
            }
            Tercero = FactoryTerceros.Item(Documento.IdTercero);
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
            DocumentoProducto.Precio2 = Producto.PrecioIva2;
            DocumentoProducto.Precio2 = Producto.PrecioIva3;
            DocumentoProducto.Precio4 = Producto.PrecioIva4;
            DocumentoProducto.Utilidad = Producto.Utilidad;
            DocumentoProducto.Utilidad2 = Producto.Utilidad;
            DocumentoProducto.Utilidad3 = Producto.Utilidad3;
            DocumentoProducto.Utilidad4 = Producto.Utilidad4;
            CalcularMontoItem(DocumentoProducto);
            Documento.CalcularTotales();
        }

        private void CalcularMontoItem(DocumentosProductos DocumentoProducto)
        {
            if (DocumentoProducto.IdProducto == null)
            {
                DocumentoProducto = new DocumentosProductos();
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
            Producto.CostoActual = DocumentoProducto.MontoNeto;
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
            DocumentoProducto = (DocumentosProductos)e.Row;
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
                Documentos doc = new Documentos();
                if (Documento.IdDocumento == null)
                {
                    
                    doc.IdDocumento = FactoryContadores.GetLast("IdDocumento");
                    doc.Numero = FactoryContadores.GetLast("PedidoProveedor");
                    doc.Status = "ABIERTA";
                    doc.Mes = DateTime.Today.Month;
                    doc.Año = DateTime.Today.Year;
                    doc.Fecha = DateTime.Today;
                    doc.Activo = true;
                    doc.Tipo = "PEDIDO PROVEEDOR";
                    doc.IdTercero = Tercero.IdTercero;
                    foreach (DocumentosProductos d in Documento.DocumentosProductos)
                    {
                        if (d.Cantidad != 0)
                        {
                            DocumentosProductos newItem = new DocumentosProductos();
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
            List<VistaProductos> ListaProductos = FactoryProductos.CargarProductosMinimo();
            Limpiar();
            foreach (VistaProductos p in ListaProductos)
            {
                DocumentosProductos newItem = new DocumentosProductos();
                newItem.Cantidad = 0;
                newItem.Codigo = p.Codigo;
                newItem.Activo = true;
                newItem.Costo = p.CostoActual;
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
