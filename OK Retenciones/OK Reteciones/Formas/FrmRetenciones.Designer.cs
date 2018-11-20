namespace HK.Formas
{
    partial class FrmRetenciones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRetenciones));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bs = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNumeroDeOperacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNumeroComprobante = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCedulaRif = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNombreRazonSocial = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPeriodoImpositivo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFechaDocumento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTipoOperacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTipoDocumento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNumeroDocumento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNumeroControlDocumento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMontoDocumento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBaseImponible = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMontoIvaRetenido = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMontoExentoIva = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAlicuota = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.BarraAcciones = new System.Windows.Forms.ToolStrip();
            this.txtMes = new System.Windows.Forms.ToolStripComboBox();
            this.txtAño = new System.Windows.Forms.ToolStripComboBox();
            this.txtPeriodo = new System.Windows.Forms.ToolStripComboBox();
            this.btnBuscar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnEditar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.btnImprimirDoble = new System.Windows.Forms.ToolStripButton();
            this.toolMontoCH = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExcel = new System.Windows.Forms.ToolStripButton();
            this.btnTxt = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.verTodosButton = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            this.BarraAcciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.DataSource = this.bs;
            this.gridControl1.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridControl1.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridControl1.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridControl1.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridControl1.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridControl1.EmbeddedNavigator.TextLocation = DevExpress.XtraEditors.NavigatorButtonsTextLocation.Begin;
            this.gridControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridControl1.Location = new System.Drawing.Point(0, 53);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemCheckEdit2});
            this.gridControl1.ShowOnlyPredefinedDetails = true;
            this.gridControl1.Size = new System.Drawing.Size(1008, 565);
            this.gridControl1.TabIndex = 20;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // bs
            // 
            this.bs.DataSource = typeof(HK.Retenciones);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.EvenRow.Font = new System.Drawing.Font("Tahoma", 12F);
            this.gridView1.Appearance.EvenRow.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 12F);
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.OddRow.Font = new System.Drawing.Font("Tahoma", 12F);
            this.gridView1.Appearance.OddRow.Options.UseFont = true;
            this.gridView1.ColumnPanelRowHeight = 65;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNumeroDeOperacion,
            this.colNumeroComprobante,
            this.colCedulaRif,
            this.colNombreRazonSocial,
            this.colPeriodoImpositivo,
            this.colFechaDocumento,
            this.colTipoOperacion,
            this.colTipoDocumento,
            this.colNumeroDocumento,
            this.colNumeroControlDocumento,
            this.colMontoDocumento,
            this.colBaseImponible,
            this.colMontoIvaRetenido,
            this.colMontoExentoIva,
            this.colAlicuota});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colNumeroDeOperacion
            // 
            this.colNumeroDeOperacion.AppearanceHeader.Options.UseTextOptions = true;
            this.colNumeroDeOperacion.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNumeroDeOperacion.FieldName = "Id";
            this.colNumeroDeOperacion.Name = "colNumeroDeOperacion";
            this.colNumeroDeOperacion.Visible = true;
            this.colNumeroDeOperacion.VisibleIndex = 0;
            this.colNumeroDeOperacion.Width = 63;
            // 
            // colNumeroComprobante
            // 
            this.colNumeroComprobante.AppearanceHeader.Options.UseTextOptions = true;
            this.colNumeroComprobante.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNumeroComprobante.FieldName = "NumeroComprobante";
            this.colNumeroComprobante.Name = "colNumeroComprobante";
            this.colNumeroComprobante.OptionsColumn.FixedWidth = true;
            this.colNumeroComprobante.Visible = true;
            this.colNumeroComprobante.VisibleIndex = 1;
            this.colNumeroComprobante.Width = 145;
            // 
            // colCedulaRif
            // 
            this.colCedulaRif.AppearanceHeader.Options.UseTextOptions = true;
            this.colCedulaRif.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCedulaRif.Caption = "Cedula Rif";
            this.colCedulaRif.FieldName = "CedulaRif";
            this.colCedulaRif.Name = "colCedulaRif";
            this.colCedulaRif.Visible = true;
            this.colCedulaRif.VisibleIndex = 2;
            this.colCedulaRif.Width = 120;
            // 
            // colNombreRazonSocial
            // 
            this.colNombreRazonSocial.AppearanceHeader.Options.UseTextOptions = true;
            this.colNombreRazonSocial.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNombreRazonSocial.FieldName = "NombreRazonSocial";
            this.colNombreRazonSocial.Name = "colNombreRazonSocial";
            this.colNombreRazonSocial.Visible = true;
            this.colNombreRazonSocial.VisibleIndex = 3;
            this.colNombreRazonSocial.Width = 250;
            // 
            // colPeriodoImpositivo
            // 
            this.colPeriodoImpositivo.AppearanceHeader.Options.UseTextOptions = true;
            this.colPeriodoImpositivo.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPeriodoImpositivo.FieldName = "PeriodoImpositivo";
            this.colPeriodoImpositivo.Name = "colPeriodoImpositivo";
            this.colPeriodoImpositivo.Visible = true;
            this.colPeriodoImpositivo.VisibleIndex = 4;
            this.colPeriodoImpositivo.Width = 100;
            // 
            // colFechaDocumento
            // 
            this.colFechaDocumento.AppearanceHeader.Options.UseTextOptions = true;
            this.colFechaDocumento.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colFechaDocumento.FieldName = "FechaDocumento";
            this.colFechaDocumento.Name = "colFechaDocumento";
            this.colFechaDocumento.Visible = true;
            this.colFechaDocumento.VisibleIndex = 5;
            this.colFechaDocumento.Width = 98;
            // 
            // colTipoOperacion
            // 
            this.colTipoOperacion.AppearanceHeader.Options.UseTextOptions = true;
            this.colTipoOperacion.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTipoOperacion.Caption = "Tipo Oper.";
            this.colTipoOperacion.FieldName = "TipoOperacion";
            this.colTipoOperacion.Name = "colTipoOperacion";
            this.colTipoOperacion.Visible = true;
            this.colTipoOperacion.VisibleIndex = 6;
            this.colTipoOperacion.Width = 67;
            // 
            // colTipoDocumento
            // 
            this.colTipoDocumento.AppearanceHeader.Options.UseTextOptions = true;
            this.colTipoDocumento.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTipoDocumento.Caption = "Tipo Doc.";
            this.colTipoDocumento.FieldName = "TipoDocumento";
            this.colTipoDocumento.Name = "colTipoDocumento";
            this.colTipoDocumento.Visible = true;
            this.colTipoDocumento.VisibleIndex = 7;
            this.colTipoDocumento.Width = 77;
            // 
            // colNumeroDocumento
            // 
            this.colNumeroDocumento.AppearanceHeader.Options.UseTextOptions = true;
            this.colNumeroDocumento.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNumeroDocumento.FieldName = "NumeroDocumento";
            this.colNumeroDocumento.Name = "colNumeroDocumento";
            this.colNumeroDocumento.Visible = true;
            this.colNumeroDocumento.VisibleIndex = 8;
            this.colNumeroDocumento.Width = 120;
            // 
            // colNumeroControlDocumento
            // 
            this.colNumeroControlDocumento.AppearanceHeader.Options.UseTextOptions = true;
            this.colNumeroControlDocumento.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNumeroControlDocumento.FieldName = "NumeroControlDocumento";
            this.colNumeroControlDocumento.Name = "colNumeroControlDocumento";
            this.colNumeroControlDocumento.Visible = true;
            this.colNumeroControlDocumento.VisibleIndex = 9;
            this.colNumeroControlDocumento.Width = 120;
            // 
            // colMontoDocumento
            // 
            this.colMontoDocumento.AppearanceHeader.Options.UseTextOptions = true;
            this.colMontoDocumento.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colMontoDocumento.FieldName = "MontoDocumento";
            this.colMontoDocumento.Name = "colMontoDocumento";
            this.colMontoDocumento.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "MontoDocumento", "{0:n2}")});
            this.colMontoDocumento.Visible = true;
            this.colMontoDocumento.VisibleIndex = 10;
            this.colMontoDocumento.Width = 120;
            // 
            // colBaseImponible
            // 
            this.colBaseImponible.AppearanceHeader.Options.UseTextOptions = true;
            this.colBaseImponible.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colBaseImponible.FieldName = "BaseImponible";
            this.colBaseImponible.Name = "colBaseImponible";
            this.colBaseImponible.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "BaseImponible", "{0:n2}")});
            this.colBaseImponible.Visible = true;
            this.colBaseImponible.VisibleIndex = 11;
            this.colBaseImponible.Width = 120;
            // 
            // colMontoIvaRetenido
            // 
            this.colMontoIvaRetenido.AppearanceHeader.Options.UseTextOptions = true;
            this.colMontoIvaRetenido.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colMontoIvaRetenido.FieldName = "MontoIvaRetenido";
            this.colMontoIvaRetenido.Name = "colMontoIvaRetenido";
            this.colMontoIvaRetenido.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "MontoIvaRetenido", "{0:n2}")});
            this.colMontoIvaRetenido.Visible = true;
            this.colMontoIvaRetenido.VisibleIndex = 12;
            this.colMontoIvaRetenido.Width = 120;
            // 
            // colMontoExentoIva
            // 
            this.colMontoExentoIva.AppearanceHeader.Options.UseTextOptions = true;
            this.colMontoExentoIva.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colMontoExentoIva.FieldName = "MontoExentoIva";
            this.colMontoExentoIva.Name = "colMontoExentoIva";
            this.colMontoExentoIva.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "MontoExentoIva", "{0:n2}")});
            this.colMontoExentoIva.Visible = true;
            this.colMontoExentoIva.VisibleIndex = 13;
            this.colMontoExentoIva.Width = 120;
            // 
            // colAlicuota
            // 
            this.colAlicuota.AppearanceHeader.Options.UseTextOptions = true;
            this.colAlicuota.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colAlicuota.FieldName = "Alicuota";
            this.colAlicuota.Name = "colAlicuota";
            this.colAlicuota.Visible = true;
            this.colAlicuota.VisibleIndex = 14;
            this.colAlicuota.Width = 120;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // BarraAcciones
            // 
            this.BarraAcciones.AutoSize = false;
            this.BarraAcciones.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.BarraAcciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verTodosButton,
            this.txtMes,
            this.txtAño,
            this.txtPeriodo,
            this.btnBuscar,
            this.toolStripSeparator4,
            this.btnNuevo,
            this.btnEditar,
            this.btnEliminar,
            this.toolStripSeparator2,
            this.btnImprimir,
            this.btnImprimirDoble,
            this.toolMontoCH,
            this.toolStripSeparator1,
            this.btnExcel,
            this.btnTxt,
            this.toolStripSeparator3});
            this.BarraAcciones.Location = new System.Drawing.Point(0, 0);
            this.BarraAcciones.Name = "BarraAcciones";
            this.BarraAcciones.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.BarraAcciones.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.BarraAcciones.Size = new System.Drawing.Size(1008, 53);
            this.BarraAcciones.TabIndex = 21;
            this.BarraAcciones.Text = "toolStrip1";
            // 
            // txtMes
            // 
            this.txtMes.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12"});
            this.txtMes.Name = "txtMes";
            this.txtMes.Size = new System.Drawing.Size(75, 53);
            // 
            // txtAño
            // 
            this.txtAño.Items.AddRange(new object[] {
            "2013",
            "2014",
            "2015",
            "2016",
            "2017",
            "2018",
            "2019",
            "2020"});
            this.txtAño.Name = "txtAño";
            this.txtAño.Size = new System.Drawing.Size(100, 53);
            // 
            // txtPeriodo
            // 
            this.txtPeriodo.Items.AddRange(new object[] {
            "PRIMER PERIODO",
            "SEGUNDO PERIODO"});
            this.txtPeriodo.Name = "txtPeriodo";
            this.txtPeriodo.Size = new System.Drawing.Size(150, 53);
            this.txtPeriodo.Visible = false;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = global::HK.Properties.Resources.note_view;
            this.btnBuscar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(46, 50);
            this.btnBuscar.Text = "Cargar";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 53);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Image = global::HK.Properties.Resources.note_new;
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(46, 50);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnEditar
            // 
            this.btnEditar.Image = global::HK.Properties.Resources.note_edit;
            this.btnEditar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(41, 50);
            this.btnEditar.Text = "Editar";
            this.btnEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Image = global::HK.Properties.Resources.note_delete;
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(54, 50);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 53);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = global::HK.Properties.Resources.printer;
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(57, 50);
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnImprimirDoble
            // 
            this.btnImprimirDoble.Image = global::HK.Properties.Resources.printer;
            this.btnImprimirDoble.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimirDoble.Name = "btnImprimirDoble";
            this.btnImprimirDoble.Size = new System.Drawing.Size(96, 50);
            this.btnImprimirDoble.Text = "Imprimir Copias";
            this.btnImprimirDoble.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolMontoCH
            // 
            this.toolMontoCH.Image = global::HK.Properties.Resources.calculator;
            this.toolMontoCH.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolMontoCH.Name = "toolMontoCH";
            this.toolMontoCH.Size = new System.Drawing.Size(87, 50);
            this.toolMontoCH.Text = "Ver Monto CH";
            this.toolMontoCH.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 53);
            // 
            // btnExcel
            // 
            this.btnExcel.Image = global::HK.Properties.Resources.copy;
            this.btnExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(83, 50);
            this.btnExcel.Text = "Exportar Excel";
            this.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExcel.Visible = false;
            // 
            // btnTxt
            // 
            this.btnTxt.Image = global::HK.Properties.Resources.data_lock;
            this.btnTxt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTxt.Name = "btnTxt";
            this.btnTxt.Size = new System.Drawing.Size(63, 51);
            this.btnTxt.Text = "Crear TXT";
            this.btnTxt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 53);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(38, 21);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = null;
            this.bindingNavigator1.BindingSource = this.bs;
            this.bindingNavigator1.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator1.CountItemFormat = "{0}";
            this.bindingNavigator1.DeleteItem = null;
            this.bindingNavigator1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2});
            this.bindingNavigator1.Location = new System.Drawing.Point(0, 621);
            this.bindingNavigator1.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator1.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator1.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator1.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.bindingNavigator1.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator1.Size = new System.Drawing.Size(1008, 25);
            this.bindingNavigator1.TabIndex = 19;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(21, 22);
            this.bindingNavigatorCountItem.Text = "{0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // verTodosButton
            // 
            this.verTodosButton.Image = global::HK.Properties.Resources.data_find;
            this.verTodosButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.verTodosButton.Name = "verTodosButton";
            this.verTodosButton.Size = new System.Drawing.Size(64, 50);
            this.verTodosButton.Text = "Ver Todos";
            this.verTodosButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // FrmRetenciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 646);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.BarraAcciones);
            this.Controls.Add(this.bindingNavigator1);
            this.Name = "FrmRetenciones";
            this.Text = "Retenciones";
            this.Load += new System.EventHandler(this.FrmRetenciones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            this.BarraAcciones.ResumeLayout(false);
            this.BarraAcciones.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private System.Windows.Forms.BindingSource bs;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        public System.Windows.Forms.ToolStrip BarraAcciones;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripButton btnEditar;
        private System.Windows.Forms.ToolStripButton btnEliminar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.ToolStripButton btnExcel;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private DevExpress.XtraGrid.Columns.GridColumn colNumeroDeOperacion;
        private DevExpress.XtraGrid.Columns.GridColumn colCedulaRif;
        private DevExpress.XtraGrid.Columns.GridColumn colNombreRazonSocial;
        private DevExpress.XtraGrid.Columns.GridColumn colPeriodoImpositivo;
        private DevExpress.XtraGrid.Columns.GridColumn colFechaDocumento;
        private DevExpress.XtraGrid.Columns.GridColumn colTipoOperacion;
        private DevExpress.XtraGrid.Columns.GridColumn colTipoDocumento;
        private DevExpress.XtraGrid.Columns.GridColumn colNumeroDocumento;
        private DevExpress.XtraGrid.Columns.GridColumn colNumeroControlDocumento;
        private DevExpress.XtraGrid.Columns.GridColumn colMontoDocumento;
        private DevExpress.XtraGrid.Columns.GridColumn colBaseImponible;
        private DevExpress.XtraGrid.Columns.GridColumn colMontoIvaRetenido;
        private DevExpress.XtraGrid.Columns.GridColumn colNumeroComprobante;
        private DevExpress.XtraGrid.Columns.GridColumn colMontoExentoIva;
        private DevExpress.XtraGrid.Columns.GridColumn colAlicuota;
        private System.Windows.Forms.ToolStripComboBox txtMes;
        private System.Windows.Forms.ToolStripComboBox txtAño;
        private System.Windows.Forms.ToolStripButton btnBuscar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnTxt;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripComboBox txtPeriodo;
        private System.Windows.Forms.ToolStripButton btnImprimirDoble;
        private System.Windows.Forms.ToolStripButton toolMontoCH;
        private System.Windows.Forms.ToolStripButton verTodosButton;

    }
}