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

        #region method
        //Add To Cart
        public bool AddToCart(Book book)
        {
            var bookincart = DAL_Cart.Instance.GetCart().FirstOrDefault(b => b.book_ID == book.book_ID);
            if (bookincart == null) // Nếu sách chưa có trong giỏ
            {
                DAL_Cart.Instance.AddToCart(book);
                return true;
            }

            // Nếu sách đã có trong giỏ, kiểm tra số lượng
            if (bookincart.book_quantity + 1 > book.book_quantity) // Kiểm tra không vượt quá số lượng trong kho
            {
                return false; // Không thể thêm vì vượt quá số lượng
            }

            // Cập nhật giỏ hàng với sách có số lượng mới
            DAL_Cart.Instance.AddToCart(book);
            return true;
        }

        //Update Cart
        public void UpdateCart(string bookId, string status)
        {
            var bookInStore = DAL_Book.Instance.LoadBooks().FirstOrDefault(b => b.book_ID == bookId);
            if (bookInStore == null) return;

            var bookInCart = DAL_Cart.Instance.GetCart().FirstOrDefault(b => b.book_ID == bookId);
            if (bookInCart == null) return;

            switch (status)
            {
                case "Increase":
                    if (bookInCart.book_quantity + 1 > bookInStore.book_quantity) // Kiểm tra không vượt quá số lượng trong kho
                    {
                        return; // Không thể tăng số lượng vì vượt quá số lượng
                    }
                    DAL_Cart.Instance.UpdateCart(bookId, "Increase");
                    break;
                case "Decrease":
                    if(bookInCart.book_quantity - 1 == 0)
                    {
                        DAL_Cart.Instance.RemoveFromCart(bookId);
                        return; // Nếu số lượng bằng 0 thì xóa khỏi giỏ
                    }
                    DAL_Cart.Instance.UpdateCart(bookId, "Decrease");
                    break;
            }

        }

        //Clear Cart
        public void ClearCart()
        {
            DAL_Cart.Instance.ClearCart();
        }

        //Remove Item From Cart
        public void RemoveFromCart(string bookId)
        { 
            DAL_Cart.Instance.RemoveFromCart(bookId);
            return;
        }

        //Get Item From Cart
        public List<Book> GetCart()
        {
            return DAL_Cart.Instance.GetCart();
        }

        //Get Price Of Cart
        public double GetTotalPrice()
        {
            return DAL_Cart.Instance.GetTotalPrice();
        }
        #endregion

    }
}
