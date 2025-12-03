using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ticketing.Domain.Models;

namespace Ticketing.WinForms
{
    public partial class RegisterForm : Form
    {
        private readonly AppServices _services;

        public RegisterForm(AppServices services)
        {
            InitializeComponent();
            _services = services;
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            var name = txtName.Text.Trim();
            var email = txtEmail.Text.Trim();
            var password = txtPassword.Text;
            var confirm = txtConfirmPassword?.Text ?? string.Empty;
            var role = (cmbRole?.SelectedItem?.ToString() ?? "User").Trim();

            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Por favor complete todos los campos obligatorios.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar formato de correo electrónico
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Por favor ingrese un correo electrónico válido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirm)
            {
                MessageBox.Show("Lo sentimos, las contraseñas no coinciden, vuelve a intentarlo.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // comprobación simple de longitud de contraseña
            if (password.Length < 4)
            {
                MessageBox.Show("La contraseña debe tener al menos 4 caracteres.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var user = new User
            {
                Name = name,
                Email = email,
                Role = string.IsNullOrWhiteSpace(role) ? "User" : role
            };

            try
            {
                // Ahora pasamos la contraseña en claro; UserService la hasheará.
                await _services.UserService.RegisterAsync(user, password);
                MessageBox.Show("Felicidades, EL registro completado correctamente.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al registrar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
