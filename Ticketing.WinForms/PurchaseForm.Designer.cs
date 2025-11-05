namespace Ticketing.WinForms
{
    partial class PurchaseForm
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
            lblTitle = new Label();
            label2 = new Label();
            lblStock = new Label();
            label4 = new Label();
            lblPrice = new Label();
            label6 = new Label();
            numQty = new NumericUpDown();
            label7 = new Label();
            txtCard = new TextBox();
            btnBuy = new Button();
            ((System.ComponentModel.ISupportInitialize)numQty).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(30, 22);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(57, 20);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Evento:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(34, 91);
            label2.Name = "label2";
            label2.Size = new Size(90, 20);
            label2.TabIndex = 1;
            label2.Text = "Disponibles:";
            // 
            // lblStock
            // 
            lblStock.AutoSize = true;
            lblStock.Location = new Point(130, 91);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(62, 20);
            lblStock.TabIndex = 2;
            lblStock.Text = "lblStock";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(34, 145);
            label4.Name = "label4";
            label4.Size = new Size(53, 20);
            label4.TabIndex = 3;
            label4.Text = "Precio:";
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Location = new Point(130, 145);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(58, 20);
            lblPrice.TabIndex = 4;
            lblPrice.Text = "lblPrice";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(34, 203);
            label6.Name = "label6";
            label6.Size = new Size(72, 20);
            label6.TabIndex = 5;
            label6.Text = "Cantidad:";
            // 
            // numQty
            // 
            numQty.Location = new Point(34, 248);
            numQty.Name = "numQty";
            numQty.Size = new Size(150, 27);
            numQty.TabIndex = 6;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(34, 292);
            label7.Name = "label7";
            label7.Size = new Size(56, 20);
            label7.TabIndex = 7;
            label7.Text = "Tarjeta:";
            // 
            // txtCard
            // 
            txtCard.Location = new Point(34, 326);
            txtCard.Name = "txtCard";
            txtCard.Size = new Size(187, 27);
            txtCard.TabIndex = 8;
            // 
            // btnBuy
            // 
            btnBuy.Location = new Point(34, 380);
            btnBuy.Name = "btnBuy";
            btnBuy.Size = new Size(94, 29);
            btnBuy.TabIndex = 9;
            btnBuy.Text = "Comprar";
            btnBuy.UseVisualStyleBackColor = true;
            btnBuy.Click += btnBuy_Click;
            // 
            // PurchaseForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(370, 450);
            Controls.Add(btnBuy);
            Controls.Add(txtCard);
            Controls.Add(label7);
            Controls.Add(numQty);
            Controls.Add(label6);
            Controls.Add(lblPrice);
            Controls.Add(label4);
            Controls.Add(lblStock);
            Controls.Add(label2);
            Controls.Add(lblTitle);
            Name = "PurchaseForm";
            Text = "PurchaseForm";
            Load += PurchaseForm_Load;
            ((System.ComponentModel.ISupportInitialize)numQty).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label label2;
        private Label lblStock;
        private Label label4;
        private Label lblPrice;
        private Label label6;
        private NumericUpDown numQty;
        private Label label7;
        private TextBox txtCard;
        private Button btnBuy;
    }
}