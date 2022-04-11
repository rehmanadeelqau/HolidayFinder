using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace OTB.HolidaySearch.FunctionalTests.Setup
{
    public class TestServerSetup
    {
        public static async Task<TestServer> CreateTestServer()
        {
            var path = Assembly.GetAssembly(typeof(TestServerSetup))?.Location;

            var hostBuilder = Host.CreateDefaultBuilder()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .UseContentRoot(Path.GetDirectoryName(path))
                .ConfigureWebHost(
                    webBuilder =>
                    {
                        webBuilder.UseTestServer();
                        webBuilder.UseStartup<HolidaySearchTestStartup>();
                    });
            var host = await hostBuilder.StartAsync();
            return host.GetTestServer();
        }
    }
}
