using PBL2_BookStoreManagement.BUS;
using PBL2_BookStoreManagement.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace PBL2_BookStoreManagement.View
{
    public partial class fCus_Product : Form
    {
        

        public fCus_Product()
        {
            InitializeComponent();
            LoadBookData();
            Customize_dtgv_Book();
        }

        private void LoadBookData()
        {
            List<Book> books = BUS_Book.Instance.GetAllBooks();
            dtgv_Books.DataSource = books;
     
            if (dtgv_Books.Columns["AddToCart"] == null)
            {
                DataGridViewButtonColumn btnAddToCart = new DataGridViewButtonColumn();
                btnAddToCart.HeaderText = "Add To Cart";
                btnAddToCart.Text = "Add";
                btnAddToCart.UseColumnTextForButtonValue = true;
                btnAddToCart.Name = "AddToCart";
                dtgv_Books.Columns.Add(btnAddToCart);
            }
            dtgv_Books.CellContentClick += dtgv_Books_CellContentClick;
        }

        private void dtgv_Books_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dtgv_Books.Columns["AddToCart"].Index)
            {
                Book selectedBook = dtgv_Books.Rows[e.RowIndex].DataBoundItem as Book;
                if (selectedBook != null)
                {
                    if (BUS_Cart.Instance.AddToCart(selectedBook) == false) 
                    {
                        MessageBox.Show("Out Of Stock", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    LoadCartData(); // Cập nhật giỏ hàng
                }
            }
        }
        private void dtgv_Cart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return; // 🔥 Fix lỗi Index -1
            if (e.RowIndex >= 0)
            {
                Book selectedBook = dtgv_Cart.Rows[e.RowIndex].DataBoundItem as Book;
                if (selectedBook == null) return;

                if (e.ColumnIndex == dtgv_Cart.Columns["Increase"].Index)
                {
                    BUS_Cart.Instance.UpdateCart(selectedBook.book_ID, "Increase");
                }
                else if (e.ColumnIndex == dtgv_Cart.Columns["Decrease"].Index)
                {
                    BUS_Cart.Instance.UpdateCart(selectedBook.book_ID, "Decrease");
                }
                else if (e.ColumnIndex == dtgv_Cart.Columns["Remove"].Index)
                {
                    BUS_Cart.Instance.RemoveFromCart(selectedBook.book_ID);
                }
                LoadCartData(); // Cập nhật giỏ hàng
            }
        }

        private void LoadCartData()
        {
            var cart = BUS_Cart.Instance.GetCart();
            dtgv_Cart.DataSource = null;
            dtgv_Cart.DataSource = cart;
            Customize_dtgv_Cart();
            lbl_totalcost.Text = BUS_Cart.Instance.GetTotalPrice().ToString();
        }
        private void Customize_dtgv_Cart()
        {
            // Xóa cột nút nếu đã tồn tại (tránh bị lặp khi load lại)
            RemoveButtonColumn("Increase");
            RemoveButtonColumn("Decrease");
            RemoveButtonColumn("Remove");

            // Đặt lại header các cột dữ liệu
            if (dtgv_Cart.Columns["book_ID"] != null) dtgv_Cart.Columns["book_ID"].HeaderText = "ID";
            if (dtgv_Cart.Columns["book_name"] != null) dtgv_Cart.Columns["book_name"].HeaderText = "Name";
            if (dtgv_Cart.Columns["book_price"] != null) dtgv_Cart.Columns["book_price"].HeaderText = "Price";
            if (dtgv_Cart.Columns["book_quantity"] != null) dtgv_Cart.Columns["book_quantity"].HeaderText = "Quantity";

            // Ẩn các cột không cần thiết
            if (dtgv_Cart.Columns["book_author"] != null) dtgv_Cart.Columns["book_author"].Visible = false;
            if (dtgv_Cart.Columns["book_genre"] != null) dtgv_Cart.Columns["book_genre"].Visible = false;

            // Thêm các nút cuối bảng
            AddButtonColumn("Increase", "+", "");
            AddButtonColumn("Decrease", "-", "");
            AddButtonColumn("Remove", "Remove", "");

            // Cố định thứ tự hiển thị cột (tùy chỉnh theo ý bạn)
            dtgv_Cart.Columns["book_ID"].DisplayIndex = 0;
            dtgv_Cart.Columns["book_name"].DisplayIndex = 1;
            dtgv_Cart.Columns["book_price"].DisplayIndex = 2;
            dtgv_Cart.Columns["book_quantity"].DisplayIndex = 3;
            dtgv_Cart.Columns["Increase"].DisplayIndex = 4;
            dtgv_Cart.Columns["Decrease"].DisplayIndex = 5;
            dtgv_Cart.Columns["Remove"].DisplayIndex = 6;

            // Giao diện chung
            dtgv_Cart.Font = new Font("Segoe UI", 8, FontStyle.Regular);
            dtgv_Cart.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgv_Cart.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dtgv_Cart.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dtgv_Cart.EnableHeadersVisualStyles = false;
            dtgv_Cart.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dtgv_Cart.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dtgv_Cart.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dtgv_Cart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dtgv_Cart.RowHeadersVisible = false;
            dtgv_Cart.AllowUserToResizeRows = false;
            dtgv_Cart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgv_Cart.CellContentClick -= dtgv_Cart_CellContentClick;
            dtgv_Cart.CellContentClick += dtgv_Cart_CellContentClick;
        }

        private void RemoveButtonColumn(string name)
        {
            if (dtgv_Cart.Columns.Contains(name))
            {
                dtgv_Cart.Columns.Remove(name);
            }
        }

        private void AddButtonColumn(string name, string text, string headerText)
        {
            if (dtgv_Cart.Columns[name] == null)
            {
                var buttonColumn = new DataGridViewButtonColumn
                {
                    Name = name,
                    Text = text,
                    HeaderText = headerText,
                    UseColumnTextForButtonValue = true
                };
                dtgv_Cart.Columns.Add(buttonColumn);
            }
        }

        private void Customize_dtgv_Book() //dtgv_Book
        {
            #region dtgv_Books
            //set up 
            dtgv_Books.Columns["book_ID"].HeaderText = "ID";
            dtgv_Books.Columns["book_author"].HeaderText = "Author";
            dtgv_Books.Columns["book_genre"].HeaderText = "Genre";
            dtgv_Books.Columns["book_quantity"].HeaderText = "Quantity";
            dtgv_Books.Columns["book_price"].HeaderText = "Price";
            dtgv_Books.Columns["book_name"].HeaderText = "Name";
            #endregion
            CustomizeDataGridView(dtgv_Books);
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
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            BUS_Book.Instance.Updated_Book();
        }
    }
}
