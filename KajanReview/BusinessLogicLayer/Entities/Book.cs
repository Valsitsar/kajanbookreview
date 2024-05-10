using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net;
using System.Security.Principal;

namespace BusinessLogicLayer.Entities
{
    public class Book
    {
        public int ID { get; set; }
        public string? CoverFilePath { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public string ISBN { get; set; }
        public BookFormat Format { get; set; }
        public string Publisher { get; set; }
        public DateTime PubDate { get; set; }
        public string Language { get; set; }
        public List<Genre> Genres { get; set; }
        public List<User> Authors { get; set; }
        public List<Review>? Reviews { get; set; }


        public override string ToString()
        {
            // No use for now
            return $"ID: {ID}, Title: {Title}, Description: {Description}, # of pages: {PageCount}";
        }
    }
}
