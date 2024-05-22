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

        public override string ToString()
        {
            return $"ID: {ID}, Title: {Title}, Description: {Description}, # of pages: {PageCount}";
        }
    }
}
