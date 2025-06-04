using BookStoreApp.DAL;
using PBL2_BookStoreManagement.BUS;
using PBL2_BookStoreManagement.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;


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
            string userID = Session.Cur_cus.Cus_ID;
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
                writer.WriteLine($"Phone Number : {Session.Cur_cus.Phone}");
                writer.WriteLine($"Email        : {Session.Cur_cus.Email}");
                writer.WriteLine($"Address      : {Session.Cur_cus.Address}");
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
            List<string> header = new List<string> { "invoice_id", "cus_id", "data_created", "total_amount" };
            DataProvider.Instance.Write_CSV(filePath, newData,header);
        }

        public void update_Invoice(Invoice invoice) //cap nhat hoa don vao 2 file Invoice.csv va Invoice_Info.csv
        {
            List<string[]> data_Inv_Info = DataProvider.Instance.ReadCsv(Path.Combine(filePath, "Invoice_Info.csv"));
            List<string[]> data_Inv = DataProvider.Instance.ReadCsv(Path.Combine(filePath, "Invoice.csv"));
            List<string[]> newData_Inv_Info = new List<string[]>();
            List<string[]> newData_Inv = new List<string[]>();
            foreach (var item in data_Inv)
            {
                if(item.Length == 4) newData_Inv.Add(item);
            }
            newData_Inv.Add(new string[] { invoice.InvoiceID, invoice.CustomerID, invoice.DateCreated.ToString(), invoice.TotalAmount.ToString() });

            foreach (var item in data_Inv_Info)
            {
                if(item.Length == 4) newData_Inv_Info.Add(item);
            }
            foreach(var book in invoice.Items)
            {
                newData_Inv_Info.Add(new string[] { invoice.InvoiceID, invoice.CustomerID, book.book_ID, book.book_quantity.ToString() });
            }

            List<string> header_Inv_Info = new List<string> { "invoice_id", "cus_id", "book_id", "book_quantity" };
            List<string> header_Inv = new List<string> { "invoice_id", "cus_id", "data_created", "total_amount" };
            DataProvider.Instance.Write_CSV(Path.Combine(filePath, "Invoice_Info.csv"), newData_Inv_Info, header_Inv_Info);
            DataProvider.Instance.Write_CSV(Path.Combine(filePath, "Invoice.csv"), newData_Inv, header_Inv);
        }
        public void update_SoldProduct(List<Cart> soldproduct) //cap nhat vao file SoldProducts.csv
        {
            List<Cart> data = get_SoldProduct();
            foreach(var item in soldproduct)
            {
                var existingItem = data.FirstOrDefault(p => p.book_ID == item.book_ID);
                if (existingItem != null)
                { 
                    existingItem.book_quantity += item.book_quantity; //tang so luong neu da ton tai
                    existingItem.book_price += item.book_price; //tang tong tien neu da ton tai
                }else data.Add(item); //neu chua ton tai thi them vao danh sach
            }

            List<string[]> newData = new List<string[]>();
            foreach (var item in data)
            {
                newData.Add(new string[] { item.book_ID, item.book_name, item.book_quantity.ToString(), item.book_price.ToString() });
            }
            List<string> header = new List<string> { "book_id", "book_name", "book_quantity", "book_price" };
            DataProvider.Instance.Write_CSV(Path.Combine(filePath, "SoldProducts.csv"), newData, header);
        }

        public List<Cart> get_SoldProduct() //danh cho thong ke - lay thong tin tu file SoldProducts.csv
        {
            List<Cart> soldProducts = new List<Cart>();
            List<string[]> data = DataProvider.Instance.ReadCsv(Path.Combine(filePath, "SoldProducts.csv"));
            foreach (var item in data)
            {
                if (item.Length == 4)
                {
                    string bookID = item[0];
                    string bookName = item[1];
                    int quantity = int.Parse(item[2]);
                    double price = double.Parse(item[3]);
                    soldProducts.Add(new Cart(bookID, bookName, quantity, price));
                }
            }
            return soldProducts;
        }
        public List<Invoice> get_Total_Invoice()//danh cho thong ke - lay thong tin hóa đơn tu 2 file Invoice.cs
        {
            List<Invoice> invoices = new List<Invoice>();

            // Đọc hóa đơn (invoice tổng quan)
            List<string[]> invoiceData = DataProvider.Instance.ReadCsv(Path.Combine(filePath,"Invoice.csv")); // vẫn giữ cũ
                                                                       // Đọc chi tiết hóa đơn
            List<string[]> itemData = DataProvider.Instance.ReadCsv(Path.Combine(filePath, "Invoice_Info.csv")); // theo format mới

            foreach (var inv in invoiceData)
            {
                string invoiceID = inv[0];
                string customerID = inv[1];
                DateTime dateCreated = DateTime.Parse(inv[2]);
                double totalAmount = double.Parse(inv[3]);

                List<Cart> items = new List<Cart>();
                List<Book> booksinstore = DAL_Book.Instance.GetBooks();
                foreach (var item in itemData)
                {
                    string itemInvoiceID = item[0];
                    string itemCustomerID = item[1];

                    if (itemInvoiceID == invoiceID && itemCustomerID == customerID)
                    {
                        string productID = item[2];
                        int quantity = int.Parse(item[3]);

                        Book book = booksinstore.FirstOrDefault(b => b.book_ID == productID);

                        items.Add(new Cart(
                            productID,
                            book?.book_name ?? "Unknown",
                            quantity,
                            (book?.book_price ?? 0.0) * quantity
                        ));
                    }
                }
                invoices.Add(new Invoice(invoiceID, customerID, dateCreated, items, totalAmount));
            }

            return invoices;
        }

        public double get_Total_Spending(string userID) { 
            string folderPath = GetOrCreateUserFolder(userID);
            //check xem folder da ton tai chua
            if (!Directory.Exists(folderPath))
            {
                // Neu khong ton tai thi tao folder
                Directory.CreateDirectory(folderPath);
                return 0;
            }
            string filePath = Path.Combine(folderPath, "summary.csv");
            List<Invoice> data = get_Invoice(userID);
            double total = 0;
            foreach (var item in data)
            {
                total = total + item.TotalAmount;
            }
            return total;
        }

        public double get_Total_Invoice(string userID) //danh cho customer-overload
        {
            string folderPath = GetOrCreateUserFolder(userID);
            //check xem folder da ton tai chua
            if (!Directory.Exists(folderPath))
            {
                // Neu khong ton tai thi tao folder
                Directory.CreateDirectory(folderPath);
                return 0;
            }
            string filePath = Path.Combine(folderPath, "summary.csv");
            List<Invoice> data = get_Invoice(userID);
            return data.Count;
        }
        #endregion
    }
}
