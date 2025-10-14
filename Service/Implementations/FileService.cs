using System.Net;
using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;
using Service.Exceptions;
using Service.Interfaces;

namespace Service.Implementations;

public class FileService(IConfiguration configuration): IFileService
{
    private readonly Cloudinary _cloudinary = new(configuration["CloudinaryUrl"]);

    public Task<Uri> UploadAvatarAsync(string fileName)
    {
        if (fileName is null or { Length: 0 })
        {
            throw new ValidationException
            {
                ErrorMessage = "File name is required",
                StatusCode = HttpStatusCode.BadRequest,
                Code = "400"
            };
        }
        var fileId = fileName.Normalize().Replace(" ", "_") + "_" +  Guid.NewGuid();
        var transform = new Transformation().Width(100).Height(100).Crop("fill").Gravity("face");
        var imageUrl = _cloudinary.Api.UrlImgUp.Transform(transform).BuildUrl(fileId);
        return Task.FromResult(new Uri(imageUrl));
    }
}