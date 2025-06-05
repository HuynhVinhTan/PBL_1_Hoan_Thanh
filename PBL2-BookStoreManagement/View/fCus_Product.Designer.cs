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
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtgv_Books)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgv_Cart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgv_Books
            // 
            this.dtgv_Books.AllowUserToAddRows = false;
            this.dtgv_Books.AllowUserToDeleteRows = false;
            this.dtgv_Books.BackgroundColor = System.Drawing.Color.White;
            this.dtgv_Books.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgv_Books.Location = new System.Drawing.Point(12, 40);
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
            this.dtgv_Cart.Location = new System.Drawing.Point(809, 40);
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
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 10.2F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(153)))), ((int)(((byte)(248)))));
            this.label1.Location = new System.Drawing.Point(809, 405);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Total cost:";
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(153)))), ((int)(((byte)(248)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Comic Sans MS", 10.2F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(153)))), ((int)(((byte)(248)))));
            this.button1.Location = new System.Drawing.Point(1175, 434);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 33);
            this.button1.TabIndex = 3;
            this.button1.Text = "Confirm";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.confirm_Cart);
            // 
            // lbl_totalcost
            // 
            this.lbl_totalcost.AutoSize = true;
            this.lbl_totalcost.Font = new System.Drawing.Font("Comic Sans MS", 10.2F, System.Drawing.FontStyle.Bold);
            this.lbl_totalcost.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(153)))), ((int)(((byte)(248)))));
            this.lbl_totalcost.Location = new System.Drawing.Point(918, 405);
            this.lbl_totalcost.Name = "lbl_totalcost";
            this.lbl_totalcost.Size = new System.Drawing.Size(22, 25);
            this.lbl_totalcost.TabIndex = 2;
            this.lbl_totalcost.Text = "0";
            // 
            // tbSearch
            // 
            this.tbSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSearch.Font = new System.Drawing.Font("Comic Sans MS", 10.2F, System.Drawing.FontStyle.Bold);
            this.tbSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(153)))), ((int)(((byte)(248)))));
            this.tbSearch.Location = new System.Drawing.Point(95, 4);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(708, 31);
            this.tbSearch.TabIndex = 31;
            this.tbSearch.TextChanged += new System.EventHandler(this.tbSearch_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = global::PBL2_BookStoreManagement.Properties.Resources.icons8_search_30;
            this.pictureBox1.Location = new System.Drawing.Point(64, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(30, 30);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 32;
            this.pictureBox1.TabStop = false;
            // 
            // fCus_Product
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1280, 648);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tbSearch);
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgv_Books;
        private System.Windows.Forms.DataGridView dtgv_Cart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbl_totalcost;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}