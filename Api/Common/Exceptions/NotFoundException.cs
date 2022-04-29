using BlazorApp.Shared.Common;
using Humanizer;
using System;

namespace BlazorApp.Api.Common.Exceptions;

[Serializable]
public class NotFoundException<T> : Exception where T : Model
{
    public NotFoundException(Guid id) : base($"The {typeof(T).Name.Humanize()} with id: {id} doesn't exist.")
    {
    }

    private NotFoundException()
    {
    }
}