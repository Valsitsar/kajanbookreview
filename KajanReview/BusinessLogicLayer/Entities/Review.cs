using BusinessLogicLayer.Interfaces;

namespace BusinessLogicLayer.Entities
{
    public class Review : IUserPost
    {
        public int ID { get; set; }
        public User Poster { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public int UpvoteCount { get; set; } = 0;
        public int DownvoteCount { get; set; } = 0;
        public DateTime PostDate { get; set; }
        public int BookRating { get; set; }
        public Book SourceBook { get; set; }
        public List<Comment>? Comments { get; set; } = new List<Comment>();
    }
}
