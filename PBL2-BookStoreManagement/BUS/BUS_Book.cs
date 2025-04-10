using System.Collections.Generic;
using PBL2_BookStoreManagement.DTO;
using PBL2_BookStoreManagement.DAL;
using System.Linq;


namespace PBL2_BookStoreManagement.BUS
{
    class BUS_Book
    {
        #region Singleton
        private static BUS_Book instance;
        private BUS_Book() { }
        public static BUS_Book Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BUS_Book();
                }
                return instance;
            }
        }
        #endregion

        #region Book Operations
        public List<Book> GetAllBooks()
        {
            return DAL_Book.Instance.LoadBooks();
        }
        public List<Book> SearchBooks(string keyword)
        {
            List<Book> books = DAL_Book.Instance.LoadBooks();
            keyword = keyword.ToLower();
            return books.FindAll(b => b.book_name.ToLower().Contains(keyword) ||
                                      b.book_author.ToLower().Contains(keyword) ||
                                      b.book_genre.ToLower().Contains(keyword));
        }

        public void Updated_Book() //cập nhật lại kho sách BUS
        {
            if(DAL_Cart.Instance.GetCart().Count == 0) return;
            List<Book> booksinstore = DAL_Book.Instance.GetBooks();
            foreach (var book in booksinstore)
            {
                var bookincart = DAL_Cart.Instance.GetCart().FirstOrDefault(b => b.book_ID == book.book_ID);
                if (bookincart != null)
                {
                    book.book_quantity -= bookincart.book_quantity;
                }
            }
            DAL_Book.Instance.Updated_Book(booksinstore);
            DAL_Cart.Instance.ClearCart();
        }
        #endregion
    }
}