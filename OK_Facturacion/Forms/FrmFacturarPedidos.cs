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
    public partial class FrmFacturarPedidos : Form
    {
        Terceros Titular = new Terceros();
        Terceros Vendedor = new Terceros();
        RegistroPagos Recibo = new RegistroPagos();
        SoroEntities dc = new SoroEntities();
        Documentos Documento = new Documentos();
        List<VistaDocumento> Pedidos = new List<VistaDocumento>();
        public FrmFacturarPedidos()
        {
            InitializeComponent();
        }

        private void FrmFacturarPedidos_Load(object sender, EventArgs e)
        {
            Busqueda();
        }
        private void Busqueda()
        {
            Pedidos = FactoryDocumentos.PedidosPorFacturar();
            this.bs.DataSource = Pedidos;
            this.bs.ResetBindings(true);
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            foreach (int id in this.gridView1.GetSelectedRows())
            {
                try
                {
                    VistaDocumento Registro = (VistaDocumento)this.bs.List[id];
                    Documentos pedido = FactoryDocumentos.Item(dc, Registro.IdDocumento);
                    CargarDocumento(pedido);
                    DividirFactura(pedido);
                    using (var newdc = new SoroEntities())
                    {
                        pedido = FactoryDocumentos.Item(newdc, pedido.IdDocumento);
                        {
                            pedido.Status = "FACTURADO";
                            newdc.SaveChanges();
                        }
                    }
                    if (pedido.Saldo != pedido.MontoTotal)
                    {
                        if (pedido.DocumentosProductos.Count <= 25)
                        {
                            Recibo.IdDocumento = Documento.IdDocumento;
                        }
                        Recibo = new RegistroPagos();
                        Recibo.Cambio = pedido.Cambio;
                        Recibo.Cheque = pedido.Cheque;
                        Recibo.Deposito = pedido.Deposito;
                        Recibo.Efectivo = pedido.Efectivo;
                        Recibo.Fecha = pedido.Fecha;
                        Recibo.IdTercero = pedido.IdTercero;
                        Recibo.Modulo = "PEDIDOS";
                        Recibo.Momento = DateTime.Now;
                        Recibo.Documento = pedido.Numero;                        
                        Recibo.MontoPagar = pedido.MontoTotal;
                        Recibo.SaldoPendiente = pedido.Saldo;                        
                        Recibo.TCredito = pedido.TarjetaCredito;
                        Recibo.TDebito = pedido.TarjetaDebito;
                        Recibo.Tipo = "FACTURA";
                        Recibo.IdRegistroPago = FactoryContadores.GetLast("IdRegistroPago");
                        Terceros Tercero = FactoryTerceros.Item(pedido.IdTercero);
                        Recibo.RazonSocial = Tercero.RazonSocial;
                        Recibo.MontoPagado = Convert.ToInt16( Recibo.Cheque ) + 
                                             Convert.ToInt16( Recibo.Deposito ) +
                                             Convert.ToInt16( Recibo.Efectivo ) +
                                             Convert.ToInt16( Recibo.TCredito )  +
                                             Convert.ToInt16( Recibo.TDebito ) -
                                             Convert.ToInt16( Recibo.Cambio );
                        Recibo.SaldoPendiente = Recibo.MontoPagar - Recibo.MontoPagado;
                        dc.RegistroPagos.Add(Recibo);                        
                        dc.SaveChanges();

                    }
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message, "Atencion Error al guardar pedido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
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
            Documento.Tipo = "FACTURA";
            Documento.Activo = true;
            Documento.Año = DateTime.Today.Year;
            Documento.Fecha = DateTime.Today;
            Terceros Tercero = FactoryTerceros.Item(Doc.IdTercero);
            Documento.Vence = Documento.Fecha.Value.AddDays(Convert.ToInt16(Tercero.DiasCredito));
            Documento.DescuentoBs = Doc.DescuentoBs;
            Documento.DescuentoPorcentaje = Doc.DescuentoPorcentaje;
            Documento.Condiciones = string.Format("CREDITO {0} DIAS", Tercero.DiasCredito);
            if (Tercero.DiasCredito == null)
            {
                Tercero.DiasCredito = 0;
            }
            List<DocumentosProductos> p = Doc.DocumentosProductos.ToList();
            foreach (DocumentosProductos Item in Doc.DocumentosProductos)
            {
                
                Productos producto = FactoryProductos.Item(Item.IdProducto);
                if(producto.Existencia>0)
                {
                    if(producto.Existencia<Item.Cantidad)
                    {
                        Item.Cantidad = producto.Existencia;
                    }
                    DocumentosProductos newItem = new DocumentosProductos();
                    newItem.Activo = Item.Activo;
                    newItem.BloqueoPrecio = Item.BloqueoPrecio;
                    newItem.Cantidad = Item.Cantidad;
                    newItem.Codigo = Item.Codigo;
                    newItem.Comentarios = Item.Comentarios;
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
                    newItem.PesoTotal = producto.Peso * Item.Cantidad;
                    newItem.Pvs = producto.PVP;
                    newItem.UnidadMedida = producto.UnidadMedida;
                    newItem.Costo = producto.CostoActual;
                    newItem.CostoIva = producto.CostoIva;
                    CalcularMontoItem(newItem);
                    Documento.DocumentosProductos.Add(newItem);

                }

            }
            Documento.CalcularTotales();
            Documento.Saldo = Documento.MontoTotal;

        }
        private void CalcularMontoItem(DocumentosProductos DocumentoProducto)
        {
            DocumentoProducto.DescuentoBs = DocumentoProducto.DescuentoPorcentaje * DocumentoProducto.Precio / 100;
            DocumentoProducto.Iva = cBasicas.Round(DocumentoProducto.Precio * DocumentoProducto.TasaIva / 100);
            DocumentoProducto.PrecioIva = cBasicas.Round(DocumentoProducto.Precio + DocumentoProducto.Iva);
            DocumentoProducto.MontoNeto = cBasicas.Round(DocumentoProducto.Precio - DocumentoProducto.DescuentoBs);
            DocumentoProducto.Iva = cBasicas.Round(DocumentoProducto.MontoNeto * DocumentoProducto.TasaIva / 100);
            DocumentoProducto.Total = (DocumentoProducto.MontoNeto + DocumentoProducto.Iva) * DocumentoProducto.Cantidad;
        }
        private bool DividirFactura(Documentos pedido)
        {
            while (true)
            {
                using (var dc = new SoroEntities())
                {
                    try
                    {
                        Documentos NuevaFactura = new Documentos();
                        Titular = FactoryTerceros.Item(pedido.IdTercero);
                        NuevaFactura.IdTercero = pedido.IdTercero;
                        NuevaFactura.Activo = true;
                        NuevaFactura.Fecha = Documento.Fecha;
                        NuevaFactura.Tipo = "FACTURA";
                        NuevaFactura.TasaIVA = Documento.TasaIVA;
                        NuevaFactura.IdTercero = Documento.IdTercero;
                        NuevaFactura.IdVendedor = Documento.IdVendedor;
                        NuevaFactura.Mes = Documento.Mes;
                        NuevaFactura.Momento = Documento.Momento;
                        NuevaFactura.Status = "ABIERTA";
                        NuevaFactura.Vence = Documento.Vence;
                        var NuevaItems = Documento.DocumentosProductos.Take((int)FactoryParametros.Item().TopeItemsFactura);
                        if (NuevaItems.Count() == 0)
                        {
                            return true;
                        }
                        foreach(var n in NuevaItems)
                        {
                            NuevaFactura.DocumentosProductos.Add(n);
                        }
                        foreach (DocumentosProductos Item in NuevaFactura.DocumentosProductos)
                        {
                            Item.Activo = true;
                        }
                        NuevaFactura.CalcularTotales();                        
                        NuevaFactura.MontoTotal = (double)decimal.Round((decimal)NuevaFactura.MontoTotal, 2);
                        NuevaFactura.Saldo = NuevaFactura.MontoTotal;
                        ResumenCuentas Resumen = FactoryCuentas.CuentasTercero(Titular.IdTercero);
                        NuevaFactura.DeudaAnterior = Convert.ToDouble(Resumen.TotalPendiente);
                        NuevaFactura.PesoFactura = NuevaFactura.DocumentosProductos.Sum(x => x.PesoTotal);                      
                        FactoryDocumentos.Save(dc, NuevaFactura, null, Titular);
                        cBasicas.ImprimirFactura(NuevaFactura);
                     //   MessageBox.Show("Retire esta factura y prepare la impresora para imprimir la proxima", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    catch (Exception x)
                    {
                        MessageBox.Show(x.Message, "Error al guardar factura", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
        }
        private bool DividirNotaEntrega(Documentos pedido)
        {
                using (var dc = new SoroEntities())
                {
                    try
                    {
                        Documentos NuevaNotaEntrega = new Documentos();
                        Titular = FactoryTerceros.Item(pedido.IdTercero);
                        NuevaNotaEntrega.IdTercero = pedido.IdTercero;
                        NuevaNotaEntrega.Activo = true;
                        NuevaNotaEntrega.Fecha = Documento.Fecha;
                        NuevaNotaEntrega.Tipo = "NOTA ENTREGA";
                        NuevaNotaEntrega.TasaIVA = Documento.TasaIVA;
                        NuevaNotaEntrega.IdTercero = Documento.IdTercero;
                        NuevaNotaEntrega.IdVendedor = Documento.IdVendedor;
                        NuevaNotaEntrega.Mes = Documento.Mes;
                        NuevaNotaEntrega.Momento = Documento.Momento;
                        NuevaNotaEntrega.Status = "ABIERTA";
                        NuevaNotaEntrega.Vence = Documento.Vence;
                        NuevaNotaEntrega.Condiciones = Documento.Condiciones;
                        var NuevaItems = Documento.DocumentosProductos;
                    foreach (var n in NuevaItems)
                    {
                        NuevaNotaEntrega.DocumentosProductos.Add(n);
                    }
                       
                        foreach (DocumentosProductos Item in NuevaNotaEntrega.DocumentosProductos)
                        {
                            Item.Activo = true;
                        }
                        NuevaNotaEntrega.CalcularTotales();
                        NuevaNotaEntrega.MontoTotal = (double)decimal.Round((decimal)NuevaNotaEntrega.MontoTotal, 2);
                        NuevaNotaEntrega.Saldo = NuevaNotaEntrega.MontoTotal;
                        ResumenCuentas Resumen = FactoryCuentas.CuentasTercero(Titular.IdTercero);
                        NuevaNotaEntrega.DeudaAnterior = Convert.ToDouble(Resumen.TotalPendiente);
                        NuevaNotaEntrega.PesoFactura = NuevaNotaEntrega.DocumentosProductos.Sum(x => x.PesoTotal);
                        FactoryDocumentos.Save(dc, NuevaNotaEntrega, null, Titular);
                        cBasicas.ImprimirNotaEntrega(NuevaNotaEntrega);
                        //   MessageBox.Show("Retire esta factura y prepare la impresora para imprimir la proxima", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    catch (Exception x)
                    {
                        MessageBox.Show(x.Message, "Error al guardar factura", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                return true;
        }

        private void Modificar_Click(object sender, EventArgs e)
        {
            ModificarPedido();
        }

        private void ModificarPedido()
        {
            VistaDocumento Registro = (VistaDocumento)this.bs.Current;
            if (Registro == null)
            {
                return;
            }
            FrmPedidosItem f = new FrmPedidosItem();
            f.Documento = FactoryDocumentos.Item(dc, Registro.IdDocumento);
            f.Editar();
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                Busqueda();
            }
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            VistaDocumento Registro = (VistaDocumento)this.bs.Current;
            if (Registro != null)
            {
                if (MessageBox.Show(string.Format("Esta seguro de eliminar este pedido del cliente {0}", Registro.RazonSocial), "Atencion", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.OK)
                {
                    return;
                }
                FactoryDocumentos.Delete(Registro.IdDocumento);
                Busqueda();

            }
        }

        private void Nuevo_Click(object sender, EventArgs e)
        {
            FrmPedidosItem f = new FrmPedidosItem();
            f.Incluir();
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                Busqueda();
            }
        }

        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                ModificarPedido();
            }
        }

        private void Imprimir_Click(object sender, EventArgs e)
        {
            this.gridControl1.ShowPrintPreview();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            foreach (int id in this.gridView1.GetSelectedRows())
            {
                try
                {
                    VistaDocumento Registro = (VistaDocumento)this.bs.List[id];
                    Documentos pedido = FactoryDocumentos.Item(dc, Registro.IdDocumento);
                    CargarDocumento(pedido);
                    DividirNotaEntrega(pedido);
                    using (var newdc = new SoroEntities())
                    {
                        pedido = FactoryDocumentos.Item(newdc, pedido.IdDocumento);
                        {
                            pedido.Status = "FACTURADO";
                            newdc.SaveChanges();
                        }
                    }
                    if (pedido.Saldo.GetValueOrDefault(0) != pedido.MontoTotal)
                    {
                        Recibo.IdDocumento = Documento.IdDocumento;
                        Recibo = new RegistroPagos();
                        Recibo.Cambio = pedido.Cambio;
                        Recibo.Cheque = pedido.Cheque;
                        Recibo.Deposito = pedido.Deposito;
                        Recibo.Efectivo = pedido.Efectivo;
                        Recibo.Fecha = pedido.Fecha;
                        Recibo.IdTercero = pedido.IdTercero;
                        Recibo.Modulo = "PEDIDOS";
                        Recibo.Momento = DateTime.Now;
                        Recibo.Documento = pedido.Numero;
                        Recibo.MontoPagar = pedido.MontoTotal;
                        Recibo.SaldoPendiente = pedido.Saldo;
                        Recibo.TCredito = pedido.TarjetaCredito;
                        Recibo.TDebito = pedido.TarjetaDebito;
                        Recibo.Tipo = "FACTURA";
                        Recibo.IdRegistroPago = FactoryContadores.GetLast("IdRegistroPago");
                        Terceros Tercero = FactoryTerceros.Item(pedido.IdTercero);
                        Recibo.RazonSocial = Tercero.RazonSocial;
                        Recibo.MontoPagado = Convert.ToInt16(Recibo.Cheque) +
                                             Convert.ToInt16(Recibo.Deposito) +
                                             Convert.ToInt16(Recibo.Efectivo) +
                                             Convert.ToInt16(Recibo.TCredito) +
                                             Convert.ToInt16(Recibo.TDebito) -
                                             Convert.ToInt16(Recibo.Cambio);
                        Recibo.SaldoPendiente = Recibo.MontoPagar - Recibo.MontoPagado;
                        dc.RegistroPagos.Add(Recibo);
                        dc.SaveChanges();

                    }
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message, "Atencion Error al guardar pedido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();

        }
    }
}
