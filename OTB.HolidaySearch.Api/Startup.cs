using FluentValidation.AspNetCore;
using OTB.HolidaySearch.Api.Validators;
using OTB.HolidaySearch.Domain.Contracts;
using OTB.HolidaySearch.Domain.Services;
using OTB.HolidaySearch.Gds.Converters;

namespace OTB.HolidaySearch.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddApplicationPart(typeof(Startup).Assembly);
            services.AddControllers();
            services.AddFluentValidation(fv => { fv.RegisterValidatorsFromAssemblyContaining<SearchRequestValidator>(); });
            services.AddSingleton<IHolidaySearchService, HolidaySearchService>();
            services.AddSingleton<IFlightsConverter<FlightsResponse>, FlightsConverter>();
            services.AddSingleton<IHotelsConverter<HotelsResponse>, HotelsConverter>();
            services.AddSingleton<ILocationsConverter<LocationsResponse>, LocationsConverter>();
            services.AddSingleton<IRecommendationSearcher, RecommendationsSearcher>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
