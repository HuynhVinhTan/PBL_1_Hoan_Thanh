using PBL2_BookStoreManagement.DTO;
using System.Collections.Generic;
using PBL2_BookStoreManagement.DAL;
using System.Windows.Forms;
using System.Linq;

namespace PBL2_BookStoreManagement.BUS
{
    class Bus_Account
    {
        #region Singleton
        private static Bus_Account instance;
        private Bus_Account() { }
        public static Bus_Account Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Bus_Account();
                }
                return instance;
            }
        }
        #endregion

        #region Login Operations
        public bool check_admin(string userName, string password)
        {
            return userName == "admin123" && password == "admin123";
        }
        public bool check_Customer(string username, string password)
        {
            List<Customer> customers = new List<Customer>();
            customers = DAL_Customer.Instance.GetCustomers();
            return customers.Exists(c => c.UserName== username && c.Password == password);
        }
        public bool check_Customer(string username) //overloading
        {
            List<Customer> customers = new List<Customer>();
            customers = DAL_Customer.Instance.GetCustomers();
            return customers.Exists(c => c.UserName == username);
        }
        public bool check_Email(string email)
        {
            List<Customer> customers = new List<Customer>();
            customers = DAL_Customer.Instance.GetCustomers();
            return customers.Exists(c => c.Email == email);
        }
        public bool check_Phone(string phone)
        {
            List<Customer> customers = new List<Customer>();
            customers = DAL_Customer.Instance.GetCustomers();
            return customers.Exists(c => c.Phone == phone);
        }
        public void save_Session(string userName) //Save info of customer
        {
            Customer cus = DAL_Customer.Instance.get_Customers(userName);
            Session.Cur_cus = cus;
        }
        public void clear_Session() //Clear info of customer
        {
            Session.Cur_cus = null;
        }

        public bool IsValidUsername(string username, out string error) //Username có hợp lệ không
        {
            error = "";
            if (username.Length < 4 || username.Length > 20)
            {
                error = "Username phải từ 4 đến 20 ký tự.";
                return false;
            }

            if (!char.IsLetter(username[0]))
            {
                error = "Username phải bắt đầu bằng chữ cái.";
                return false;
            }

            foreach (char c in username)
            {
                if (!(char.IsLetterOrDigit(c) || c == '_' || c == '.'))
                {
                    error = "Username chỉ được chứa chữ, số, dấu _ hoặc dấu .";
                    return false;
                }
            }

            return true;
        }

        public bool IsValidPassword(string password, out string error)
        {
            error = "";
            if (password.Length < 6)
            {
                error = "Mật khẩu phải có ít nhất 6 ký tự.";
                return false;
            }

            // Gợi ý thêm: chứa cả chữ và số
            if (!password.Any(char.IsLetter) || !password.Any(char.IsDigit))
            {
                error = "Mật khẩu phải chứa cả chữ và số.";
                return false;
            }

            return true;
        }

        public bool IsValidEmail(string email, out string error)
        {
            error = "";
            if (!email.Contains("@") || !email.Contains("."))
            {
                error = "Email không hợp lệ.";
                return false;
            }
            return true;
        }

        public bool IsValidPhone(string phone, out string error)
        {
            error = "";
            if (phone.Length != 10 || !phone.All(char.IsDigit))
            {
                error = "Số điện thoại phải có 10 chữ số.";
                return false;
            }
            return true;
        }

        public bool IsValidAddress(string address, out string error)
        {
            error = "";
            if (string.IsNullOrWhiteSpace(address))
            {
                error = "Địa chỉ không được để trống.";
                return false;
            }
            return true;
        }

        #endregion
    }
}
