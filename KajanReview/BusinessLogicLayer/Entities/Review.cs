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
        public string? Body { get; set; }
        // Make 0 if Body is null
        public int UpvoteCount { get; set; }
        //Makae 0 if Body is null
        public int DownvoteCount { get; set; }
        public DateTime PostDate { get; set; }
        public int BookRating { get; set; }
        public Book SourceBook { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
