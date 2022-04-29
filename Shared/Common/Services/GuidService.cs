using System;

namespace BlazorApp.Shared.Common.Services;

public interface IGuid
{
    Guid NewGuid { get; }
}

public class GuidService : IGuid
{
    public Guid NewGuid => Guid.NewGuid();
}