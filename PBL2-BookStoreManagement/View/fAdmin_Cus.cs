using PBL2_BookStoreManagement.BUS;
using PBL2_BookStoreManagement.DTO;
using System.Drawing;
using System.Linq;
using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace PBL2_BookStoreManagement.View
{
    public partial class fAdmin_Cus : Form
    {
        public fAdmin_Cus()
        {
            InitializeComponent();
            dtgvCustomer.CellFormatting += dtgvCustomer_CellFormatting; // Thêm sự kiện ẩn mật khẩu
        }

        #region atri
        string sta = "";
        int Index = -1;
        #endregion

        #region Method

        void createCol()
        {
            dtgvCustomer.Columns.Clear();
            dtgvCustomer.AutoGenerateColumns = false;

            var colUserID = new DataGridViewTextBoxColumn
            {
                Name = "colUserID",
                DataPropertyName = "cus_id",
                HeaderText = "ID",
            };

            var colUserName = new DataGridViewTextBoxColumn
            {
                Name = "colUserName",
                DataPropertyName = "userName",
                HeaderText = "Username",
            };

            var colName = new DataGridViewTextBoxColumn
            {
                Name = "colName",
                DataPropertyName = "Name",
                HeaderText = "Name",
            };

            var colEmail = new DataGridViewTextBoxColumn
            {
                Name = "colEmail",
                DataPropertyName = "Email",
                HeaderText = "Email",
            };
            var colAddress = new DataGridViewTextBoxColumn
            {
                Name = "colAddress",
                DataPropertyName = "Address",
                HeaderText = "Address",
            };

            var colPhone = new DataGridViewTextBoxColumn
            {
                Name = "colPhone",
                DataPropertyName = "Phone",
                HeaderText = "Phone",
            };

            var colPassword = new DataGridViewTextBoxColumn
            {
                Name = "colPassword",
                DataPropertyName = "password",
                HeaderText = "Pass",
            };

            dtgvCustomer.Columns.AddRange(new DataGridViewColumn[] {
                colUserID, colName, colUserName, colPhone, colEmail, colAddress, colPassword
            });

            CustomizeDataGridView(dtgvCustomer);
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

        void button_isEnable(bool t_s_x, bool h_l)
        {
            button1.Enabled = t_s_x;
            button2.Enabled = t_s_x;
            button3.Enabled = t_s_x;
            button4.Enabled = h_l;
            button5.Enabled = h_l;
        }

        void LoadCustome()
        {
            tbSearch.Height = 51;
            dtgvCustomer.DataSource = null;
            createCol();
            dtgvCustomer.DataSource = BUS_Customer.Instance.GetAllCustomer();
            dtgvCustomer.Refresh();
            lbcount.Text = BUS_Customer.Instance.CountCustomer().ToString();
        }

        private void dtgvCustomer_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            Index = e.RowIndex;
        }

        void isEnable(bool tb, bool dtgv)
        {
            textBox1.Enabled = tb;
            textBox2.Enabled = tb;
            textBox3.Enabled = tb;
            textBox4.Enabled = tb;
            textBox5.Enabled = tb;
            textBox6.Enabled = tb;
            textBox7.Enabled = tb;
            dtgvCustomer.Enabled = dtgv;
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

        bool check_SDT(string phoneNumber)
        {
            string cleanedNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());
            return cleanedNumber.Length == 10 && long.TryParse(cleanedNumber, out long number) && number >= 0;
        }

        string check_email(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return email;

            email = email.Trim();
            if (!email.Contains("@"))
                return email + "@gmail.com";
            else if (!email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
            {
                int atIndex = email.IndexOf("@");
                return email.Substring(0, atIndex) + "@gmail.com";
            }
            return email;
        }
        #endregion

        #region event
        private void fAdmin_Cus_Load(object sender, EventArgs e)
        {
            tbSearch.TextChanged += textBox7_TextChanged;
            isEnable(false, true);
            button_isEnable(true, false);
            LoadCustome();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isEnable(true, false);
            button_isEnable(false, true);
            sta = "add";
            if (BUS_Customer.Instance.CountCustomer() < 100)
                textBox1.Text = "C0" + (BUS_Customer.Instance.CountCustomer() + 1);
            else
                textBox1.Text = "C" + (BUS_Customer.Instance.CountCustomer() + 1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Index < 0)
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để Sửa.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            textBox2.ReadOnly = true;
            button_isEnable(false, true);
            var cus = BUS_Customer.Instance.GetAllCustomer()[Index];
            textBox1.Text = cus.Cus_ID;
            textBox2.Text = cus.Name;
            textBox3.Text = cus.UserName;
            textBox4.Text = cus.Phone;
            textBox5.Text = cus.Email;
            textBox6.Text = cus.Address;
            textBox7.Text = cus.Password;
            isEnable(true, false);
            sta = "edit";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Index < 0)
            {
                MessageBox.Show("Vui lòng chọn một khách hàng thuộc bảng để xóa.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            BUS_Customer.Instance.DeleteCustomer(Index);
            LoadCustome();
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
            textBox2.ReadOnly = false;
            if (!check_null_panel())
                return;
            if (!check_SDT(textBox4.Text))
            {
                MessageBox.Show("Số điện thoại không hợp lệ. Vui lòng nhập lại.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox4.Focus();
                return;
            }
            textBox5.Text = check_email(textBox5.Text);

            if (sta == "add")
            {
                BUS_Customer.Instance.AddCustomer(new Customer(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text));
            }
            else if (sta == "edit" && Index >= 0)
            {
                BUS_Customer.Instance.UpdateCustomer(new Customer(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text), Index);
            }
            button_isEnable(true, false);
            LoadCustome();
            clear_panel();
            isEnable(false, true);
        }
        #endregion

        private void lbcount_Click(object sender, EventArgs e) { }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            var raw = tbSearch.Text.Trim().ToLower();
            var all = BUS_Customer.Instance.GetAllCustomer();
            List<Customer> filtered;

            if (string.IsNullOrEmpty(raw))
            {
                filtered = all;
            }
            else
            {
                var keywords = raw.Split(',').Select(k => k.Trim()).Where(k => !string.IsNullOrEmpty(k)).ToList();
                filtered = all.Where(u =>
                    keywords.All(kw =>
                        (u.UserName != null && u.UserName.ToLower().Contains(kw)) ||
                        (u.Name != null && u.Name.ToLower().Contains(kw))
                    )
                ).ToList();
            }

            dtgvCustomer.AutoGenerateColumns = false;
            dtgvCustomer.DataSource = null;
            createCol();
            dtgvCustomer.DataSource = filtered;
            dtgvCustomer.Refresh();
            lbcount.Text = filtered.Count.ToString();
        }

        private void dtgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        // Ẩn mật khẩu thành dấu ***
        private void dtgvCustomer_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dtgvCustomer.Columns[e.ColumnIndex].Name == "colPassword" && e.Value != null)
            {
                e.Value = new string('*', e.Value.ToString().Length);
                e.FormattingApplied = true;
            }
        }
    }
}
