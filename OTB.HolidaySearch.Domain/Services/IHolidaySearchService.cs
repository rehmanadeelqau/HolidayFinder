using OTB.HolidaySearch.Domain.Contracts;

namespace OTB.HolidaySearch.Domain.Services
{
    public interface IHolidaySearchService
    {
        SearchResponse Search(SearchRequest request);
    }
}
