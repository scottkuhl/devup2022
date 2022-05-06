using BlazorApp.Api.Data.Images;
using BlazorApp.Shared.Common.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorApp.Api;

public class ImageFunction
{
    private readonly IImageRepository _imageRepository;
    private readonly IGuid _guid;

    public ImageFunction(IImageRepository imageRepository, IGuid guid)
    {
        _imageRepository = imageRepository;
        _guid = guid;
    }

    [FunctionName("ImageUpload")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req, CancellationToken cancellationToken)
    {
        try
        {
            IFormFile file = req.Form.Files[0];

            if (file.Length > 0)
            {
                ContentDispositionHeaderValue content = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
                if (content?.FileName != null)
                {
                    string fileNameCleaned = content.FileName.Trim('"');
                    string fileExtension = fileNameCleaned.Split('.').Last();
                    string fileWithoutExtension = fileNameCleaned.Replace($".{fileExtension}", string.Empty);
                    string fileName = $"{fileWithoutExtension}-{_guid.NewGuid}.{fileExtension}";
                    string fileUrl = await _imageRepository.UploadFileAsync(file.OpenReadStream(), fileName, file.ContentType, cancellationToken);
                    return new OkObjectResult(fileUrl);
                }
                else
                {
                    return new BadRequestResult();
                }
            }
            else
            {
                return new BadRequestResult();
            }
        }
        catch (Exception)
        {
            return new BadRequestResult();
        }
    }
}
