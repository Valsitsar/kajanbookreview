namespace BusinessLogicLayer.Entities
{
    public class Bookshelf
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public User Owner { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
