using System;
using System.Windows.Forms;
using PBL2_BookStoreManagement.BUS;

namespace PBL2_BookStoreManagement.View
{
    public partial class fLogin: Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            fRegister register = new fRegister();
            register.Show();
            this.Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            login_Password.PasswordChar = checkBox1.Checked ? '\0' : '*';
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            if (login_Password.Text == "" || login_Username.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu");
            }
            else if (Bus_Account.Instance.check_admin(login_Username.Text, login_Password.Text))
            {
                fAdmin admin = new fAdmin();
                admin.Show();
                this.Hide();
            }
            else if (Bus_Account.Instance.check_Customer(login_Username.Text, login_Password.Text))
            {
                Bus_Account.Instance.save_Session(login_Username.Text);
                fCustomer customer = new fCustomer();
                customer.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu");
            }
        }
    }
}
