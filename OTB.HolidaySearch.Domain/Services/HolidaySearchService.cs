using OTB.HolidaySearch.Domain.Contracts;

namespace OTB.HolidaySearch.Domain.Services
{
    public class HolidaySearchService : IHolidaySearchService
    {
        private readonly IRecommendationSearcher searchRecommendations;
        public HolidaySearchService(IRecommendationSearcher searchRecommendations)
        {
            this.searchRecommendations = searchRecommendations;
        }
        public SearchResponse Search(SearchRequest request)
        {
            var recommendations = this.searchRecommendations.SearchHolidayRecommendations(request);
            return new SearchResponse() { Recommendations = recommendations.ToList() };
        }
    }
}
