using PBL2_BookStoreManagement.DTO;
using System.Collections.Generic;
using System.IO;
using BookStoreApp.DAL;
using System.Linq;

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

        #region Methods
        public List<Book> GetBooks()
        {
            List<Book> books = new List<Book>();
            List<string[]> data = DataProvider.Instance.ReadCsv(filePath);
            foreach (var row in data)
            {
                if (row.Length == 6)
                {
                    books.Add(new Book(row[0], row[1], row[2], row[3], int.Parse(row[4]), double.Parse(row[5])));
                }
            }
            return books;
        }
        public List<Book> LoadBooks()
        {
            List<Book> books = new List<Book>();
            List<string[]> data = DataProvider.Instance.ReadCsv(filePath);
            foreach (var row in data)
            {
                if (int.Parse(row[4]) == 0) continue;
                if (row.Length == 6)
                {
                    books.Add(new Book(row[0], row[1], row[2], row[3],int.Parse(row[4]), double.Parse(row[5])));
                }
            }
            return books;
        }
        public void Updated_Book(List<Cart> books) //Cập nhật lại kho sách
        {
            //lay 1 list trong file + 1 list trong cart => cập nhật => rồi ghi lại trên file
            List<Book> booksinstore = GetBooks();
            foreach (var book in books)
            {
                var bookinstore1 = booksinstore.FirstOrDefault(b => b.book_ID == book.book_ID);
                if (bookinstore1 != null)
                {
                    bookinstore1.book_quantity -= book.book_quantity;
                }
            }
            List<string[]> data = new List<string[]>();
            foreach (var book in booksinstore)
            {
                data.Add(new string[] { book.book_ID, book.book_name, book.book_author, book.book_genre, book.book_quantity.ToString(), book.book_price.ToString() });
            }
            DataProvider.Instance.Write_CSV(filePath, data);
        }
        #endregion

    }
}
