using PBL2_BookStoreManagement.DTO;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using BookStoreApp.DAL;

namespace PBL2_BookStoreManagement.DAL
{
    class DAL_Book
    {
        #region khoi tao
        private static DAL_Book _instance;
        public static DAL_Book Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DAL_Book();
                }
                return _instance;
            }
            private set { }
        }
        private DAL_Book() { }
        #endregion

        #region File Path
        private static string filePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\PBL2-BookStoreManagement\Data\Books.csv";
        #endregion

        #region Book_Cart
        public List<Book> Cart { get; set; } = new List<Book>();
        public void AddToCart(Book book)
        {
            Cart.Add(book);
        }
        public void RemoveFromCart(Book book)
        {
            Cart.Remove(book);
        }
        public void ClearCart()
        {
            Cart.Clear();
        }
        public double GetTotalPrice()
        {
            return Cart.Sum(book => book.book_price);
        }
        public int GetTotalQuantity()
        {
            return Cart.Sum(book => book.book_quantity);
        }
        public List<Book> GetCart()
        {
            return Cart;
        }
        public void SetCart(List<Book> books)
        {
            Cart = books;
        }
        #endregion

        #region Methods
        public List<Book> LoadBooks()
        {
            List<Book> books = new List<Book>();
            List<string[]> data = DataProvider.Instance.ReadCsv(filePath);
            foreach (var row in data)
            {
                if (row.Length == 6)
                {
                    books.Add(new Book(row[0], row[1], row[2], row[3],int.Parse(row[4]), double.Parse(row[5])));
                }
            }
            return books;
        }

        public void SaveBooks(List<Book> books)
        { 
            List<string[]> data = books.Select(book => new string[] { book.book_ID, book.book_name, book.book_author, book.book_genre, book.book_quantity.ToString(), book.book_price.ToString() }).ToList();
            DataProvider.Instance.WriteCsv(filePath, data);
        }
        #endregion

    }
}
