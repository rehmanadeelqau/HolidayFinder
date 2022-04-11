using Autofac.Extras.Moq;
using Moq;
using NUnit.Framework;
using OTB.HolidaySearch.Domain.Contracts;
using OTB.HolidaySearch.Domain.Services;
using System;
using System.Collections.Generic;

namespace OTB.HolidaySearch.Tests.Domain
{
    [TestFixture]
    public class SearchServiceTests
    {
        private AutoMock mock = default!;
        private Mock<IRecommendationSearcher> recommendationSearcher = default!;
        private IHolidaySearchService holidaySearchService = default!;

        [SetUp]
        public void Setup()
        {
            this.mock = AutoMock.GetLoose();
            this.recommendationSearcher = this.mock.Mock<IRecommendationSearcher>();

            this.recommendationSearcher.Setup(x =>
               x.SearchHolidayRecommendations(It.IsAny<SearchRequest>())).Returns(CreateHolidayRecommendations());

            this.holidaySearchService = new HolidaySearchService(this.recommendationSearcher.Object);
        }

        [Test]
        public void CallsSearch()
        {
            var response = this.holidaySearchService.Search(CreateSearchRequest());
            Assert.That(response.Recommendations, Has.Count.EqualTo(1));
        }

        private static SearchRequest CreateSearchRequest()
        {
            return new SearchRequest()
            {
                DepartureAirport = "MAN",
                ArrivalAirport = "AGP",
                DepartureDate = new DateOnly(2023, 07, 01),
                Duration = 7
            };
        }

        private static List<Recommendation> CreateHolidayRecommendations()
        {
            return new List<Recommendation>()
            {
                new Recommendation()
                {
                    Flight = new Flight()
                    {
                        DepartureDate = new DateOnly(2023, 07, 01),
                        From = "MAN",
                        To = "AGP",
                        Airline = "Oceanic Airlines",
                        Id = 2,
                        Price = 245
                    },
                    Hotel = new Hotel()
                    {
                        Id = 9,
                        HotelName = "Nh Malaga",
                        ArrivalDate = new DateOnly(2023, 07, 01),
                        PricePerNight = 83,
                        AvailableNights = 7,
                        LocalAirports = new List<string>(){ "AGP" }
                    }
                }
            };
        }
    }
}
