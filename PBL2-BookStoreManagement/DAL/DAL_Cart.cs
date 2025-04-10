using System.Collections.Generic;
using System.Linq;
using PBL2_BookStoreManagement.DTO;
using System.IO;
using System;

namespace PBL2_BookStoreManagement.DAL
{
    class DAL_Cart
    {
        #region attribute
        private List<Book> Cart { get; set; } = new List<Book>();
        #endregion

        #region khoi tao
        private static DAL_Cart _Instance;
        public static DAL_Cart Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DAL_Cart();
                }
                return _Instance;
            }
            private set { }
        }
        private DAL_Cart() { }
        #endregion

        #region File Path
        private static string filePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\PBL2-BookStoreManagement\Data\Carts.csv";
        #endregion

        #region Methods
        public void AddToCart(Book book)
        {
            // Kiểm tra xem sách đã có trong giỏ chưa
            var existingBook = Cart.FirstOrDefault(b => b.book_ID == book.book_ID);

            if (existingBook != null)
            {
                // Nếu sách đã có trong giỏ, cập nhật số lượng và giá
                existingBook.book_quantity += 1;  // Tăng số lượng lên 1
                existingBook.book_price += book.book_price;  // Cập nhật giá trị
                existingBook.book_price = Math.Round(existingBook.book_price,2);  // Làm tròn giá trị
            }
            else
            { 
                Cart.Add(new Book(book.book_ID, book.book_name, book.book_author, book.book_genre, 1, book.book_price));
            }
        }
        public void UpdateCart(string bookId, string status)
        {
            var bookincart = Cart.FirstOrDefault(b => b.book_ID == bookId);
            
            if (bookincart == null) return;

            switch (status)
            {
                case "Increase":
                    bookincart.book_price += bookincart.book_price / bookincart.book_quantity;// Tăng giá trị theo số lượng
                    bookincart.book_price = Math.Round(bookincart.book_price, 2);
                    bookincart.book_quantity += 1;
                    break;

                case "Decrease":
                    bookincart.book_price -= bookincart.book_price / bookincart.book_quantity; // Giảm giá trị theo số lượng
                    bookincart.book_price = Math.Round(bookincart.book_price, 2);
                    bookincart.book_quantity -= 1;
                    break;
            }
        }
        public void RemoveFromCart(string bookId)
        {
            var bookToRemove = Cart.FirstOrDefault(b => b.book_ID == bookId);
            Cart.Remove(bookToRemove);
        }
        public void ClearCart()
        {
            Cart.Clear();
        }
        public double GetTotalPrice()
        {
            return Cart.Sum(book => Math.Round(book.book_price, 2));  // Tính tổng giá trị dựa trên số lượng
        }
        public List<Book> GetCart()
        {
            return Cart;
        }
        #endregion
    }
}