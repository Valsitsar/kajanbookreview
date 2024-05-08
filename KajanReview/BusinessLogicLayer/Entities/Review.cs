using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Entities
{
    public class Review
    {
        public int BookRating { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
