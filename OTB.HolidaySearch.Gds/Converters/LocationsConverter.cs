using OTB.HolidaySearch.Domain;
using OTB.HolidaySearch.Domain.Contracts;
using OTB.HolidaySearch.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OTB.HolidaySearch.Gds.Converters
{
    public class LocationsConverter : ILocationsConverter<LocationsResponse>
    {
        public LocationsResponse ConvertResponse()
        {
            string jsonString = FileUtils.ReadJsonFileAsString(JsonFilePaths.LocationsFilePath);
            List<Location> locations = JsonSerializer.Deserialize<List<Location>>(jsonString)!;
            return new LocationsResponse() { Locations = locations };
        }
    }
}
