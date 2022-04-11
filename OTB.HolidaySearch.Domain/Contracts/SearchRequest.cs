using System.Text.Json.Serialization;

namespace OTB.HolidaySearch.Domain.Contracts
{
    public class SearchRequest
    {
        public string DepartureAirport { get; set; }

        public string ArrivalAirport { get; set; }

        [JsonConverter(typeof(DateOnlyConverter))]
        public DateOnly DepartureDate { get; set; }

        public int Duration { get; set; }
    }
}
