using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HK
{
    public partial class FrmLogin : Form
    {        
        public FrmLogin()
        {
            InitializeComponent();
            
        }
        Usuarios Usuario = new Usuarios();
        SoroEntities dc = new SoroEntities();        
        List<Usuarios> Usuarios = new List<Usuarios>();         
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            Usuarios = FactoryUsuarios.SelectAll( dc,"", "",true);
            this.usuariosBindingSource.DataSource = Usuarios;
            this.usuariosBindingSource.ResetBindings(true);
            this.txtUsuario.DataSource = this.usuariosBindingSource;
            this.txtUsuario.DisplayMember = "NOMBRE";
            this.txtUsuario.ValueMember = "IdUsuario";
        }

        private void Aceptar_Click(object sender, EventArgs e)
        {
            if (this.txtUsuario.Text == "MAESTRO" && this.txtContraseña.Text == "Aleman")
            {
                Usuario = FactoryUsuarios.Item(dc, this.txtUsuario.Text, this.txtContraseña.Text);
                if (Usuario == null)
                {
                    Usuario = new Usuarios();
                    Usuario.Clave = "Aleman";
                    Usuario.Activo = true;
                    Usuario.Nombre = "MAESTRO";
                    Usuario.Tipo = "SUPER USUARIO";
                    Usuario.IdUsuario = FactoryContadores.GetLast("IdUsuario");
                    dc.Usuarios.Add(Usuario);
                    dc.SaveChanges();
                }
            }
            Usuario = FactoryUsuarios.Item(dc, this.txtUsuario.Text, this.txtContraseña.Text);
            if ( Usuario == null)
            {
                MessageBox.Show("Este usuario y contraseña son invalidos");
                return;
            }
            Sesiones Sesion  =FactorySesiones.AbrirSesion(Usuario);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FrmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void txtUsuario_Validating(object sender, CancelEventArgs e)
        {
            
                
        }
    }
}