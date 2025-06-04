using PBL2_BookStoreManagement.BUS;
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
        }

        private void LoadBookData()
        {
            var books = BUS_Book.Instance.GetAllBooks()
                            .Where(book => book.book_quantity > 0)
                            .ToList();

            dtgv_Books.DataSource = books;
            AddButtonToGrid(dtgv_Books, "AddToCart", "Add To Cart", "Add");
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
            lbl_totalcost.Text = BUS_Cart.Instance.GetTotalPrice().ToString();
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
                Size = new Size(600, 400),
                StartPosition = FormStartPosition.CenterScreen
            };

            RichTextBox rtb = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                Font = new Font("Segoe UI", 13),
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
            AddButtonToGrid(dtgv_Cart, "Increase", "", "+");
            AddButtonToGrid(dtgv_Cart, "Decrease", "", "-");
            AddButtonToGrid(dtgv_Cart, "Remove", "", "Remove");

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

        private void AddButtonToGrid(DataGridView dgv, string name, string headerText, string text)
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
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
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
            dgv.Font = new Font("Segoe UI", 10);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToResizeRows = false;
            dgv.EnableHeadersVisualStyles = false;
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
    }
}