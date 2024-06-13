using BusinessLogicLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.RecommendationAlgorithm
{
    public class FeatureVector
    {
        public Dictionary<string, double> Features { get; set; }

        public FeatureVector(Book book, int maxPageCount)
        {
            Features = new Dictionary<string, double>();

            foreach (var author in book.Authors)
            {
                AddFeature($"Author:{author.FirstName} {author.MiddleNames} {author.LastName}", 1.0);
            }

            AddFeature("PageCount", NormalizePageCount(book.PageCount, maxPageCount));

            foreach (var genre in book.Genres)
            {
                AddFeature($"Genre:{genre.Name}", 1.0);
            }
        }

        private double NormalizePageCount(int pageCount, int maxPageCount)
        {
            return (double)pageCount / maxPageCount;
        }

        private void AddFeature(string featureName, double featureValue)
        {
            if (Features.ContainsKey(featureName))
            {
                Features[featureName] += featureValue;
            }
            else
            {
                Features[featureName] = featureValue;
            }
        }
    }
}
