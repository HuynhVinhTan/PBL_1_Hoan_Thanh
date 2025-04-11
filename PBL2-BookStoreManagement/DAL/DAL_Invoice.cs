using BookStoreApp.DAL;
using PBL2_BookStoreManagement.BUS;
using PBL2_BookStoreManagement.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace PBL2_BookStoreManagement.DAL
{
    class DAL_Invoice
    {
        #region khoi tao
        private static DAL_Invoice _instance;
        public static DAL_Invoice Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DAL_Invoice();
                }
                return _instance;
            }
            private set { }
        }
        private DAL_Invoice() { }
        #endregion
        #region File Path
        private static string filePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\PBL2-BookStoreManagement\Data";
        #endregion

        #region Methods
        //Tao folder "{userID}" trong Data
        public string GetOrCreateUserFolder(string userID)
        {
            string folderPath = filePath + $"/{userID}";
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            return folderPath;
        }
        public List<Invoice> get_Invoice(string userID)
        {
            string folderPath = GetOrCreateUserFolder(userID);
            string filePath = Path.Combine(folderPath, "summary.csv");
            List<string[]> data = DataProvider.Instance.ReadCsv(filePath);
            List<Invoice> invoices = new List<Invoice>();
            foreach (var item in data)
            {
                if (item.Length == 4)
                {
                    Invoice invoice = new Invoice(item[0], item[1], DateTime.Parse(item[2]), null, double.Parse(item[3]));
                    invoices.Add(invoice);
                }
            }
            return invoices;
        }

        public void create_Invoice_Info(string userid, string username, Invoice invoice) //tao thong tin hoa don
        {
            string userID = Session.Cur_cus.User_id;
            string folderPath = GetOrCreateUserFolder(userID); // ví dụ: Data/123456

            // Tạo file TXT định dạng hóa đơn
            string txtFileName = $"Invoice_{invoice.InvoiceID}_{userID}.txt"; // format file: "Invoice_{invoice.InvoiceID}_{userID}"
            string txtPath = Path.Combine(folderPath, txtFileName); 

            using (StreamWriter writer = new StreamWriter(txtPath))
            {
                writer.WriteLine("========== BOOK STORE INVOICE ==========");
                writer.WriteLine($"Invoice ID   : {invoice.InvoiceID}");
                writer.WriteLine($"Customer ID  : {userid}");
                writer.WriteLine($"Customer Name: {username}");
                writer.WriteLine($"Date Created : {invoice.DateCreated:dd/MM/yyyy}");
                writer.WriteLine("----------------------------------------");
                // Tìm độ dài lớn nhất của tên sách
                int maxNameLength = invoice.Items.Max(i => i.book_name.Length);
                int nameColWidth = Math.Max(maxNameLength + 5, 30); // ít nhất 30

                // In tiêu đề cột
                writer.WriteLine(string.Format($"{{0,-{nameColWidth}}} {{1,10}} {{2,15}}", "Item", "Quantity", "Price"));

                foreach (var item in invoice.Items)
                {
                    string quantityStr = item.book_quantity.ToString();
                    int quantityWidth = 10;
                    int padding = (quantityWidth - quantityStr.Length) / 2;
                    string quantityCentered = quantityStr.PadLeft(padding + quantityStr.Length).PadRight(quantityWidth);

                    // Format dòng từng sách
                    string line = string.Format($"{{0,-{nameColWidth}}} {{1}} {{2,15:C}}",
                                                item.book_name,
                                                quantityCentered,
                                                item.book_price);
                    writer.WriteLine(line);
                }



                writer.WriteLine("----------------------------------------");
                writer.WriteLine(String.Format("{0,50}", $"Total: {invoice.TotalAmount:C}"));
                writer.WriteLine("============= THANK YOU! ===============");
            }
        }
        public string ReadInvoiceTextFile(string userId, string invoiceId)
        {
            string folderPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + $@"\PBL2-BookStoreManagement\Data\{userId}";
            string filePath = Path.Combine(folderPath, $"Invoice_{invoiceId}_{userId}.txt");

            if (!File.Exists(filePath))
                return "Invoice not found.";

            return File.ReadAllText(filePath);
        }

        public void update_UserID_summary(string userID, Invoice invoice) //cap nhat summary.csv
        { 
            string folderPath = GetOrCreateUserFolder(userID);
            string filePath = Path.Combine(folderPath, "summary.csv");
            List<string[]> data = DataProvider.Instance.ReadCsv(filePath);
            List<string[]> newData = new List<string[]>();
            foreach (var item in data)
            {
                if (item.Length == 4)
                {
                    newData.Add(item);
                }
            }
            newData.Add(new string[] { invoice.InvoiceID, invoice.CustomerID, invoice.DateCreated.ToString(), invoice.TotalAmount.ToString() });
            DataProvider.Instance.Write_CSV(filePath, newData);
        } 
    #endregion
}
}
