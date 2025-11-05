namespace Ticketing.WinForms
{
    partial class PurchaseHistoryForm
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
            gridPurchases = new DataGridView();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)gridPurchases).BeginInit();
            SuspendLayout();
            // 
            // gridPurchases
            // 
            gridPurchases.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridPurchases.Location = new Point(12, 29);
            gridPurchases.Name = "gridPurchases";
            gridPurchases.RowHeadersWidth = 51;
            gridPurchases.Size = new Size(981, 360);
            gridPurchases.TabIndex = 0;
            gridPurchases.CellContentClick += gridPurchases_CellContentClick;
            // 
            // button1
            // 
            button1.Location = new Point(414, 409);
            button1.Name = "button1";
            button1.Size = new Size(176, 29);
            button1.TabIndex = 1;
            button1.Text = "Descargar Ticket";
            button1.UseVisualStyleBackColor = true;
            // 
            // PurchaseHistoryForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1024, 450);
            Controls.Add(button1);
            Controls.Add(gridPurchases);
            Name = "PurchaseHistoryForm";
            Text = "Historial de Compras de Tickets";
            Load += PurchaseHistoryForm_Load;
            ((System.ComponentModel.ISupportInitialize)gridPurchases).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView gridPurchases;
        private Button button1;
    }
}