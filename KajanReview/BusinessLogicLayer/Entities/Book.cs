using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net;

namespace BusinessLogicLayer.Entities
{
    public class Book
    {
        public int? Id { get; }
        public string CoverFilePath { get; }
        public string Title { get; }
        //public List<Author> Authors { get; set; }
        public string Description { get; }
        public int NoOfPages { get; }
        //public List<Genre> Genres { get; set; }
        public string ISBN { get; }
        //public BookFormat Format { get; set; }
        public string Publisher { get; }
        public DateTime PubDate { get; }
        public string Language { get; }
        //public List<int> Ratings { get; set; }
        //public List<Review> Reviews { get; set; }

        public Book(int? id, string coverFilePath, string title, string description, int noOfPages,
            string isbn, string publisher, DateTime pubDate, string language)
        {
            Id = id;
            CoverFilePath = coverFilePath;
            Title = title;
            Description = description;
            NoOfPages = noOfPages;
            ISBN = isbn;
            Publisher = publisher;
            PubDate = pubDate;
            Language = language;
        }

        public override string ToString()
        {
            // No use for now
            return $"ID: {Id}, Title: {Title}, Description: {Description}, # of pages: {NoOfPages}";
        }
    }
}
