using System;

namespace PBL2_BookStoreManagement.DTO
{
    class Cart : BookBase
    {
        public double total_price { get; set; }

        public Cart(string id, string name, int quantity, double total_price)
            : base(id, name, quantity)
        {
            this.total_price = Math.Round(total_price, 2);
        }
    }
}
