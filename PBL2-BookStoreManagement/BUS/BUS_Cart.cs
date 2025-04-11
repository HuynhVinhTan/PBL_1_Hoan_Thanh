using System;
using System.Collections.Generic;
using System.Linq;
using PBL2_BookStoreManagement.DAL;
using PBL2_BookStoreManagement.DTO;


namespace PBL2_BookStoreManagement.BUS
{
    class BUS_Cart
    {
        #region khoi tao
        private static BUS_Cart instance;
        private BUS_Cart() { }
        public static BUS_Cart Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BUS_Cart();
                }
                return instance;
            }
        }
        #endregion
        private static List<Cart> CartItems = new List<Cart>();

        #region method
        //Add To Cart
        public bool AddToCart(Book book)
        {
            var bookincart = CartItems.FirstOrDefault(b => b.book_ID == book.book_ID);

            if (bookincart == null)
            {
                CartItems.Add(new Cart(book.book_ID, book.book_name, 1, book.book_price));
                return true;
            }

            if (bookincart.book_quantity + 1 > book.book_quantity)
            {
                return false;
            }

            bookincart.book_price += book.book_price;
            bookincart.book_price = Math.Round(bookincart.book_price, 2);
            bookincart.book_quantity += 1;
            return true;
        }

        //Update Cart
        public bool UpdateCart(string bookId, string status)
        {
            var bookInStore = DAL_Book.Instance.LoadBooks().FirstOrDefault(b => b.book_ID == bookId);
            if (bookInStore == null) return false;

            var bookincart = CartItems.FirstOrDefault(b => b.book_ID == bookId);
            if (bookincart == null) return false;

            switch (status)
            {
                case "Increase":
                    if (bookincart.book_quantity + 1 > bookInStore.book_quantity) // Kiểm tra không vượt quá số lượng trong kho
                    {
                        return false; // Không thể tăng số lượng vì vượt quá số lượng
                    }
                    bookincart.book_price += bookincart.book_price / bookincart.book_quantity;// Tăng giá trị theo số lượng
                    bookincart.book_price = Math.Round(bookincart.book_price, 2);
                    bookincart.book_quantity += 1;
                    break;
                case "Decrease":
                    if(bookincart.book_quantity - 1 == 0)
                    { 
                        CartItems.Remove(bookincart); // Nếu số lượng bằng 0 thì xóa khỏi giỏ
                    }
                    bookincart.book_price -= bookincart.book_price / bookincart.book_quantity; // Giảm giá trị theo số lượng
                    bookincart.book_price = Math.Round(bookincart.book_price, 2);
                    bookincart.book_quantity -= 1;
                    break;
            }
            return true;
        }

        //Clear Cart
        public void ClearCart()
        {
            CartItems.Clear();
        }

        //Remove Item From Cart
        public void RemoveFromCart(string bookId)
        {
            var bookToRemove = CartItems.FirstOrDefault(b => b.book_ID == bookId);
            CartItems.Remove(bookToRemove);
        }
        //Get Item From Cart
        public List<Cart> GetCart()
        {
            return CartItems;
        }

        //Get Price Of Cart
        public double GetTotalPrice()
        {
            return CartItems.Sum(cart => Math.Round(cart.book_price, 2));
        }
        #endregion

    }
}
