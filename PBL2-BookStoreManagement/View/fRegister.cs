using System;
using System.Windows.Forms;
using PBL2_BookStoreManagement.DTO;
using PBL2_BookStoreManagement.BUS;


namespace PBL2_BookStoreManagement.View
{
    public partial class fRegister : Form
    {
        public fRegister()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            fLogin login = new fLogin();
            login.Show();
            this.Hide();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            label4_Click(sender, e);
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string error = "";
            if (register_Username.Text == "" || register_Password.Text == "" || register_Email.Text == "" || register_ConPassword.Text == "" || register_Address.Text == "" || register_Phone.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return;
            }
            if (register_Password.Text != register_ConPassword.Text)
            {
                MessageBox.Show("Mật khẩu không khớp");
                return;
            }
            if (!Bus_Account.Instance.IsValidUsername(register_Username.Text, out error))
            {
                MessageBox.Show(error);
                return;
            }
            if (Bus_Account.Instance.check_Customer(register_Username.Text))
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại");
                return;
            }

            if (Bus_Account.Instance.IsValidPassword(register_Password.Text, out error) == false)
            {
                MessageBox.Show(error);
                return;
            }
            if (Bus_Account.Instance.IsValidEmail(register_Email.Text, out error) == false)
            {
                MessageBox.Show(error);
                return;
            }
            if (Bus_Account.Instance.check_Email(register_Email.Text))
            {
                MessageBox.Show("Email đã tồn tại");
                return;
            }
            if (Bus_Account.Instance.IsValidPhone(register_Phone.Text, out error) == false)
            {
                MessageBox.Show(error);
                return;
            }
            if (Bus_Account.Instance.check_Phone(register_Phone.Text))
            {
                MessageBox.Show("Số điện thoại đã tồn tại");
                return;
            }
            
            if (Bus_Account.Instance.IsValidAddress(register_Address.Text, out error) == false)
            {
                MessageBox.Show(error);
                return;
            }
            
            string cus_id = $"CUS{BUS_Customer.Instance.CountCustomer().ToString("D3")}";
            Customer cus = new Customer(cus_id, register_Name.Text, register_Username.Text, register_Phone.Text, register_Email.Text, register_Address.Text, register_Password.Text);
            BUS_Customer.Instance.AddCustomer(cus);
            MessageBox.Show("Đăng ký thành công");
            fLogin login = new fLogin();
            login.Show();
            this.Hide();
        }
    }
}