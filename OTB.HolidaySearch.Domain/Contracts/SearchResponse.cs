using System.Text.Json.Serialization;

namespace OTB.HolidaySearch.Domain.Contracts
{
    public class SearchResponse
    {
        public List<Recommendation> Recommendations { get; set; }
    }
}
