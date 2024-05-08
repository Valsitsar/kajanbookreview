using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Entities
{
    public class Author : User
    {
        public List<Book> BooksWritten { get; set; }
    }
}
