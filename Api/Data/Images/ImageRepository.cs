using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorApp.Api.Data.Images;

public interface IImageRepository
{
    Task DeleteFileAsync(string fileName, CancellationToken cancellationToken);

    Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType, CancellationToken cancellationToken);
}

public class ImageRepository : IImageRepository
{
    private readonly BlobContainerClient _container;

    public ImageRepository(IConfiguration configuration)
    {
        _container = new BlobContainerClient(configuration["AzureStorage"], "file-container");
        _container.CreateIfNotExistsAsync().Wait();
        _container.SetAccessPolicyAsync(PublicAccessType.Blob).Wait();
    }

    public Task DeleteFileAsync(string fileName, CancellationToken cancellationToken)
    {
        BlobClient blob = _container.GetBlobClient(fileName);
        return blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots, cancellationToken: cancellationToken);
    }

    public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType, CancellationToken cancellationToken)
    {
        BlobClient blob = _container.GetBlobClient(fileName);
        _ = await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots, cancellationToken: cancellationToken);
        _ = await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = contentType }, cancellationToken: cancellationToken);
        return blob.Uri.ToString();
    }
}