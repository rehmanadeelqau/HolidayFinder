using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTB.HolidaySearch.Domain.Services
{
    public interface ILocationsConverter<out LocationsResponse> : IResponseConverter<LocationsResponse>
    {

    }
}
