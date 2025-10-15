using System.Net;
using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;
using Service.Exceptions;
using Service.Interfaces;

namespace Service.Implementations;

public class FileService: IFileService
{
    private readonly Cloudinary _cloudinary;

    public FileService(IConfiguration configuration)
    {
        var cloudName = configuration["CloudinarySettings:CloudName"];
        var apiKey = configuration["CloudinarySettings:ApiKey"];
        var apiSecret = configuration["CloudinarySettings:ApiSecret"];
        var account = new Account(cloudName, apiKey, apiSecret);
        _cloudinary = new Cloudinary(account);
    }

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