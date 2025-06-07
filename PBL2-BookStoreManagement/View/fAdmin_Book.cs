using PBL2_BookStoreManagement.BUS;
using PBL2_BookStoreManagement.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PBL2_BookStoreManagement.View
{
    public partial class fAdmin_Book : Form
    {
        public fAdmin_Book()
        {
            InitializeComponent();
        }

        void clear_panel()
        {
            foreach (var c in panel1.Controls)
            {
                if (c is TextBox textBox)
                {
                    textBox.Clear();
                }
            }
        }

        void CustomizeDataGridView(DataGridView dgv)
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

        void LoadBook()
        {
            dtgvBook.DataSource = null;
            CustomizeDataGridView(dtgvBook);
            dtgvBook.DataSource = BUS_Book.Instance.GetAllBooks();

            dtgvBook.Columns["book_ID"].HeaderText = "ID";
            dtgvBook.Columns["book_author"].HeaderText = "Author";
            dtgvBook.Columns["book_genre"].HeaderText = "Genre";
            dtgvBook.Columns["book_quantity"].HeaderText = "Quantity";
            dtgvBook.Columns["book_price"].HeaderText = "Price";
            dtgvBook.Columns["book_name"].HeaderText = "Name";

            dtgvBook.Refresh();
            lbcount.Text = BUS_Book.Instance.GetAllBooks().Count.ToString();
        }

        void isEnable(bool tb, bool dtgv)
        {
            textBox1.Enabled = tb;
            textBox2.Enabled = tb;
            textBox3.Enabled = tb;
            textBox4.Enabled = tb;
            textBox5.Enabled = tb;
            textBox6.Enabled = tb;
            dtgvBook.Enabled = dtgv;
        }

        void button_isEnable(bool t_s_x, bool h_l)
        {
            button1.Enabled = t_s_x;
            button2.Enabled = t_s_x;
            button3.Enabled = t_s_x;
            button4.Enabled = h_l;
            button5.Enabled = h_l;
        }

        bool check_null_panel()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text)
                || string.IsNullOrWhiteSpace(textBox2.Text)
                || string.IsNullOrWhiteSpace(textBox3.Text)
                || string.IsNullOrWhiteSpace(textBox4.Text)
                || string.IsNullOrWhiteSpace(textBox5.Text)
                || string.IsNullOrWhiteSpace(textBox6.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return false;
            }
            return true;
        }

        private void fAdmin_Book_Load(object sender, EventArgs e)
        {
            LoadBook();
            button_isEnable(true, false);
            clear_panel();
            isEnable(false, true);
        }

        string sta = "";
        int Index = -1;

        private void dtgvBook_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dtgvBook_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Index = e.RowIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isEnable(true, false);
            button_isEnable(false, true);
            sta = "add";
            int count = BUS_Book.Instance.GetAllBooks().Count;
            textBox1.Text = count < 100 ? $"B0{count + 1}" : $"B{count + 1}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Index < 0)
            {
                MessageBox.Show("Vui lòng chọn sách để sửa.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var book = BUS_Book.Instance.GetAllBooks()[Index];
            textBox1.Text = book.book_ID;
            textBox2.Text = book.book_name;
            textBox3.Text = book.book_genre;
            textBox4.Text = book.book_price.ToString();
            textBox5.Text = book.book_quantity.ToString();
            textBox6.Text = book.book_author;

            isEnable(true, false);
            button_isEnable(false, true);
            sta = "edit";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Index < 0 || Index >= BUS_Book.Instance.GetAllBooks().Count)
            {
                MessageBox.Show("Vui lòng chọn sách để xóa.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Cảnh Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                BUS_Book.Instance.DeleteBook(Index);
                LoadBook();
                isEnable(false, true);
                button_isEnable(true, false);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clear_panel();
            isEnable(false, true);
            button_isEnable(true, false);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!check_null_panel()) return;

            string bookId = textBox1.Text;
            string name = textBox2.Text;
            string category = textBox3.Text;
            double price = double.Parse(textBox4.Text);
            int stock = int.Parse(textBox5.Text);
            string author = textBox6.Text;

            if (sta == "add")
            {
                BUS_Book.Instance.AddBook(bookId, name, author, category, stock, price);
            }
            else if (sta == "edit")
            {
                Book book = new Book(bookId, name, author, category, stock, price);
                BUS_Book.Instance.UpdateBook(book, Index);
            }

            LoadBook();
            isEnable(false, true);
            button_isEnable(true, false);
            clear_panel();
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            string raw = tbSearch.Text.Trim().ToLower();
            var all = BUS_Book.Instance.GetAllBooks();
            List<Book> filtered;

            if (string.IsNullOrEmpty(raw))
            {
                filtered = all;
            }
            else
            {
                var keywords = raw.Split(',')
                                  .Select(k => k.Trim())
                                  .Where(k => !string.IsNullOrEmpty(k))
                                  .ToList();

                filtered = all.Where(u =>
                    keywords.All(kw =>
                        u.book_name != null && u.book_name.ToLower().Contains(kw)
                    )
                ).ToList();
            }

            dtgvBook.AutoGenerateColumns = false;
            dtgvBook.DataSource = null;
            CustomizeDataGridView(dtgvBook);
            dtgvBook.DataSource = filtered;
            dtgvBook.Refresh();
            lbcount.Text = filtered.Count.ToString();
        }
    }
}
