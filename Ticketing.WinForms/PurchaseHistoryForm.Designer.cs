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
            this.Load += new System.EventHandler(this.PurchaseHistoryForm_Load);
            // 
            // PurchaseHistoryForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1024, 450);
            Controls.Add(gridPurchases);
            Name = "PurchaseHistoryForm";
            Text = "Historial de Compras de Tickets";
            ((System.ComponentModel.ISupportInitialize)gridPurchases).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView gridPurchases;
    }
}