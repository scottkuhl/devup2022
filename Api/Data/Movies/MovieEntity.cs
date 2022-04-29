using AutoMapper;
using BlazorApp.Api.Common.Data;
using BlazorApp.Shared;
using System;

namespace BlazorApp.Api.Data.Movies;

public class MovieEntity : Entity
{
    public string PosterImageUrl { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public int Year { get; set; } = DateTime.Today.Year;
}

public class MovieMappingProfile : Profile
{
    public MovieMappingProfile()
    {
        _ = CreateMap<MovieEntity, Movie>().ReverseMap();
    }
}