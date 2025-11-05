namespace Ticketing.WinForms
{
    partial class RegisterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label5 = new Label();
            label3 = new Label();
            label4 = new Label();
            txtName = new TextBox();
            txtEmail = new TextBox();
            cmbRole = new ComboBox();
            txtPassword = new TextBox();
            txtConfirmPassword = new TextBox();
            btnCancel = new Button();
            btnRegister = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(27, 9);
            label1.Name = "label1";
            label1.Size = new Size(67, 20);
            label1.TabIndex = 0;
            label1.Text = "Nombre:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(27, 66);
            label2.Name = "label2";
            label2.Size = new Size(57, 20);
            label2.TabIndex = 1;
            label2.Text = "Correo:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(27, 116);
            label5.Name = "label5";
            label5.Size = new Size(34, 20);
            label5.TabIndex = 2;
            label5.Text = "Rol:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(27, 182);
            label3.Name = "label3";
            label3.Size = new Size(86, 20);
            label3.TabIndex = 3;
            label3.Text = "Contraseña:";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(27, 235);
            label4.Name = "label4";
            label4.Size = new Size(156, 20);
            label4.TabIndex = 4;
            label4.Text = "Confirmar Contraseña:";
            // 
            // txtName
            // 
            txtName.Location = new Point(27, 36);
            txtName.Name = "txtName";
            txtName.Size = new Size(230, 27);
            txtName.TabIndex = 5;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(27, 86);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(230, 27);
            txtEmail.TabIndex = 6;
            // 
            // cmbRole
            // 
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.FormattingEnabled = true;
            cmbRole.Items.AddRange(new object[] { "User", "Admin" });
            cmbRole.Location = new Point(27, 139);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new Size(230, 28);
            cmbRole.TabIndex = 7;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(27, 205);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(230, 27);
            txtPassword.TabIndex = 8;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.Location = new Point(27, 258);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.Size = new Size(230, 27);
            txtConfirmPassword.TabIndex = 9;
            txtConfirmPassword.UseSystemPasswordChar = true;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(27, 308);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(94, 29);
            btnCancel.TabIndex = 10;
            btnCancel.Text = "Cancelar";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(163, 308);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(94, 29);
            btnRegister.TabIndex = 11;
            btnRegister.Text = "Confirmar";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 349);
            Controls.Add(btnRegister);
            Controls.Add(btnCancel);
            Controls.Add(txtConfirmPassword);
            Controls.Add(txtPassword);
            Controls.Add(cmbRole);
            Controls.Add(txtEmail);
            Controls.Add(txtName);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label5);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "RegisterForm";
            Text = "Register";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox txtName;
        private TextBox txtEmail;
        private ComboBox cmbRole;
        private TextBox txtPassword;
        private TextBox txtConfirmPassword;
        private Button btnCancel;
        private Button btnRegister;
    }
}