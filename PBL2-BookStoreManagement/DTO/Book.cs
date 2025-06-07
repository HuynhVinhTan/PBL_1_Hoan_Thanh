namespace PBL2_BookStoreManagement.DTO
{
    class Book : BookBase
    {
        public string book_author { get; set; }
        public string book_genre { get; set; }
        public double book_price { get; set; }

        public Book(string id, string name, string author, string genre, int quantity, double price)
            : base(id, name, quantity)
        {
            book_author = author;
            book_genre = genre;
            book_price = price;
        }

        public override string ToString()
        {
            return $"{book_ID}, {book_name}, {book_author}, {book_genre}, {book_quantity}, {book_price}";
        }
    }
}
