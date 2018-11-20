using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HK.Clases;

namespace HK.Formas
{
    public partial class FrmRetencionIVA : Form
    {
        Data d = new Data();
        private Retenciones registro;
        public List<Retenciones> data = new List<Retenciones>();
        string Mes = "";
        string Año = "";
        string Periodo = "";
        public FrmRetencionIVA()
        {
            InitializeComponent();
            this.Load += new EventHandler(FrmRetencionIVA_Load);
        }
        void FrmRetencionIVA_Load(object sender, EventArgs e)
        {

            this.cedulaRif.Validating += new CancelEventHandler(cedulaRif_Validating);
            this.cedulaRif.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(cedulaRif_ButtonClick);
            this.cedulaRif.Properties.ValidateOnEnterKey = true;
            this.Cancelar.Click += new EventHandler(Cancelar_Click);
            this.Aceptar.Click += new EventHandler(Aceptar_Click);
            PrimerItem();
            this.bs.DataSource = data;
            this.bsMaster.DataSource = registro;
            this.gridView1.Click += new EventHandler(gridView1_Click);
            this.Agregar.Click += new EventHandler(Agregar_Click);
            this.Quitar.Click += new EventHandler(Quitar_Click);
            
        }

        void Quitar_Click(object sender, EventArgs e)
        {
            if (bs.Current != null)
            {
                bs.Remove(bs.Current);
            }

        }
        void Agregar_Click(object sender, EventArgs e)
        {
            FrmRetencionIvaEdit f = new FrmRetencionIvaEdit();
            f.Editar(new Retenciones());
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                this.data.Add(f.registro);
                this.bs.DataSource = this.data;
                this.bs.ResetBindings( true);
            }
        }
        void gridView1_Click(object sender, EventArgs e)
        {
            Retenciones r = (Retenciones) bs.Current;
            FrmRetencionIvaEdit f = new FrmRetencionIvaEdit();
            if (bs.Current!=null)
            {
                f.Editar((Retenciones)bs.Current);
            }

        }
        void cedulaRif_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            BuscarCedula();
        }
        void PrimerItem()
        {
            registro = new Retenciones();
            registro.FechaDocumento = DateTime.Today;
            registro.FechaComprobante = DateTime.Today;
            registro.Alicuota = Basicas.parametros().TasaIva;
            registro.BaseImponible = 0;
            registro.MontoExentoIva = 0;
            registro.MontoIvaRetenido = 0;
            registro.NumeroComprobante = FactoryContadores.GetMaxComprobante(Mes, Año);
            registro.Id = FactoryContadores.GetMax("ID");
            registro.MontoDocumento = 0;
            registro.PeriodoImpositivo = Año + Mes;
            registro.RifAgenteRetencion = Basicas.parametros().CedulaRif;
            registro.TipoDocumento = "01";
            registro.TipoOperacion = "C";
            registro.NumeroExpediente = "0";
            registro.NumeroDocumentoAfectado = "0";
            registro.PorcentajeRetencion = Basicas.parametros().PorcentajeRetencion;
            if (DateTime.Today.Date.Day <= 15)
            {
                registro.Periodo = "PRIMER PERIODO";
            }
            else
            {
                registro.Periodo = "SEGUNDO PERIODO";
            }
        }
        void cedulaRif_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.TextEdit Editor = (DevExpress.XtraEditors.TextEdit)sender;
            if (!Editor.IsModified)
                return;
            BuscarCedula(Editor.Text);
        }
        void BuscarCedula(string Texto ="")
        {
            List<Proveedore> T = FactoryProveedores.getItems(Texto);
            switch (T.Count)
            {
                case 0:
                    registro.CedulaRif = Basicas.CedulaRif(Texto);
                    txtDireccion.Text = "";
                    break;
                case 1:
                    registro.CedulaRif = T[0].CedulaRif;
                    registro.NombreRazonSocial = T[0].RazonSocial;
                    txtDireccion.Text = T[0].Direccion;
                    break;
                default:
                    FrmBuscarEntidades F = new FrmBuscarEntidades();
                    F.BuscarTerceros(Texto);
                    if (F.registro != null)
                    {
                        registro.CedulaRif = ((Proveedore)F.registro).CedulaRif;
                        registro.NombreRazonSocial = ((Proveedore)F.registro).RazonSocial;
                        txtDireccion.Text = ((Proveedore)F.registro).Direccion;
                    }
                    else
                    {
                        registro.CedulaRif = null;
                        registro.NombreRazonSocial = null;
                        txtDireccion.Text = null;
                    }
                    break;
            }
        }
        void Aceptar_Click(object sender, EventArgs e)
        {
            this.bs.EndEdit();
            this.bsMaster.EndEdit();
            Guardar();
        }
        void Cancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        public void Nueva( string año, string mes)
        {
            this.Mes = mes;
            this.Año = año;
            this.ShowDialog();
        }
        private void Guardar()
        {  
            if (validar())
            {
                FactoryContadores.SetMax("COMPROBANTE_IVA");
                foreach (var item in data)
                {
                    item.CedulaRif = registro.CedulaRif;
                    item.FechaComprobante = registro.FechaComprobante;
                    item.NombreRazonSocial = registro.NombreRazonSocial;
                    item.NumeroComprobante = registro.NumeroComprobante;
                    item.Periodo = registro.Periodo;
                    item.PeriodoImpositivo= registro.PeriodoImpositivo;
                    item.RifAgenteRetencion = registro.RifAgenteRetencion;
                    item.Id = FactoryContadores.GetMax("id");
                }
                Data d = new Data();
                var p = (from x in d.Proveedores
                        where x.CedulaRif == registro.CedulaRif
                        select x).FirstOrDefault();
                if (p != null)
                {
                    p.Direccion = txtDireccion.Text;
                    d.SaveChanges();
                }
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }
        private bool validar()
        {
            registro = (Retenciones)this.bsMaster.Current;
            if (string.IsNullOrEmpty(registro.NumeroComprobante))
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
                if (string.IsNullOrEmpty(item.NumeroControlDocumento))
                {
                    MessageBox.Show("Error el numero de control no puede estar vacio");
                    return false; 
                }
                if (string.IsNullOrEmpty(item.NumeroDocumento))
                {
                    MessageBox.Show("Error el numero de documento no puede estar vacio");
                    return false;
                }
            }
            return true;
        }

    }
}
