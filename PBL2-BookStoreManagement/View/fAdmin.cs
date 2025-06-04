using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBL2_BookStoreManagement.View
{
    public partial class fAdmin: Form
    {
        public fAdmin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            LoadFormIntoPanel(new fAdmin_Cus());
        }
        private void LoadFormIntoPanel(Form form)
        {
            panel3.Controls.Clear();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.None;
            form.Size = panel3.ClientSize;
            form.Location = new Point(0, 0);
            panel3.Controls.Add(form);
            form.Show();
        }

        private void label2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            fLogin login = new fLogin();
            login.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            LoadFormIntoPanel(new fAdmin_Cus());
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            LoadFormIntoPanel(new fAdmin_Book());
        }

        private void btn_logout(object sender, EventArgs e)
        {
            this.Hide();
            fLogin login = new fLogin();
            login.Show();
        }
    }
}
