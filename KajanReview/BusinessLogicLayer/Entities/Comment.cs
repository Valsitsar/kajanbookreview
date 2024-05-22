using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Interfaces;

namespace BusinessLogicLayer.Entities
{
    public class Comment : IUserPost
    {

        public int ID { get; set; }

        public User Poster { get; set; }

        public string Body { get; set; } = string.Empty;

        public int UpvoteCount { get; set; }

        public int DownvoteCount { get; set; }

        public DateTime PostDate { get; set; }
        public Review SourceReview { get; set; }
    }
}
