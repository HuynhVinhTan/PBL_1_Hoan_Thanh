using System.Net;

namespace PBL2_BookStoreManagement.DTO
{
    class Manager : User
    {
        string Man_ID;

        public Manager() { }
        public Manager(string man_id, string name, string userName, string phone, string email, string address, string password)
            : base(userName, password, name, phone, email)
        {
            this.Man_ID = man_id;
        }
    }
}
