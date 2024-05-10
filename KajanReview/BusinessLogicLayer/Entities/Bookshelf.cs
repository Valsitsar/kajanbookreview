using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Entities
{
    public class Bookshelf
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public User Owner { get; set; }
        public List<Book> Books { get; set; }
    }
}
