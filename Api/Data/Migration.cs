using BlazorApp.Api.Data.Movies;
using BlazorApp.Shared;
using Bogus;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BlazorApp.Api.Data;

public static class Migration
{
    public static void SeedData(IFunctionsHostBuilder builder)
    {
        string environment = Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT");
        if (environment != "Development")
        {
            return;
        }

        Randomizer.Seed = new Random(11232021);
        ServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
        SeedMovieData(serviceProvider);
    }

    private static void SeedMovieData(ServiceProvider serviceProvider)
    {
        IMovieRepository repository = serviceProvider.GetRequiredService<IMovieRepository>();

        System.Collections.Generic.IEnumerable<Movie> movies = repository.ListAsync(default).Result;

        if (!repository.ListAsync(default).Result.Any())
        {
            Faker<Movie> testModels = new Faker<Movie>()
                .RuleFor(r => r.Summary, f => f.Lorem.Sentence())
                .RuleFor(r => r.Id, _ => Guid.NewGuid())
                .RuleFor(r => r.PosterImageUrl, f => f.Image.PicsumUrl())
                .RuleFor(r => r.Year, f => f.Date.Past(yearsToGoBack: 100).Year)
                .RuleFor(r => r.Title, f => f.Company.CompanyName());

            for (int i = 1; i <= 100; i++)
            {
                Movie model = testModels.Generate();
                repository.CreateAsync(model, default).Wait();
            }
        }
    }
}