using PBL2_BookStoreManagement.BUS;
using PBL2_BookStoreManagement.DAL;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System;

namespace PBL2_BookStoreManagement.View
{
    public partial class fCus_Overview : Form
    {
        private bool isEditing = false;

        // Panel đổi mật khẩu và các control trong panel
        private Panel panel_ChangePassword;
        private TextBox txt_OldPass, txt_NewPass, txt_ConfirmPass;
        private Button btn_ConfirmPass, btn_CancelPass;

        public fCus_Overview()
        {
            InitializeComponent();
            InitChangePasswordPanel();
            LoadCustomerInfo();
            SetupInterface();

        }

        private void LoadCustomerInfo()
        {
            txt_Name.Enabled = false;
            txt_Email.Enabled = false;
            txt_Phone.Enabled = false;
            txt_Address.Enabled = false;

            txt_Name.Text = Session.Cur_cus.Name;
            txt_Email.Text = Session.Cur_cus.Email;
            txt_Phone.Text = Session.Cur_cus.Phone;
            txt_Address.Text = Session.Cur_cus.Address;

            var invoices = BUS_Invoice.Instance.GetInvoice(Session.Cur_cus.Cus_ID);
            lbl_Totalinvoice.Text = invoices.Count.ToString();
            lbl_Totalspending.Text = invoices.Sum(i => i.TotalAmount)
                .ToString("C", System.Globalization.CultureInfo.CurrentCulture);

            btn_ChangeInfo.Text = "Change Info";
            isEditing = false;
        }

        private void SetupInterface()
        {
            txt_Name.ForeColor = ColorTranslator.FromHtml("#0D1B2A");
            txt_Email.ForeColor = ColorTranslator.FromHtml("#0D1B2A");
            txt_Phone.ForeColor = ColorTranslator.FromHtml("#0D1B2A");
            txt_Address.ForeColor = ColorTranslator.FromHtml("#0D1B2A");

            lbl_Totalspending.Location = new Point((panel6.Width - lbl_Totalspending.Width) / 2,
                                                   (panel6.Height - lbl_Totalspending.Height) / 2);
            lbl_Totalinvoice.Location = new Point((panel5.Width - lbl_Totalinvoice.Width) / 2,
                                                  (panel5.Height - lbl_Totalinvoice.Height) / 2);
        }

        private void btn_ChangeInfo_Click(object sender, EventArgs e)
        {
            if (!isEditing)
            {
                txt_Name.Enabled = true;
                txt_Email.Enabled = true;
                txt_Phone.Enabled = true;
                txt_Address.Enabled = true;

                txt_Name.BorderStyle = BorderStyle.FixedSingle;
                txt_Email.BorderStyle = BorderStyle.FixedSingle;
                txt_Phone.BorderStyle = BorderStyle.FixedSingle;
                txt_Address.BorderStyle = BorderStyle.FixedSingle;

                btn_ChangeInfo.Text = "Apply Change";
                isEditing = true;
            }
            else
            {
                Session.Cur_cus.Name = txt_Name.Text;
                Session.Cur_cus.Email = txt_Email.Text;
                Session.Cur_cus.Phone = txt_Phone.Text;
                Session.Cur_cus.Address = txt_Address.Text;

                BUS_Customer.Instance.UpdateCustomer(Session.Cur_cus);

                txt_Name.Enabled = false;
                txt_Email.Enabled = false;
                txt_Phone.Enabled = false;
                txt_Address.Enabled = false;

                txt_Name.BorderStyle = BorderStyle.None;
                txt_Email.BorderStyle = BorderStyle.None;
                txt_Phone.BorderStyle = BorderStyle.None;
                txt_Address.BorderStyle = BorderStyle.None;

                btn_ChangeInfo.Text = "Change Info";
                isEditing = false;

                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // 👉 Tạo khung nhỏ đổi mật khẩu ngay trên form chính
        private void InitChangePasswordPanel()
        {
            panel_ChangePassword = new Panel
            {
                Size = new Size(300, 200),
                Location = new Point((this.Width - 300) / 2, (this.Height - 200) / 2),
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Comic Sans MS", 10, FontStyle.Bold),
                Visible = false,
                BackColor = Color.White
            };

            Label lblOld = new Label { Text = "Old Password", Location = new Point(10, 10), Width = 120 };
            txt_OldPass = new TextBox { Location = new Point(140, 10), Width = 140, UseSystemPasswordChar = true };

            Label lblNew = new Label { Text = "New Password", Location = new Point(10, 50), Width = 120 };
            txt_NewPass = new TextBox { Location = new Point(140, 50), Width = 140, UseSystemPasswordChar = true };

            Label lblConfirm = new Label { Text = "Confirm Password", Location = new Point(10, 90), Width = 120 };
            txt_ConfirmPass = new TextBox { Location = new Point(140, 90), Width = 140, UseSystemPasswordChar = true };

            btn_ConfirmPass = new Button { Text = "Confirm", Location = new Point(60, 140), Width = 80 };
            btn_ConfirmPass.Click += Btn_ConfirmPass_Click;

            btn_CancelPass = new Button { Text = "Cancel", Location = new Point(160, 140), Width = 80 };
            btn_CancelPass.Click += Btn_CancelPass_Click;

            panel_ChangePassword.Controls.Add(lblOld);
            panel_ChangePassword.Controls.Add(txt_OldPass);
            panel_ChangePassword.Controls.Add(lblNew);
            panel_ChangePassword.Controls.Add(txt_NewPass);
            panel_ChangePassword.Controls.Add(lblConfirm);
            panel_ChangePassword.Controls.Add(txt_ConfirmPass);
            panel_ChangePassword.Controls.Add(btn_ConfirmPass);
            panel_ChangePassword.Controls.Add(btn_CancelPass);

            this.Controls.Add(panel_ChangePassword);
        }

        private void btn_ChangePassword_Click(object sender, EventArgs e)
        {
            panel_ChangePassword.Visible = true;
            panel_ChangePassword.BringToFront();
        }

        private void Btn_ConfirmPass_Click(object sender, EventArgs e)
        {
            string oldPass = txt_OldPass.Text.Trim();
            string newPass = txt_NewPass.Text.Trim();
            string confirmPass = txt_ConfirmPass.Text.Trim();

            if (oldPass != Session.Cur_cus.Password)
            {
                MessageBox.Show("Mật khẩu cũ không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (newPass != confirmPass)
            {
                MessageBox.Show("Xác nhận mật khẩu không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (newPass == "")
            {
                MessageBox.Show("Mật khẩu mới không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Session.Cur_cus.Password = newPass;
            BUS_Customer.Instance.UpdateCustomer(Session.Cur_cus);

            MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            panel_ChangePassword.Visible = false;
            txt_OldPass.Clear();
            txt_NewPass.Clear();
            txt_ConfirmPass.Clear();
        }

        private void Btn_CancelPass_Click(object sender, EventArgs e)
        {
            panel_ChangePassword.Visible = false;
            txt_OldPass.Clear();
            txt_NewPass.Clear();
            txt_ConfirmPass.Clear();
        }
    }
}
