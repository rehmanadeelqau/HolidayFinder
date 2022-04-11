using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OTB.HolidaySearch.FunctionalTests.Setup
{
    public abstract class HolidaySearchScenarioBase
    {
        protected HolidaySearchScenarioBase()
        {
        }

        protected TestServer TestServer { get; private set; } = default!;

        [OneTimeSetUp]
        public virtual async Task OneTimeSetup()
        {
            this.TestServer = await TestServerSetup.CreateTestServer();
        }

        [OneTimeTearDown]
        public virtual void OneTimeTeardown()
        {
            this.TestServer.Dispose();
        }

        [SetUp]
        public virtual void Setup()
        {
        }

        protected static async Task<JsonElement> GetRootElementAsync(HttpResponseMessage response)
        {
            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonDocument.Parse(responseJson).RootElement;
        }

        protected async Task<HttpResponseMessage> PostAsync<T>(string requestUri, T request, bool ensureSuccess = false)
        {
            using var client = this.TestServer.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(requestUri, content);
            TestContext.WriteLine(response);
            if (ensureSuccess)
            {
                response.EnsureSuccessStatusCode();
            }

            return response;
        }

        protected static class Post
        {
            public static string Search => "api/HolidaySearch";
        }
    }
}
