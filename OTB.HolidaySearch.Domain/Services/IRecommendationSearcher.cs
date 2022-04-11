using OTB.HolidaySearch.Domain.Contracts;

namespace OTB.HolidaySearch.Domain.Services
{
    public interface IRecommendationSearcher
    {
        IEnumerable<Recommendation> SearchHolidayRecommendations(SearchRequest request);
    }

}