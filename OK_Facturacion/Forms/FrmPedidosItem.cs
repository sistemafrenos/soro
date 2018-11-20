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
    public partial class FrmPedidosItem : Form
    {
        public FrmPedidosItem()
        {
            InitializeComponent();
        }
        SoroEntities dc = new SoroEntities();
        Terceros Titular = null;
        Terceros Vendedor = null;
        Productos Producto = null;
        public Documentos Documento = new Documentos();
        DocumentosProductos DocumentoProducto = null;
        RegistroPagos Recibo = new RegistroPagos();
        List<Terceros> Tecnicos = FactoryTerceros.ItemsxTipo("TECNICO");

        private void Forma_Load(object sender, EventArgs e)
        {
            this.txtCodigo.Validating += new CancelEventHandler(txtCodigo_Validating);
            this.txtCantidad.Validating += new CancelEventHandler(txtCantidad_Validating);
            this.txtPrecio.Validating += new CancelEventHandler(txtPrecio_Validating);
            this.txtPrecioIVA.Validating += new CancelEventHandler(txtPrecioIVA_Validating);
            this.txtDescuentoPorcentaje.Validating += new CancelEventHandler(DescuentoPorcentaje_Validating);
            this.txtDescuentoBs.Validating += new CancelEventHandler(DescuentoBolivares_Validating);
            this.cmdRealizadoPor.Validating += new CancelEventHandler(cmbRealizadoPor_Validating);
            #region Custom
            switch (FactoryParametros.Item().Empresa)
            {
                case "HK SOLUCIONES,C.A.":
                    {
                        foreach (Terceros Tecnico in Tecnicos)
                        {
                            this.cmdRealizadoPor.Items.Add(Tecnico.RazonSocial);
                        }
                        this.colRealizadoPor.Visible = true;
                        this.colUnidadMedida.Visible = false;
                        this.colPeso.Visible = false;
                        break;
                    }
                case "REPUESTOS SAN MIGUEL,C.A.":
                    {
                        this.colRealizadoPor.Visible = false;
                        this.colUnidadMedida.Visible = false;
                        this.colPeso.Visible = false;
                        break;
                    }

                case "COMERCIAL PADRE E HIJO,C.A.":
                    {
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
                        break;
                    }
                case "FARMASHOP 2000,c.a.":
                    {
                        this.colRealizadoPor.Visible = false;
                        this.colUnidadMedida.Visible = false;
                        this.colPeso.Visible = false;
                        break;
                    }
                case "REPUESTOS TALLERES LATINOS,C.A.":
                    {
                        this.colRealizadoPor.Visible = false;
                        this.colUnidadMedida.Visible = false;
                        this.colPeso.Visible = false;
                        break;
                    }
                case "MERCANTIL EL ROSARIO,C.A.":
                    {
                        this.colRealizadoPor.Visible = false;
                        this.colUnidadMedida.Visible = false;
                        this.colPeso.Visible = false;
                        this.globalBolivares.Properties.ReadOnly = true;
                        this.globalPorcentaje.Properties.ReadOnly = true;
                        this.colDescuentoBs.Visible = false;
                        this.colDescuentoPorcentaje.Visible = false;
                        this.colDescripcion.OptionsColumn.AllowFocus = false;
                        this.colPrecio.OptionsColumn.AllowFocus = false;
                        this.colPrecioIva.OptionsColumn.AllowFocus = false;
                        break;
                    }
            }
            #endregion
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
        public void Incluir()
        {
            Limpiar(); 
            Documento.Activo = true;
            Documento.Fecha = DateTime.Today;
            Documento.Tipo = "PEDIDO";
            Documento.TasaIVA = FactoryParametros.Item().TasaIVA;
            Titular.Direccion = FactoryParametros.Item().EmpresaCiudad;
            Vendedor = FactoryTerceros.ItemxNombre(FactoryParametros.Item().Vendedor);
            this.toolCargarDocumento.Enabled = true;
            if (Vendedor!=null)
            {
                Documento.IdVendedor = Vendedor.IdTercero;
            }
            Enlazar();
            this.ShowDialog();
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
                MessageBox.Show("Esta PEDIDO ya fue impresa y no se puede eliminar debe realizar una devolucion","Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //{
            //    try
            //    {
            //        cBasicas.ImprimirDevolucion(Documento);
            //    }
            //    catch (Exception x)
            //    {
            //        MessageBox.Show(x.Message,"Error al anular PEDIDO",  MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
            Limpiar();
            Enlazar();
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
            try
            {
                DoGuardar();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception x)
            {
                string s = x.Message;
            }
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
            try
            {
                EscribirTitular();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // Guardar
  
            Documento.Tipo = "PEDIDO";
            Documento.IdVendedor = Vendedor.IdTercero;
            Documento.MontoTotal = (double)decimal.Round((decimal)Documento.MontoTotal,2);
            Documento.DeudaAnterior = Convert.ToDouble(this.txtMontoPendiente.Value);
            Documento.Status = "ABIERTA";
            Documento.Saldo = Documento.MontoTotal;
            Recibo.MontoPagar = Documento.MontoTotal;
            #region pagos
            //if (!CargarPagos())
            //    return false;
            //Documento.Saldo = Recibo.SaldoPendiente;
            //if (Documento.Saldo > 0)
            //{
            //    if (MessageBox.Show("Esta seguro de guardar esta PEDIDO a credito ?", "Atencion", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
            //    {
            //        return false;
            //    }
            //    if (Titular.DiasCredito == null)
            //    {
 
            //    }
            //    Documento.Condiciones = string.Format("CREDITO {0} DIAS", Titular.DiasCredito);
            //    Documento.Vence = Documento.Fecha.Value.AddDays((double)Titular.DiasCredito);
            //}
            //else
            //{
            //    Documento.Condiciones = "CONTADO";
            //}
            //if (Recibo != null)
            //{
            //    Recibo.Fecha = DateTime.Today;
            //    Recibo.Momento = DateTime.Now;
            //    Recibo.Modulo = "PEDIDOS";
            //}
            #endregion
            try
            {
                FactoryDocumentos.Save(dc, Documento, Recibo, Titular);
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "Error al guardar PEDIDO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            try
            {
           //     cBasicas.ImprimirPedido(Documento);
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "Error al imprimir PEDIDO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void toolCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        private void doImprimir()
        {
            if (Documento == null)
                return;
        //    cBasicas.ImprimirPedido(Documento);
            ImprimirTickera();

        }
        private void ImprimirTickera()
        {
            Parametros Empresa = FactoryParametros.Item();
            if (Documento == null)
                return;
            LPrintWriter p = new LPrintWriter();
            //p.WriteLine(Empresa.Empresa);
            //p.WriteLine(Empresa.EmpresaRif);
            //p.WriteLine(Empresa.EmpresaDireccion);
            //p.WriteLine(Empresa.EmpresaTelefonos);
            p.WriteLine("========================================");
            p.WriteLine(cBasicas.PrintFix("PEDIDO", 40, 3));
            p.WriteLine("========================================");
            p.WriteLine(cBasicas.PrintFix(" Fecha:" + Documento.Fecha.Value.ToShortDateString(), 40, 2));
            p.WriteLine(cBasicas.PrintFix("Numero:" + Documento.Numero, 40, 2));
            p.WriteLine(Titular.RazonSocial);
            p.WriteLine(Titular.CedulaRif);
            p.WriteLine(Titular.Direccion);
            p.WriteLine("========================================");
            foreach (DocumentosProductos Item in Documento.DocumentosProductos)
            {
                if (Item.Cantidad == 1)
                {
                    p.Write(cBasicas.PrintFix(Item.Descripcion, 32, 1));
                    p.WriteLine(cBasicas.PrintNumero(Item.Total, 8));
                }
                else
                {
                    p.WriteLine(cBasicas.PrintFix(Item.Descripcion, 40, 1));
                    p.Write(cBasicas.PrintNumero(Item.Cantidad, 6) + " x ");
                    p.Write(cBasicas.PrintNumero(Item.MontoNeto, 8));
                    p.WriteLine(cBasicas.PrintNumero(Item.Total, 8));
                }
            }
            p.WriteLine("========================================");
            p.WriteLine(cBasicas.PrintFix("      SubTotal: " + cBasicas.PrintNumero(Documento.SubTotal, 8), 40, 2));
            p.WriteLine(cBasicas.PrintFix("     Descuento: " + cBasicas.PrintNumero(Documento.DescuentoBs, 8), 40, 2));
            //p.WriteLine(cBasicas.PrintFix("  Monto Exento: " + cBasicas.PrintNumero(Documento.MontoExento, 8), 40, 2));
            //p.WriteLine(cBasicas.PrintFix("Base Imponible: " + cBasicas.PrintNumero(Documento.BaseImponible, 8), 40, 2));
            //p.WriteLine(cBasicas.PrintFix("     Monto Iva: " + cBasicas.PrintNumero(Documento.MontoIva, 8), 40, 2));
            p.WriteLine(cBasicas.PrintFix("   Monto Total: " + cBasicas.PrintNumero(Documento.MontoTotal, 8), 40, 2));
            p.WriteLine("========================================");
            p.WriteLine(".");
            p.WriteLine(" ");
            p.WriteLine(".");
            p.WriteLine(" ");
            p.WriteLine(".");
            p.WriteLine(" ");
            p.WriteLine(".");
            p.WriteLine(" ");
            p.WriteLine(".");
            p.WriteLine(" ");
            p.Flush();
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
            this.Text = "PEDIDO-" + Titular.RazonSocial;
            if (String.IsNullOrEmpty(Titular.TipoPrecio))
            {
                Titular.TipoPrecio="PRECIO 1";
            }
            this.txtTipoPrecio.Text = Titular.TipoPrecio;
            ResumenCuentas Resumen = FactoryCuentas.CuentasTercero(Titular.IdTercero);
            this.txtMontoPendiente.Value = Convert.ToDecimal( Resumen.TotalPendiente) ;
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
                    Titular = new Terceros();
                    Titular.CedulaRif = cBasicas.CedulaRif(Editor.Text);
                    Titular.Direccion = FactoryParametros.Item().EmpresaCiudad;
                    Titular.DescuentoPorcentaje = 0;
                    Titular.TipoPrecio = "PRECIO 1";
                    Titular.Condiciones = "CONTADO";
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
        public void Editar()
        {
            if (Documento == null)
                return;
            Documento = FactoryDocumentos.Item(dc, Documento.IdDocumento);
            Enlazar();
            this.ShowDialog();
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
        private void CargarDocumento_Click(object sender, EventArgs e)
        {
            FrmBuscarEntidades F = new FrmBuscarEntidades();
            F.myLayout = "BUSCARVENTAS";
            F.ShowDialog();
            Documentos Doc;
            if (F.DialogResult == DialogResult.OK)
            {
                Doc = FactoryDocumentos.Item(dc, ((VistaDocumento)F.Registro).IdDocumento);
                CargarDocumento(Doc);                
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
            Documento.Tipo = "PEDIDO";
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
                Producto = FactoryProductos.Item(Item.IdProducto);
                newItem.ExistenciaAnterior = Producto.Existencia;
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
            List<VistaProductos> T = FactoryProductos.Buscar(Texto);
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
                    F.Filtro = "";
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
            Documento.DescuentoBs = (double)Editor.Value * Documento.SubTotal / 100;
            CalcularMontoItem();
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
            Documento.DescuentoPorcentaje = (Documento.DescuentoBs) / (Documento.SubTotal / 100);
            CalcularMontoItem();
        }
        private void DescuentoPorcentaje_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.SpinEdit Editor = (DevExpress.XtraEditors.SpinEdit)this.gridControl1.MainView.ActiveEditor;
            if (!Editor.IsModified)
                return;
            DocumentoProducto = (DocumentosProductos)this.bsDetalles.Current;
            DocumentoProducto.DescuentoPorcentaje = (double)Editor.Value;
            CalcularMontoItem();
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
            DocumentoProducto.Iva = cBasicas.Round(DocumentoProducto.Precio * DocumentoProducto.TasaIva / 100);
            DocumentoProducto.PrecioIva = cBasicas.Round(DocumentoProducto.Precio + DocumentoProducto.Iva);
            DocumentoProducto.MontoNeto = cBasicas.Round(DocumentoProducto.Precio - DocumentoProducto.DescuentoBs);
            DocumentoProducto.Iva = cBasicas.Round(DocumentoProducto.MontoNeto * DocumentoProducto.TasaIva / 100);
            DocumentoProducto.Total = (DocumentoProducto.MontoNeto + DocumentoProducto.Iva) * DocumentoProducto.Cantidad;
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
                case "PRECIO 5":
                    DocumentoProducto.Precio = cBasicas.Round(Producto.Precio5);
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
            var prod = (from x in dc.Productos
                        where x.IdProducto == producto.IdProducto
                        select x).FirstOrDefault();
            producto.Pedido = producto.Pedido > prod.Existencia ? prod.Existencia : producto.Pedido;

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
                case "PRECIO 5":
                    DocumentoProducto.Precio = cBasicas.Round(producto.Precio5);
                    break;
            }
            DocumentoProducto.Codigo = producto.Codigo;
            DocumentoProducto.Descripcion = producto.Descripcion;
            DocumentoProducto.Iva = cBasicas.Round(DocumentoProducto.Precio * (DocumentoProducto.TasaIva / 100));
            DocumentoProducto.PesoUnitario = producto.Peso;
            DocumentoProducto.PesoTotal = DocumentoProducto.PesoUnitario * DocumentoProducto.Cantidad;
            DocumentoProducto.UnidadMedida = producto.UnidadMedida;
            DocumentoProducto.ExistenciaAnterior = producto.Existencia;
            CalcularMontoItem();
            this.bsDetalles.List.Add(DocumentoProducto);
            // Documento.PedidosDetalles.Add(DocumentoProducto);
        }
    }
}

