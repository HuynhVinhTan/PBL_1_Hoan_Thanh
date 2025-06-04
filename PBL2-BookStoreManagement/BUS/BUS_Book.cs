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
            return DAL_Book.Instance.GetBooks();
        }
        public List<Book> SearchBooks(string keyword)
        {
            List<Book> books = DAL_Book.Instance.LoadBooks();
            keyword = keyword.ToLower();
            return books.FindAll(b => b.book_name.ToLower().Contains(keyword) ||
                                      b.book_author.ToLower().Contains(keyword) ||
                                      b.book_genre.ToLower().Contains(keyword));
        }

        public bool Updated_Book() //cập nhật lại kho sách BUS
        {
            if(BUS_Cart.Instance.GetCart().Count == 0) return false;
            List<Book> booksinstore = DAL_Book.Instance.GetBooks();
            foreach (var book in booksinstore)
            {
                var bookincart = BUS_Cart.Instance.GetCart().FirstOrDefault(b => b.book_ID == book.book_ID);
                if (bookincart != null)
                {
                    book.book_quantity -= bookincart.book_quantity;
                }
            }
            DAL_Book.Instance.Updated_Book(BUS_Cart.Instance.GetCart());
            return true;
        }
        public void AddBook(string id, string name, string author, string genre, int quantity, double price)
        {
            DAL_Book.Instance.AddBook(id, name, author, genre, quantity, price);
        }

        public void UpdateBook(Book book, int index)
        {
            DAL_Book.Instance.UpdateBook(book, index);
        }

        public void DeleteBook(int index)
        {
            DAL_Book.Instance.DeleteBook(index);
        }


        #endregion
    }
}