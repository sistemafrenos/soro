using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HK;
using HK.Clases;


namespace HK.Formas
{
    public partial class FrmRetencionesISLRItem : Form
    {
        public RetencionesISLR registro = new RetencionesISLR();
        string Mes = "";
        string Año = "";
        string Periodo = "";
        Data d = new Data();
        public List<RetencionesISLR> data = new List<RetencionesISLR>();
        public FrmRetencionesISLRItem()
        {
            InitializeComponent();
            this.Load += new EventHandler(FrmRetencionesISLRItem_Load);
        }
        void FrmRetencionesISLRItem_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Frm_KeyDown);
            this.Aceptar.Click += new EventHandler(Aceptar_Click);
            this.Cancelar.Click += new EventHandler(Cancelar_Click);
            this.CedulaRifButtonEdit.Validating += new CancelEventHandler(CedulaRifTextEdit_Validating);
            this.CedulaRifButtonEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(CedulaRifTextEdit_ButtonClick);
            this.agregar.Click += new EventHandler(agregar_Click);
            this.quitar.Click += new EventHandler(quitar_Click);
            this.gridView1.Click += new EventHandler(gridView1_Click);
        }
        void quitar_Click(object sender, EventArgs e)
        {
            if (bs.Current != null)
            {
                bs.Remove(bs.Current);
            }

        }
        void agregar_Click(object sender, EventArgs e)
        {
            FrmRetencionsISLRitemEdit f = new FrmRetencionsISLRitemEdit();
            f.Editar(new RetencionesISLR());
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                this.data.Add(f.registro);
                this.bs.DataSource = this.data;
                this.bs.ResetBindings(true);
            }
        }
        void gridView1_Click(object sender, EventArgs e)
        {
            var r = (RetencionesISLR)bs.Current;
            if (r != null)
            {
                FrmRetencionsISLRitemEdit f = new FrmRetencionsISLRitemEdit();
                f.Editar((RetencionesISLR)bs.Current);
            }
        }
        void CedulaRifTextEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmBuscarEntidades F = new FrmBuscarEntidades();
            F.BuscarTerceros("");
            if (F.registro != null)
            {
                registro.CedulaRif = ((Proveedore)F.registro).CedulaRif;
                registro.NombreRazonSocial = ((Proveedore)F.registro).RazonSocial;
                registro.Direccion = ((Proveedore)F.registro).Direccion;
            }
            else
            {
                registro.CedulaRif = null;
                registro.NombreRazonSocial = null;
                registro.Direccion =null;
            }
        }
        void CedulaRifTextEdit_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.TextEdit Editor = (DevExpress.XtraEditors.TextEdit)sender;
            if (!Editor.IsModified)
                return;
            string Texto = Editor.Text;
            List<Proveedore> T = FactoryProveedores.getItems(Texto);
            switch (T.Count)
            {
                case 0:
                    Editor.Text = Basicas.CedulaRif(Texto);
                    registro.CedulaRif = Editor.Text;
                    break;
                case 1:
                    registro.CedulaRif = T[0].CedulaRif;
                    registro.NombreRazonSocial = T[0].RazonSocial;
                    registro.Direccion = T[0].Direccion;
                    break;
                default:
                    FrmBuscarEntidades F = new FrmBuscarEntidades();
                    F.BuscarTerceros(Texto);
                    if (F.registro != null)
                    {
                        registro.CedulaRif = ((Proveedore)F.registro).CedulaRif;
                        registro.NombreRazonSocial = ((Proveedore)F.registro).RazonSocial;
                        registro.Direccion = ((Proveedore)F.registro).Direccion;
                    }
                    else
                    {
                        registro.CedulaRif = null;
                        registro.NombreRazonSocial = null;
                        registro.Direccion = null;
                    }
                    break;
            }
        }
        private void Limpiar()
        {
            registro = new RetencionesISLR();
            registro.Fecha = DateTime.Today;
            registro.FechaFactura = DateTime.Today;
            registro.BaseImponible = 0;
            registro.Numero = FactoryContadores.GetMaxComprobanteISLR(Mes,Año);
            registro.IdRetencionISLR = FactoryContadores.GetMax("IdRetencionISLR");
            registro.MontoDocumento = 0;
            registro.Periodo = Año + Mes;
            registro.PorcentajeRetencion = 3 / 100;
            registro.Sustraendo = 0;

        }
        public void Incluir(string mes, string año)
        {
            Mes = mes;
            Año = año;
            Limpiar();
            Enlazar();
            this.ShowDialog();
        }
        public void Modificar()
        {
            Enlazar();
            this.ShowDialog();
        }
        private void Enlazar()
        {
            if (registro == null)
            {
                Limpiar();
            }
            this.retencionesISLRBindingSource.DataSource = registro;
            this.retencionesISLRBindingSource.ResetBindings(true);
        }
        private void Aceptar_Click(object sender, EventArgs e)
        {
            try
            {
                retencionesISLRBindingSource.EndEdit();
                registro.CedulaRif = Basicas.CedulaRif(registro.CedulaRif);
                registro = (RetencionesISLR)retencionesISLRBindingSource.Current;
                Guardar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar los datos \n" + ex.Source + "\n" + ex.Message, "Atencion", MessageBoxButtons.OK);
            }
        }
        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.retencionesISLRBindingSource.ResetCurrentItem();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void Frm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Cancelar.PerformClick();
                    e.Handled = true;
                    break;
                case Keys.F12:
                    this.Aceptar.PerformClick();
                    e.Handled = true;
                    break;
            }
        }
        private void Guardar()
        {
            if (validar())
            {
                FactoryContadores.SetMax("COMPROBANTE_ISLR");
                Proveedore proveedor = FactoryProveedores.ItemCedulaRif(d, registro.CedulaRif);
                if (proveedor == null)
                {
                    proveedor = new Proveedore();
                    proveedor.Activo = true;
                }
                proveedor.CedulaRif = registro.CedulaRif;
                proveedor.RazonSocial = registro.NombreRazonSocial;
                proveedor.Direccion = registro.Direccion;
                FactoryProveedores.Validar(proveedor);
                if (proveedor.IdProveedor == null)
                {
                    proveedor.IdProveedor = FactoryContadores.GetMax("IdProveedor");
                    d.Proveedores.AddObject(proveedor);
                }
                foreach (var item in data)
                {
                    item.Fecha = registro.Fecha;
                    item.CedulaRif = registro.CedulaRif;
                    item.Control = item.Control;
                    //item.FechaFactura = registro.FechaFactura;
                    item.NombreRazonSocial = registro.NombreRazonSocial;
                    item.CedulaRif = registro.CedulaRif;
                    item.Periodo = registro.Periodo;
                    item.Direccion = registro.Direccion;
                    item.Numero = registro.Numero;
                    item.IdRetencionISLR = FactoryContadores.GetMax("id");
                }
                var p = (from x in d.Proveedores
                         where x.CedulaRif == registro.CedulaRif
                         select x).FirstOrDefault();
                if (p != null)
                {
                    p.Direccion = DireccionTextEdit.Text;
                    d.SaveChanges();
                }
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }
        private bool validar()
        {
            registro = (RetencionesISLR)this.retencionesISLRBindingSource.Current;
            if (string.IsNullOrEmpty(registro.Numero))
            {
                MessageBox.Show("Error el numero de comprobante no puede estar vacio");
                return false;
            }
            if (string.IsNullOrEmpty(registro.CedulaRif))
            {
                MessageBox.Show("Error el numero de cedula o rif no puede estar vacio");
                return false;
            }
            if (string.IsNullOrEmpty(registro.NombreRazonSocial))
            {
                MessageBox.Show("Error el nombre o Razon Social no puede estar vacia");
                return false;
            }
            foreach (var item in data)
            {
                if (string.IsNullOrEmpty(item.NumeroFactura))
                {
                    MessageBox.Show("Error el numero de control no puede estar vacio");
                    return false;
                }
            }
            return true;
        }
    }
}
