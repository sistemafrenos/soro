﻿namespace HK.Formas
{
    partial class FrmProveedoresItem
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.BarraAcciones = new System.Windows.Forms.ToolStrip();
            this.Aceptar = new System.Windows.Forms.ToolStripButton();
            this.Cancelar = new System.Windows.Forms.ToolStripButton();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.IdProveedorTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.proveedoreBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.RazonSocialTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.DireccionTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.TelefonosTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.EmailTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.LimiteCreditoCalcEdit = new DevExpress.XtraEditors.CalcEdit();
            this.CedulaRifTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.DiasCreditoCalcEdit = new DevExpress.XtraEditors.CalcEdit();
            this.ContactoTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.ItemForIdProveedor = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.ItemForRazonSocial = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForDireccion = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForTelefonos = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForEmail = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForLimiteCredito = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForCedulaRif = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.ItemForDiasCredito = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForContacto = new DevExpress.XtraLayout.LayoutControlItem();
            this.BarraAcciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IdProveedorTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.proveedoreBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RazonSocialTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DireccionTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TelefonosTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmailTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LimiteCreditoCalcEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CedulaRifTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DiasCreditoCalcEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContactoTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForIdProveedor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForRazonSocial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForDireccion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTelefonos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForEmail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForLimiteCredito)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForCedulaRif)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForDiasCredito)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForContacto)).BeginInit();
            this.SuspendLayout();
            // 
            // BarraAcciones
            // 
            this.BarraAcciones.AutoSize = false;
            this.BarraAcciones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BarraAcciones.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.BarraAcciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Aceptar,
            this.Cancelar});
            this.BarraAcciones.Location = new System.Drawing.Point(0, 197);
            this.BarraAcciones.Name = "BarraAcciones";
            this.BarraAcciones.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.BarraAcciones.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.BarraAcciones.Size = new System.Drawing.Size(579, 58);
            this.BarraAcciones.TabIndex = 40;
            this.BarraAcciones.Text = "toolStrip1";
            // 
            // Aceptar
            // 
            this.Aceptar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Aceptar.Image = global::HK.Properties.Resources.disk_blue_ok;
            this.Aceptar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Aceptar.Name = "Aceptar";
            this.Aceptar.Size = new System.Drawing.Size(77, 55);
            this.Aceptar.Text = "Aceptar - F12";
            this.Aceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // Cancelar
            // 
            this.Cancelar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Cancelar.Image = global::HK.Properties.Resources.disk_blue_error;
            this.Cancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Cancelar.Name = "Cancelar";
            this.Cancelar.Size = new System.Drawing.Size(82, 55);
            this.Cancelar.Text = "Cancelar - ESC";
            this.Cancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.IdProveedorTextEdit);
            this.dataLayoutControl1.Controls.Add(this.RazonSocialTextEdit);
            this.dataLayoutControl1.Controls.Add(this.DireccionTextEdit);
            this.dataLayoutControl1.Controls.Add(this.TelefonosTextEdit);
            this.dataLayoutControl1.Controls.Add(this.EmailTextEdit);
            this.dataLayoutControl1.Controls.Add(this.LimiteCreditoCalcEdit);
            this.dataLayoutControl1.Controls.Add(this.CedulaRifTextEdit);
            this.dataLayoutControl1.Controls.Add(this.DiasCreditoCalcEdit);
            this.dataLayoutControl1.Controls.Add(this.ContactoTextEdit);
            this.dataLayoutControl1.DataSource = this.proveedoreBindingSource;
            this.dataLayoutControl1.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ItemForIdProveedor});
            this.dataLayoutControl1.Location = new System.Drawing.Point(12, 12);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.layoutControlGroup1;
            this.dataLayoutControl1.Size = new System.Drawing.Size(564, 184);
            this.dataLayoutControl1.TabIndex = 41;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // IdProveedorTextEdit
            // 
            this.IdProveedorTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.proveedoreBindingSource, "IdProveedor", true));
            this.IdProveedorTextEdit.Location = new System.Drawing.Point(0, 0);
            this.IdProveedorTextEdit.Name = "IdProveedorTextEdit";
            this.IdProveedorTextEdit.Size = new System.Drawing.Size(0, 20);
            this.IdProveedorTextEdit.StyleController = this.dataLayoutControl1;
            this.IdProveedorTextEdit.TabIndex = 4;
            // 
            // proveedoreBindingSource
            // 
            this.proveedoreBindingSource.DataSource = typeof(HK.Proveedore);
            // 
            // RazonSocialTextEdit
            // 
            this.RazonSocialTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.proveedoreBindingSource, "RazonSocial", true));
            this.RazonSocialTextEdit.Location = new System.Drawing.Point(71, 26);
            this.RazonSocialTextEdit.Name = "RazonSocialTextEdit";
            this.RazonSocialTextEdit.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.RazonSocialTextEdit.Size = new System.Drawing.Size(491, 20);
            this.RazonSocialTextEdit.StyleController = this.dataLayoutControl1;
            this.RazonSocialTextEdit.TabIndex = 5;
            // 
            // DireccionTextEdit
            // 
            this.DireccionTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.proveedoreBindingSource, "Direccion", true));
            this.DireccionTextEdit.Location = new System.Drawing.Point(71, 74);
            this.DireccionTextEdit.Name = "DireccionTextEdit";
            this.DireccionTextEdit.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.DireccionTextEdit.Size = new System.Drawing.Size(491, 20);
            this.DireccionTextEdit.StyleController = this.dataLayoutControl1;
            this.DireccionTextEdit.TabIndex = 7;
            // 
            // TelefonosTextEdit
            // 
            this.TelefonosTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.proveedoreBindingSource, "Telefonos", true));
            this.TelefonosTextEdit.Location = new System.Drawing.Point(71, 98);
            this.TelefonosTextEdit.Name = "TelefonosTextEdit";
            this.TelefonosTextEdit.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TelefonosTextEdit.Size = new System.Drawing.Size(491, 20);
            this.TelefonosTextEdit.StyleController = this.dataLayoutControl1;
            this.TelefonosTextEdit.TabIndex = 8;
            // 
            // EmailTextEdit
            // 
            this.EmailTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.proveedoreBindingSource, "Email", true));
            this.EmailTextEdit.Location = new System.Drawing.Point(71, 122);
            this.EmailTextEdit.Name = "EmailTextEdit";
            this.EmailTextEdit.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.EmailTextEdit.Size = new System.Drawing.Size(491, 20);
            this.EmailTextEdit.StyleController = this.dataLayoutControl1;
            this.EmailTextEdit.TabIndex = 9;
            // 
            // LimiteCreditoCalcEdit
            // 
            this.LimiteCreditoCalcEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.proveedoreBindingSource, "LimiteCredito", true));
            this.LimiteCreditoCalcEdit.Location = new System.Drawing.Point(71, 146);
            this.LimiteCreditoCalcEdit.Name = "LimiteCreditoCalcEdit";
            this.LimiteCreditoCalcEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.LimiteCreditoCalcEdit.Properties.DisplayFormat.FormatString = "n2";
            this.LimiteCreditoCalcEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.LimiteCreditoCalcEdit.Properties.EditFormat.FormatString = "n2";
            this.LimiteCreditoCalcEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.LimiteCreditoCalcEdit.Size = new System.Drawing.Size(209, 20);
            this.LimiteCreditoCalcEdit.StyleController = this.dataLayoutControl1;
            this.LimiteCreditoCalcEdit.TabIndex = 10;
            // 
            // CedulaRifTextEdit
            // 
            this.CedulaRifTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.proveedoreBindingSource, "CedulaRif", true));
            this.CedulaRifTextEdit.Location = new System.Drawing.Point(71, 2);
            this.CedulaRifTextEdit.Name = "CedulaRifTextEdit";
            this.CedulaRifTextEdit.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.CedulaRifTextEdit.Size = new System.Drawing.Size(208, 20);
            this.CedulaRifTextEdit.StyleController = this.dataLayoutControl1;
            this.CedulaRifTextEdit.TabIndex = 6;
            // 
            // DiasCreditoCalcEdit
            // 
            this.DiasCreditoCalcEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.proveedoreBindingSource, "DiasCredito", true));
            this.DiasCreditoCalcEdit.Location = new System.Drawing.Point(353, 146);
            this.DiasCreditoCalcEdit.Name = "DiasCreditoCalcEdit";
            this.DiasCreditoCalcEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
            this.DiasCreditoCalcEdit.Properties.DisplayFormat.FormatString = "n2";
            this.DiasCreditoCalcEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.DiasCreditoCalcEdit.Properties.EditFormat.FormatString = "n2";
            this.DiasCreditoCalcEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.DiasCreditoCalcEdit.Size = new System.Drawing.Size(209, 20);
            this.DiasCreditoCalcEdit.StyleController = this.dataLayoutControl1;
            this.DiasCreditoCalcEdit.TabIndex = 11;
            // 
            // ContactoTextEdit
            // 
            this.ContactoTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.proveedoreBindingSource, "Contacto", true));
            this.ContactoTextEdit.Location = new System.Drawing.Point(71, 50);
            this.ContactoTextEdit.Name = "ContactoTextEdit";
            this.ContactoTextEdit.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.ContactoTextEdit.Size = new System.Drawing.Size(491, 20);
            this.ContactoTextEdit.StyleController = this.dataLayoutControl1;
            this.ContactoTextEdit.TabIndex = 12;
            // 
            // ItemForIdProveedor
            // 
            this.ItemForIdProveedor.Control = this.IdProveedorTextEdit;
            this.ItemForIdProveedor.CustomizationFormText = "Id Proveedor";
            this.ItemForIdProveedor.Location = new System.Drawing.Point(0, 0);
            this.ItemForIdProveedor.Name = "ItemForIdProveedor";
            this.ItemForIdProveedor.Size = new System.Drawing.Size(0, 0);
            this.ItemForIdProveedor.Text = "Id Proveedor";
            this.ItemForIdProveedor.TextSize = new System.Drawing.Size(50, 20);
            this.ItemForIdProveedor.TextToControlDistance = 5;
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
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(564, 184);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.AllowDrawBackground = false;
            this.layoutControlGroup2.CustomizationFormText = "autoGeneratedGroup0";
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ItemForRazonSocial,
            this.ItemForDireccion,
            this.ItemForTelefonos,
            this.ItemForEmail,
            this.ItemForLimiteCredito,
            this.ItemForCedulaRif,
            this.emptySpaceItem1,
            this.ItemForDiasCredito,
            this.ItemForContacto});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "autoGeneratedGroup0";
            this.layoutControlGroup2.Size = new System.Drawing.Size(564, 184);
            this.layoutControlGroup2.Text = "autoGeneratedGroup0";
            // 
            // ItemForRazonSocial
            // 
            this.ItemForRazonSocial.Control = this.RazonSocialTextEdit;
            this.ItemForRazonSocial.CustomizationFormText = "Razon Social";
            this.ItemForRazonSocial.Location = new System.Drawing.Point(0, 24);
            this.ItemForRazonSocial.Name = "ItemForRazonSocial";
            this.ItemForRazonSocial.Size = new System.Drawing.Size(564, 24);
            this.ItemForRazonSocial.Text = "Razon Social";
            this.ItemForRazonSocial.TextSize = new System.Drawing.Size(65, 13);
            // 
            // ItemForDireccion
            // 
            this.ItemForDireccion.Control = this.DireccionTextEdit;
            this.ItemForDireccion.CustomizationFormText = "Direccion";
            this.ItemForDireccion.Location = new System.Drawing.Point(0, 72);
            this.ItemForDireccion.Name = "ItemForDireccion";
            this.ItemForDireccion.Size = new System.Drawing.Size(564, 24);
            this.ItemForDireccion.Text = "Direccion";
            this.ItemForDireccion.TextSize = new System.Drawing.Size(65, 13);
            // 
            // ItemForTelefonos
            // 
            this.ItemForTelefonos.Control = this.TelefonosTextEdit;
            this.ItemForTelefonos.CustomizationFormText = "Telefonos";
            this.ItemForTelefonos.Location = new System.Drawing.Point(0, 96);
            this.ItemForTelefonos.Name = "ItemForTelefonos";
            this.ItemForTelefonos.Size = new System.Drawing.Size(564, 24);
            this.ItemForTelefonos.Text = "Telefonos";
            this.ItemForTelefonos.TextSize = new System.Drawing.Size(65, 13);
            // 
            // ItemForEmail
            // 
            this.ItemForEmail.Control = this.EmailTextEdit;
            this.ItemForEmail.CustomizationFormText = "Email";
            this.ItemForEmail.Location = new System.Drawing.Point(0, 120);
            this.ItemForEmail.Name = "ItemForEmail";
            this.ItemForEmail.Size = new System.Drawing.Size(564, 24);
            this.ItemForEmail.Text = "Email";
            this.ItemForEmail.TextSize = new System.Drawing.Size(65, 13);
            // 
            // ItemForLimiteCredito
            // 
            this.ItemForLimiteCredito.Control = this.LimiteCreditoCalcEdit;
            this.ItemForLimiteCredito.CustomizationFormText = "Limite Credito";
            this.ItemForLimiteCredito.Location = new System.Drawing.Point(0, 144);
            this.ItemForLimiteCredito.Name = "ItemForLimiteCredito";
            this.ItemForLimiteCredito.Size = new System.Drawing.Size(282, 40);
            this.ItemForLimiteCredito.Text = "Limite Credito";
            this.ItemForLimiteCredito.TextSize = new System.Drawing.Size(65, 13);
            // 
            // ItemForCedulaRif
            // 
            this.ItemForCedulaRif.Control = this.CedulaRifTextEdit;
            this.ItemForCedulaRif.CustomizationFormText = "Cedula Rif";
            this.ItemForCedulaRif.Location = new System.Drawing.Point(0, 0);
            this.ItemForCedulaRif.Name = "ItemForCedulaRif";
            this.ItemForCedulaRif.Size = new System.Drawing.Size(281, 24);
            this.ItemForCedulaRif.Text = "Cedula Rif";
            this.ItemForCedulaRif.TextSize = new System.Drawing.Size(65, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(281, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(283, 24);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // ItemForDiasCredito
            // 
            this.ItemForDiasCredito.Control = this.DiasCreditoCalcEdit;
            this.ItemForDiasCredito.CustomizationFormText = "Dias Credito";
            this.ItemForDiasCredito.Location = new System.Drawing.Point(282, 144);
            this.ItemForDiasCredito.Name = "ItemForDiasCredito";
            this.ItemForDiasCredito.Size = new System.Drawing.Size(282, 40);
            this.ItemForDiasCredito.Text = "Dias Credito";
            this.ItemForDiasCredito.TextSize = new System.Drawing.Size(65, 13);
            // 
            // ItemForContacto
            // 
            this.ItemForContacto.Control = this.ContactoTextEdit;
            this.ItemForContacto.CustomizationFormText = "Contacto";
            this.ItemForContacto.Location = new System.Drawing.Point(0, 48);
            this.ItemForContacto.Name = "ItemForContacto";
            this.ItemForContacto.Size = new System.Drawing.Size(564, 24);
            this.ItemForContacto.Text = "Contacto";
            this.ItemForContacto.TextSize = new System.Drawing.Size(65, 13);
            // 
            // FrmProveedoresItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 255);
            this.Controls.Add(this.dataLayoutControl1);
            this.Controls.Add(this.BarraAcciones);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmProveedoresItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Proveedor";
            this.Load += new System.EventHandler(this.FrmProveedoresItem_Load);
            this.BarraAcciones.ResumeLayout(false);
            this.BarraAcciones.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.IdProveedorTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.proveedoreBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RazonSocialTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DireccionTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TelefonosTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmailTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LimiteCreditoCalcEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CedulaRifTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DiasCreditoCalcEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContactoTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForIdProveedor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForRazonSocial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForDireccion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTelefonos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForEmail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForLimiteCredito)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForCedulaRif)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForDiasCredito)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForContacto)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ToolStrip BarraAcciones;
        private System.Windows.Forms.ToolStripButton Aceptar;
        private System.Windows.Forms.ToolStripButton Cancelar;
        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraEditors.TextEdit IdProveedorTextEdit;
        private System.Windows.Forms.BindingSource proveedoreBindingSource;
        private DevExpress.XtraEditors.TextEdit RazonSocialTextEdit;
        private DevExpress.XtraEditors.TextEdit CedulaRifTextEdit;
        private DevExpress.XtraEditors.TextEdit DireccionTextEdit;
        private DevExpress.XtraEditors.TextEdit TelefonosTextEdit;
        private DevExpress.XtraEditors.TextEdit EmailTextEdit;
        private DevExpress.XtraEditors.CalcEdit LimiteCreditoCalcEdit;
        private DevExpress.XtraEditors.CalcEdit DiasCreditoCalcEdit;
        private DevExpress.XtraEditors.TextEdit ContactoTextEdit;
        private DevExpress.XtraLayout.LayoutControlItem ItemForIdProveedor;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem ItemForRazonSocial;
        private DevExpress.XtraLayout.LayoutControlItem ItemForDireccion;
        private DevExpress.XtraLayout.LayoutControlItem ItemForTelefonos;
        private DevExpress.XtraLayout.LayoutControlItem ItemForEmail;
        private DevExpress.XtraLayout.LayoutControlItem ItemForLimiteCredito;
        private DevExpress.XtraLayout.LayoutControlItem ItemForCedulaRif;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem ItemForDiasCredito;
        private DevExpress.XtraLayout.LayoutControlItem ItemForContacto;
    }
}