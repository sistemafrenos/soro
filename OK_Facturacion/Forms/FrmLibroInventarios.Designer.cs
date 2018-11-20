namespace HK
{
    partial class FrmLibroInventarios
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
            this.Aceptar = new System.Windows.Forms.ToolStripButton();
            this.Cancelar = new System.Windows.Forms.ToolStripButton();
            this.Imprimir = new System.Windows.Forms.ToolStripButton();
            this.gridViewGrupos = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.libroBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInicial = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInicialBs = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEntradas = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEntradasBs = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSalidas = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSalidasBs = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFinal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFinalBs = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.BarraAcciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewGrupos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.libroBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
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
            this.Aceptar,
            this.Cancelar,
            this.Imprimir});
            this.BarraAcciones.Location = new System.Drawing.Point(0, 0);
            this.BarraAcciones.Name = "BarraAcciones";
            this.BarraAcciones.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.BarraAcciones.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.BarraAcciones.Size = new System.Drawing.Size(784, 53);
            this.BarraAcciones.TabIndex = 5;
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
            // Aceptar
            // 
            this.Aceptar.Enabled = false;
            this.Aceptar.Image = global::HK.Properties.Resources.disk_blue_ok;
            this.Aceptar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Aceptar.Name = "Aceptar";
            this.Aceptar.Size = new System.Drawing.Size(52, 50);
            this.Aceptar.Text = "Aceptar";
            this.Aceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Aceptar.Click += new System.EventHandler(this.Aceptar_Click);
            // 
            // Cancelar
            // 
            this.Cancelar.Enabled = false;
            this.Cancelar.Image = global::HK.Properties.Resources.disk_blue_error;
            this.Cancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Cancelar.Name = "Cancelar";
            this.Cancelar.Size = new System.Drawing.Size(57, 50);
            this.Cancelar.Text = "Cancelar";
            this.Cancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Cancelar.Click += new System.EventHandler(this.Cancelar_Click);
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
            // gridControl1
            // 
            this.gridControl1.DataSource = this.libroBindingSource;
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
            this.gridControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridControl1_KeyDown);
            // 
            // libroBindingSource
            // 
            this.libroBindingSource.DataSource = typeof(HK.LibroInventarios);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 10F);
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn7,
            this.gridColumn8,
            this.colInicial,
            this.colInicialBs,
            this.colEntradas,
            this.colEntradasBs,
            this.colSalidas,
            this.colSalidasBs,
            this.colFinal,
            this.colFinalBs});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Codigo";
            this.gridColumn7.FieldName = "Codigo";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 0;
            this.gridColumn7.Width = 90;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Descripcion";
            this.gridColumn8.FieldName = "Descripcion";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 1;
            this.gridColumn8.Width = 99;
            // 
            // colInicial
            // 
            this.colInicial.AppearanceCell.Options.UseTextOptions = true;
            this.colInicial.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colInicial.Caption = "Inicial";
            this.colInicial.FieldName = "Inicial";
            this.colInicial.Name = "colInicial";
            this.colInicial.OptionsColumn.FixedWidth = true;
            this.colInicial.Visible = true;
            this.colInicial.VisibleIndex = 2;
            // 
            // colInicialBs
            // 
            this.colInicialBs.AppearanceCell.Options.UseTextOptions = true;
            this.colInicialBs.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colInicialBs.Caption = "InicialBs";
            this.colInicialBs.FieldName = "InicialBs";
            this.colInicialBs.GroupFormat.FormatString = "N2";
            this.colInicialBs.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colInicialBs.Name = "colInicialBs";
            this.colInicialBs.OptionsColumn.FixedWidth = true;
            this.colInicialBs.SummaryItem.DisplayFormat = "{0:n2}";
            this.colInicialBs.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colInicialBs.Visible = true;
            this.colInicialBs.VisibleIndex = 3;
            // 
            // colEntradas
            // 
            this.colEntradas.AppearanceCell.Options.UseTextOptions = true;
            this.colEntradas.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colEntradas.Caption = "Entradas";
            this.colEntradas.FieldName = "Entradas";
            this.colEntradas.Name = "colEntradas";
            this.colEntradas.OptionsColumn.FixedWidth = true;
            this.colEntradas.Visible = true;
            this.colEntradas.VisibleIndex = 4;
            // 
            // colEntradasBs
            // 
            this.colEntradasBs.AppearanceCell.Options.UseTextOptions = true;
            this.colEntradasBs.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colEntradasBs.Caption = "EntradasBs";
            this.colEntradasBs.DisplayFormat.FormatString = "N2";
            this.colEntradasBs.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colEntradasBs.FieldName = "EntradasBs";
            this.colEntradasBs.GroupFormat.FormatString = "N2";
            this.colEntradasBs.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colEntradasBs.Name = "colEntradasBs";
            this.colEntradasBs.OptionsColumn.FixedWidth = true;
            this.colEntradasBs.SummaryItem.DisplayFormat = "{0:n2}";
            this.colEntradasBs.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colEntradasBs.Visible = true;
            this.colEntradasBs.VisibleIndex = 5;
            // 
            // colSalidas
            // 
            this.colSalidas.AppearanceCell.Options.UseTextOptions = true;
            this.colSalidas.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colSalidas.Caption = "Salidas";
            this.colSalidas.FieldName = "Salidas";
            this.colSalidas.Name = "colSalidas";
            this.colSalidas.OptionsColumn.FixedWidth = true;
            this.colSalidas.Visible = true;
            this.colSalidas.VisibleIndex = 6;
            // 
            // colSalidasBs
            // 
            this.colSalidasBs.AppearanceCell.Options.UseTextOptions = true;
            this.colSalidasBs.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colSalidasBs.Caption = "SalidasBs";
            this.colSalidasBs.DisplayFormat.FormatString = "N2";
            this.colSalidasBs.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSalidasBs.FieldName = "SalidasBs";
            this.colSalidasBs.GroupFormat.FormatString = "N2";
            this.colSalidasBs.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSalidasBs.Name = "colSalidasBs";
            this.colSalidasBs.OptionsColumn.FixedWidth = true;
            this.colSalidasBs.SummaryItem.DisplayFormat = "{0:n2}";
            this.colSalidasBs.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colSalidasBs.Visible = true;
            this.colSalidasBs.VisibleIndex = 7;
            // 
            // colFinal
            // 
            this.colFinal.AppearanceCell.Options.UseTextOptions = true;
            this.colFinal.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colFinal.Caption = "Final";
            this.colFinal.FieldName = "Final";
            this.colFinal.Name = "colFinal";
            this.colFinal.OptionsColumn.FixedWidth = true;
            this.colFinal.Visible = true;
            this.colFinal.VisibleIndex = 8;
            this.colFinal.Width = 60;
            // 
            // colFinalBs
            // 
            this.colFinalBs.AppearanceCell.Options.UseTextOptions = true;
            this.colFinalBs.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colFinalBs.Caption = "FinalBs";
            this.colFinalBs.DisplayFormat.FormatString = "N2";
            this.colFinalBs.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colFinalBs.FieldName = "FinalBs";
            this.colFinalBs.GroupFormat.FormatString = "N2";
            this.colFinalBs.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colFinalBs.Name = "colFinalBs";
            this.colFinalBs.OptionsColumn.FixedWidth = true;
            this.colFinalBs.SummaryItem.DisplayFormat = "{0:n2}";
            this.colFinalBs.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colFinalBs.Visible = true;
            this.colFinalBs.VisibleIndex = 9;
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.gridControl1);
            this.groupControl1.Location = new System.Drawing.Point(0, 57);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(784, 505);
            this.groupControl1.TabIndex = 6;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(486, 22);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(144, 17);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Incluir Productos en cero";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // FrmLibroInventarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.BarraAcciones);
            this.Controls.Add(this.groupControl1);
            this.Name = "FrmLibroInventarios";
            this.Text = "Libro Inventarios";
            this.Load += new System.EventHandler(this.Frm_Load);
            this.BarraAcciones.ResumeLayout(false);
            this.BarraAcciones.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewGrupos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.libroBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ToolStrip BarraAcciones;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox txtAño;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox txtMes;
        private System.Windows.Forms.ToolStripButton Buscar;
        private System.Windows.Forms.ToolStripButton Aceptar;
        private System.Windows.Forms.ToolStripButton Cancelar;
        private System.Windows.Forms.ToolStripButton Imprimir;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewGrupos;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private System.Windows.Forms.BindingSource libroBindingSource;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colInicial;
        private DevExpress.XtraGrid.Columns.GridColumn colInicialBs;
        private DevExpress.XtraGrid.Columns.GridColumn colEntradas;
        private DevExpress.XtraGrid.Columns.GridColumn colEntradasBs;
        private DevExpress.XtraGrid.Columns.GridColumn colSalidas;
        private DevExpress.XtraGrid.Columns.GridColumn colSalidasBs;
        private DevExpress.XtraGrid.Columns.GridColumn colFinal;
        private DevExpress.XtraGrid.Columns.GridColumn colFinalBs;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}