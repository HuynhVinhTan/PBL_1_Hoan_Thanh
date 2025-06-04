namespace PBL2_BookStoreManagement.DTO
{
    class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; } // Optional, can be used for Customer or Employee
        public string Phone { get; set; } // Optional, can be used for Customer or Employee
        public string Email { get; set; } // Optional, can be used for Customer or Employee
        public User() { }

        public User(string userName, string password, string name, string phone, string email)
        {
            UserName = userName;
            Password = password;
            Name = name;
            Phone = phone;
            Email = email;
        }
    }
}
