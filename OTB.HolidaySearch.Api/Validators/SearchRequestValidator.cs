using FluentValidation;
using OTB.HolidaySearch.Domain.Contracts;
using OTB.HolidaySearch.Domain.Services;

namespace OTB.HolidaySearch.Api.Validators
{
    public class SearchRequestValidator : AbstractValidator<SearchRequest>
    {
        private readonly ILocationsConverter<LocationsResponse> locationsConverter;

        public SearchRequestValidator(ILocationsConverter<LocationsResponse> locationsConverter)
        {
            this.locationsConverter = locationsConverter;
            this.RuleFor(x => x.DepartureAirport).MinimumLength(3);
            this.RuleFor(x => x.DepartureAirport).Must(BeAValidLocation);
            this.RuleFor(x => x.ArrivalAirport).MinimumLength(3);
            this.RuleFor(x => x.ArrivalAirport).Must(BeAValidLocation);
            this.RuleFor(x => x.DepartureDate).GreaterThan(DateOnly.Parse(DateTime.Now.ToString("yyyy-MM-dd")));
        }

        private bool BeAValidLocation(SearchRequest searchRequest, string searchLocation)
        {
            LocationsResponse locationsResponse = this.locationsConverter.ConvertResponse();
            return locationsResponse.Locations
                    .Exists(a => a.CityCode.Equals(searchLocation, StringComparison.OrdinalIgnoreCase)
                    || a.CityName.Equals(searchLocation, StringComparison.OrdinalIgnoreCase)
                    || a.Airports.Any(b => b.AirportCode.Equals(searchLocation, StringComparison.OrdinalIgnoreCase)
                    || b.Name.Equals(searchLocation, StringComparison.OrdinalIgnoreCase)));
        }
    }
}
