using PBL2_BookStoreManagement.BUS;
using PBL2_BookStoreManagement.DAL;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;

namespace PBL2_BookStoreManagement.View
{
    public partial class fCus_Overview: Form
    {
        public fCus_Overview()
        {
            InitializeComponent();
            if (this.Parent != null)
            {
                this.Width = this.Parent.Width;
                this.Height = this.Parent.Height;
            }


            txt_Address.Enabled = false;
            txt_Email.Enabled = false;
            txt_Phone.Enabled = false;
            txt_Name.Enabled = false;
            txt_Name.Enabled = false;
            txt_Name.Text = Session.Cur_cus.Name;
            txt_Phone.Text = Session.Cur_cus.Phone;
            txt_Email.Text = Session.Cur_cus.Email;
            txt_Address.Text = Session.Cur_cus.Address;

            lbl_Totalspending.Text = BUS_Invoice.Instance.GetInvoice(Session.Cur_cus.Cus_ID).Sum(i => i.TotalAmount).ToString("C", System.Globalization.CultureInfo.CurrentCulture);
            lbl_Totalinvoice.Text = BUS_Invoice.Instance.GetInvoice(Session.Cur_cus.Cus_ID).Count.ToString();
            //set up
            setup_Interface();
        }
        private void setup_Interface()
        {
            //set màu chữ cho txtbox
            txt_Name.ForeColor = ColorTranslator.FromHtml("#0D1B2A");
            txt_Email.ForeColor = ColorTranslator.FromHtml("#0D1B2A");
            txt_Phone.ForeColor = ColorTranslator.FromHtml("#0D1B2A");
            txt_Address.ForeColor = ColorTranslator.FromHtml("#0D1B2A");

            //set màu chữ cho label
            lbl_Totalspending.Location = new Point(
            (panel6.Width - lbl_Totalspending.Width) / 2,
            (panel6.Height - lbl_Totalspending.Height) / 2
            );
            lbl_Totalinvoice.Location = new Point(
            (panel5.Width - lbl_Totalinvoice.Width) / 2,
            (panel5.Height - lbl_Totalinvoice.Height) / 2
            );
            label6.Location = new Point(
            (panel7.Width - label6.Width) / 2,
            (panel7.Height - label6.Height) / 2
            );
            label7.Location = new Point(
            (panel8.Width - label7.Width) / 2,
            (panel8.Height - label7.Height) / 2
            );
        }

        private void fCus_Overview_Load(object sender, System.EventArgs e)
        {
            txt_Name.ForeColor = ColorTranslator.FromHtml("#0D1B2A");
            txt_Email.ForeColor = ColorTranslator.FromHtml("#0D1B2A");
            txt_Phone.ForeColor = ColorTranslator.FromHtml("#0D1B2A");
            txt_Address.ForeColor = ColorTranslator.FromHtml("#0D1B2A");
        }

        private void txt_Name_TextChanged(object sender, System.EventArgs e)
        {

        }
    }
}
