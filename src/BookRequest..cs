namespace WayOfWork
{
    public class BookRequest
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
    }

    public class BookAddResult
    {
        public int Id { get; set; }
    }
}
