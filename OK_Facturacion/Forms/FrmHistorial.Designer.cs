namespace HK.Formas
{
    partial class FrmHistorial
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.BarraAcciones = new System.Windows.Forms.ToolStrip();
            this.Aceptar = new System.Windows.Forms.ToolStripButton();
            this.Cancelar = new System.Windows.Forms.ToolStripButton();
            this.Imprimir = new System.Windows.Forms.ToolStripButton();
            this.FrmDesdeHasta = new DevExpress.XtraEditors.GroupControl();
            this.txtHasta = new DevExpress.XtraEditors.DateEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.ButtonDesdeHasta = new DevExpress.XtraEditors.SimpleButton();
            this.txtDesde = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.productoHistorialBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colFecha = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCantidad = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRazonSocial = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMontoNeto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExistenciaAnterior = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTipo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSaldo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNumero = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEntrada = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSalida = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.BarraAcciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FrmDesdeHasta)).BeginInit();
            this.FrmDesdeHasta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHasta.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHasta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDesde.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDesde.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productoHistorialBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.DataSource = this.productoHistorialBindingSource;
            this.gridControl1.Location = new System.Drawing.Point(8, 78);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(964, 421);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridView1.ColumnPanelRowHeight = 45;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colFecha,
            this.colCantidad,
            this.colRazonSocial,
            this.colMontoNeto,
            this.colExistenciaAnterior,
            this.colTipo,
            this.colSaldo,
            this.colNumero,
            this.colEntrada,
            this.colSalida});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupPanelText = "Agrupar aqui";
            this.gridView1.Name = "gridView1";
            // 
            // BarraAcciones
            // 
            this.BarraAcciones.AutoSize = false;
            this.BarraAcciones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BarraAcciones.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.BarraAcciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Aceptar,
            this.Cancelar,
            this.Imprimir});
            this.BarraAcciones.Location = new System.Drawing.Point(0, 502);
            this.BarraAcciones.Name = "BarraAcciones";
            this.BarraAcciones.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.BarraAcciones.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.BarraAcciones.Size = new System.Drawing.Size(984, 60);
            this.BarraAcciones.TabIndex = 35;
            this.BarraAcciones.Text = "toolStrip1";
            // 
            // Aceptar
            // 
            this.Aceptar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Aceptar.Image = global::HK.Properties.Resources.disk_blue_ok;
            this.Aceptar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Aceptar.Name = "Aceptar";
            this.Aceptar.Size = new System.Drawing.Size(81, 57);
            this.Aceptar.Text = "Aceptar - F10";
            this.Aceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // Cancelar
            // 
            this.Cancelar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Cancelar.Image = global::HK.Properties.Resources.disk_blue_error;
            this.Cancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Cancelar.Name = "Cancelar";
            this.Cancelar.Size = new System.Drawing.Size(88, 57);
            this.Cancelar.Text = "Cancelar - ESC";
            this.Cancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripButton1
            // 
            this.Imprimir.Image = global::HK.Properties.Resources.printer;
            this.Imprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Imprimir.Name = "toolStripButton1";
            this.Imprimir.Size = new System.Drawing.Size(89, 57);
            this.Imprimir.Text = "Imprimir";
            // 
            // FrmDesdeHasta
            // 
            this.FrmDesdeHasta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FrmDesdeHasta.Controls.Add(this.txtHasta);
            this.FrmDesdeHasta.Controls.Add(this.labelControl3);
            this.FrmDesdeHasta.Controls.Add(this.ButtonDesdeHasta);
            this.FrmDesdeHasta.Controls.Add(this.txtDesde);
            this.FrmDesdeHasta.Controls.Add(this.labelControl2);
            this.FrmDesdeHasta.Location = new System.Drawing.Point(8, 8);
            this.FrmDesdeHasta.Name = "FrmDesdeHasta";
            this.FrmDesdeHasta.Size = new System.Drawing.Size(964, 64);
            this.FrmDesdeHasta.TabIndex = 36;
            this.FrmDesdeHasta.Visible = false;
            // 
            // txtHasta
            // 
            this.txtHasta.EditValue = null;
            this.txtHasta.Location = new System.Drawing.Point(252, 24);
            this.txtHasta.Name = "txtHasta";
            this.txtHasta.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtHasta.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtHasta.Size = new System.Drawing.Size(116, 20);
            this.txtHasta.TabIndex = 4;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(205, 23);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(28, 13);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "Hasta";
            // 
            // ButtonDesdeHasta
            // 
            this.ButtonDesdeHasta.Location = new System.Drawing.Point(437, 27);
            this.ButtonDesdeHasta.Name = "ButtonDesdeHasta";
            this.ButtonDesdeHasta.Size = new System.Drawing.Size(180, 23);
            this.ButtonDesdeHasta.TabIndex = 2;
            this.ButtonDesdeHasta.Text = "Filtrar Datos";
            // 
            // txtDesde
            // 
            this.txtDesde.EditValue = null;
            this.txtDesde.Location = new System.Drawing.Point(54, 24);
            this.txtDesde.Name = "txtDesde";
            this.txtDesde.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDesde.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDesde.Size = new System.Drawing.Size(116, 20);
            this.txtDesde.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(7, 23);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(30, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Desde";
            // 
            // productoHistorialBindingSource
            // 
            //this.productoHistorialBindingSource.DataSource = typeof(HK.ProductoHistorial);
            // 
            // colFecha
            // 
            this.colFecha.FieldName = "Fecha";
            this.colFecha.Name = "colFecha";
            this.colFecha.OptionsColumn.FixedWidth = true;
            this.colFecha.Visible = true;
            this.colFecha.VisibleIndex = 0;
            // 
            // colCantidad
            // 
            this.colCantidad.FieldName = "Cantidad";
            this.colCantidad.Name = "colCantidad";
            this.colCantidad.OptionsColumn.FixedWidth = true;
            // 
            // colRazonSocial
            // 
            this.colRazonSocial.FieldName = "RazonSocial";
            this.colRazonSocial.Name = "colRazonSocial";
            this.colRazonSocial.Visible = true;
            this.colRazonSocial.VisibleIndex = 3;
            // 
            // colMontoNeto
            // 
            this.colMontoNeto.DisplayFormat.FormatString = "n2";
            this.colMontoNeto.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMontoNeto.FieldName = "MontoNeto";
            this.colMontoNeto.GroupFormat.FormatString = "n2";
            this.colMontoNeto.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMontoNeto.Name = "colMontoNeto";
            this.colMontoNeto.OptionsColumn.FixedWidth = true;
            this.colMontoNeto.Visible = true;
            this.colMontoNeto.VisibleIndex = 8;
            // 
            // colExistenciaAnterior
            // 
            this.colExistenciaAnterior.DisplayFormat.FormatString = "n2";
            this.colExistenciaAnterior.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colExistenciaAnterior.FieldName = "ExistenciaAnterior";
            this.colExistenciaAnterior.GroupFormat.FormatString = "n2";
            this.colExistenciaAnterior.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colExistenciaAnterior.Name = "colExistenciaAnterior";
            this.colExistenciaAnterior.OptionsColumn.FixedWidth = true;
            this.colExistenciaAnterior.Visible = true;
            this.colExistenciaAnterior.VisibleIndex = 4;
            // 
            // colTipo
            // 
            this.colTipo.DisplayFormat.FormatString = "n2";
            this.colTipo.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTipo.FieldName = "Tipo";
            this.colTipo.GroupFormat.FormatString = "n2";
            this.colTipo.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTipo.Name = "colTipo";
            this.colTipo.OptionsColumn.FixedWidth = true;
            this.colTipo.Visible = true;
            this.colTipo.VisibleIndex = 1;
            // 
            // colSaldo
            // 
            this.colSaldo.DisplayFormat.FormatString = "n2";
            this.colSaldo.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSaldo.FieldName = "Saldo";
            this.colSaldo.GroupFormat.FormatString = "n2";
            this.colSaldo.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSaldo.Name = "colSaldo";
            this.colSaldo.OptionsColumn.FixedWidth = true;
            this.colSaldo.Visible = true;
            this.colSaldo.VisibleIndex = 7;
            // 
            // colNumero
            // 
            this.colNumero.DisplayFormat.FormatString = "n2";
            this.colNumero.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNumero.FieldName = "Numero";
            this.colNumero.GroupFormat.FormatString = "n2";
            this.colNumero.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNumero.Name = "colNumero";
            this.colNumero.OptionsColumn.FixedWidth = true;
            this.colNumero.Visible = true;
            this.colNumero.VisibleIndex = 2;
            // 
            // colEntrada
            // 
            this.colEntrada.DisplayFormat.FormatString = "n2";
            this.colEntrada.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colEntrada.FieldName = "Entrada";
            this.colEntrada.GroupFormat.FormatString = "n2";
            this.colEntrada.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colEntrada.Name = "colEntrada";
            this.colEntrada.OptionsColumn.FixedWidth = true;
            this.colEntrada.Visible = true;
            this.colEntrada.VisibleIndex = 5;
            // 
            // colSalida
            // 
            this.colSalida.DisplayFormat.FormatString = "n2";
            this.colSalida.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSalida.FieldName = "Salida";
            this.colSalida.GroupFormat.FormatString = "n2";
            this.colSalida.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSalida.Name = "colSalida";
            this.colSalida.OptionsColumn.FixedWidth = true;
            this.colSalida.Visible = true;
            this.colSalida.VisibleIndex = 6;
            // 
            // FrmHistorial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 562);
            this.Controls.Add(this.FrmDesdeHasta);
            this.Controls.Add(this.BarraAcciones);
            this.Controls.Add(this.gridControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmHistorial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Historial";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.BarraAcciones.ResumeLayout(false);
            this.BarraAcciones.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FrmDesdeHasta)).EndInit();
            this.FrmDesdeHasta.ResumeLayout(false);
            this.FrmDesdeHasta.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHasta.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHasta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDesde.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDesde.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productoHistorialBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        public System.Windows.Forms.ToolStrip BarraAcciones;
        private System.Windows.Forms.ToolStripButton Aceptar;
        private System.Windows.Forms.ToolStripButton Cancelar;
        private System.Windows.Forms.ToolStripButton Imprimir;
        private DevExpress.XtraEditors.GroupControl FrmDesdeHasta;
        private DevExpress.XtraEditors.DateEdit txtHasta;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton ButtonDesdeHasta;
        private DevExpress.XtraEditors.DateEdit txtDesde;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.BindingSource productoHistorialBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colFecha;
        private DevExpress.XtraGrid.Columns.GridColumn colCantidad;
        private DevExpress.XtraGrid.Columns.GridColumn colRazonSocial;
        private DevExpress.XtraGrid.Columns.GridColumn colMontoNeto;
        private DevExpress.XtraGrid.Columns.GridColumn colExistenciaAnterior;
        private DevExpress.XtraGrid.Columns.GridColumn colTipo;
        private DevExpress.XtraGrid.Columns.GridColumn colSaldo;
        private DevExpress.XtraGrid.Columns.GridColumn colNumero;
        private DevExpress.XtraGrid.Columns.GridColumn colEntrada;
        private DevExpress.XtraGrid.Columns.GridColumn colSalida;
    }
}