namespace PBL2_BookStoreManagement.DTO
{
    class Customer : User
    {
        public string Cus_ID { get; set; }
        public string Address { get; set; }

        public Customer() { }

        public Customer(string cus_id, string name, string userName, string phone, string email, string address, string password)
            : base(userName, password, name, phone, email)
        {
            Cus_ID = cus_id;
            Address = address;
        }
    }
}
