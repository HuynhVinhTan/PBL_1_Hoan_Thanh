using PBL2_BookStoreManagement.BUS;
using PBL2_BookStoreManagement.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PBL2_BookStoreManagement.View
{
    public partial class fCus_Invoice: Form
    {
        public fCus_Invoice()
        {
            InitializeComponent();
            Load_Invoice();
            Customize_dtgv_Invoice();
        }

        private void Load_Invoice()
        {
            List<Invoice> invoices = BUS_Invoice.Instance.GetInvoice(Session.Cur_cus.Cus_ID);
            dtgv_Invoice.DataSource = invoices;

            // Thêm nút "DETAIL" nếu chưa có
            if (dtgv_Invoice.Columns["DETAIL"] == null)
            {
                DataGridViewButtonColumn btnDetail = new DataGridViewButtonColumn();
                btnDetail.HeaderText = "";
                btnDetail.Text = "DETAIL";
                btnDetail.UseColumnTextForButtonValue = true;
                btnDetail.Name = "DETAIL";
                dtgv_Invoice.Columns.Add(btnDetail);
            }

            // Đảm bảo không gắn trùng sự kiện
            dtgv_Invoice.CellContentClick -= dtgv_Invoice_CellContentClick;
            dtgv_Invoice.CellContentClick += dtgv_Invoice_CellContentClick;
        }


        private void dtgv_Invoice_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dtgv_Invoice.Columns[e.ColumnIndex].Name == "DETAIL")
            {
                string invoiceID = dtgv_Invoice.Rows[e.RowIndex].Cells["InvoiceID"].Value.ToString();
                string invoicetext = BUS_Invoice.Instance.GetInvoiceText(Session.Cur_cus.Cus_ID, invoiceID);

                // Tạo form đơn giản để hiển thị văn bản
                Form frm = new Form();
                frm.Text = "Hóa đơn của bạn";
                frm.Size = new Size(600, 400);

                RichTextBox rtb = new RichTextBox();
                rtb.Dock = DockStyle.Fill;
                rtb.ReadOnly = true;
                rtb.Font = new Font("Segoe UI", 13); // chỉnh cỡ chữ tại đây
                frm.StartPosition = FormStartPosition.CenterScreen;
                rtb.Text = invoicetext;

                frm.Controls.Add(rtb);
                frm.ShowDialog();
            }
        }


        private void Customize_dtgv_Invoice() //dtgv_Invoice
        {
            #region dtgv_Books
            //set up 
            dtgv_Invoice.Columns["InvoiceID"].HeaderText = "InvoiceID";
            dtgv_Invoice.Columns["CustomerID"].HeaderText = "CustomerID";
            dtgv_Invoice.Columns["DateCreated"].HeaderText = "DateCreated";
            dtgv_Invoice.Columns["TotalAmount"].HeaderText = "TotalAmount";
            #endregion
            CustomizeDataGridView(dtgv_Invoice);
        }

        private void CustomizeDataGridView(DataGridView dgv)
        {
            dgv.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
