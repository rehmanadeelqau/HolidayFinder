using System.Text.Json.Serialization;

namespace OTB.HolidaySearch.Domain.Contracts
{
    public class Hotel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string HotelName { get; set; }

        [JsonPropertyName("arrival_date")]
        [JsonConverter(typeof(DateOnlyConverter))]
        public DateOnly ArrivalDate { get; set; }

        [JsonPropertyName("price_per_night")]
        public decimal PricePerNight { get; set; }

        [JsonPropertyName("local_airports")]
        public List<string> LocalAirports { get; set; }

        [JsonPropertyName("nights")]
        public int AvailableNights { get; set; }
    }
}
