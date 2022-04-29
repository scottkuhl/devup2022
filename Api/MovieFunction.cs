using BlazorApp.Api.Data.Movies;
using BlazorApp.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorApp.Api
{
    public class MovieFunction
    {
        private readonly IMovieRepository _repository;

        public MovieFunction(IMovieRepository repository)
        {
            _repository = repository;
        }

        [FunctionName("Movies")]
        public async Task<IActionResult> List([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log, CancellationToken cancellationToken)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                System.Collections.Generic.IEnumerable<Movie> movies = await _repository.ListAsync(cancellationToken);
                return new OkObjectResult(movies);
            }
            catch (OperationCanceledException)
            {
                log.LogWarning("Function canceled.");
                return new OkResult();
            }
            catch (Exception ex)
            {
                log.LogError($"Something went wrong in the {nameof(List)} service method {ex}");
                return new BadRequestResult();
            }
        }
    }
}
