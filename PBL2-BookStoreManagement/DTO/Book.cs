using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL2_BookStoreManagement.DTO
{
    class Book
    {
        #region attributes
        public string book_ID { get; set; }
        public string book_name { get; set; }
        public string book_author { get; set; }
        public string book_genre { get; set; }
        public int book_quantity { get; set; }
        public double book_price { get; set; }
        #endregion

        public Book(string id, string name, string author, string genre, int quantity, double price)
        {
            book_ID = id;
            book_name = name;
            book_author = author;
            book_genre = genre;
            book_quantity = quantity;
            book_price = price;
        }
        public override string ToString()
        {
            return $"{book_ID}, {book_name}, {book_author}, {book_genre}, {book_quantity}, {book_price}";
        }
    }
}
