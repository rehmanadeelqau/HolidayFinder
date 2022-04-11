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
    public class HotelsConverter : IHotelsConverter<HotelsResponse>
    {
        public HotelsResponse ConvertResponse()
        {
            string jsonString = FileUtils.ReadJsonFileAsString(JsonFilePaths.HotelsFilePath);
            List<Hotel> hotels = JsonSerializer.Deserialize<List<Hotel>>(jsonString)!;
            return new HotelsResponse() { Hotels = hotels };
        }
    }
}
