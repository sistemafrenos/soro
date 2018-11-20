namespace HK
{
    partial class FrmParametros
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.parametroBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.IdTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.ItemForId = new DevExpress.XtraLayout.LayoutControlItem();
            this.EmpresaTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.ItemForEmpresa = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.EmpresaDireccionTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.ItemForEmpresaDireccion = new DevExpress.XtraLayout.LayoutControlItem();
            this.EquipoTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.ItemForEquipo = new DevExpress.XtraLayout.LayoutControlItem();
            this.ServidorTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.ItemForServidor = new DevExpress.XtraLayout.LayoutControlItem();
            this.BaseDeDatosTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.ItemForBaseDeDatos = new DevExpress.XtraLayout.LayoutControlItem();
            this.Cancelar = new System.Windows.Forms.Button();
            this.Aceptar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.parametroBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IdTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmpresaTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForEmpresa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmpresaDireccionTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForEmpresaDireccion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EquipoTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForEquipo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ServidorTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForServidor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BaseDeDatosTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForBaseDeDatos)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.dataLayoutControl1);
            this.groupControl1.Location = new System.Drawing.Point(12, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(463, 181);
            this.groupControl1.TabIndex = 0;
            // 
            // parametroBindingSource
            // 
            this.parametroBindingSource.DataSource = typeof(HK.Parametro);
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.IdTextEdit);
            this.dataLayoutControl1.Controls.Add(this.EmpresaTextEdit);
            this.dataLayoutControl1.Controls.Add(this.EmpresaDireccionTextEdit);
            this.dataLayoutControl1.Controls.Add(this.EquipoTextEdit);
            this.dataLayoutControl1.Controls.Add(this.ServidorTextEdit);
            this.dataLayoutControl1.Controls.Add(this.BaseDeDatosTextEdit);
            this.dataLayoutControl1.DataSource = this.parametroBindingSource;
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ItemForId});
            this.dataLayoutControl1.Location = new System.Drawing.Point(2, 22);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.layoutControlGroup1;
            this.dataLayoutControl1.Size = new System.Drawing.Size(459, 157);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutControlGroup1.Size = new System.Drawing.Size(459, 157);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // IdTextEdit
            // 
            this.IdTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.parametroBindingSource, "Id", true));
            this.IdTextEdit.Location = new System.Drawing.Point(0, 0);
            this.IdTextEdit.Name = "IdTextEdit";
            this.IdTextEdit.Size = new System.Drawing.Size(0, 20);
            this.IdTextEdit.StyleController = this.dataLayoutControl1;
            this.IdTextEdit.TabIndex = 4;
            // 
            // ItemForId
            // 
            this.ItemForId.Control = this.IdTextEdit;
            this.ItemForId.CustomizationFormText = "Id";
            this.ItemForId.Location = new System.Drawing.Point(0, 0);
            this.ItemForId.Name = "ItemForId";
            this.ItemForId.Size = new System.Drawing.Size(0, 0);
            this.ItemForId.Text = "Id";
            this.ItemForId.TextSize = new System.Drawing.Size(50, 20);
            this.ItemForId.TextToControlDistance = 5;
            // 
            // EmpresaTextEdit
            // 
            this.EmpresaTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.parametroBindingSource, "Empresa", true));
            this.EmpresaTextEdit.Location = new System.Drawing.Point(94, 3);
            this.EmpresaTextEdit.Name = "EmpresaTextEdit";
            this.EmpresaTextEdit.Size = new System.Drawing.Size(362, 20);
            this.EmpresaTextEdit.StyleController = this.dataLayoutControl1;
            this.EmpresaTextEdit.TabIndex = 5;
            // 
            // ItemForEmpresa
            // 
            this.ItemForEmpresa.Control = this.EmpresaTextEdit;
            this.ItemForEmpresa.CustomizationFormText = "Empresa";
            this.ItemForEmpresa.Location = new System.Drawing.Point(0, 0);
            this.ItemForEmpresa.Name = "ItemForEmpresa";
            this.ItemForEmpresa.Size = new System.Drawing.Size(457, 24);
            this.ItemForEmpresa.Text = "Empresa";
            this.ItemForEmpresa.TextSize = new System.Drawing.Size(87, 13);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.AllowDrawBackground = false;
            this.layoutControlGroup2.CustomizationFormText = "autoGeneratedGroup0";
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ItemForEmpresa,
            this.ItemForEmpresaDireccion,
            this.ItemForEquipo,
            this.ItemForServidor,
            this.ItemForBaseDeDatos});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "autoGeneratedGroup0";
            this.layoutControlGroup2.Size = new System.Drawing.Size(457, 155);
            this.layoutControlGroup2.Text = "autoGeneratedGroup0";
            // 
            // EmpresaDireccionTextEdit
            // 
            this.EmpresaDireccionTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.parametroBindingSource, "EmpresaDireccion", true));
            this.EmpresaDireccionTextEdit.Location = new System.Drawing.Point(94, 27);
            this.EmpresaDireccionTextEdit.Name = "EmpresaDireccionTextEdit";
            this.EmpresaDireccionTextEdit.Size = new System.Drawing.Size(362, 20);
            this.EmpresaDireccionTextEdit.StyleController = this.dataLayoutControl1;
            this.EmpresaDireccionTextEdit.TabIndex = 6;
            // 
            // ItemForEmpresaDireccion
            // 
            this.ItemForEmpresaDireccion.Control = this.EmpresaDireccionTextEdit;
            this.ItemForEmpresaDireccion.CustomizationFormText = "Empresa Direccion";
            this.ItemForEmpresaDireccion.Location = new System.Drawing.Point(0, 24);
            this.ItemForEmpresaDireccion.Name = "ItemForEmpresaDireccion";
            this.ItemForEmpresaDireccion.Size = new System.Drawing.Size(457, 24);
            this.ItemForEmpresaDireccion.Text = "Empresa Direccion";
            this.ItemForEmpresaDireccion.TextSize = new System.Drawing.Size(87, 13);
            // 
            // EquipoTextEdit
            // 
            this.EquipoTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.parametroBindingSource, "Equipo", true));
            this.EquipoTextEdit.Location = new System.Drawing.Point(94, 51);
            this.EquipoTextEdit.Name = "EquipoTextEdit";
            this.EquipoTextEdit.Size = new System.Drawing.Size(362, 20);
            this.EquipoTextEdit.StyleController = this.dataLayoutControl1;
            this.EquipoTextEdit.TabIndex = 7;
            // 
            // ItemForEquipo
            // 
            this.ItemForEquipo.Control = this.EquipoTextEdit;
            this.ItemForEquipo.CustomizationFormText = "Equipo";
            this.ItemForEquipo.Location = new System.Drawing.Point(0, 48);
            this.ItemForEquipo.Name = "ItemForEquipo";
            this.ItemForEquipo.Size = new System.Drawing.Size(457, 24);
            this.ItemForEquipo.Text = "Equipo";
            this.ItemForEquipo.TextSize = new System.Drawing.Size(87, 13);
            // 
            // ServidorTextEdit
            // 
            this.ServidorTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.parametroBindingSource, "Servidor", true));
            this.ServidorTextEdit.Location = new System.Drawing.Point(94, 75);
            this.ServidorTextEdit.Name = "ServidorTextEdit";
            this.ServidorTextEdit.Size = new System.Drawing.Size(362, 20);
            this.ServidorTextEdit.StyleController = this.dataLayoutControl1;
            this.ServidorTextEdit.TabIndex = 8;
            // 
            // ItemForServidor
            // 
            this.ItemForServidor.Control = this.ServidorTextEdit;
            this.ItemForServidor.CustomizationFormText = "Servidor";
            this.ItemForServidor.Location = new System.Drawing.Point(0, 72);
            this.ItemForServidor.Name = "ItemForServidor";
            this.ItemForServidor.Size = new System.Drawing.Size(457, 24);
            this.ItemForServidor.Text = "Servidor";
            this.ItemForServidor.TextSize = new System.Drawing.Size(87, 13);
            // 
            // BaseDeDatosTextEdit
            // 
            this.BaseDeDatosTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.parametroBindingSource, "BaseDeDatos", true));
            this.BaseDeDatosTextEdit.Location = new System.Drawing.Point(94, 99);
            this.BaseDeDatosTextEdit.Name = "BaseDeDatosTextEdit";
            this.BaseDeDatosTextEdit.Size = new System.Drawing.Size(362, 20);
            this.BaseDeDatosTextEdit.StyleController = this.dataLayoutControl1;
            this.BaseDeDatosTextEdit.TabIndex = 9;
            // 
            // ItemForBaseDeDatos
            // 
            this.ItemForBaseDeDatos.Control = this.BaseDeDatosTextEdit;
            this.ItemForBaseDeDatos.CustomizationFormText = "Base De Datos";
            this.ItemForBaseDeDatos.Location = new System.Drawing.Point(0, 96);
            this.ItemForBaseDeDatos.Name = "ItemForBaseDeDatos";
            this.ItemForBaseDeDatos.Size = new System.Drawing.Size(457, 59);
            this.ItemForBaseDeDatos.Text = "Base De Datos";
            this.ItemForBaseDeDatos.TextSize = new System.Drawing.Size(87, 13);
            // 
            // Cancelar
            // 
            this.Cancelar.AutoSize = true;
            this.Cancelar.BackColor = System.Drawing.SystemColors.Control;
            this.Cancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancelar.Location = new System.Drawing.Point(332, 197);
            this.Cancelar.Name = "Cancelar";
            this.Cancelar.Size = new System.Drawing.Size(142, 39);
            this.Cancelar.TabIndex = 6;
            this.Cancelar.Text = "&Cancelar";
            this.Cancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Cancelar.UseVisualStyleBackColor = false;
            this.Cancelar.Click += new System.EventHandler(this.Cancelar_Click);
            // 
            // Aceptar
            // 
            this.Aceptar.AutoSize = true;
            this.Aceptar.BackColor = System.Drawing.SystemColors.Control;
            this.Aceptar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Aceptar.Location = new System.Drawing.Point(184, 199);
            this.Aceptar.Name = "Aceptar";
            this.Aceptar.Size = new System.Drawing.Size(142, 35);
            this.Aceptar.TabIndex = 5;
            this.Aceptar.Text = "Aceptar";
            this.Aceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Aceptar.UseVisualStyleBackColor = false;
            this.Aceptar.Click += new System.EventHandler(this.Aceptar_Click);
            // 
            // FrmParametros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 238);
            this.Controls.Add(this.Cancelar);
            this.Controls.Add(this.Aceptar);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmParametros";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Parametros";
            this.Load += new System.EventHandler(this.FrmParametros_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.parametroBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IdTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmpresaTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForEmpresa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmpresaDireccionTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForEmpresaDireccion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EquipoTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForEquipo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ServidorTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForServidor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BaseDeDatosTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForBaseDeDatos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.BindingSource parametroBindingSource;
        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraEditors.TextEdit IdTextEdit;
        private DevExpress.XtraEditors.TextEdit EmpresaTextEdit;
        private DevExpress.XtraEditors.TextEdit EmpresaDireccionTextEdit;
        private DevExpress.XtraEditors.TextEdit EquipoTextEdit;
        private DevExpress.XtraEditors.TextEdit ServidorTextEdit;
        private DevExpress.XtraEditors.TextEdit BaseDeDatosTextEdit;
        private DevExpress.XtraLayout.LayoutControlItem ItemForId;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem ItemForEmpresa;
        private DevExpress.XtraLayout.LayoutControlItem ItemForEmpresaDireccion;
        private DevExpress.XtraLayout.LayoutControlItem ItemForEquipo;
        private DevExpress.XtraLayout.LayoutControlItem ItemForServidor;
        private DevExpress.XtraLayout.LayoutControlItem ItemForBaseDeDatos;
        private System.Windows.Forms.Button Cancelar;
        private System.Windows.Forms.Button Aceptar;
    }
}