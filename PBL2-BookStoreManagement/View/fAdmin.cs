using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices; // Thêm thư viện để kéo form

namespace PBL2_BookStoreManagement.View
{
    public partial class fAdmin : Form
    {
        // Dùng Windows API để kéo form
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;

        public fAdmin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            LoadFormIntoPanel(new fAdmin_Overview());

            // Gắn sự kiện kéo form cho thanh bar (panel2 hoặc panelBar tùy tên bạn đặt)
            panel2.MouseDown += bar_MouseDown; // panel2 chính là thanh bar của bạn
        }

        private void LoadFormIntoPanel(Form form)
        {
            panel3.Controls.Clear();

            if (form is fAdmin_Overview overviewForm)
            {
                overviewForm.SetParentForm(this);
            }

            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            panel3.Controls.Add(form);
            form.Show();
        }

        private void label2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            fLogin login = new fLogin();
            login.Show();
        }

        private void btn_logout(object sender, EventArgs e)
        {
            this.Hide();
            fLogin login = new fLogin();
            login.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            LoadFormIntoPanel(new fAdmin_Overview());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new fAdmin_Cus());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new fAdmin_Book());
        }

        // Hàm xử lý kéo form khi giữ chuột trái
        private void bar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new fAdmin_Invoices());
        }
    }
}
