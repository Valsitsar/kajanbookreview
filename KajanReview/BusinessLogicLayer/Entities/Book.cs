namespace BusinessLogicLayer.Entities
{
    public class Book
    {
        public int ID { get; set; }
        public string? CoverFilePath { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int PageCount { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public BookFormat Format { get; set; }
        public string Publisher { get; set; } = string.Empty;
        public DateTime PubDate { get; set; }
        public string Language { get; set; } = string.Empty;
        public List<Genre> Genres { get; set; } = new List<Genre>();
        public List<User> Authors { get; set; } = new List<User>();
        public List<Review>? Reviews { get; set; } = new List<Review>();
    }
}
