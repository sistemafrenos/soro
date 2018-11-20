namespace HK.Formas
{
    partial class FrmPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipal));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonRetencionesIVA = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonRetencionesISLR = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonRespaldo = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonRecuperacion = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonParametros = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonProveedores = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ApplicationButtonText = null;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonRetencionesIVA,
            this.barButtonRetencionesISLR,
            this.barButtonRespaldo,
            this.barButtonRecuperacion,
            this.barButtonParametros,
            this.barButtonProveedores});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 6;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.SelectedPage = this.ribbonPage1;
            this.ribbon.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.ribbon.Size = new System.Drawing.Size(724, 148);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            this.ribbon.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Above;
            // 
            // barButtonRetencionesIVA
            // 
            this.barButtonRetencionesIVA.Caption = "Retenciones IVA";
            this.barButtonRetencionesIVA.Id = 0;
            this.barButtonRetencionesIVA.Name = "barButtonRetencionesIVA";
            // 
            // barButtonRetencionesISLR
            // 
            this.barButtonRetencionesISLR.Caption = "Retenciones ISLR";
            this.barButtonRetencionesISLR.Id = 1;
            this.barButtonRetencionesISLR.Name = "barButtonRetencionesISLR";
            // 
            // barButtonRespaldo
            // 
            this.barButtonRespaldo.Caption = "Respaldo";
            this.barButtonRespaldo.Id = 2;
            this.barButtonRespaldo.Name = "barButtonRespaldo";
            // 
            // barButtonRecuperacion
            // 
            this.barButtonRecuperacion.Caption = "Recuperacion";
            this.barButtonRecuperacion.Id = 3;
            this.barButtonRecuperacion.Name = "barButtonRecuperacion";
            // 
            // barButtonParametros
            // 
            this.barButtonParametros.Caption = "Parametros";
            this.barButtonParametros.Id = 4;
            this.barButtonParametros.Name = "barButtonParametros";
            // 
            // barButtonProveedores
            // 
            this.barButtonProveedores.Caption = "Proveedores";
            this.barButtonProveedores.Id = 5;
            this.barButtonProveedores.Name = "barButtonProveedores";
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup2});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "ribbonPage1";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonRetencionesIVA);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonRetencionesISLR);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Retenciones";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonRespaldo);
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonRecuperacion);
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonProveedores);
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonParametros);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "Utilidades";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 426);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(724, 23);
            // 
            // xtraTabbedMdiManager1
            // 
            this.xtraTabbedMdiManager1.MdiParent = this;
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Lilian";
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 449);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "FrmPrincipal";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "Ok Retenciones";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarButtonItem barButtonRetencionesIVA;
        private DevExpress.XtraBars.BarButtonItem barButtonRetencionesISLR;
        private DevExpress.XtraBars.BarButtonItem barButtonRespaldo;
        private DevExpress.XtraBars.BarButtonItem barButtonRecuperacion;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.BarButtonItem barButtonParametros;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        private DevExpress.XtraBars.BarButtonItem barButtonProveedores;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
    }
}