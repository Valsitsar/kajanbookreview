using NUnit.Framework;
using BusinessLogicLayer.RecommendationAlgorithm;
using BusinessLogicLayer.Entities; // Ensure this namespace is correctly referenced
using System.Collections.Generic;
using NUnit.Framework.Legacy;

namespace BusinessLogicLayer.Tests
{
    [TestFixture]
    public class SimilarityCalculatorTests
    {
        private SimilarityCalculator _similarityCalculator;
        private int _maxPageCount;

        [SetUp]
        public void Setup()
        {
            _similarityCalculator = new SimilarityCalculator();
            _maxPageCount = 500; // Set a value for maxPageCount
        }

        private FeatureVector CreateFeatureVector(Dictionary<string, double> features)
        {
            var book = new Book
            {
                Authors = new List<User> { new User { FirstName = "Test", MiddleNames = "", LastName = "Author" } },
                PageCount = (int)(features.ContainsKey("PageCount") ? features["PageCount"] * _maxPageCount : 100),
                Genres = new List<Genre> { new Genre { Name = "TestGenre" } }
            };

            var featureVector = new FeatureVector(book, _maxPageCount);

            foreach (var feature in features)
            {
                if (feature.Key != "PageCount")
                {
                    featureVector.Features[feature.Key] = feature.Value;
                }
            }

            return featureVector;
        }

        [Test]
        public void CalculateCosineSimilarity_SameVectors_ReturnsOne()
        {
            var vector1 = CreateFeatureVector(new Dictionary<string, double> { { "A", 1 }, { "B", 2 }, { "PageCount", 0.6 } });
            var vector2 = CreateFeatureVector(new Dictionary<string, double> { { "A", 1 }, { "B", 2 }, { "PageCount", 0.6 } });

            var result = _similarityCalculator.CalculateCosineSimilarity(vector1, vector2);

            ClassicAssert.AreEqual(1.0, result, 0.0001);
        }

        [Test]
        public void CalculateCosineSimilarity_DifferentVectors_ReturnsCorrectValue()
        {
            var vector1 = CreateFeatureVector(new Dictionary<string, double> { { "A", 1 }, { "B", 2 }, { "PageCount", 0.6 } });
            var vector2 = CreateFeatureVector(new Dictionary<string, double> { { "A", 2 }, { "B", 3 }, { "PageCount", 0.8 } });

            var result = _similarityCalculator.CalculateCosineSimilarity(vector1, vector2);

            ClassicAssert.AreEqual(0.9926, result, 0.0001);
        }

           
        [Test]
        public void CalculateCosineSimilarity_NoCommonFeatures_ReturnsZero()
        {
            var vector1 = CreateFeatureVector(new Dictionary<string, double> { { "A", 1 }, { "B", 2 }, { "PageCount", 0.6 } });
            var vector2 = CreateFeatureVector(new Dictionary<string, double> { { "C", 2 }, { "D", 3 }, { "PageCount", 0.8 } });

            var result = _similarityCalculator.CalculateCosineSimilarity(vector1, vector2);

            ClassicAssert.AreEqual(0.0, result);
        }

        [Test]
        public void CalculateCosineSimilarity_ZeroMagnitude_ReturnsZero()
        {
            var vector1 = CreateFeatureVector(new Dictionary<string, double> { { "A", 0 }, { "B", 0 }, { "PageCount", 0.0 } });
            var vector2 = CreateFeatureVector(new Dictionary<string, double> { { "A", 1 }, { "B", 2 }, { "PageCount", 0.6 } });

            var result = _similarityCalculator.CalculateCosineSimilarity(vector1, vector2);

            ClassicAssert.AreEqual(0.0, result, 0.0001);
        }

        // Additional test cases can be added as needed
    }
}
