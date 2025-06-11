using PBL2_BookStoreManagement.BUS;
using PBL2_BookStoreManagement.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBL2_BookStoreManagement.View
{
    public partial class fAdmin_Invoices: Form
    {
        private List<Invoice> originalInvoices;

        public fAdmin_Invoices()
        {
            InitializeComponent();
            Load_Invoice();
            Customize_dtgv_Invoice();
            InitFilterControls();
        }

        private void Load_Invoice()
        {
            originalInvoices = BUS_Invoice.Instance.GetInvoice(0);
            dtgv_Invoice.DataSource = originalInvoices;

            // Nếu cột DETAIL chưa có thì thêm mới
            if (dtgv_Invoice.Columns["DETAIL"] == null)
            {
                DataGridViewButtonColumn btnDetail = new DataGridViewButtonColumn();
                btnDetail.HeaderText = "";
                btnDetail.Text = "DETAIL";
                btnDetail.UseColumnTextForButtonValue = true;
                btnDetail.Name = "DETAIL";
                dtgv_Invoice.Columns.Add(btnDetail);
            }

            // Đảm bảo cột DETAIL luôn nằm ở cuối
            dtgv_Invoice.Columns["DETAIL"].DisplayIndex = dtgv_Invoice.Columns.Count - 1;

            // Gắn lại sự kiện tránh gắn trùng nhiều lần
            dtgv_Invoice.CellContentClick -= dtgv_Invoice_CellContentClick;
            dtgv_Invoice.CellContentClick += dtgv_Invoice_CellContentClick;

            dtgv_Invoice.CellPainting -= Dtgv_Invoice_CellPainting;
            dtgv_Invoice.CellPainting += Dtgv_Invoice_CellPainting;
        }

        private void InitFilterControls()
        {
            if (originalInvoices.Count == 0) return;

            dtpFrom.Value = originalInvoices.Min(inv => inv.DateCreated);
            dtpTo.Value = originalInvoices.Max(inv => inv.DateCreated);

            nudMinAmount.Minimum = 0;
            nudMinAmount.Maximum = decimal.MaxValue;
            nudMinAmount.Value = (decimal)originalInvoices.Min(inv => inv.TotalAmount);

            nudMaxAmount.Minimum = 0;
            nudMaxAmount.Maximum = decimal.MaxValue;
            nudMaxAmount.Value = (decimal)originalInvoices.Max(inv => inv.TotalAmount);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            DateTime fromDate = dtpFrom.Value.Date;
            DateTime toDate = dtpTo.Value.Date;
            decimal minAmount = nudMinAmount.Value;
            decimal maxAmount = nudMaxAmount.Value;

            var filteredInvoices = originalInvoices.Where(inv =>
                inv.DateCreated.Date >= fromDate &&
                inv.DateCreated.Date <= toDate &&
                (decimal)inv.TotalAmount >= minAmount &&
                (decimal)inv.TotalAmount <= maxAmount).ToList();

            dtgv_Invoice.DataSource = null;
            dtgv_Invoice.DataSource = filteredInvoices;

            // Nếu cột DETAIL chưa có (sau khi reset DataSource) thì thêm lại
            if (dtgv_Invoice.Columns["DETAIL"] == null)
            {
                DataGridViewButtonColumn btnDetail = new DataGridViewButtonColumn();
                btnDetail.HeaderText = "";
                btnDetail.Text = "DETAIL";
                btnDetail.UseColumnTextForButtonValue = true;
                btnDetail.Name = "DETAIL";
                dtgv_Invoice.Columns.Add(btnDetail);
            }

            // Đảm bảo DETAIL luôn ở cuối
            dtgv_Invoice.Columns["DETAIL"].DisplayIndex = dtgv_Invoice.Columns.Count - 1;

            Customize_dtgv_Invoice();
        }

        private void Dtgv_Invoice_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && dtgv_Invoice.Columns[e.ColumnIndex].Name == "DETAIL" && e.RowIndex >= 0)
            {
                using (Brush backColorBrush = new SolidBrush(Color.White))
                {
                    e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                }

                using (Pen borderPen = new Pen(Color.Black, 0.5f))
                {
                    Rectangle rect = e.CellBounds;
                    rect.Width -= 1;
                    rect.Height -= 1;
                    e.Graphics.DrawRectangle(borderPen, rect);
                }

                Color textColor = Color.Black;

                using (Font customFont = new Font("Comic Sans MS", 11f, FontStyle.Bold))
                {
                    TextRenderer.DrawText(e.Graphics, "DETAIL", customFont, e.CellBounds, textColor,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                }

                e.Handled = true;
            }
        }

        private void dtgv_Invoice_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dtgv_Invoice.Columns[e.ColumnIndex].Name == "DETAIL")
            {
                string invoiceID = dtgv_Invoice.Rows[e.RowIndex].Cells["InvoiceID"].Value.ToString();
                string cus_Id = dtgv_Invoice.Rows[e.RowIndex].Cells["CustomerID"].Value.ToString();
                string invoicetext = BUS_Invoice.Instance.GetInvoiceText(cus_Id, invoiceID);

                Form frm = new Form();
                frm.Text = "Hóa đơn của bạn";
                frm.Size = new Size(800, 600);

                RichTextBox rtb = new RichTextBox();
                rtb.Dock = DockStyle.Fill;
                rtb.ReadOnly = true;
                rtb.Font = new Font("Comic Sans MS", 12f, FontStyle.Bold);
                frm.StartPosition = FormStartPosition.CenterScreen;
                rtb.Text = invoicetext;

                frm.Controls.Add(rtb);
                frm.ShowDialog();
            }
        }

        private void Customize_dtgv_Invoice()
        {
            dtgv_Invoice.Columns["InvoiceID"].HeaderText = "InvoiceID";
            dtgv_Invoice.Columns["CustomerID"].HeaderText = "CustomerID";
            dtgv_Invoice.Columns["DateCreated"].HeaderText = "DateCreated";
            dtgv_Invoice.Columns["TotalAmount"].HeaderText = "TotalAmount";

            CustomizeDataGridView(dtgv_Invoice);
        }

        private void CustomizeDataGridView(DataGridView dgv)
        {
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.RowHeadersVisible = false;
            dgv.BackgroundColor = Color.White;
            dgv.GridColor = Color.Black;
            dgv.Font = new Font("Comic Sans MS", 11f, FontStyle.Bold);
            dgv.ForeColor = Color.Black;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Comic Sans MS", 12f, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgv.RowsDefaultCellStyle.BackColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.White;

            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgv.ColumnHeadersDefaultCellStyle.BackColor;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = dgv.ColumnHeadersDefaultCellStyle.ForeColor;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(17, 153, 248);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
