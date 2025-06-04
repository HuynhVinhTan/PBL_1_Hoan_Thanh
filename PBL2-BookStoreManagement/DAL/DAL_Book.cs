using PBL2_BookStoreManagement.DTO;
using System.Collections.Generic;
using System.IO;
using BookStoreApp.DAL;
using System.Linq;
using System.Collections;
using System.Net;
using System.Diagnostics;
using System.Xml.Linq;

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
            List<string> header = new List<string> { "book_ID", "book_name", "book_author", "book_genre", "book_quantity", "book_price" };
            DataProvider.Instance.Write_CSV(filePath, data, header);
        }

        public void AddBook(string bookId, string name, string author, string category, int stock, double price)
        {
            List<Book> books = GetBooks();
            Book book = new Book(bookId, name, author, category, stock, price);
            books.Add(book);

            // Convert the list of books to a list of string arrays for writing to CSV  
            List<string[]> bookData = books.Select(b => new string[]
            {
                   b.book_ID,
                   b.book_name,
                   b.book_author,
                   b.book_genre,
                   b.book_quantity.ToString(),
                   b.book_price.ToString()
            }).ToList();

            DataProvider.Instance.Write_CSV(filePath, bookData);
        }

        public void UpdateBook(Book book, int index)
        {
            // Cập nhật thông tin sách  
            List<Book> books = GetBooks();
            books[index] = book;

            // Ghi lại toàn bộ danh sách vào file CSV  
            List<string[]> allData = new List<string[]>();
            allData.AddRange(books.Select(c => new string[] { c.book_ID, c.book_name, c.book_author, c.book_genre, c.book_quantity.ToString(), c.book_price.ToString()}).ToList());
            DataProvider.Instance.Write_CSV(filePath, allData);
        }
        public void Update_Book_ID(List<Book> books, int index)
        {
            for (int i = index; i < books.Count; i++)
            {
                books[i].book_ID = "B" + (i + 1).ToString("D3");
            }
        }
        public void DeleteBook(int index)
        {
            List<Book> books = GetBooks();
            books.RemoveAt(index);
            Update_Book_ID(books, index);
            List<string[]> allData = new List<string[]>();
            allData.AddRange(books.Select(c => new string[] { c.book_ID, c.book_name, c.book_author, c.book_genre, c.book_quantity.ToString(), c.book_price.ToString() }).ToList());
            DataProvider.Instance.Write_CSV(filePath, allData);
        }
        #endregion

    }
}
