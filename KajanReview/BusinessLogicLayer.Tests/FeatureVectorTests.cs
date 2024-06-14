using NUnit.Framework;
using BusinessLogicLayer.RecommendationAlgorithm;
using BusinessLogicLayer.Entities; // Make sure this namespace is correctly referenced
using System.Collections.Generic;
using NUnit.Framework.Legacy;

namespace BusinessLogicLayer.Tests
{
    [TestFixture]
    public class FeatureVectorTests
    {
        private Book _book1;
        private Book _book2;
        private Book _book3;
        private int _maxPageCount;

        [SetUp]
        public void Setup()
        {
            var authors1 = new List<User>
            {
                new User { FirstName = "John", MiddleNames = "A", LastName = "Doe" },
                new User { FirstName = "Jane", MiddleNames = "B", LastName = "Smith" }
            };

            var genres1 = new List<Genre>
            {
                new Genre { Name = "Fiction" },
                new Genre { Name = "Mystery" }
            };

            _book1 = new Book
            {
                Authors = authors1,
                PageCount = 300,
                Genres = genres1
            };

            var authors2 = new List<User>
            {
                new User { FirstName = "Alice", MiddleNames = "C", LastName = "Johnson" }
            };

            var genres2 = new List<Genre>
            {
                new Genre { Name = "Science Fiction" },
                new Genre { Name = "Adventure" }
            };

            _book2 = new Book
            {
                Authors = authors2,
                PageCount = 150,
                Genres = genres2
            };

            var authors3 = new List<User>
            {
                new User { FirstName = "Bob", MiddleNames = "D", LastName = "Brown" }
            };

            var genres3 = new List<Genre>
            {
                new Genre { Name = "Romance" }
            };

            _book3 = new Book
            {
                Authors = authors3,
                PageCount = 500,
                Genres = genres3
            };

            _maxPageCount = 500;
        }

        [Test]
        public void FeatureVector_AuthorsAddedCorrectly()
        {
            var featureVector = new BusinessLogicLayer.RecommendationAlgorithm.FeatureVector(_book1, _maxPageCount);

            ClassicAssert.AreEqual(1.0, featureVector.Features["Author:John A Doe"]);
            ClassicAssert.AreEqual(1.0, featureVector.Features["Author:Jane B Smith"]);
        }

        [Test]
        public void FeatureVector_SingleAuthorAddedCorrectly()
        {
            var featureVector = new FeatureVector(_book2, _maxPageCount);

            ClassicAssert.AreEqual(1.0, featureVector.Features["Author:Alice C Johnson"]);
        }

        [Test]
        public void FeatureVector_AnotherSingleAuthorAddedCorrectly()
        {
            var featureVector = new FeatureVector(_book3, _maxPageCount);

            ClassicAssert.AreEqual(1.0, featureVector.Features["Author:Bob D Brown"]);
        }

        [Test]
        public void FeatureVector_PageCountNormalizedCorrectly()
        {
            var featureVector = new FeatureVector(_book1, _maxPageCount);
            var expectedNormalizedPageCount = (double)_book1.PageCount / _maxPageCount;

            ClassicAssert.AreEqual(expectedNormalizedPageCount, featureVector.Features["PageCount"], 0.0001);
        }

        [Test]
        public void FeatureVector_PageCountHalfNormalizedCorrectly()
        {
            var featureVector = new FeatureVector(_book2, _maxPageCount);
            var expectedNormalizedPageCount = (double)_book2.PageCount / _maxPageCount;

            ClassicAssert.AreEqual(expectedNormalizedPageCount, featureVector.Features["PageCount"], 0.0001);
        }

        [Test]
        public void FeatureVector_MaxPageCountNormalizedCorrectly()
        {
            var featureVector = new FeatureVector(_book3, _maxPageCount);
            var expectedNormalizedPageCount = (double)_book3.PageCount / _maxPageCount;

            ClassicAssert.AreEqual(expectedNormalizedPageCount, featureVector.Features["PageCount"], 0.0001);
        }

        [Test]
        public void FeatureVector_GenresAddedCorrectly()
        {
            var featureVector = new FeatureVector(_book1, _maxPageCount);

            ClassicAssert.AreEqual(1.0, featureVector.Features["Genre:Fiction"]);
            ClassicAssert.AreEqual(1.0, featureVector.Features["Genre:Mystery"]);
        }

        [Test]
        public void FeatureVector_SingleGenreAddedCorrectly()
        {
            var featureVector = new FeatureVector(_book2, _maxPageCount);

            ClassicAssert.AreEqual(1.0, featureVector.Features["Genre:Science Fiction"]);
            ClassicAssert.AreEqual(1.0, featureVector.Features["Genre:Adventure"]);
        }

        [Test]
        public void FeatureVector_AnotherSingleGenreAddedCorrectly()
        {
            var featureVector = new FeatureVector(_book3, _maxPageCount);

            ClassicAssert.AreEqual(1.0, featureVector.Features["Genre:Romance"]);
        }
    }
}
