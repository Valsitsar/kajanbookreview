using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net;

namespace BusinessLogicLayer
{
    public class Book
    {
        public string Id { get; set; }
        //public Image Cover { get; set; }
        public string Title { get; set; }
        //public List<Author> Authors { get; set; }
        public string Description { get; set; }
        public int NoOfPages { get; set; }
        //public List<Genre> Genres { get; set; }
        public string Isbn { get; set; }
        //public BookFormat Format { get; set; }
        public string Publisher { get; set; }
        public DateTime PubDate { get; set; }
        public string Language { get; set; }
        //public List<int> Ratings { get; set; }
        //public List<Review> Reviews { get; set; }

        public Book(
            string id, string title, string description, int noOfPages, string isbn, 
            string publisher, DateTime pubDate, string language/*, List<int> ratings*/)
        {
            Id = id;
            Title = title;
            Description = description;
            NoOfPages = noOfPages;
            Isbn = isbn;
            Publisher = publisher;
            PubDate = pubDate;
            Language = language;
            //Ratings = ratings;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Title: {Title}, Description: {Description}, # of pages: {NoOfPages}";
        }
    }
}
