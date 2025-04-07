using System.Collections.Generic;
using PBL2_BookStoreManagement.DTO;
using PBL2_BookStoreManagement.DAL;

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
        #endregion
    }
}