using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ticketing.Domain.Models;

namespace Ticketing.WinForms
{
    public partial class LoginForm : Form
    {
        private readonly AppServices _services;

        public LoginForm(AppServices services)
        {
            InitializeComponent();
            _services = services;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            var email = txtEmail.Text.Trim();
            var pass = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Por favor ingrese su correo y contraseña.");
                return;
            }

            var user = await _services.UserService.LoginAsync(email, pass);
            if (user is null)
            {
                MessageBox.Show("Lo sentimos sus credenciales son incorrectas.");
                return;
            }

            MessageBox.Show($"Hola! Bienvenido {user.Name}! Rol: {user.Role}");

            Hide();
            using (var frm = new EventListForm(_services, user))
            {
                frm.ShowDialog();
            }
            Close();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            using (var frm = new RegisterForm(_services))
            {
                var result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // Registro exitoso: informar al usuario y preparar el formulario para iniciar sesión.
                    MessageBox.Show("Registro completado correctamente. Inicie sesión con sus nuevas credenciales.", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPassword.Clear();
                    txtEmail.Focus();
                }
            }
            }
    }
}
