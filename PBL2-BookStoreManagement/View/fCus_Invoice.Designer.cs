namespace PBL2_BookStoreManagement.View
{
    partial class fCus_Invoice
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
            this.dtgv_Invoice = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dtgv_Invoice)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgv_Invoice
            // 
            this.dtgv_Invoice.BackgroundColor = System.Drawing.Color.White;
            this.dtgv_Invoice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgv_Invoice.Location = new System.Drawing.Point(12, 13);
            this.dtgv_Invoice.Name = "dtgv_Invoice";
            this.dtgv_Invoice.RowHeadersWidth = 51;
            this.dtgv_Invoice.RowTemplate.Height = 24;
            this.dtgv_Invoice.Size = new System.Drawing.Size(1256, 623);
            this.dtgv_Invoice.TabIndex = 0;
            // 
            // fCus_Invoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1280, 648);
            this.Controls.Add(this.dtgv_Invoice);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "fCus_Invoice";
            this.Text = "fCus_Invoice";
            ((System.ComponentModel.ISupportInitialize)(this.dtgv_Invoice)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgv_Invoice;
    }
}