using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using PBL2_BookStoreManagement.DTO;
using PBL2_BookStoreManagement.BUS;
using System.Linq;


namespace PBL2_BookStoreManagement.View
{
    public partial class fAdmin_Overview : Form
    {
        public fAdmin_Overview()
        {
            InitializeComponent();

            txt_SoldBook.Text = BUS_Invoice.Instance.GetSoldBook().Count().ToString();
            txt_Revenue.Text = BUS_Invoice.Instance.GetInvoice().Sum(i => i.TotalAmount).ToString("C", System.Globalization.CultureInfo.CurrentCulture);
            txt_Customer.Text = BUS_Customer.Instance.GetAllCustomer().Count().ToString();
            txt_Invoices.Text = BUS_Invoice.Instance.GetInvoice().Count().ToString();
        }

        private Dictionary<string, double> GetRevenueByDay()
        {
            List<Invoice> invoices = BUS_Invoice.Instance.GetInvoice();
            Dictionary<string, double> revenue = new Dictionary<string, double>();

            foreach (var invoice in invoices)
            {
                string date = invoice.DateCreated.ToString("yyyy-MM-dd"); // Format theo ngày
                if (revenue.ContainsKey(date))
                {
                    revenue[date] += invoice.TotalAmount;
                }
                else
                {
                    revenue[date] = invoice.TotalAmount;
                }
            }
            return revenue;
        }


        private Dictionary<string, double> GetTopSellingProducts()
        {
            List<Cart> soldbook = BUS_Invoice.Instance.GetSoldBook();
            Dictionary<string, double> productSales = new Dictionary<string, double>();
            foreach (var item in soldbook)
            {
                    productSales[item.book_name] = item.book_quantity;
            }
            return productSales;
        }

        private Dictionary<string, double> GetTopCustomers()
        {
            List<Invoice> invoices = BUS_Invoice.Instance.GetInvoice();
            Dictionary<string, double> customerTotals = new Dictionary<string, double>();

            foreach (Invoice invoice in invoices)
            {
                if (invoice == null || string.IsNullOrEmpty(invoice.CustomerID)) continue;

                // Lấy thông tin khách hàng
                Customer customer = BUS_Customer.Instance.GetAllCustomer().FirstOrDefault(c => c.Cus_ID == invoice.CustomerID);
                if (customer == null) continue;

                string customerName = customer.Name;

                if (!customerTotals.ContainsKey(customerName))
                {
                    customerTotals[customerName] = invoice.TotalAmount;
                }
                else
                {
                    customerTotals[customerName] += invoice.TotalAmount;
                }
            }

            // Sắp xếp giảm dần theo tổng chi tiêu
            var sorted = customerTotals.OrderByDescending(x => x.Value)
                                       .ToDictionary(x => x.Key, x => x.Value);

            return sorted;
        }


        private void CenterForm(Form form)
        {
            if (form == null) return;

            form.StartPosition = FormStartPosition.Manual;

            var screen = Screen.FromControl(form).WorkingArea;
            form.Location = new Point(
                screen.X + (screen.Width - form.Width) / 2,
                screen.Y + (screen.Height - form.Height) / 2
            );
        }
        private Form parentForm;
        public void SetParentForm(Form parent)
        {
            parentForm = parent;
        }
        private void ShowChartForm(string title, Dictionary<string, double> data, string chartType)
        {
            fChart chartForm = new fChart(title, data, chartType)
            {
                StartPosition = FormStartPosition.CenterScreen
            };

            this.Hide();               // Ẩn fAdmin_Overview
            parentForm?.Hide();        // Ẩn fAdmin

            chartForm.FormClosed += (s, args) =>
            {
                CenterForm(parentForm);
                parentForm?.Show();
                parentForm?.BringToFront();

                CenterForm(this);
                this.Show();
                this.BringToFront();
            };

            chartForm.Show();
        }
        private void btn_Revenue_Click_1(object sender, EventArgs e)
        {
            ShowChartForm("Revenue by Day", GetRevenueByDay(), "Line");
        }

        private void btn_Product_Click(object sender, EventArgs e)
        {
            ShowChartForm("Top Selling Products", GetTopSellingProducts(), "Column");
        }

        private void btn_Customer_Click(object sender, EventArgs e)
        {
            ShowChartForm("Top Customers", GetTopCustomers(), "Column");
        }
    }
}
