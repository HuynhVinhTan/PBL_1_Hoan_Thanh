
using PBL2_BookStoreManagement.BUS;
using PBL2_BookStoreManagement.DTO;
using System;
using System.Drawing;
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
                if (c is System.Windows.Forms.TextBox textBox) // Fully qualify 'TextBox' to resolve ambiguity  
                {
                    textBox.Clear();
                }
            }
        }
        void createCol()
        {
            dtgvBook.RowHeadersVisible = false;
            dtgvBook.AllowUserToResizeColumns = false;
            dtgvBook.AllowUserToResizeRows = false;
            dtgvBook.RowTemplate.Height = 35;
            dtgvBook.ColumnHeadersHeight = 40;
            dtgvBook.ColumnHeadersDefaultCellStyle.BackColor = Color.Blue;
            dtgvBook.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dtgvBook.EnableHeadersVisualStyles = false;
            dtgvBook.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dtgvBook.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            dtgvBook.RowsDefaultCellStyle.BackColor = Color.White;
            dtgvBook.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
        }
        void LoadBook()
        {
            dtgvBook.DataSource = null;
            createCol(); // Gọi createCol trước khi gán DataSource
            dtgvBook.DataSource = BUS_Book.Instance.GetAllBooks();
            dtgvBook.Columns["book_ID"].HeaderText = "ID";
            dtgvBook.Columns["book_author"].HeaderText = "Author";
            dtgvBook.Columns["book_genre"].HeaderText = "Genre";
            dtgvBook.Columns["book_quantity"].HeaderText = "Quantity";
            dtgvBook.Columns["book_price"].HeaderText = "Price";
            dtgvBook.Columns["book_name"].HeaderText = "Name";
            dtgvBook.Refresh();

            // Cập nhật số lượng khách hàng trong lbcount
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
        private void dtgvBook_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            isEnable(true, false);
            button_isEnable(false, true);
            sta = "add";
            if (BUS_Book.Instance.GetAllBooks().Count < 100)
                textBox1.Text = "B0" + (BUS_Book.Instance.GetAllBooks().Count + 1).ToString();
            else
                textBox1.Text = "B" + (BUS_Book.Instance.GetAllBooks().Count + 1).ToString();
        }
        int Index = -1;
        private void dtgvBook_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Index = e.RowIndex;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (Index < 0)
            {
                MessageBox.Show("Vui lòng chọn sách để sửa.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            textBox1.Text = BUS_Book.Instance.GetAllBooks()[Index].book_ID;
            textBox2.Text = BUS_Book.Instance.GetAllBooks()[Index].book_name;
            textBox3.Text = BUS_Book.Instance.GetAllBooks()[Index].book_genre;
            textBox4.Text = BUS_Book.Instance.GetAllBooks()[Index].book_price.ToString();
            textBox5.Text = BUS_Book.Instance.GetAllBooks()[Index].book_quantity.ToString();
            textBox6.Text = BUS_Book.Instance.GetAllBooks()[Index].book_author;
            isEnable(true, false);
            button_isEnable(false, true);
            sta = "edit";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Index < 0 || Index > BUS_Book.Instance.GetAllBooks().Count)
            {
                MessageBox.Show("Vui lòng chọn sách để xóa.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Cảnh Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                BUS_Book.Instance.DeleteBook(Index);
            }
            LoadBook();
            isEnable(false, true);
            button_isEnable(true, false);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.ReadOnly = false;
            button_isEnable(true, false);
            clear_panel();
            isEnable(false, true);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (sta == "add")
            {
                if (check_null_panel())
                {
                    string bookId = textBox1.Text;
                    string name = textBox2.Text;
                    string category = textBox3.Text;
                    double price = double.Parse(textBox4.Text);
                    int stock = int.Parse(textBox5.Text);
                    string author = textBox6.Text;
                    BUS_Book.Instance.AddBook(bookId, name, author, category, stock, price);
                    LoadBook();
                    clear_panel();
                }
            }
            if (sta == "edit")
            {
                if (check_null_panel())
                {
                    Book book = new Book(textBox1.Text, textBox2.Text, textBox6.Text, textBox3.Text, int.Parse(textBox5.Text), double.Parse(textBox4.Text));
                    BUS_Book.Instance.UpdateBook(book, Index);
                    LoadBook();
                    clear_panel();
                }
            }
            LoadBook();
            isEnable(false, true);
            button_isEnable(true, false);
            clear_panel();
        }
    }
}
