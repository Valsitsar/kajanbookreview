using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Entities
{
    public abstract class UserPost
    {
        public int ID { get; set; }
        public User Poster { get; set; }
        public string Body { get; set; }
        public int UpvoteCount { get; set; }
        public int DownvoteCount { get; set; }
        public DateTime PostDate { get; set; }
    }
}
