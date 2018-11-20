using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace HK
{
    public partial class CodigosBarra : DevExpress.XtraReports.UI.XtraReport
    {
        public bool MostrarCodigo=true;
        public bool MostrarEmpresa = true;
        public bool MostrarPrecio = true;
        public bool MostrarDescripcion = true;
        public bool MostrarFecha = true;
        private DetailBand detailBand1;
        private System.Windows.Forms.BindingSource bindingSource2;
        private XRPanel xrPanel1;
        public XRLabel txtFecha;
        private XRLabel txtDescripcion;
        public XRLabel txtEmpresa;
        private XRBarCode txtCodigoBarras;
        private XRLabel txtPrecio;
        private IContainer components;
    
        public CodigosBarra()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraPrinting.BarCode.Code39ExtendedGenerator code39ExtendedGenerator1 = new DevExpress.XtraPrinting.BarCode.Code39ExtendedGenerator();
            this.detailBand1 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
            this.txtPrecio = new DevExpress.XtraReports.UI.XRLabel();
            this.bindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.txtFecha = new DevExpress.XtraReports.UI.XRLabel();
            this.txtDescripcion = new DevExpress.XtraReports.UI.XRLabel();
            this.txtEmpresa = new DevExpress.XtraReports.UI.XRLabel();
            this.txtCodigoBarras = new DevExpress.XtraReports.UI.XRBarCode();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detailBand1
            // 
            this.detailBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1});
            this.detailBand1.MultiColumn.ColumnSpacing = 13F;
            this.detailBand1.MultiColumn.ColumnWidth = 262F;
            this.detailBand1.MultiColumn.Layout = DevExpress.XtraPrinting.ColumnLayout.AcrossThenDown;
            this.detailBand1.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnWidth;
            this.detailBand1.Name = "detailBand1";
            // this.detailBand1.RepeatCountOnEmptyDataSource = 30;
            this.detailBand1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrBarCode1_BeforePrint);
            // 
            // xrPanel1
            // 
            this.xrPanel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPanel1.CanGrow = false;
            this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.txtPrecio,
            this.txtFecha,
            this.txtDescripcion,
            this.txtEmpresa,
            this.txtCodigoBarras});
            this.xrPanel1.Location = new System.Drawing.Point(0, 0);
            this.xrPanel1.Name = "xrPanel1";
            this.xrPanel1.ParentStyleUsing.UseBorders = false;
            this.xrPanel1.Size = new System.Drawing.Size(262, 100);
            // 
            // txtPrecio
            // 
            this.txtPrecio.BorderWidth = 0;
            this.txtPrecio.CanShrink = true;
            this.txtPrecio.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", this.bindingSource2, "Precio", "{0:n2}")});
            this.txtPrecio.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecio.Location = new System.Drawing.Point(162, 42);
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtPrecio.ParentStyleUsing.UseBorderWidth = false;
            this.txtPrecio.ParentStyleUsing.UseFont = false;
            this.txtPrecio.Size = new System.Drawing.Size(100, 16);
            this.txtPrecio.Text = "txtPrecio";
            this.txtPrecio.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // bindingSource2
            // 
            this.bindingSource2.DataSource = typeof(HK.DocumentosProductos);
            // 
            // txtFecha
            // 
            this.txtFecha.BorderWidth = 0;
            this.txtFecha.CanShrink = true;
            this.txtFecha.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFecha.Location = new System.Drawing.Point(0, 42);
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtFecha.ParentStyleUsing.UseBorderWidth = false;
            this.txtFecha.ParentStyleUsing.UseFont = false;
            this.txtFecha.Size = new System.Drawing.Size(100, 17);
            this.txtFecha.Text = "Fecha";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.BorderWidth = 0;
            this.txtDescripcion.CanGrow = false;
            this.txtDescripcion.CanShrink = true;
            this.txtDescripcion.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", this.bindingSource2, "Descripcion", "")});
            this.txtDescripcion.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcion.Location = new System.Drawing.Point(0, 17);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtDescripcion.ParentStyleUsing.UseBorderWidth = false;
            this.txtDescripcion.ParentStyleUsing.UseFont = false;
            this.txtDescripcion.Size = new System.Drawing.Size(262, 25);
            this.txtDescripcion.Text = "txtDescripcion";
            // 
            // txtEmpresa
            // 
            this.txtEmpresa.BorderWidth = 0;
            this.txtEmpresa.CanShrink = true;
            this.txtEmpresa.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpresa.Location = new System.Drawing.Point(0, 0);
            this.txtEmpresa.Name = "txtEmpresa";
            this.txtEmpresa.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtEmpresa.ParentStyleUsing.UseBorderWidth = false;
            this.txtEmpresa.ParentStyleUsing.UseFont = false;
            this.txtEmpresa.Size = new System.Drawing.Size(262, 17);
            this.txtEmpresa.Text = "Empresa";
            // 
            // txtCodigoBarras
            // 
            this.txtCodigoBarras.AutoModule = true;
            this.txtCodigoBarras.BorderWidth = 0;
            this.txtCodigoBarras.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", this.bindingSource2, "Codigo", "")});
            this.txtCodigoBarras.Location = new System.Drawing.Point(0, 64);
            this.txtCodigoBarras.Name = "txtCodigoBarras";
            this.txtCodigoBarras.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 0, 0, 100F);
            this.txtCodigoBarras.ParentStyleUsing.UseBorderWidth = false;
            this.txtCodigoBarras.Size = new System.Drawing.Size(262, 34);
            code39ExtendedGenerator1.WideNarrowRatio = 3F;
            this.txtCodigoBarras.Symbology = code39ExtendedGenerator1;
            this.txtCodigoBarras.Text = "txtCodigo";
            // 
            // CodigosBarra
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.detailBand1});
            this.Margins = new System.Drawing.Printing.Margins(19, 19, 50, 0);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        private void xrBarCode1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if( string.IsNullOrEmpty( (string)GetCurrentColumnValue("Codigo") ))
            {
                OcultarEtiqueta();
            }
            else
            {
                MostrarEtiqueta();
            }
        }
        private void MostrarEtiqueta()
        {
            this.txtEmpresa.Visible = MostrarEmpresa;
            this.txtFecha.Visible = MostrarFecha;
            this.txtCodigoBarras.Visible = MostrarCodigo;
            this.txtDescripcion.Visible = MostrarDescripcion;
            this.txtPrecio.Visible = MostrarPrecio;
        }
        private void OcultarEtiqueta()
        {
            this.txtEmpresa.Visible = false;
            this.txtFecha.Visible = false;
            this.txtCodigoBarras.Visible = false;
            this.txtDescripcion.Visible = false;
            this.txtPrecio.Visible = false;
        }

    }
}
