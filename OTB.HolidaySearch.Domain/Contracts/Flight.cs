using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OTB.HolidaySearch.Domain.Contracts
{
    public class Flight
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("airline")]
        public string Airline { get; set; }

        [JsonPropertyName("from")]
        public string From { get; set; }

        [JsonPropertyName("to")]
        public string To { get; set; }

        [JsonPropertyName("price")]
        public int Price { get; set; }

        [JsonPropertyName("departure_date")]
        [JsonConverter(typeof(DateOnlyConverter))]
        public DateOnly DepartureDate { get; set; }
    }
}
