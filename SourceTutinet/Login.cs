using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using T.Modelo;
using T.Negocio;

namespace SourceTutinet
{
    public partial class Login : Form
    {
        EmpleadoNegocios empN = new EmpleadoNegocios();
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario, contrasena;
            Empleado emp;
            usuario = txtUser.Text;
            contrasena = Encrypt.GetSHA256(txtPassword.Text);

            emp = empN.getValidacion(usuario,contrasena);

            if (emp != null) {
                if (emp.empleado_estado==1)
                {
                    MessageBox.Show("Bienvenido Sr." + emp.empleado_nombre.ToString() + " " + emp.empleado_apellido);
                    this.Hide();
                    Main m = new Main(emp);
                    m.Show();
                }
                else
                {
                    MessageBox.Show("El usuario esta desactivado");
                }

            }
            else
            {
                MessageBox.Show("Usuario o Contraseña incorrecta");
            }

        }
    }
}
