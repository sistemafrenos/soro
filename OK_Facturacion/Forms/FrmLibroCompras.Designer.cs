namespace HK
{
    partial class FrmLibroCompras
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.BarraAcciones = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtAño = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.txtMes = new System.Windows.Forms.ToolStripComboBox();
            this.Buscar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Nuevo = new System.Windows.Forms.ToolStripButton();
            this.Editar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.Imprimir = new System.Windows.Forms.ToolStripButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.libroComprasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colFecha = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNumero = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colControl = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRazonSocial = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCedulaRif = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalIncluyendoIva = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colComprasSinCreditoIVA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colComprasNoSujetas = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBaseImponible = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTasaIva = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colImpuestoIVA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridViewGrupos = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.BarraAcciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.libroComprasBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewGrupos)).BeginInit();
            this.SuspendLayout();
            // 
            // BarraAcciones
            // 
            this.BarraAcciones.AutoSize = false;
            this.BarraAcciones.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.BarraAcciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.txtAño,
            this.toolStripLabel2,
            this.txtMes,
            this.Buscar,
            this.toolStripSeparator1,
            this.Nuevo,
            this.Editar,
            this.btnEliminar,
            this.toolStripSeparator2,
            this.Imprimir});
            this.BarraAcciones.Location = new System.Drawing.Point(0, 0);
            this.BarraAcciones.Name = "BarraAcciones";
            this.BarraAcciones.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.BarraAcciones.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.BarraAcciones.Size = new System.Drawing.Size(784, 53);
            this.BarraAcciones.TabIndex = 3;
            this.BarraAcciones.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(29, 50);
            this.toolStripLabel1.Text = "Año";
            // 
            // txtAño
            // 
            this.txtAño.Name = "txtAño";
            this.txtAño.Size = new System.Drawing.Size(75, 53);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(29, 50);
            this.toolStripLabel2.Text = "Mes";
            // 
            // txtMes
            // 
            this.txtMes.Name = "txtMes";
            this.txtMes.Size = new System.Drawing.Size(75, 53);
            this.txtMes.Validating += new System.ComponentModel.CancelEventHandler(this.txtMes_Validating);
            // 
            // Buscar
            // 
            this.Buscar.Image = global::HK.Properties.Resources.note_find;
            this.Buscar.Name = "Buscar";
            this.Buscar.Size = new System.Drawing.Size(46, 50);
            this.Buscar.Text = "Buscar";
            this.Buscar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Buscar.Click += new System.EventHandler(this.Buscar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 53);
            // 
            // Nuevo
            // 
            this.Nuevo.Image = global::HK.Properties.Resources.note_new;
            this.Nuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Nuevo.Name = "Nuevo";
            this.Nuevo.Size = new System.Drawing.Size(46, 50);
            this.Nuevo.Text = "Nuevo";
            this.Nuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Nuevo.Click += new System.EventHandler(this.Nuevo_Click);
            // 
            // Editar
            // 
            this.Editar.Image = global::HK.Properties.Resources.note_edit;
            this.Editar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Editar.Name = "Editar";
            this.Editar.Size = new System.Drawing.Size(41, 50);
            this.Editar.Text = "Editar";
            this.Editar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Editar.Click += new System.EventHandler(this.Editar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Image = global::HK.Properties.Resources.note_delete;
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(54, 50);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 53);
            // 
            // Imprimir
            // 
            this.Imprimir.Image = global::HK.Properties.Resources.printer;
            this.Imprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Imprimir.Name = "Imprimir";
            this.Imprimir.Size = new System.Drawing.Size(57, 50);
            this.Imprimir.Text = "Imprimir";
            this.Imprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Imprimir.Click += new System.EventHandler(this.Imprimir_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.gridControl1);
            this.groupControl1.Location = new System.Drawing.Point(0, 56);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(784, 505);
            this.groupControl1.TabIndex = 4;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.libroComprasBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 22);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.ShowOnlyPredefinedDetails = true;
            this.gridControl1.Size = new System.Drawing.Size(780, 481);
            this.gridControl1.TabIndex = 5;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1,
            this.gridViewGrupos});
            this.gridControl1.Click += new System.EventHandler(this.gridControl1_Click);
            this.gridControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridControl1_KeyDown);
            // 
            // libroComprasBindingSource
            // 
            this.libroComprasBindingSource.DataSource = typeof(HK.LibroCompras);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 10F);
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridView1.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.AppearancePrint.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridView1.ColumnPanelRowHeight = 80;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colFecha,
            this.colNumero,
            this.colControl,
            this.colRazonSocial,
            this.colCedulaRif,
            this.colTotalIncluyendoIva,
            this.colComprasSinCreditoIVA,
            this.colComprasNoSujetas,
            this.colBaseImponible,
            this.colTasaIva,
            this.colImpuestoIVA,
            this.gridColumn7,
            this.gridColumn8});
            this.gridView1.DetailHeight = 400;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.VertScrollTipFieldName = "Numero";
            // 
            // colFecha
            // 
            this.colFecha.Caption = "Fecha";
            this.colFecha.DisplayFormat.FormatString = "d";
            this.colFecha.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colFecha.FieldName = "Fecha";
            this.colFecha.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colFecha.Name = "colFecha";
            this.colFecha.OptionsColumn.FixedWidth = true;
            this.colFecha.Visible = true;
            this.colFecha.VisibleIndex = 0;
            // 
            // colNumero
            // 
            this.colNumero.Caption = "Numero";
            this.colNumero.FieldName = "Numero";
            this.colNumero.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colNumero.Name = "colNumero";
            this.colNumero.OptionsColumn.FixedWidth = true;
            this.colNumero.Visible = true;
            this.colNumero.VisibleIndex = 1;
            // 
            // colControl
            // 
            this.colControl.Caption = "Nro Control";
            this.colControl.FieldName = "Control";
            this.colControl.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colControl.Name = "colControl";
            this.colControl.OptionsColumn.FixedWidth = true;
            this.colControl.Visible = true;
            this.colControl.VisibleIndex = 2;
            // 
            // colRazonSocial
            // 
            this.colRazonSocial.Caption = "Razon Social";
            this.colRazonSocial.FieldName = "RazonSocial";
            this.colRazonSocial.MinWidth = 100;
            this.colRazonSocial.Name = "colRazonSocial";
            this.colRazonSocial.OptionsColumn.FixedWidth = true;
            this.colRazonSocial.Visible = true;
            this.colRazonSocial.VisibleIndex = 5;
            this.colRazonSocial.Width = 168;
            // 
            // colCedulaRif
            // 
            this.colCedulaRif.Caption = "CedulaRif";
            this.colCedulaRif.FieldName = "CedulaRif";
            this.colCedulaRif.Name = "colCedulaRif";
            this.colCedulaRif.OptionsColumn.FixedWidth = true;
            this.colCedulaRif.Visible = true;
            this.colCedulaRif.VisibleIndex = 6;
            // 
            // colTotalIncluyendoIva
            // 
            this.colTotalIncluyendoIva.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalIncluyendoIva.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalIncluyendoIva.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10F);
            this.colTotalIncluyendoIva.AppearanceHeader.Options.UseFont = true;
            this.colTotalIncluyendoIva.Caption = "Total Incl. Iva";
            this.colTotalIncluyendoIva.DisplayFormat.FormatString = "N2";
            this.colTotalIncluyendoIva.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalIncluyendoIva.FieldName = "TotalIncluyendoIva";
            this.colTotalIncluyendoIva.Name = "colTotalIncluyendoIva";
            this.colTotalIncluyendoIva.OptionsColumn.FixedWidth = true;
            this.colTotalIncluyendoIva.SummaryItem.DisplayFormat = "{0:n2}";
            this.colTotalIncluyendoIva.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colTotalIncluyendoIva.Visible = true;
            this.colTotalIncluyendoIva.VisibleIndex = 7;
            this.colTotalIncluyendoIva.Width = 80;
            // 
            // colComprasSinCreditoIVA
            // 
            this.colComprasSinCreditoIVA.AppearanceCell.Options.UseTextOptions = true;
            this.colComprasSinCreditoIVA.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colComprasSinCreditoIVA.Caption = "Compras Sin Credito IVA";
            this.colComprasSinCreditoIVA.DisplayFormat.FormatString = "N2";
            this.colComprasSinCreditoIVA.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colComprasSinCreditoIVA.FieldName = "ComprasSinCreditoIVA";
            this.colComprasSinCreditoIVA.Name = "colComprasSinCreditoIVA";
            this.colComprasSinCreditoIVA.OptionsColumn.FixedWidth = true;
            this.colComprasSinCreditoIVA.SummaryItem.DisplayFormat = "{0:n2}";
            this.colComprasSinCreditoIVA.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colComprasSinCreditoIVA.Visible = true;
            this.colComprasSinCreditoIVA.VisibleIndex = 8;
            this.colComprasSinCreditoIVA.Width = 80;
            // 
            // colComprasNoSujetas
            // 
            this.colComprasNoSujetas.AppearanceCell.Options.UseTextOptions = true;
            this.colComprasNoSujetas.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colComprasNoSujetas.Caption = "Compras No Sujetas";
            this.colComprasNoSujetas.DisplayFormat.FormatString = "N2";
            this.colComprasNoSujetas.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colComprasNoSujetas.FieldName = "ComprasNoSujetas";
            this.colComprasNoSujetas.Name = "colComprasNoSujetas";
            this.colComprasNoSujetas.OptionsColumn.FixedWidth = true;
            this.colComprasNoSujetas.SummaryItem.DisplayFormat = "{0:n2}";
            this.colComprasNoSujetas.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colComprasNoSujetas.Visible = true;
            this.colComprasNoSujetas.VisibleIndex = 12;
            this.colComprasNoSujetas.Width = 80;
            // 
            // colBaseImponible
            // 
            this.colBaseImponible.AppearanceCell.Options.UseTextOptions = true;
            this.colBaseImponible.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colBaseImponible.Caption = "Base Imp.";
            this.colBaseImponible.DisplayFormat.FormatString = "N2";
            this.colBaseImponible.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBaseImponible.FieldName = "BaseImponible";
            this.colBaseImponible.Name = "colBaseImponible";
            this.colBaseImponible.OptionsColumn.FixedWidth = true;
            this.colBaseImponible.SummaryItem.DisplayFormat = "{0:n2}";
            this.colBaseImponible.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colBaseImponible.Visible = true;
            this.colBaseImponible.VisibleIndex = 11;
            this.colBaseImponible.Width = 80;
            // 
            // colTasaIva
            // 
            this.colTasaIva.AppearanceCell.Options.UseTextOptions = true;
            this.colTasaIva.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTasaIva.Caption = "Tasa Iva";
            this.colTasaIva.DisplayFormat.FormatString = "N2";
            this.colTasaIva.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTasaIva.FieldName = "TasaIva";
            this.colTasaIva.MinWidth = 55;
            this.colTasaIva.Name = "colTasaIva";
            this.colTasaIva.OptionsColumn.FixedWidth = true;
            this.colTasaIva.Visible = true;
            this.colTasaIva.VisibleIndex = 10;
            this.colTasaIva.Width = 45;
            // 
            // colImpuestoIVA
            // 
            this.colImpuestoIVA.AppearanceCell.Options.UseTextOptions = true;
            this.colImpuestoIVA.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colImpuestoIVA.Caption = "ImpuestoIVA";
            this.colImpuestoIVA.DisplayFormat.FormatString = "N2";
            this.colImpuestoIVA.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colImpuestoIVA.FieldName = "ImpuestoIVA";
            this.colImpuestoIVA.Name = "colImpuestoIVA";
            this.colImpuestoIVA.OptionsColumn.FixedWidth = true;
            this.colImpuestoIVA.SummaryItem.DisplayFormat = "{0:n2}";
            this.colImpuestoIVA.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colImpuestoIVA.Visible = true;
            this.colImpuestoIVA.VisibleIndex = 9;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Mes";
            this.gridColumn7.FieldName = "Mes";
            this.gridColumn7.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.FixedWidth = true;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 3;
            this.gridColumn7.Width = 55;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Año";
            this.gridColumn8.FieldName = "Año";
            this.gridColumn8.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.FixedWidth = true;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 4;
            this.gridColumn8.Width = 55;
            // 
            // gridViewGrupos
            // 
            this.gridViewGrupos.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6});
            this.gridViewGrupos.GridControl = this.gridControl1;
            this.gridViewGrupos.Name = "gridViewGrupos";
            this.gridViewGrupos.OptionsBehavior.Editable = false;
            this.gridViewGrupos.OptionsView.ShowAutoFilterRow = true;
            this.gridViewGrupos.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Fecha";
            this.gridColumn1.FieldName = "Fecha";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.FixedWidth = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 80;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Numero";
            this.gridColumn2.FieldName = "Numero";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.FixedWidth = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 98;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "CedulaRif";
            this.gridColumn3.FieldName = "CedulaRif";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.FixedWidth = true;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 98;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "RazonSocial";
            this.gridColumn4.FieldName = "RazonSocial";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 220;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "MontoTotal";
            this.gridColumn5.DisplayFormat.FormatString = "N2";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn5.FieldName = "MontoTotal";
            this.gridColumn5.GroupFormat.FormatString = "N2";
            this.gridColumn5.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.FixedWidth = true;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 107;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Comentarios";
            this.gridColumn6.FieldName = "Comentarios";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            this.gridColumn6.Width = 170;
            // 
            // FrmLibroCompras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.BarraAcciones);
            this.Name = "FrmLibroCompras";
            this.Text = "Libro Compras";
            this.Load += new System.EventHandler(this.FrmLibroCompras_Load);
            this.BarraAcciones.ResumeLayout(false);
            this.BarraAcciones.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.libroComprasBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewGrupos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ToolStrip BarraAcciones;
        private System.Windows.Forms.ToolStripButton Imprimir;
        private System.Windows.Forms.ToolStripButton Buscar;
        private System.Windows.Forms.ToolStripComboBox txtAño;
        private System.Windows.Forms.ToolStripComboBox txtMes;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewGrupos;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private System.Windows.Forms.BindingSource libroComprasBindingSource;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private DevExpress.XtraGrid.Columns.GridColumn colFecha;
        private DevExpress.XtraGrid.Columns.GridColumn colNumero;
        private DevExpress.XtraGrid.Columns.GridColumn colControl;
        private DevExpress.XtraGrid.Columns.GridColumn colRazonSocial;
        private DevExpress.XtraGrid.Columns.GridColumn colCedulaRif;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalIncluyendoIva;
        private DevExpress.XtraGrid.Columns.GridColumn colComprasSinCreditoIVA;
        private DevExpress.XtraGrid.Columns.GridColumn colComprasNoSujetas;
        private DevExpress.XtraGrid.Columns.GridColumn colBaseImponible;
        private DevExpress.XtraGrid.Columns.GridColumn colTasaIva;
        private DevExpress.XtraGrid.Columns.GridColumn colImpuestoIVA;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton Nuevo;
        private System.Windows.Forms.ToolStripButton Editar;
        private System.Windows.Forms.ToolStripButton btnEliminar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}