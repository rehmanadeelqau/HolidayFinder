using NUnit.Framework;
using OTB.HolidaySearch.Domain.Contracts;
using OTB.HolidaySearch.FunctionalTests.Setup;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace OTB.HolidaySearch.FunctionalTests.Scenarios
{
    public class HolidaySearchScenarios : HolidaySearchScenarioBase
    {
        private JsonSerializerOptions jsonOptions;

        public HolidaySearchScenarios()
        {
            this.jsonOptions = new JsonSerializerOptions();
            this.jsonOptions.PropertyNameCaseInsensitive = true;
        }

        [Test]
        public async Task ShouldPerformBestHolidaySearchFor_MAN_TO_AGP_Itinerary()
        {
            var request = CreateSearchRequest("MAN", "AGP", new DateOnly(2023, 07, 01), 7);
            var response = await this.PostAsync(Post.Search, request);
            var searchResponse = await GetRootElementAsync(response);
            var recommendations = searchResponse.GetProperty("recommendations");
            var holidayRecommendations = JsonSerializer.Deserialize<List<Recommendation>>(recommendations.GetRawText(), this.jsonOptions);
            Assert.That(holidayRecommendations?.Count, Is.GreaterThan(0));
            Assert.That(holidayRecommendations?[0].TotalPrice, Is.EqualTo(826));
            Assert.That(holidayRecommendations?[0].Flight.Id, Is.EqualTo(2));
            Assert.That(holidayRecommendations?[0].Flight.From, Is.EqualTo("MAN"));
            Assert.That(holidayRecommendations?[0].Flight.To, Is.EqualTo("AGP"));
            Assert.That(holidayRecommendations?[0].Flight.Price, Is.EqualTo(245));
            Assert.That(holidayRecommendations?[0].Hotel.Id, Is.EqualTo(9));
            Assert.That(holidayRecommendations?[0].Hotel.HotelName, Is.EqualTo("Nh Malaga"));
            Assert.That(holidayRecommendations?[0].Hotel.PricePerNight, Is.EqualTo(83));
        }

        [Test]
        public async Task ShouldPerformBestHolidaySearchFor_ANY_LON_TO_PMI_Itinerary()
        {
            var request = CreateSearchRequest("LON", "PMI", new DateOnly(2023, 06, 15), 10);
            var response = await this.PostAsync(Post.Search, request);
            var searchResponse = await GetRootElementAsync(response);
            var recommendations = searchResponse.GetProperty("recommendations");
            var holidayRecommendations = JsonSerializer.Deserialize<List<Recommendation>>(recommendations.GetRawText(), this.jsonOptions);
            Assert.That(holidayRecommendations?.Count, Is.GreaterThan(0));
            Assert.That(holidayRecommendations?[0].TotalPrice, Is.EqualTo(675));
            Assert.That(holidayRecommendations?[0].Flight.Id, Is.EqualTo(6));
            Assert.That(holidayRecommendations?[0].Flight.From, Is.EqualTo("LGW"));
            Assert.That(holidayRecommendations?[0].Flight.To, Is.EqualTo("PMI"));
            Assert.That(holidayRecommendations?[0].Flight.Price, Is.EqualTo(75));
            Assert.That(holidayRecommendations?[0].Hotel.Id, Is.EqualTo(5));
            Assert.That(holidayRecommendations?[0].Hotel.HotelName, Is.EqualTo("Sol Katmandu Park & Resort"));
            Assert.That(holidayRecommendations?[0].Hotel.PricePerNight, Is.EqualTo(60));
        }

        [Test]
        public async Task ShouldPerformBestHolidaySearchFor_ANYWHERE_TO_LPA_Itinerary()
        {
            var request = CreateSearchRequest("ANY", "LPA", new DateOnly(2022, 11, 10), 14);
            var response = await this.PostAsync(Post.Search, request);
            var searchResponse = await GetRootElementAsync(response);
            var recommendations = searchResponse.GetProperty("recommendations");
            var holidayRecommendations = JsonSerializer.Deserialize<List<Recommendation>>(recommendations.GetRawText(), this.jsonOptions);
            Assert.That(holidayRecommendations?.Count, Is.GreaterThan(0));
            Assert.That(holidayRecommendations?[0].TotalPrice, Is.EqualTo(1175));
            Assert.That(holidayRecommendations?[0].Flight.Id, Is.EqualTo(7));
            Assert.That(holidayRecommendations?[0].Flight.From, Is.EqualTo("MAN"));
            Assert.That(holidayRecommendations?[0].Flight.To, Is.EqualTo("LPA"));
            Assert.That(holidayRecommendations?[0].Flight.Price, Is.EqualTo(125));
            Assert.That(holidayRecommendations?[0].Hotel.Id, Is.EqualTo(6));
            Assert.That(holidayRecommendations?[0].Hotel.HotelName, Is.EqualTo("Club Maspalomas Suites and Spa"));
            Assert.That(holidayRecommendations?[0].Hotel.PricePerNight, Is.EqualTo(75));
        }

        [Test]
        public async Task ShouldBringMultipleHolidaySearchResultsFor_ANYWHERE_TO_PMI_Itinerary()
        {
            var request = CreateSearchRequest("ANY", "PMI", new DateOnly(2023, 06, 15), 14);
            var response = await this.PostAsync(Post.Search, request);
            var searchResponse = await GetRootElementAsync(response);
            var recommendations = searchResponse.GetProperty("recommendations");
            var holidayRecommendations = JsonSerializer.Deserialize<List<Recommendation>>(recommendations.GetRawText(), this.jsonOptions);
            Assert.That(holidayRecommendations?.Count, Is.EqualTo(4));
            Assert.That(holidayRecommendations?[0].TotalPrice, Is.EqualTo(901));
            Assert.That(holidayRecommendations?[0].Flight.Id, Is.EqualTo(6));
            Assert.That(holidayRecommendations?[0].Flight.From, Is.EqualTo("LGW"));
            Assert.That(holidayRecommendations?[0].Flight.To, Is.EqualTo("PMI"));
            Assert.That(holidayRecommendations?[0].Flight.Price, Is.EqualTo(75));
            Assert.That(holidayRecommendations?[0].Hotel.Id, Is.EqualTo(3));
            Assert.That(holidayRecommendations?[0].Hotel.HotelName, Is.EqualTo("Sol Katmandu Park & Resort"));
            Assert.That(holidayRecommendations?[0].Hotel.PricePerNight, Is.EqualTo(59));

            Assert.That(holidayRecommendations?[1].TotalPrice, Is.EqualTo(956));
            Assert.That(holidayRecommendations?[1].Flight.Id, Is.EqualTo(5));
            Assert.That(holidayRecommendations?[1].Flight.From, Is.EqualTo("MAN"));
            Assert.That(holidayRecommendations?[1].Flight.To, Is.EqualTo("PMI"));
            Assert.That(holidayRecommendations?[1].Flight.Price, Is.EqualTo(130));
            Assert.That(holidayRecommendations?[1].Hotel.Id, Is.EqualTo(3));
            Assert.That(holidayRecommendations?[1].Hotel.HotelName, Is.EqualTo("Sol Katmandu Park & Resort"));
            Assert.That(holidayRecommendations?[1].Hotel.PricePerNight, Is.EqualTo(59));

            Assert.That(holidayRecommendations?[2].TotalPrice, Is.EqualTo(979));
            Assert.That(holidayRecommendations?[2].Flight.Id, Is.EqualTo(4));
            Assert.That(holidayRecommendations?[2].Flight.From, Is.EqualTo("LTN"));
            Assert.That(holidayRecommendations?[2].Flight.To, Is.EqualTo("PMI"));
            Assert.That(holidayRecommendations?[2].Flight.Price, Is.EqualTo(153));
            Assert.That(holidayRecommendations?[2].Hotel.Id, Is.EqualTo(3));
            Assert.That(holidayRecommendations?[2].Hotel.HotelName, Is.EqualTo("Sol Katmandu Park & Resort"));
            Assert.That(holidayRecommendations?[2].Hotel.PricePerNight, Is.EqualTo(59));

            Assert.That(holidayRecommendations?[3].TotalPrice, Is.EqualTo(996));
            Assert.That(holidayRecommendations?[3].Flight.Id, Is.EqualTo(3));
            Assert.That(holidayRecommendations?[3].Flight.From, Is.EqualTo("MAN"));
            Assert.That(holidayRecommendations?[3].Flight.To, Is.EqualTo("PMI"));
            Assert.That(holidayRecommendations?[3].Flight.Price, Is.EqualTo(170));
            Assert.That(holidayRecommendations?[3].Hotel.Id, Is.EqualTo(3));
            Assert.That(holidayRecommendations?[3].Hotel.HotelName, Is.EqualTo("Sol Katmandu Park & Resort"));
            Assert.That(holidayRecommendations?[3].Hotel.PricePerNight, Is.EqualTo(59));
        }

        [Test]
        public async Task ShouldNotPerformSearchFor_InvalidAirportOrCityCode()
        {
            var request = CreateSearchRequest("ABC", "LPA", new DateOnly(2022, 11, 10), 14);
            var response = await this.PostAsync(Post.Search, request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task ShouldNotPerformSearchFor_PastDepartureDate()
        {
            var request = CreateSearchRequest("MAN", "AGP", new DateOnly(2021, 11, 10), 7);
            var response = await this.PostAsync(Post.Search, request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
        }

        private static SearchRequest CreateSearchRequest(string from, string to, DateOnly departureDate, int duration)
        {
            return new SearchRequest()
            {
                DepartureAirport = from,
                ArrivalAirport = to,
                DepartureDate = departureDate,
                Duration = duration
            };
        }
    }
}
