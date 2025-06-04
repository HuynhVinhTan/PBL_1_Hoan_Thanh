using System;
using System.Drawing;
using System.Windows.Forms;
using PBL2_BookStoreManagement.DTO;
using PBL2_BookStoreManagement.BUS;
using System.Collections.Generic;


namespace PBL2_BookStoreManagement.View
    {
    public partial class fCustomer : Form
    {

        public fCustomer()
        {
            InitializeComponent();
            LoadFormIntoPanel(new fCus_Overview());
            lbl_welcome.Text = "Welcome " + Session.Cur_cus.UserName;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Session.Cur_cus = null;
            fLogin login = new fLogin();
            login.Show();
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

        private void button1_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new fCus_Overview());
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new fCus_Product());
        }

        private void btn_Logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Session.Cur_cus = null;
            fLogin login = new fLogin();
            login.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new fCus_Invoice());
        }
    }
}
