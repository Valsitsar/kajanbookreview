using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Interfaces;

namespace BusinessLogicLayer.Entities
{
    public class Review : IUserPost
    {
        public int ID { get; set; }
        public User Poster { get; set; }
        public string? Title { get; set; } // TODO: Add this to DB
        public string? Body { get; set; }
        public int UpvoteCount { get; set; } = 0; // Make 0 if Body is null
        public int DownvoteCount { get; set; } = 0; //Make 0 if Body is null
        public DateTime PostDate { get; set; }
        public int BookRating { get; set; }
        public Book SourceBook { get; set; }
        public List<Comment>? Comments { get; set; } = new List<Comment>();
    }
}
