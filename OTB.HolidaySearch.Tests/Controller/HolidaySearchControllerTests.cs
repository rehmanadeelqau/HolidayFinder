using Autofac.Extras.Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using OTB.HolidaySearch.Api.Controllers;
using OTB.HolidaySearch.Domain.Contracts;
using OTB.HolidaySearch.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTB.HolidaySearch.Tests.Controller
{
    public class HolidaySearchControllerTests
    {
        private HolidaySearchController controller = default!;
        private Mock<IHolidaySearchService> mockSearchService = default!;
        private Mock<ILogger<HolidaySearchController>> logger = default!;

        [SetUp]
        public void Setup()
        {
            using var mock = AutoMock.GetLoose();

            mock.Mock<IHolidaySearchService>();
            this.mockSearchService = new Mock<IHolidaySearchService>();
            this.logger = new Mock<ILogger<HolidaySearchController>>();

            this.controller = new HolidaySearchController(this.logger.Object, this.mockSearchService.Object);
        }

        [Test]
        public void PostsAsync()
        {
            var response = CreateSearchResponse();
            var request = CreateSearchRequest();

            this.mockSearchService.Setup(x =>
                x.Search(It.IsAny<SearchRequest>())).Returns(response);

            var result = this.controller.PostAsync(request);

            Assert.That(result, Is.TypeOf<Task<ActionResult>>());
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

        private static SearchResponse CreateSearchResponse()
        {
            return new SearchResponse()
            {
                Recommendations = new List<Recommendation>()
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
            }
            };
        }
    }
}
