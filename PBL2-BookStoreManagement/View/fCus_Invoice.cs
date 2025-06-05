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

            if (dtgv_Invoice.Columns["DETAIL"] == null)
            {
                DataGridViewButtonColumn btnDetail = new DataGridViewButtonColumn();
                btnDetail.HeaderText = "";
                btnDetail.Text = "DETAIL";
                btnDetail.UseColumnTextForButtonValue = true;
                btnDetail.Name = "DETAIL";
                dtgv_Invoice.Columns.Add(btnDetail);
            }

            dtgv_Invoice.CellContentClick -= dtgv_Invoice_CellContentClick;
            dtgv_Invoice.CellContentClick += dtgv_Invoice_CellContentClick;

            // Thêm sự kiện CellPainting nếu chưa đăng ký
            dtgv_Invoice.CellPainting -= Dtgv_Invoice_CellPainting;
            dtgv_Invoice.CellPainting += Dtgv_Invoice_CellPainting;
        }

        // Xử lý vẽ lại nút với màu chữ tùy ý
        private void Dtgv_Invoice_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && dtgv_Invoice.Columns[e.ColumnIndex].Name == "DETAIL" && e.RowIndex >= 0)
            {
                // Vẽ nền trắng
                using (Brush backColorBrush = new SolidBrush(Color.White))
                {
                    e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                }

                // Vẽ viền xanh
                using (Pen borderPen = new Pen(Color.Black, 0.5f)) // Độ dày 2, màu xanh
                {
                    Rectangle rect = e.CellBounds;
                    rect.Width -= 1;
                    rect.Height -= 1;
                    e.Graphics.DrawRectangle(borderPen, rect);
                }

                // Màu chữ xanh
                Color textColor = Color.Black;

                // Font chữ tuỳ chỉnh
                using (Font customFont = new Font("Comic Sans MS", 12f, FontStyle.Bold))
                {
                    TextRenderer.DrawText(e.Graphics, "DETAIL", customFont, e.CellBounds, textColor,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                }

                e.Handled = true; // báo đã xử lý vẽ
            }
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
                rtb.Font = new Font("Comic Sans MS", 12f, FontStyle.Italic); // chỉnh cỡ chữ tại đây
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
            // Không cho chỉnh sửa
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.EditMode = DataGridViewEditMode.EditProgrammatically;

            // Không cho resize cột và dòng
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;

            // Không có viền ngoài
            dgv.BorderStyle = BorderStyle.None;

            // Chỉ viền ngang (không viền dọc)
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Không hiển thị chỉ số dòng (bên trái)
            dgv.RowHeadersVisible = false;

            // Màu và font
            dgv.BackgroundColor = Color.White;
            dgv.GridColor = Color.Black;
            dgv.Font = new Font("Comic Sans MS", 12f, FontStyle.Bold);
            dgv.ForeColor = Color.FromArgb(17, 153, 248); 
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Căn giữa và font cho header
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Comic Sans MS", 14f, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(17, 153, 248);
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None; // ❌ Không dấu phân cách ở header

            // Màu dòng
            dgv.RowsDefaultCellStyle.BackColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.White;

            // Dòng được chọn
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgv.ColumnHeadersDefaultCellStyle.BackColor;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = dgv.ColumnHeadersDefaultCellStyle.ForeColor;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(17, 153, 248);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;

            // Giãn cột vừa bảng, không cho resize
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
