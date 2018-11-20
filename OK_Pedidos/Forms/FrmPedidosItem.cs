using HK.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace HK
{
    public partial class FrmPedidosItem : Form
    {
        public FrmPedidosItem()
        {
            InitializeComponent();
            this.TxtTasaIva.Properties.Items.AddRange(new object[] { 16, 12, 9 });
            this.TxtTasaIva.EditValue = 16;
        }
        
        Cliente Tercero = null;
        Producto Producto = null;
        Pedido Documento = null;
        PedidosDetalle DocumentoProducto = null;
        Entities Entities = new Entities();
        RegistroPago Recibo = new RegistroPago();
        private void txtCantidad_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.SpinEdit Editor = (DevExpress.XtraEditors.SpinEdit)this.gridControl1.MainView.ActiveEditor;
            if (!Editor.IsModified)
            {
                return;
            }
            try
            {
                if ((double)Editor.Value > DocumentoProducto.ExistenciaPrevia)
                {
                    Editor.Value = (decimal)DocumentoProducto.ExistenciaPrevia;
                }
            }
            catch
            { 
            }
            DocumentoProducto = (PedidosDetalle)this.bsDetalles.Current;
            DocumentoProducto.Cantidad = (double)Editor.Value;           
            CalcularMontoItem();
        }
        private void CalcularMontoItem()
        {
            this.bsDetalles.EndEdit();
            DocumentoProducto.DescuentoBolivares = DocumentoProducto.DescuentoPorcentaje * DocumentoProducto.Precio / 100;
            DocumentoProducto.PrecioNeto = cBasicas.Round(DocumentoProducto.Precio - DocumentoProducto.DescuentoBolivares);
            DocumentoProducto.Iva = cBasicas.Round(DocumentoProducto.PrecioNeto * DocumentoProducto.TasaIva / 100) * DocumentoProducto.Cantidad;
            DocumentoProducto.Total = cBasicas.Round((DocumentoProducto.PrecioNeto * DocumentoProducto.Cantidad) + DocumentoProducto.Iva);
        }
        private void FrmPedidoProveedorItem_Load(object sender, EventArgs e)
        {
            this.txtCodigo.Validating += new CancelEventHandler(txtCodigo_Validating);
            this.txtDescuentoPorcentaje.Validating += new CancelEventHandler(DescuentoPorcentaje_Validating);
            this.txtCant.Validating += new CancelEventHandler(txtCantidad_Validating);
            this.txtCant.Enter += new EventHandler(txtCant_Enter);
        }
        void txtCant_Enter(object sender, EventArgs e)
        {
            if (DocumentoProducto == null)
            {                
                return;
            }
            if (string.IsNullOrEmpty(DocumentoProducto.Codigo))
            {
                SendKeys.Send("{ESC}");
            //    SelectNextControl((Control)txtCodigo.Connect(, false, true, false, true);
            }
        }
        private void DescuentoPorcentaje_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.SpinEdit Editor = (DevExpress.XtraEditors.SpinEdit)this.gridControl1.MainView.ActiveEditor;
            if (!Editor.IsModified)
                return;
            DocumentoProducto = (PedidosDetalle)this.bsDetalles.Current;
            DocumentoProducto.DescuentoPorcentaje = (double)Editor.Value;
            CalcularMontoItem();
            Documento.CalcularTotales();
        }
        private void Limpiar()
        {
            Documento = new Pedido();                
            Tercero = new Cliente();
            Tercero.TipoPrecio = "PRECIO 1";
            Tercero.DescuentoPorcentaje = 0;
            RegistroPago Recibo = new RegistroPago();
        }
        public void Incluir()
        {
            Limpiar();
            Enlazar();
            this.ShowDialog();
        }
        public void Modificar(string IDpedido)
        {
            Limpiar();
            Documento = FactoryPedidos.Item(IDpedido, Entities);
            Enlazar();
            LeerTitular();
            this.txtCedulaRif.Text = Tercero.CedulaRif;
            this.Text = "MODIFICACION DE PEDIDO";
            this.ShowDialog();
        }
        public void Copiar(string IDpedido)
        {
            Limpiar();
            Documento = new Pedido();
            Pedido oldDocumento = FactoryPedidos.Item(IDpedido, Entities);
            Documento.IdCliente = oldDocumento.IdCliente;
            foreach (PedidosDetalle d in oldDocumento.PedidosDetalles)
            {
                PedidosDetalle p = new PedidosDetalle
                {
                    IdProducto = d.IdProducto,
                    Cantidad = d.Cantidad,
                    Codigo = d.Codigo,
                    Descripcion = d.Descripcion,
                    DescuentoBolivares = d.DescuentoBolivares,
                    DescuentoPorcentaje = d.DescuentoPorcentaje,
                    ExistenciaPrevia = d.ExistenciaPrevia,
                    Iva = d.Iva,
                    Precio = d.Precio,
                    PesoUnitario = d.PesoUnitario,
                    PesoTotal = d.PesoTotal,
                    PrecioNeto = d.PrecioNeto,
                    Pvs = d.Pvs,
                    TasaIva = d.TasaIva,
                    Total = d.Total,
                    UnidadMedida = d.UnidadMedida
                };
                Documento.PedidosDetalles.Add(p);
            }            
            Enlazar();
            LeerTitular();
            this.txtCedulaRif.Text = Tercero.CedulaRif;
            this.Text = "MODIFICACION DE PEDIDO";
            this.ShowDialog();
        }
        private void Enlazar()
        {
            if (Documento == null)
            {
                Documento = new Pedido();
            }
            Tercero = FactoryTerceros.Item(Documento.IdCliente);
            this.bsCliente.DataSource = Tercero;
            this.bs.DataSource = Documento;
            this.bs.ResetBindings(true);
            this.bsDetalles.DataSource = Documento.PedidosDetalles;
            this.bsDetalles.ResetBindings(true);

        }
        private void txtCodigo_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.TextEdit Editor = (DevExpress.XtraEditors.TextEdit)sender;
            if (!Editor.IsModified)
                return;
            this.bsDetalles.EndEdit();
            if (Editor.Text == "*")
                Editor.Text = "";
            string Texto = Editor.Text;
            List<Producto> T = FactoryProductos.Buscar(Texto);
            switch (T.Count)
            {
                case 0:
                    MessageBox.Show("Producto o Servicio no Encontrado", "Atencion", MessageBoxButtons.OK);
                    return;
                case 1:
                    Producto = FactoryProductos.Item(T[0].IdProducto);
                    Editor.Text = Producto.Codigo;
                    break;
                default:
                    FrmBuscarEntidades F = new FrmBuscarEntidades();
                    F.Texto = Texto;
                    F.myLayout = "PRODUCTOS";
                    F.Filtro = "";
                    if (Tercero == null)
                    {
                        Tercero = new Cliente();
                        Tercero.TipoPrecio="PRECIO 4";
                    }
                    F.TipoPrecio = Tercero.TipoPrecio;
                    F.ShowDialog();
                    if (F.Registro != null)
                    {
                        Producto = (Producto)F.Registro;
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
            DocumentoProducto = (PedidosDetalle)this.bsDetalles.Current;
            this.gridControl1.MainView.ActiveEditor.Text = Producto.Codigo;
            DocumentoProducto.IdProducto = Producto.IdProducto;
            DocumentoProducto.Cantidad = 1;
            DocumentoProducto.DescuentoBolivares = 0;
            if (Tercero == null)
            {
                Tercero = new Cliente();
                Tercero.DescuentoPorcentaje = 0;
                Tercero.TipoPrecio = "PRECIO 4";
            }
            if (!Tercero.DescuentoPorcentaje.HasValue)
            {
                Tercero.DescuentoPorcentaje = 0;
            }
            DocumentoProducto.DescuentoPorcentaje = Tercero.DescuentoPorcentaje;            
            switch (Tercero.TipoPrecio)
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
                case "PRECIO 5":
                    DocumentoProducto.Precio = cBasicas.Round(Producto.Precio5);
                    break;
            }
            DocumentoProducto.TasaIva = Producto.Iva == 0 ? 0 : float.Parse(TxtTasaIva.Text);
            DocumentoProducto.Codigo = Producto.Codigo;
            DocumentoProducto.Descripcion = Producto.Descripcion;
            DocumentoProducto.Iva = cBasicas.Round(DocumentoProducto.Precio * (DocumentoProducto.TasaIva / 100));
            DocumentoProducto.ExistenciaPrevia = Producto.Existencia;
            DocumentoProducto.PesoUnitario = Producto.Peso;
            DocumentoProducto.PesoTotal = DocumentoProducto.PesoUnitario * DocumentoProducto.Cantidad;
            DocumentoProducto.UnidadMedida = Producto.UnidadMedida;
            CalcularMontoItem();
        }
        private void LeerProducto(Producto producto)
        {
            var prod = (from x in Entities.Productos
                        where x.IdProducto == producto.IdProducto
                        select x).FirstOrDefault();
            producto.Pedido = producto.Pedido > prod.Existencia ? prod.Existencia : producto.Pedido;
            var verificiar = from p in Documento.PedidosDetalles
                             where p.IdProducto == producto.IdProducto
                             select p;
            if (verificiar.FirstOrDefault() == null)
            {
                DocumentoProducto = new PedidosDetalle();
                DocumentoProducto.Codigo = producto.Codigo;
                DocumentoProducto.IdProducto = producto.IdProducto;
                DocumentoProducto.Cantidad = producto.Pedido.GetValueOrDefault(0);
                DocumentoProducto.DescuentoBolivares = 0;
                if (Tercero == null)
                {
                    Tercero = new Cliente();
                    Tercero.DescuentoPorcentaje = 0;
                    Tercero.TipoPrecio = "PRECIO 4";
                }
                if (!Tercero.DescuentoPorcentaje.HasValue)
                {
                    Tercero.DescuentoPorcentaje = 0;
                }
                DocumentoProducto.DescuentoPorcentaje = Tercero.DescuentoPorcentaje;                
                switch (Tercero.TipoPrecio)
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
                    case "PRECIO 5":
                        DocumentoProducto.Precio = cBasicas.Round(producto.Precio5);
                        break;

                }
                DocumentoProducto.TasaIva = producto.Iva == 0 ? 0 : float.Parse(TxtTasaIva.Text);
                DocumentoProducto.Codigo = producto.Codigo;
                DocumentoProducto.Descripcion = producto.Descripcion;
                DocumentoProducto.Iva = cBasicas.Round(DocumentoProducto.Precio * (DocumentoProducto.TasaIva / 100));
                DocumentoProducto.ExistenciaPrevia = producto.Existencia;
                DocumentoProducto.PesoUnitario = producto.Peso;
                DocumentoProducto.PesoTotal = DocumentoProducto.PesoUnitario * DocumentoProducto.Cantidad;
                DocumentoProducto.UnidadMedida = producto.UnidadMedida;
                CalcularMontoItem();
                // this.bsDetalles.List.Add(DocumentoProducto);
                Documento.PedidosDetalles.Add(DocumentoProducto);
                this.bsDetalles.DataSource = Documento.PedidosDetalles;
                this.bsDetalles.ResetBindings(false);
            }
        }
        private void CalcularMontoItem(PedidosDetalle DocumentoProducto)
        {
            if (DocumentoProducto.IdProducto == null)
            {
                DocumentoProducto = new PedidosDetalle();
                return;
            }
            this.bsDetalles.EndEdit();
            DocumentoProducto.DescuentoBolivares = cBasicas.Round(DocumentoProducto.DescuentoPorcentaje * DocumentoProducto.Precio / 100);
            DocumentoProducto.PrecioNeto = cBasicas.Round(DocumentoProducto.Precio - DocumentoProducto.DescuentoBolivares);
            DocumentoProducto.Iva = cBasicas.Round(DocumentoProducto.PrecioNeto * DocumentoProducto.TasaIva / 100) * DocumentoProducto.Cantidad;            
            DocumentoProducto.Total = (DocumentoProducto.PrecioNeto * DocumentoProducto.Cantidad) + DocumentoProducto.Iva;
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        private void gridView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DocumentoProducto = (PedidosDetalle)e.Row;
            CalcularMontoItem(DocumentoProducto);
        }
        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            Documento.CalcularTotales();
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
                        PedidosDetalle Registro = (PedidosDetalle)this.bsDetalles.Current;
                        try
                        {
                            this.bsDetalles.Remove(Registro);
                        }
                        catch { }                        
                    }
                    e.Handled = true;
                }
            }
        }
        private void txtTitular_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.TextEdit Editor = (DevExpress.XtraEditors.TextEdit)sender;
            if (!Editor.IsModified)
                return;
            string Texto = Editor.Text;
            List<Cliente> T = FactoryTerceros.Buscar(Entities,Texto);
            switch (T.Count)
            {
                case 0:
                    Tercero = new Cliente();
                    Tercero.CedulaRif = cBasicas.CedulaRif(Editor.Text);
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
                    F.ShowDialog();
                    if (F.Registro != null)
                    {
                        Tercero = (Cliente)F.Registro;
                        Tercero = FactoryTerceros.Item(Tercero.IdTercero);
                    }
                    else
                    {
                        Tercero = new Cliente();
                    }
                    break;
            }
            LeerTitular();
            Editor.Text = Tercero.CedulaRif;
            if (Tercero.IdTercero != null)
            {
                this.gridControl1.Focus();
                this.gridView1.Focus();
            }
        }
        private void BuscarTitulares_Click(object sender, EventArgs e)
        {
            FrmBuscarEntidades F = new FrmBuscarEntidades();
            F.Texto = "";
            F.myLayout = "TERCEROS";
            F.Filtro = "";
            F.ShowDialog();
            if (F.Registro != null)
            {
                Tercero = (Cliente)F.Registro;
                Tercero = FactoryTerceros.Item(Tercero.IdTercero);
                LeerTitular();
            }
            else
            {
                Tercero = new Cliente();
                LeerTitular();
            }
        }
        private void LeerTitular()
        {
            if (Tercero == null)
            {
                Tercero = new Cliente();
                Tercero.TipoPrecio = "PRECIO 4";
            }
            this.CedulaRifTextEdit.Text = Tercero.CedulaRif;
            this.RazonSocialTextEdit.Text = Tercero.RazonSocial;
            this.DireccionTextEdit.Text = Tercero.Direccion;
            this.CiudadTextEdit.Text = Tercero.Ciudad;
            this.TelefonosTextEdit.Text = Tercero.Telefonos;
            this.txtTipoPrecio.Text = Tercero.TipoPrecio;
            if (Tercero.SaldoDeudor.HasValue)
            {
                this.SaldoDeudorTextEdit.Text = Tercero.SaldoDeudor.Value.ToString("n2");
            }

        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {

            Parametro parametro = Entities.Parametros.FirstOrDefault();
            if (Tercero == null)
            {
                return;
            }
            FrmPedidosItemSave f = new FrmPedidosItemSave()
            {
                Tasaiva = float.Parse(this.TxtTasaIva.Text)
            };
            var result = f.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }
            foreach (var item in Documento.PedidosDetalles)
            {
                item.TasaIva = item.TasaIva == 0 ? 0 : f.Tasaiva;
                CalcularMontoItem(item);

            }
            Documento.CalcularTotales();
            Documento.Enviado = false;
            Documento.IdCliente = Tercero.IdTercero;
            Documento.Fecha = DateTime.Today;
            Documento.Enviado = false;
            Documento.CedulaRif = Tercero.CedulaRif;
            Documento.MontoTotal = Documento.MontoTotal;
            Documento.RazonSocial = Tercero.RazonSocial;
            if (string.IsNullOrEmpty(Documento.IdPedido))
            {
                Documento.IdPedido = parametro.Equipo + FactoryContadores.GetLast("IdPedido");
                Documento.Numero = parametro.Equipo + FactoryContadores.GetLast("Pedido");
                Entities.Pedidos.Add(Documento);
            }            
            foreach (PedidosDetalle d in Documento.PedidosDetalles)
            {
                if (d.Cantidad != 0)
                {
                    if (string.IsNullOrEmpty(d.IdDetalle))
                    {
                        d.IdDetalle = FactoryContadores.GetLast("IdDetallePedido");
                    }
                    if (this.Text != "MODIFICACION DE PEDIDO")
                    {
                        Producto p = Entities.Productos.FirstOrDefault(x => x.IdProducto == d.IdProducto);
                        if (p != null)
                        {
                            p.Existencia = p.Existencia - d.Cantidad;
                        }
                    }
                }
            }
            try
            {
                Entities.SaveChanges();
            }
            catch (Exception x)
            {
                MessageBox.Show(string.Format("Error al guardar pedido,{0}", x.Message));
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        private void gridView1_KeyDown(object sender, KeyEventArgs e)
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
                        PedidosDetalle Registro = (PedidosDetalle)this.bsDetalles.Current;
                        if (Registro != null)
                        {
                            this.bsDetalles.List.Remove(Registro);
                            this.gridControl1.RefreshDataSource();
                        }
                    }
                    e.Handled = true;
                }
            }
        }
        private void bsDetalles_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (Documento != null)
            {
                Documento.CalcularTotales();
            }
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FrmBuscarProductos f = new FrmBuscarProductos();
            if (Tercero != null)
            {
                f.TipoPrecio = Tercero.TipoPrecio;
            }
            else
            {
                f.TipoPrecio = "PRECIO 4";
            }
            f.ShowDialog();
            if (f.DialogResult != System.Windows.Forms.DialogResult.OK)
                return;
            if (f.Productos.Where(x => x.Pedido > 0).Count() > 1)
            {
                foreach (Producto p in f.Productos.Where(x => x.Pedido > 0))
                {
                    LeerProducto(p);
                }
                Documento.CalcularTotales();
                Producto = new Producto();
                //this.bsDetalles.DataSource = Documento;
                //this.bsDetalles.ResetBindings(true);
                //this.gridControl1.DataSource = bsDetalles;
            }
        }
        private void gridControl1_Enter(object sender, EventArgs e)
        {
            this.gridView1.Focus();            
        }
     }
}


