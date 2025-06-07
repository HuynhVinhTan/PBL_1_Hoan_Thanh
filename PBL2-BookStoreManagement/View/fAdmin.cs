using System;
using System.Drawing;
using System.Windows.Forms;

namespace PBL2_BookStoreManagement.View
{
    public partial class fAdmin : Form
    {
        public fAdmin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            LoadFormIntoPanel(new fAdmin_Overview());
        }
        private void LoadFormIntoPanel(Form form)
        {
            panel3.Controls.Clear();

            if (form is fAdmin_Overview overviewForm)
            {
                overviewForm.SetParentForm(this); // Truyền fAdmin vào
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
    }
}
