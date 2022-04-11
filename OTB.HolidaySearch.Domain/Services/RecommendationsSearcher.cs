using OTB.HolidaySearch.Domain.Contracts;

namespace OTB.HolidaySearch.Domain.Services
{    
    public class RecommendationsSearcher : IRecommendationSearcher
    {
        private readonly IFlightsConverter<FlightsResponse> flightsConverter;
        private readonly IHotelsConverter<HotelsResponse> hotelsConverter;
        private readonly ILocationsConverter<LocationsResponse> locationsConverter;

        public RecommendationsSearcher(
            IFlightsConverter<FlightsResponse> flightsConverter,
            IHotelsConverter<HotelsResponse> hotelsConverter,
            ILocationsConverter<LocationsResponse> locationsConverter)
        {
            this.flightsConverter = flightsConverter;
            this.hotelsConverter = hotelsConverter;
            this.locationsConverter = locationsConverter;
        }

        public IEnumerable<Recommendation> SearchHolidayRecommendations(SearchRequest request)
        {
            LocationsResponse locationsResponse = this.locationsConverter.ConvertResponse();
            List<string> anywhereLocations = new List<string>() { "Any", "Anywhere" };

            bool shouldMatchDepartureAirports = anywhereLocations.Any(a => a.Equals(request.DepartureAirport, StringComparison.OrdinalIgnoreCase));
            bool shouldMatchArrivalAirports = anywhereLocations.Any(a => a.Equals(request.ArrivalAirport, StringComparison.OrdinalIgnoreCase));

            var departureAirportsToCompare = locationsResponse
                                             .Locations
                                             .FirstOrDefault(a => a.CityCode.Equals(request.DepartureAirport, StringComparison.OrdinalIgnoreCase)
                                             || a.CityName.Equals(request.DepartureAirport, StringComparison.OrdinalIgnoreCase))?
                                             .Airports
                                             .Select(a => a.AirportCode);

            var arrivalAirportsToCompare = locationsResponse
                                             .Locations
                                             .FirstOrDefault(a => a.CityCode.Equals(request.ArrivalAirport, StringComparison.OrdinalIgnoreCase)
                                             || a.CityName.Equals(request.ArrivalAirport, StringComparison.OrdinalIgnoreCase))?
                                             .Airports
                                             .Select(a => a.AirportCode);

            var flightsResponse = this.flightsConverter.ConvertResponse();
            var hotelsResponse = this.hotelsConverter.ConvertResponse();

            var availableFlights = flightsResponse.Flights
                                   .Where(a => (departureAirportsToCompare != null && departureAirportsToCompare.Contains(a.From) || shouldMatchDepartureAirports)
                                   && (arrivalAirportsToCompare != null && arrivalAirportsToCompare.Contains(a.To) || shouldMatchArrivalAirports)
                                   && a.DepartureDate == request.DepartureDate)
                                   .OrderBy(a => a.Price);

            foreach (var flight in availableFlights)
            {
                var hotel = hotelsResponse.Hotels.Where(a => a.LocalAirports.Contains(flight.To)
                        && a.ArrivalDate == flight.DepartureDate
                        && a.AvailableNights == request.Duration)
                        .OrderBy(a => a.PricePerNight)
                        .FirstOrDefault();
                if (flight != null && hotel != null)
                {
                    yield return new Recommendation() { Flight = flight, Hotel = hotel };
                }
            }
        }
    }
}

