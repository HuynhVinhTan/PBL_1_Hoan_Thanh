using PBL2_BookStoreManagement.DTO;
using System.Collections.Generic;
using PBL2_BookStoreManagement.DAL;


namespace PBL2_BookStoreManagement.BUS
{
    class BUS_Customer
    {
        #region Singleton
        private static BUS_Customer instance;
        private BUS_Customer() { }
        public static BUS_Customer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BUS_Customer();
                }
                return instance;
            }
        }
        #endregion

        #region Customer Operations

        List<Customer> customers = new List<Customer>();
        // lấy toàn bộ các dòng dữ liệu
        public List<Customer> GetAllCustomer()
        {
            customers = DAL_Customer.Instance.GetCustomers();
            return customers;
        }

        public int CountCustomer()
        {
            return DAL_Customer.Instance.CountCustomer();
        }

        public void DeleteCustomer(int index)
        {
            DAL_Customer.Instance.DeleteCustomer(index);
        }

        public void UpdateCustomer(Customer customer, int index)
        {
            DAL_Customer.Instance.UpdateCustomer(customer, index);
        }
        public void UpdateCustomer(Customer customer) // overload for updating without index
        {
            DAL_Customer.Instance.UpdateCustomer(customer);
        }
        public void AddCustomer(Customer customer)
        {
            DAL_Customer.Instance.AddCustomer(customer);
        }
        #endregion

    }
}
