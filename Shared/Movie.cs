using BlazorApp.Shared.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Shared;

public class Movie : Model
{
    public bool IsNew => Id == Guid.Empty;

    [MaxLength(2048, ErrorMessage = "Poster URL must be 2048 characters or less.")]
    public string PosterImageUrl { get; set; } = string.Empty;

    public string Summary { get; set; } = string.Empty;

    [Required(ErrorMessage = "Title is required.")]
    [MaxLength(200, ErrorMessage = "Title must be 200 characters or less.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Year is required.")]
    [Range(1890, 2120, ErrorMessage = "Year must be between 1890 and 2120")]
    public int Year { get; set; } = DateTime.Today.Year;
}