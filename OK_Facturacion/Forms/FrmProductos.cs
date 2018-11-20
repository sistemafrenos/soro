using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Data.Linq;
using HK.Formas;

namespace HK
{
    public partial class FrmProductos : Form
    {

        public FrmProductos()
        {
            InitializeComponent();
        }
        
        SoroEntities dc = new SoroEntities();
        List<VistaProductos> Lista = new List<VistaProductos>();
        private void Frm_Load(object sender, EventArgs e)
        {
            Busqueda();
            #region Custom
            switch (FactoryParametros.Item().Empresa)
            {                   
                case "HK SOLUCIONES,C.A.":
                    this.colUnidadMedida.Visible = true;
                    this.colPeso.Visible = true;
                    break;
                case "FARMACIA CHUPARIN,C.A.":
                    this.colUnidadMedida.Visible = false;
                    this.colPeso.Visible = false;
                    this.colMarca.Visible = false;
                    this.colModelo.Visible = false;
                    break;
                case "COMERCIAL PADRE E HIJO,C.A.":
                    {
                        this.colUnidadMedida.Visible = true;
                        this.colPeso.Visible = true;
                        break;
                    }
                case "FARMASHOP 2000,c.a.":
                    {
                        this.colUnidadMedida.Visible = false;
                        this.colPeso.Visible = false;
                        break;
                    }
                case "REPUESTOS TALLERES LATINOS,C.A.":
                    {
                        this.colUnidadMedida.Visible = false;
                        this.colPeso.Visible = false;
                        this.colMarca.Visible = true;
                        this.colModelo.Visible = true;
                        break;
                    }
                case "REPUESTOS SAN MIGUEL,C.A.":
                        this.colUnidadMedida.Visible = false;
                        this.colPeso.Visible = false;
                        this.colMarca.Visible = true;
                        this.colModelo.Visible = true;
                        break;
                case "MERCANTIL EL ROSARIO,C.A.":
                    {
                        this.colUnidadMedida.Visible = false;
                        this.colPeso.Visible = false;
                        break;
                    }
            }
            #endregion
        }

        private void Buscar_Click(object sender, EventArgs e)
        {
            Busqueda();
        }

        private void Busqueda()
        {
            dc = new SoroEntities();
            Lista = FactoryProductos.Buscar(dc,this.txtBuscar.Text);
            this.bs.DataSource = Lista;
        }
        private void GridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (gridView1.ActiveEditor == null)
            {
                if (e.KeyCode == Keys.Return)
                {
                    EditarRegistro();
                }
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Subtract)
                {
                    EliminarRegistro();
                }
                if (e.KeyCode == Keys.Insert)
                {
                    AgregarRegistro();
                }
            }

        }

        private void Editar_Click(object sender, EventArgs e)
        {
            EditarRegistro();
           // Busqueda();

        }
        private void AgregarRegistro()
        {
            switch (FactoryParametros.Item().Empresa)
            {
                case "COMERCIAL PADRE E HIJO,C.A.":
                    FrmProductosItem F = new FrmProductosItem();
                    F.ShowDialog();
                    if (F.DialogResult == DialogResult.OK)
                    {
                        this.bs.Add(FactoryProductos.ItemVista(dc, F.Producto.IdProducto));
                    }
                    break;
                default:
                    FrmProductosItemSimple F2 = new FrmProductosItemSimple();
                    F2.ShowDialog();
                    if (F2.DialogResult == DialogResult.OK)
                    {
                        this.bs.Add(FactoryProductos.ItemVista(dc, F2.Producto.IdProducto));
                    }
                    break;
            }
        }
        private void DuplicarRegistro()
        {
            Productos Registro = FactoryProductos.Item(((VistaProductos)this.bs.Current).IdProducto);
            if (Registro == null)
                return;
            switch (FactoryParametros.Item().Empresa)
            {
                case "COMERCIAL PADRE E HIJO,C.A.":
                    FrmProductosItem F = new FrmProductosItem();
                  //  F.Copy(Registro);
                    if (F.DialogResult == DialogResult.OK)
                    {
                        this.bs.Add(FactoryProductos.ItemVista(dc, F.Producto.IdProducto));
                    }
                    break;
                default:
                    FrmProductosItemSimple F2 = new FrmProductosItemSimple
                    {
                        OldProducto = Registro
                    };
                    F2.ShowDialog();
                    if (F2.DialogResult == DialogResult.OK)
                    {
                        this.bs.Add(FactoryProductos.ItemVista(dc, F2.Producto.IdProducto));
                    }
                    break;
            }

        }
        private void EditarRegistro()
        {
            switch (FactoryParametros.Item().Empresa)
            {
                case "COMERCIAL PADRE E HIJO,C.A.":
                    FrmProductosItem F = new FrmProductosItem();
                    VistaProductos mRegistro = (VistaProductos)this.bs.Current;
                    if (mRegistro == null)
                        return;
                    F.Producto = FactoryProductos.Item(mRegistro.IdProducto);
                    F.ShowDialog();
                    if (F.DialogResult == DialogResult.OK)
                    {
                        this.bs.Add(FactoryProductos.ItemVista(dc, F.Producto.IdProducto));
                    }
                    break;
                default:
                    FrmProductosItemSimple F2 = new FrmProductosItemSimple();
                    VistaProductos mRegistro2 = (VistaProductos)this.bs.Current;
                    if (mRegistro2 == null)
                        return;
                    F2.Producto = FactoryProductos.Item(mRegistro2.IdProducto);
                    F2.ShowDialog();
                    if (F2.DialogResult == DialogResult.OK)
                    {
                        this.bs.Add(FactoryProductos.ItemVista(dc, F2.Producto.IdProducto));
                    }
                    break;
            }
        }
        private void EliminarRegistro()
        {
            if (this.gridView1.IsFocusedView)
            {
                VistaProductos Vista = (VistaProductos)this.bs.Current;
                if (Vista == null)
                    return;
                Productos Registro = FactoryProductos.Item(Vista.IdProducto);
                try
                {
                    dc.Productos.Remove(Registro);
                    dc.SaveChanges();
                }
                catch(Exception x)
                {
                    MessageBox.Show(x.Message);
                    Registro.Activo = false;
                    dc.SaveChanges();
                }
                this.bs.Remove(Registro);
            }
            Busqueda();
        }

        private void Nuevo_Click(object sender, EventArgs e)
        {
            AgregarRegistro();
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }

        private void TxtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Busqueda();
            }
        }

        private void BtnDuplicar_Click(object sender, EventArgs e)
        {
            DuplicarRegistro();
        }

        private void ToolStripHistorial_Click(object sender, EventArgs e)
        {
            VistaProductos Vista = (VistaProductos)this.bs.Current;
            if (Vista == null)
                return;
            Productos Registro = FactoryProductos.Item(Vista.IdProducto);
            FrmHistorial f = new FrmHistorial
            {
                producto = Registro
            };
            f.ShowDialog();
        }

        private void ToolBarras_Click(object sender, EventArgs e)
        {
            Documentos Doc = new Documentos
            {
                Fecha = DateTime.Today
            };

            foreach (int id in this.gridView1.GetSelectedRows())
            {
                VistaProductos Producto = (VistaProductos)this.bs.List[id];
                DocumentosProductos Prod = new DocumentosProductos
                {
                    Codigo = Producto.Codigo,
                    Cantidad = 1,
                    Precio = Producto.PrecioIVA,
                    Descripcion = Producto.Descripcion
                };
                Doc.DocumentosProductos.Add(Prod);
            }
            ImprimirCodigoBarras F = new ImprimirCodigoBarras
            {
                Documento = Doc
            };
            F.ShowDialog();
        }

        private void BarraAcciones_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Imprimir_Click(object sender, EventArgs e)
        {
            this.colUnidadMedida.Visible = false;
            this.colUbicacion.Visible = false;
            this.colCodigo.Visible = false;
            this.colMarca.Visible = false;
            this.colModelo.Visible = false;
            this.colPeso.Visible = false;
            
            this.gridControl1.ShowPrintPreview();
            this.colUnidadMedida.Visible = true;
            this.colUbicacion.Visible = true;
            this.colCodigo.Visible = true;
            this.colPeso.Visible = true;
            this.colModelo.Visible = true;
        }

    }
}