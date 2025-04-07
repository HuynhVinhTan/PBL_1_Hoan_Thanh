using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL2_BookStoreManagement.DTO
{
    class Customer
    {
        private string cus_id;
        private string cus_name;
        
        public Customer(string user_id, string user_name)
        {
            this.cus_id = user_id;
            this.cus_name = user_name;
        }
        public string User_id { get { return cus_id; } set { cus_id = value; } }
        public string User_name { get { return cus_name; } set { cus_name = value; } }
    }
}
