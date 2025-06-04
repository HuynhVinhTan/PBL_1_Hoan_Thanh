namespace PBL2_BookStoreManagement.DTO
{
    class BookBase //Hiện tại chưa cần quan tâm
    {
        public string book_ID { get; set; }
        public string book_name { get; set; }
        public int book_quantity { get; set; }

        public BookBase(string id, string name, int quantity)
        {
            book_ID = id;
            book_name = name;
            book_quantity = quantity;
        }
    }
}
