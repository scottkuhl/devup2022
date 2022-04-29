using BlazorApp.Api;
using BlazorApp.Api.Data;
using BlazorApp.Api.Data.Movies;
using BlazorApp.Shared.Common.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace BlazorApp.Api;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        _ = builder.Services.AddLogging();
        _ = builder.Services.AddAutoMapper(typeof(Startup));
        _ = builder.Services.AddTransient<IDateTime, DateTimeService>();
        _ = builder.Services.AddTransient<IGuid, GuidService>();

        _ = builder.Services.AddScoped<IMovieRepository, MovieRepository>();

        Migration.SeedData(builder);
    }
}