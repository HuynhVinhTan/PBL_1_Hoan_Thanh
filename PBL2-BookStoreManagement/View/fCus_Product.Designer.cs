namespace PBL2_BookStoreManagement.View
{
    partial class fCus_Product
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
            this.dtgv_Books = new System.Windows.Forms.DataGridView();
            this.dtgv_Cart = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lbl_totalcost = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtgv_Books)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgv_Cart)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgv_Books
            // 
            this.dtgv_Books.AllowUserToAddRows = false;
            this.dtgv_Books.AllowUserToDeleteRows = false;
            this.dtgv_Books.BackgroundColor = System.Drawing.Color.White;
            this.dtgv_Books.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgv_Books.Location = new System.Drawing.Point(12, 35);
            this.dtgv_Books.Name = "dtgv_Books";
            this.dtgv_Books.ReadOnly = true;
            this.dtgv_Books.RowHeadersWidth = 51;
            this.dtgv_Books.RowTemplate.Height = 24;
            this.dtgv_Books.Size = new System.Drawing.Size(791, 601);
            this.dtgv_Books.TabIndex = 1;
            // 
            // dtgv_Cart
            // 
            this.dtgv_Cart.AllowUserToAddRows = false;
            this.dtgv_Cart.AllowUserToDeleteRows = false;
            this.dtgv_Cart.BackgroundColor = System.Drawing.Color.White;
            this.dtgv_Cart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgv_Cart.Location = new System.Drawing.Point(809, 35);
            this.dtgv_Cart.Name = "dtgv_Cart";
            this.dtgv_Cart.ReadOnly = true;
            this.dtgv_Cart.RowHeadersWidth = 51;
            this.dtgv_Cart.RowTemplate.Height = 24;
            this.dtgv_Cart.Size = new System.Drawing.Size(467, 362);
            this.dtgv_Cart.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(809, 400);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Total cost:";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(1193, 428);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Confirm";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.confirm_Cart);
            // 
            // lbl_totalcost
            // 
            this.lbl_totalcost.AutoSize = true;
            this.lbl_totalcost.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_totalcost.Location = new System.Drawing.Point(881, 400);
            this.lbl_totalcost.Name = "lbl_totalcost";
            this.lbl_totalcost.Size = new System.Drawing.Size(16, 17);
            this.lbl_totalcost.TabIndex = 2;
            this.lbl_totalcost.Text = "0";
            // 
            // fCus_Product
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1280, 648);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbl_totalcost);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtgv_Cart);
            this.Controls.Add(this.dtgv_Books);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "fCus_Product";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "fCus_Product";
            ((System.ComponentModel.ISupportInitialize)(this.dtgv_Books)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgv_Cart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgv_Books;
        private System.Windows.Forms.DataGridView dtgv_Cart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbl_totalcost;
    }
}