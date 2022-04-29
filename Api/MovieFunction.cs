using BlazorApp.Api.Common;
using BlazorApp.Api.Data.Movies;
using BlazorApp.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorApp.Api
{
    public class MovieFunction : Function
    {
        private readonly IMovieRepository _repository;

        public MovieFunction(IMovieRepository repository, IHttpContextAccessor httpContextAccessor, ILogger<MovieFunction> logger) : base(httpContextAccessor, logger)
        {
            _repository = repository;
        }

        [FunctionName("Movies")]
        public async Task<IActionResult> List([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log, CancellationToken cancellationToken)
        {
            System.Collections.Generic.IEnumerable<Movie> movies = await _repository.ListAsync(cancellationToken);
            return new OkObjectResult(movies);
        }
    }
}
