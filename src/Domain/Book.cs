namespace WayOfWork.Domain
{
    // Pretend this is a Entity Framework class with all the mappings in place
    public class Book : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public decimal Price { get; set; }
    }
}
