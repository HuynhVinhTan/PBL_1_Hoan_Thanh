using BookStoreApp.DAL;
using PBL2_BookStoreManagement.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBL2_BookStoreManagement.DAL
{
    class DAL_Customer
    {

        #region Singleton
        private static DAL_Customer instance;
        public static DAL_Customer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DAL_Customer();
                }
                return instance;
            }
        }
        private DAL_Customer() { }
        #endregion

        #region filepath
        private static string filePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\PBL2-BookStoreManagement\Data\Customers.csv";
        #endregion

        #region Methods

        public List<Customer> customers = new List<Customer>();
        // truy xuất dữ liệu
        public List<Customer> GetCustomers()
        {
            try
            {
                customers.Clear();
                List<string[]> data = DataProvider.Instance.ReadCsv(filePath);
                if (data.Count == 0)
                {
                    System.Diagnostics.Debug.WriteLine("Không có dữ liệu trong file CSV.");
                }
                foreach (var row in data)
                {
                    if (row.Length == 7)
                    {
                        customers.Add(new Customer(row[0], row[1], row[2], row[3], row[4], row[5], row[6]));
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"Dòng không đủ cột: {string.Join(",", row)}");
                    }
                }
                System.Diagnostics.Debug.WriteLine($"Tổng số khách hàng: {customers.Count}");
                return customers;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi đọc CSV: {ex.Message}");
                throw new Exception($"Lỗi khi đọc dữ liệu khách hàng: {ex.Message}");
            }
        }

        public void UpdateCustomer(Customer customer, int index)
        {
            // Cập nhật thông tin khách hàng
            customers[index] = customer;

            // Ghi lại toàn bộ danh sách vào file CSV
            List<string[]> allData = new List<string[]>();
            allData.AddRange(customers.Select(c => new string[] { c.Cus_ID, c.Name, c.UserName, c.Phone, c.Email, c.Address, c.Password }).ToList());
            List<string> header = new List<string> {"cus_id", "cus_name", "cus_phone", "cus_email", "cus_address", "cus_pass"  };
            DataProvider.Instance.Write_CSV(filePath, allData,header);
        }

        public void UpdateCustomer(Customer customer) //overloading
        {
            List<Customer> allCustomers = GetCustomers();
            var customerToUpdate = allCustomers.FirstOrDefault(c => c.Cus_ID == customer.Cus_ID);
            if (customerToUpdate != null)
            {
                customerToUpdate.Name = customer.Name;
                customerToUpdate.UserName = customer.UserName;
                customerToUpdate.Phone = customer.Phone;
                customerToUpdate.Email = customer.Email;
                customerToUpdate.Address = customer.Address;
                customerToUpdate.Password = customer.Password;
                // Ghi lại toàn bộ danh sách vào file CSV
                List<string[]> allData = new List<string[]>();
                allData.AddRange(allCustomers.Select(c => new string[] { c.Cus_ID, c.Name, c.UserName, c.Phone, c.Email, c.Address, c.Password }).ToList());
                List<string> header = new List<string> { "cus_id", "cus_name", "cus_phone", "cus_email", "cus_address", "cus_pass" };
                DataProvider.Instance.Write_CSV(filePath, allData, header);
            }
        }

        public Customer get_Customers(string username) //overloading
        {
            return customers.FirstOrDefault(c => c.UserName == username);
        }

        public void AddCustomer(Customer customer)
        {
            customers.Add(customer);
            List<string[]> row = new List<string[]>
            {
                new string[] { customer.Cus_ID, customer.Name, customer.UserName, customer.Phone, customer.Email, customer.Address, customer.Password }
            };
            DataProvider.Instance.Append_CSV(filePath, row);
        }

        public int CountCustomer()
        { 
            return GetCustomers().Count;
        }

        public void DeleteCustomer(int index)
        {
            // Kiểm tra xem index có hợp lệ không
            if (index < 0 || index >= customers.Count)
            {
                throw new ArgumentOutOfRangeException("Index is out of range.");
            }

            // Xóa khách hàng tại vị trí index
            customers.RemoveAt(index);

            // Cập nhật lại UserId từ vị trí index trở đi
            Update_User_ID(customers, index);

            // Ghi lại toàn bộ danh sách vào file CSV
            List<string[]> allData = new List<string[]>();
            allData.AddRange(customers.Select(c => new string[] { c.Cus_ID, c.UserName,/* c.Name,*/ c.Phone, c.Email, c.Address, c.Password }).ToList());
            DataProvider.Instance.Write_CSV(filePath, allData);
        }

        public void Update_User_ID(List<Customer> customers, int index)
        {
            // Cập nhật UserId cho tất cả khách hàng
            for (int i = index; i < customers.Count; i++)
            {
                customers[i].Cus_ID = "U" + (i + 1).ToString("D3");
            }
        }
        #endregion
    }
}
