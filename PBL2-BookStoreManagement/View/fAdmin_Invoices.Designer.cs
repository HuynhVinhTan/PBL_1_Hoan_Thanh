namespace PBL2_BookStoreManagement.View
{
    partial class fAdmin_Invoices
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
            this.btnFilter = new System.Windows.Forms.Button();
            this.lblMaxAmount = new System.Windows.Forms.Label();
            this.nudMaxAmount = new System.Windows.Forms.NumericUpDown();
            this.lblMinAmount = new System.Windows.Forms.Label();
            this.nudMinAmount = new System.Windows.Forms.NumericUpDown();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dtgv_Invoice = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgv_Invoice)).BeginInit();
            this.SuspendLayout();
            // 
            // btnFilter
            // 
            this.btnFilter.BackColor = System.Drawing.Color.White;
            this.btnFilter.Font = new System.Drawing.Font("Comic Sans MS", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(153)))), ((int)(((byte)(248)))));
            this.btnFilter.Image = global::PBL2_BookStoreManagement.Properties.Resources.icons8_filter_24;
            this.btnFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFilter.Location = new System.Drawing.Point(1142, 11);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(79, 34);
            this.btnFilter.TabIndex = 15;
            this.btnFilter.Text = "Filter";
            this.btnFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFilter.UseVisualStyleBackColor = false;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // lblMaxAmount
            // 
            this.lblMaxAmount.AutoSize = true;
            this.lblMaxAmount.Font = new System.Drawing.Font("Comic Sans MS", 7.8F, System.Drawing.FontStyle.Bold);
            this.lblMaxAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(153)))), ((int)(((byte)(248)))));
            this.lblMaxAmount.Location = new System.Drawing.Point(895, 15);
            this.lblMaxAmount.Name = "lblMaxAmount";
            this.lblMaxAmount.Size = new System.Drawing.Size(94, 19);
            this.lblMaxAmount.TabIndex = 13;
            this.lblMaxAmount.Text = "Max Amount:";
            // 
            // nudMaxAmount
            // 
            this.nudMaxAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudMaxAmount.Font = new System.Drawing.Font("Comic Sans MS", 7.8F, System.Drawing.FontStyle.Bold);
            this.nudMaxAmount.ForeColor = System.Drawing.Color.Black;
            this.nudMaxAmount.Location = new System.Drawing.Point(986, 13);
            this.nudMaxAmount.Name = "nudMaxAmount";
            this.nudMaxAmount.Size = new System.Drawing.Size(49, 26);
            this.nudMaxAmount.TabIndex = 11;
            // 
            // lblMinAmount
            // 
            this.lblMinAmount.AutoSize = true;
            this.lblMinAmount.Font = new System.Drawing.Font("Comic Sans MS", 7.8F, System.Drawing.FontStyle.Bold);
            this.lblMinAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(153)))), ((int)(((byte)(248)))));
            this.lblMinAmount.Location = new System.Drawing.Point(733, 15);
            this.lblMinAmount.Name = "lblMinAmount";
            this.lblMinAmount.Size = new System.Drawing.Size(90, 19);
            this.lblMinAmount.TabIndex = 14;
            this.lblMinAmount.Text = "Min Amount:";
            // 
            // nudMinAmount
            // 
            this.nudMinAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudMinAmount.Font = new System.Drawing.Font("Comic Sans MS", 7.8F, System.Drawing.FontStyle.Bold);
            this.nudMinAmount.ForeColor = System.Drawing.Color.Black;
            this.nudMinAmount.Location = new System.Drawing.Point(821, 13);
            this.nudMinAmount.Name = "nudMinAmount";
            this.nudMinAmount.Size = new System.Drawing.Size(49, 26);
            this.nudMinAmount.TabIndex = 12;
            // 
            // dtpTo
            // 
            this.dtpTo.Font = new System.Drawing.Font("Comic Sans MS", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTo.Location = new System.Drawing.Point(408, 13);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(242, 26);
            this.dtpTo.TabIndex = 9;
            // 
            // dtpFrom
            // 
            this.dtpFrom.Font = new System.Drawing.Font("Comic Sans MS", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFrom.Location = new System.Drawing.Point(88, 14);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(242, 26);
            this.dtpFrom.TabIndex = 10;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Comic Sans MS", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(153)))), ((int)(((byte)(248)))));
            this.lblTo.Location = new System.Drawing.Point(347, 15);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(66, 19);
            this.lblTo.TabIndex = 7;
            this.lblTo.Text = "To Date:";
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Font = new System.Drawing.Font("Comic Sans MS", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(153)))), ((int)(((byte)(248)))));
            this.lblFrom.Location = new System.Drawing.Point(9, 16);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(81, 19);
            this.lblFrom.TabIndex = 8;
            this.lblFrom.Text = "From Date:";
            // 
            // dtgv_Invoice
            // 
            this.dtgv_Invoice.BackgroundColor = System.Drawing.Color.White;
            this.dtgv_Invoice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgv_Invoice.Location = new System.Drawing.Point(15, 47);
            this.dtgv_Invoice.Name = "dtgv_Invoice";
            this.dtgv_Invoice.RowHeadersWidth = 51;
            this.dtgv_Invoice.RowTemplate.Height = 24;
            this.dtgv_Invoice.Size = new System.Drawing.Size(1256, 631);
            this.dtgv_Invoice.TabIndex = 6;
            // 
            // fAdmin_Invoices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1280, 688);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.lblMaxAmount);
            this.Controls.Add(this.nudMaxAmount);
            this.Controls.Add(this.lblMinAmount);
            this.Controls.Add(this.nudMinAmount);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.lblFrom);
            this.Controls.Add(this.dtgv_Invoice);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "fAdmin_Invoices";
            this.Text = "fAdmin_Invoices";
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgv_Invoice)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Label lblMaxAmount;
        private System.Windows.Forms.NumericUpDown nudMaxAmount;
        private System.Windows.Forms.Label lblMinAmount;
        private System.Windows.Forms.NumericUpDown nudMinAmount;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DataGridView dtgv_Invoice;
    }
}