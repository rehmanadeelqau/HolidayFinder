
namespace OTB.HolidaySearch.Domain.Services
{
    public interface IResponseConverter<out TResponse>
    {
        TResponse ConvertResponse();
    }
}
