using AutoMapper;
using BlazorApp.Api.Common.Data;
using BlazorApp.Api.Common.Exceptions;
using BlazorApp.Shared;
using BlazorApp.Shared.Common.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorApp.Api.Data.Movies;

public interface IMovieRepository
{
    Task<Movie> CreateAsync(Movie movie, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);

    Task<Movie> GetAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<Movie>> ListAsync(CancellationToken cancellationToken);

    Task<Movie> UpdateAsync(Movie movie, CancellationToken cancellationToken);
}

public sealed class MovieRepository : CosmosDbRepository<MovieEntity>, IMovieRepository
{
    private readonly IGuid _guid;
    private readonly IMapper _mapper;

    public MovieRepository(IMapper mapper, IGuid guid, IConfiguration configuration)
        : base(configuration, nameof(MovieEntity.id))
    {
        _guid = guid;
        _mapper = mapper;
    }

    public async Task<Movie> CreateAsync(Movie movie, CancellationToken cancellationToken)
    {
        movie.Id = _guid.NewGuid;

        MovieEntity entity = _mapper.Map<MovieEntity>(movie);
        await AddAsync(entity, entity.id, cancellationToken);
        return movie;
    }

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return DeleteAsync(id.ToString(), id.ToString(), cancellationToken);
    }

    public async Task<Movie> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        MovieEntity entity = await ExistsAsync(id, cancellationToken);
        return _mapper.Map<Movie>(entity);
    }

    public new async Task<IEnumerable<Movie>> ListAsync(CancellationToken cancellationToken)
    {
        IEnumerable<MovieEntity> entities = await base.ListAsync(cancellationToken);
        return _mapper.Map<IEnumerable<Movie>>(entities);
    }

    public async Task<Movie> UpdateAsync(Movie movie, CancellationToken cancellationToken)
    {
        MovieEntity entity = await ExistsAsync(movie.Id, cancellationToken);

        _ = _mapper.Map(movie, entity);
        await UpdateAsync(entity, entity.id, cancellationToken);

        return movie;
    }

    private async Task<MovieEntity> ExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        MovieEntity entity = await GetAsync(id.ToString(), id.ToString(), cancellationToken);
        return entity is null ? throw new NotFoundException<Movie>(id) : entity;
    }
}