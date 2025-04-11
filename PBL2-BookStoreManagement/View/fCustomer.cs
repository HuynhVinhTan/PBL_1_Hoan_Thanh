using System;
using System.Drawing;
using System.Windows.Forms;
using PBL2_BookStoreManagement.DTO;
using PBL2_BookStoreManagement.BUS;

namespace PBL2_BookStoreManagement.View
    {
    public partial class fCustomer : Form
    {
        private string userID = "userid_001";  // ID giả lập
        private string userName = "Vo Nhu Chien";
        public fCustomer()
        {
            InitializeComponent();
            Session.Cur_cus = new Customer(userID, userName);
            LoadFormIntoPanel(new fCus_Overview());
            lbl_welcome.Text = "Welcome " + Session.Cur_cus.User_name;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}
