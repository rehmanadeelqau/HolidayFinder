using System.Text.Json.Serialization;

namespace OTB.HolidaySearch.Domain.Contracts
{
    public class Location
    {
        [JsonPropertyName("city_code")]
        public string CityCode { get; set; }

        [JsonPropertyName("city_name")]
        public string CityName { get; set; }

        [JsonPropertyName("airports")]
        public List<Airport> Airports { get; set; }
    }

    public class Airport
    {
        [JsonPropertyName("airport_code")]
        public string AirportCode { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
