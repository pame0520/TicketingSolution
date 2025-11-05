namespace Ticketing.WinForms
{
    partial class EventListForm
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
            txtSearch = new TextBox();
            label1 = new Label();
            btnSearch = new Button();
            gridEvents = new DataGridView();
            btnMy = new Button();
            btnClose = new Button();
            btnAddEvent = new Button();
            btnEditEvent = new Button();
            btnDeleteEvent = new Button();
            ((System.ComponentModel.ISupportInitialize)gridEvents).BeginInit();
            SuspendLayout();
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(12, 40);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(195, 27);
            txtSearch.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(67, 20);
            label1.TabIndex = 1;
            label1.Text = "Nombre:";
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(213, 38);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(94, 29);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Buscar";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // gridEvents
            // 
            gridEvents.BackgroundColor = Color.RosyBrown;
            gridEvents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridEvents.Location = new Point(12, 95);
            gridEvents.Name = "gridEvents";
            gridEvents.RowHeadersWidth = 51;
            gridEvents.Size = new Size(969, 314);
            gridEvents.TabIndex = 3;
            gridEvents.CellDoubleClick += gridEvents_CellDoubleClick;
            // 
            // btnMy
            // 
            btnMy.Location = new Point(313, 38);
            btnMy.Name = "btnMy";
            btnMy.Size = new Size(182, 29);
            btnMy.TabIndex = 4;
            btnMy.Text = "Mis reservas de Tickets";
            btnMy.UseVisualStyleBackColor = true;
            btnMy.Click += btnMy_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(811, 415);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(151, 29);
            btnClose.TabIndex = 5;
            btnClose.Text = "Volver al Login";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // btnAddEvent
            // 
            btnAddEvent.Location = new Point(574, 28);
            btnAddEvent.Name = "btnAddEvent";
            btnAddEvent.Size = new Size(94, 29);
            btnAddEvent.TabIndex = 6;
            btnAddEvent.Text = "Agregar evento";
            btnAddEvent.UseVisualStyleBackColor = true;
            btnAddEvent.Click += btnAddEvent_Click;
            // 
            // btnEditEvent
            // 
            btnEditEvent.Location = new Point(724, 30);
            btnEditEvent.Name = "btnEditEvent";
            btnEditEvent.Size = new Size(94, 29);
            btnEditEvent.TabIndex = 7;
            btnEditEvent.Text = "Editar evento";
            btnEditEvent.UseVisualStyleBackColor = true;
            btnEditEvent.Click += btnEditEvent_Click;
            // 
            // btnDeleteEvent
            // 
            btnDeleteEvent.Location = new Point(854, 30);
            btnDeleteEvent.Name = "btnDeleteEvent";
            btnDeleteEvent.Size = new Size(94, 29);
            btnDeleteEvent.TabIndex = 8;
            btnDeleteEvent.Text = "Eliminar evento";
            btnDeleteEvent.UseVisualStyleBackColor = true;
            btnDeleteEvent.Click += btnDeleteEvent_Click;
            // 
            // EventListForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.RosyBrown;
            ClientSize = new Size(993, 450);
            Controls.Add(btnDeleteEvent);
            Controls.Add(btnEditEvent);
            Controls.Add(btnAddEvent);
            Controls.Add(btnClose);
            Controls.Add(btnMy);
            Controls.Add(gridEvents);
            Controls.Add(btnSearch);
            Controls.Add(label1);
            Controls.Add(txtSearch);
            Name = "EventListForm";
            Text = "EventListForm";
            Load += EventListForm_Load;
            ((System.ComponentModel.ISupportInitialize)gridEvents).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtSearch;
        private Label label1;
        private Button btnSearch;
        private DataGridView gridEvents;
        private Button btnMy;
        private Button btnClose;
        private Button btnAddEvent;
        private Button btnEditEvent;
        private Button btnDeleteEvent;
    }
}