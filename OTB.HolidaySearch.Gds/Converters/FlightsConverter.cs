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
    public class FlightsConverter : IFlightsConverter<FlightsResponse>
    {
        public FlightsResponse ConvertResponse()
        {
            string jsonString = FileUtils.ReadJsonFileAsString(JsonFilePaths.FlightsFilePath);
            List<Flight> flights = JsonSerializer.Deserialize<List<Flight>>(jsonString)!;
            return new FlightsResponse() { Flights = flights };
        }
    }
}
