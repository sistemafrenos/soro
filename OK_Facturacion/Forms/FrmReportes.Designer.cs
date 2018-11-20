namespace HK
{
    partial class FrmReportes
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.FrmDesdeHasta = new DevExpress.XtraEditors.GroupControl();
            this.txtHasta = new DevExpress.XtraEditors.DateEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.ButtonDesdeHasta = new DevExpress.XtraEditors.SimpleButton();
            this.txtDesde = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.FrmFecha = new DevExpress.XtraEditors.GroupControl();
            this.CargarReporte = new DevExpress.XtraEditors.SimpleButton();
            this.txtFecha = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupGrupos = new DevExpress.XtraEditors.GroupControl();
            this.BotonReportePorLineas = new DevExpress.XtraEditors.SimpleButton();
            this.lblGrupos = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxGrupos = new DevExpress.XtraEditors.ComboBoxEdit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FrmDesdeHasta)).BeginInit();
            this.FrmDesdeHasta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHasta.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHasta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDesde.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDesde.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FrmFecha)).BeginInit();
            this.FrmFecha.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFecha.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFecha.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupGrupos)).BeginInit();
            this.groupGrupos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxGrupos.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.FrmDesdeHasta, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.reportViewer1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.FrmFecha, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupGrupos, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(632, 453);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // FrmDesdeHasta
            // 
            this.FrmDesdeHasta.Controls.Add(this.txtHasta);
            this.FrmDesdeHasta.Controls.Add(this.labelControl3);
            this.FrmDesdeHasta.Controls.Add(this.ButtonDesdeHasta);
            this.FrmDesdeHasta.Controls.Add(this.txtDesde);
            this.FrmDesdeHasta.Controls.Add(this.labelControl2);
            this.FrmDesdeHasta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FrmDesdeHasta.Location = new System.Drawing.Point(3, 3);
            this.FrmDesdeHasta.Name = "FrmDesdeHasta";
            this.FrmDesdeHasta.Size = new System.Drawing.Size(626, 64);
            this.FrmDesdeHasta.TabIndex = 7;
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
            this.ButtonDesdeHasta.Text = "Cargar Reporte";
            this.ButtonDesdeHasta.Click += new System.EventHandler(this.CargarReporte_Click);
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
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.DocumentMapCollapsed = true;
            this.reportViewer1.Location = new System.Drawing.Point(3, 208);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(626, 242);
            this.reportViewer1.TabIndex = 6;
            this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            this.reportViewer1.ZoomPercent = 80;
            // 
            // FrmFecha
            // 
            this.FrmFecha.Controls.Add(this.CargarReporte);
            this.FrmFecha.Controls.Add(this.txtFecha);
            this.FrmFecha.Controls.Add(this.labelControl1);
            this.FrmFecha.Dock = System.Windows.Forms.DockStyle.Top;
            this.FrmFecha.Location = new System.Drawing.Point(3, 147);
            this.FrmFecha.Name = "FrmFecha";
            this.FrmFecha.Size = new System.Drawing.Size(626, 55);
            this.FrmFecha.TabIndex = 5;
            this.FrmFecha.Visible = false;
            // 
            // CargarReporte
            // 
            this.CargarReporte.Location = new System.Drawing.Point(188, 23);
            this.CargarReporte.Name = "CargarReporte";
            this.CargarReporte.Size = new System.Drawing.Size(180, 23);
            this.CargarReporte.TabIndex = 2;
            this.CargarReporte.Text = "CargarReporte";
            this.CargarReporte.Click += new System.EventHandler(this.CargarReporte_Click);
            // 
            // txtFecha
            // 
            this.txtFecha.EditValue = null;
            this.txtFecha.Location = new System.Drawing.Point(54, 24);
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtFecha.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtFecha.Size = new System.Drawing.Size(116, 20);
            this.txtFecha.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(7, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(29, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Fecha";
            // 
            // groupGrupos
            // 
            this.groupGrupos.Controls.Add(this.BotonReportePorLineas);
            this.groupGrupos.Controls.Add(this.lblGrupos);
            this.groupGrupos.Controls.Add(this.comboBoxGrupos);
            this.groupGrupos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupGrupos.Location = new System.Drawing.Point(3, 73);
            this.groupGrupos.Name = "groupGrupos";
            this.groupGrupos.Size = new System.Drawing.Size(626, 68);
            this.groupGrupos.TabIndex = 8;
            this.groupGrupos.Visible = false;
            // 
            // BotonReportePorLineas
            // 
            this.BotonReportePorLineas.Location = new System.Drawing.Point(437, 30);
            this.BotonReportePorLineas.Name = "BotonReportePorLineas";
            this.BotonReportePorLineas.Size = new System.Drawing.Size(180, 23);
            this.BotonReportePorLineas.TabIndex = 3;
            this.BotonReportePorLineas.Text = "Cargar Reporte";
            this.BotonReportePorLineas.Click += new System.EventHandler(this.BotonReportePorLineas_Click);
            // 
            // lblGrupos
            // 
            this.lblGrupos.Location = new System.Drawing.Point(7, 36);
            this.lblGrupos.Name = "lblGrupos";
            this.lblGrupos.Size = new System.Drawing.Size(25, 13);
            this.lblGrupos.TabIndex = 1;
            this.lblGrupos.Text = "Linea";
            // 
            // comboBoxGrupos
            // 
            this.comboBoxGrupos.Location = new System.Drawing.Point(54, 33);
            this.comboBoxGrupos.Name = "comboBoxGrupos";
            this.comboBoxGrupos.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxGrupos.Size = new System.Drawing.Size(314, 20);
            this.comboBoxGrupos.TabIndex = 0;
            // 
            // FrmReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 453);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmReportes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visor de Reportes";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FrmDesdeHasta)).EndInit();
            this.FrmDesdeHasta.ResumeLayout(false);
            this.FrmDesdeHasta.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHasta.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHasta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDesde.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDesde.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FrmFecha)).EndInit();
            this.FrmFecha.ResumeLayout(false);
            this.FrmFecha.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFecha.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFecha.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupGrupos)).EndInit();
            this.groupGrupos.ResumeLayout(false);
            this.groupGrupos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxGrupos.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private DevExpress.XtraEditors.GroupControl FrmFecha;
        private DevExpress.XtraEditors.SimpleButton CargarReporte;
        private DevExpress.XtraEditors.DateEdit txtFecha;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.GroupControl FrmDesdeHasta;
        private DevExpress.XtraEditors.DateEdit txtHasta;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton ButtonDesdeHasta;
        private DevExpress.XtraEditors.DateEdit txtDesde;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.GroupControl groupGrupos;
        private DevExpress.XtraEditors.SimpleButton BotonReportePorLineas;
        private DevExpress.XtraEditors.LabelControl lblGrupos;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxGrupos;



    }
}