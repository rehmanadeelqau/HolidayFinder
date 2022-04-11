using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OTB.HolidaySearch.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTB.HolidaySearch.FunctionalTests.Setup
{
    public class HolidaySearchTestStartup: Startup
    {
        public HolidaySearchTestStartup(IConfiguration configuration)
            : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
        }
    }
}
