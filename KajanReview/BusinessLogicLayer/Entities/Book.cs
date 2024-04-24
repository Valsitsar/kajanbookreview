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
        public int? Id { get; private set; }
        //public byte[] Cover { get; private set; }
        public string Title { get; private set; }
        //public List<Author> Authors { get; set; }
        public string Description { get; private set; }
        public int NoOfPages { get; private set; }
        //public List<Genre> Genres { get; set; }
        public string ISBN { get; private set; }
        //public BookFormat Format { get; set; }
        public string Publisher { get; private set; }
        public DateTime PubDate { get; private set; }
        public string Language { get; private set; }
        //public List<int> Ratings { get; set; }
        //public List<Review> Reviews { get; set; }

        public Book(int? id/*, byte[] cover*/, string title, string description, int noOfPages,
            string isbn, string publisher, DateTime pubDate, string language)
        {
            Id = id;
            //Cover = cover;
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
