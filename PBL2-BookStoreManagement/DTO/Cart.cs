using System;

namespace PBL2_BookStoreManagement.DTO
{
    class Cart
    {
        public string book_ID { get; set; }
        public string book_name { get; set; }
        public int book_quantity { get; set; }
        public double book_price { get; set; }

        public Cart(string book_ID, string book_name, int book_quantity, double book_price)
        {
            this.book_ID = book_ID;
            this.book_name = book_name;
            this.book_quantity = book_quantity;
            this.book_price = Math.Round(book_price, 2);
        }
    }
}
