﻿using PBL2_BookStoreManagement.BUS;
using PBL2_BookStoreManagement.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace PBL2_BookStoreManagement.View
{
    public partial class fCus_Product : Form
    {
        public fCus_Product()
        {
            InitializeComponent();
            LoadBookData();
            CustomizeDataGridView(dtgv_Books);
            InitFilterControls();
        }

        private void InitFilterControls()
        {
            List<Book> booksinstore = BUS_Book.Instance.GetAllBooks();
            if (booksinstore.Count == 0) return;

            nudMinPrice.Minimum = 0;
            nudMinPrice.Maximum = decimal.MaxValue;
            nudMinPrice.Value = (decimal)booksinstore.Min(inv => inv.book_price);

            nudMaxPrice.Minimum = 0;
            nudMaxPrice.Maximum = decimal.MaxValue;
            nudMaxPrice.Value = (decimal)booksinstore.Max(inv => inv.book_price);

            cbSortPrice.Items.Clear();
            cbSortPrice.Items.AddRange(new string[] { "None", "Price Ascending", "Price Descending" });
            cbSortPrice.SelectedIndex = 0;
            cbSortPrice.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSortPrice.ForeColor = Color.FromArgb(17, 153, 248);
        }


        private void LoadBookData()
        {
            var books = BUS_Book.Instance.GetAllBooks()
                            .Where(book => book.book_quantity > 0)
                            .ToList();

            dtgv_Books.DataSource = books;
            AddButtonToGrid(dtgv_Books, "AddToCart", "Add To Cart", "ADD", new Font("Comic Sans MS", 12, FontStyle.Bold));
            dtgv_Books.CellContentClick -= dtgv_Books_CellContentClick;
            dtgv_Books.CellContentClick += dtgv_Books_CellContentClick;
            Customize_dtgv_Book();
        }

        private void dtgv_Books_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dtgv_Books.Columns[e.ColumnIndex].Name != "AddToCart") return;

            var selectedBook = dtgv_Books.Rows[e.RowIndex].DataBoundItem as Book;
            if (selectedBook != null && !BUS_Cart.Instance.AddToCart(selectedBook))
            {
                MessageBox.Show("Out Of Stock", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            LoadCartData();
        }

        private void dtgv_Cart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var selectedBook = dtgv_Cart.Rows[e.RowIndex].DataBoundItem as Cart;
            if (selectedBook == null) return;

            string columnName = dtgv_Cart.Columns[e.ColumnIndex].Name;
            switch (columnName)
            {
                case "Increase":
                case "Decrease":
                    BUS_Cart.Instance.UpdateCart(selectedBook.book_ID, columnName);
                    break;
                case "Remove":
                    BUS_Cart.Instance.RemoveFromCart(selectedBook.book_ID);
                    break;
            }
            LoadCartData();
        }

        private void LoadCartData()
        {
            dtgv_Cart.DataSource = null;
            dtgv_Cart.DataSource = BUS_Cart.Instance.GetCart();
            Customize_dtgv_Cart();
            lbl_totalcost.Text = BUS_Cart.Instance.GetTotalPrice().ToString() + " $";
        }

        private void confirm_Cart(object sender, EventArgs e)
        {
            if (!BUS_Book.Instance.Updated_Book())
            {
                MessageBox.Show("No items in cart", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Purchase successfully", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ShowInvoice(BUS_Invoice.Instance.create_Invoice_Info());
            BUS_Cart.Instance.ClearCart();
            LoadBookData();
            LoadCartData();
        }

        private void ShowInvoice(string invoicetext)
        {
            Form frm = new Form
            {
                Text = "Hóa đơn của bạn",
                Size = new Size(800, 600),
                StartPosition = FormStartPosition.CenterScreen
            };

            RichTextBox rtb = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                Font = new Font("Comic Sans MS", 12, FontStyle.Bold),
                Text = invoicetext
            };

            frm.Controls.Add(rtb);
            frm.ShowDialog();
        }

        private void Customize_dtgv_Cart()
        {
            string[] removeColumns = { "Increase", "Decrease", "Remove" };
            foreach (var col in removeColumns)
            {
                if (dtgv_Cart.Columns.Contains(col)) dtgv_Cart.Columns.Remove(col);
            }

            SetCartHeader();
            AddButtonToGrid(dtgv_Cart, "Increase", "", "+", new Font("Comic Sans MS", 12, FontStyle.Bold));
            AddButtonToGrid(dtgv_Cart, "Decrease", "", "-", new Font("Comic Sans MS", 12, FontStyle.Bold));
            AddButtonToGrid(dtgv_Cart, "Remove", "", "Remove", new Font("Comic Sans MS", 12, FontStyle.Bold));

            int i = 0;
            foreach (string colName in new[] { "book_ID", "book_name", "book_quantity", "book_price", "Increase", "Decrease", "Remove" })
            {
                if (dtgv_Cart.Columns[colName] != null)
                    dtgv_Cart.Columns[colName].DisplayIndex = i++;
            }

            CustomizeDataGridView(dtgv_Cart);
            dtgv_Cart.CellContentClick -= dtgv_Cart_CellContentClick;
            dtgv_Cart.CellContentClick += dtgv_Cart_CellContentClick;
        }

        private void SetCartHeader()
        {
            if (dtgv_Cart.Columns["book_ID"] != null) dtgv_Cart.Columns["book_ID"].HeaderText = "ID";
            if (dtgv_Cart.Columns["book_name"] != null) dtgv_Cart.Columns["book_name"].HeaderText = "Name";
            if (dtgv_Cart.Columns["book_price"] != null) dtgv_Cart.Columns["book_price"].HeaderText = "Price";
            if (dtgv_Cart.Columns["book_quantity"] != null) dtgv_Cart.Columns["book_quantity"].HeaderText = "Quantity";
            if (dtgv_Cart.Columns["book_author"] != null) dtgv_Cart.Columns["book_author"].Visible = false;
            if (dtgv_Cart.Columns["book_genre"] != null) dtgv_Cart.Columns["book_genre"].Visible = false;
        }

        private void AddButtonToGrid(DataGridView dgv, string name, string headerText, string text, Font font = null)
        {
            if (dgv.Columns[name] != null) return;

            var buttonColumn = new DataGridViewButtonColumn
            {
                Name = name,
                HeaderText = headerText,
                Text = text,
                UseColumnTextForButtonValue = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    ForeColor = Color.Red,
                    Font = font ?? new Font("Comic Sans MS", 10, FontStyle.Italic)
                }
            };
            dgv.Columns.Add(buttonColumn);
        }

        private void Customize_dtgv_Book()
        {
            var headers = new Dictionary<string, string>
            {
                ["book_ID"] = "ID",
                ["book_name"] = "Name",
                ["book_author"] = "Author",
                ["book_genre"] = "Genre",
                ["book_quantity"] = "Quantity",
                ["book_price"] = "Price"
            };

            foreach (var kvp in headers)
            {
                if (dtgv_Books.Columns[kvp.Key] != null)
                    dtgv_Books.Columns[kvp.Key].HeaderText = kvp.Value;
            }

            if (dtgv_Books.Columns["AddToCart"] != null)
                dtgv_Books.Columns["AddToCart"].DisplayIndex = dtgv_Books.Columns.Count - 1;
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
            dgv.Font = new Font("Comic Sans MS", 10f, FontStyle.Bold);
            dgv.ForeColor = Color.Black;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Căn giữa và font cho header
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Comic Sans MS", 13f, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
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

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            var kw = tbSearch.Text.Trim().ToLower();
            var all = BUS_Book.Instance.GetAllBooks()
                        .Where(book => book.book_quantity > 0)
                        .ToList();

            var filtered = string.IsNullOrEmpty(kw)
                            ? all
                            : all.Where(b => b.book_name.ToLower().Contains(kw)).ToList();

            dtgv_Books.DataSource = null;
            dtgv_Books.DataSource = filtered;
            Customize_dtgv_Book();
        }

        // Hàm xử lý nút Lọc giá
        private void btnFilterPrice_Click(object sender, EventArgs e)
        {
            decimal minPrice = nudMinPrice.Value;
            decimal maxPrice = nudMaxPrice.Value;
            string sortOption = cbSortPrice.SelectedItem.ToString();

            // Lấy toàn bộ sách từ BUS (hoặc source của bạn)
            List<Book> books = BUS_Book.Instance.GetAllBooks()
                .Where(book => book.book_quantity > 0 &&
                               (decimal)book.book_price >= minPrice &&
                               (decimal)book.book_price <= maxPrice)
                .ToList();

            // Sắp xếp theo lựa chọn
            switch (sortOption)
            {
                case "Price Ascending":
                    books = books.OrderBy(b => b.book_price).ToList();
                    break;
                case "Price Descending":
                    books = books.OrderByDescending(b => b.book_price).ToList();
                    break;
                default:
                    break; // Không sắp xếp
            }

            // Cập nhật DataGridView
            dtgv_Books.DataSource = null;
            dtgv_Books.DataSource = books;

            Customize_dtgv_Book(); // Nếu bạn có hàm custom hiển thị thì gọi lại
        }

    }
}
