namespace Ticketing.WinForms
{
    partial class LoginForm
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
            lblCorreo = new Label();
            txtEmail = new TextBox();
            lblContrasena = new Label();
            txtPassword = new TextBox();
            btnLogin = new Button();
            pictureBox3 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            btnRegister = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // lblCorreo
            // 
            lblCorreo.AutoSize = true;
            lblCorreo.BackColor = Color.Transparent;
            lblCorreo.Location = new Point(151, 34);
            lblCorreo.Name = "lblCorreo";
            lblCorreo.Size = new Size(57, 20);
            lblCorreo.TabIndex = 0;
            lblCorreo.Text = "Correo:";
            // 
            // txtEmail
            // 
            txtEmail.BackColor = SystemColors.Window;
            txtEmail.Location = new Point(75, 57);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(207, 27);
            txtEmail.TabIndex = 1;
            // 
            // lblContrasena
            // 
            lblContrasena.AutoSize = true;
            lblContrasena.BackColor = Color.Transparent;
            lblContrasena.Location = new Point(133, 108);
            lblContrasena.Name = "lblContrasena";
            lblContrasena.Size = new Size(86, 20);
            lblContrasena.TabIndex = 2;
            lblContrasena.Text = "Contraseña:";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(75, 146);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(207, 27);
            txtPassword.TabIndex = 3;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.Transparent;
            btnLogin.Location = new Point(111, 228);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(125, 29);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Iniciar Seción";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.Transparent;
            pictureBox3.Image = Properties.Resources.login;
            pictureBox3.Location = new Point(31, 218);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(58, 53);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 7;
            pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Image = Properties.Resources.email;
            pictureBox2.Location = new Point(9, 34);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(56, 57);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 8;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = Properties.Resources.contraseña;
            pictureBox1.Location = new Point(9, 127);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(60, 64);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 9;
            pictureBox1.TabStop = false;
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(244, 275);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(94, 29);
            btnRegister.TabIndex = 10;
            btnRegister.Text = "Registrarse";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // LoginForm
            // 
            AcceptButton = btnLogin;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.RosyBrown;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(371, 316);
            Controls.Add(btnRegister);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox3);
            Controls.Add(btnLogin);
            Controls.Add(txtPassword);
            Controls.Add(lblContrasena);
            Controls.Add(txtEmail);
            Controls.Add(lblCorreo);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Name = "LoginForm";
            Text = "Iniciar Secion";
            Load += LoginForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCorreo;
        private TextBox txtEmail;
        private Label lblContrasena;
        private TextBox txtPassword;
        private Button btnLogin;
        private PictureBox pictureBox3;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private Button btnRegister;
    }
}