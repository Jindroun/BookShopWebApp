using BusinessLayer.Models;
using BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer.Services.Implementations;

public class ImageService : IImageService
{
    private readonly static string defaultImageDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BookHub", "Images");
    private readonly string imageDirectory;

    public ImageService(IConfiguration configuration)
    {
        imageDirectory = configuration["ImageDirectory"] ?? defaultImageDirectory;
    }

    public OperationResult<FileStream> GetImage(int entityId, IImageService.EntityType entityType, string suffix)
    {
        var imagePath = Path.Combine(imageDirectory, entityType.ToString(), entityId.ToString() + suffix);
        if (!File.Exists(imagePath))
        {
            return OperationResult<FileStream>.Failure("Image not found.");
        }

        return OperationResult<FileStream>.Success(File.OpenRead(imagePath));
    }

    public OperationResult SaveImage(int entityId, IImageService.EntityType entityType, string suffix, Stream imageStream)
    {
        var imageDirectoryPath = Path.Combine(imageDirectory, entityType.ToString());
        if (!Directory.Exists(imageDirectoryPath))
        {
            Directory.CreateDirectory(imageDirectoryPath);
        }

        var imagePath = Path.Combine(imageDirectoryPath, entityId.ToString() + suffix);
        using var fileStream = File.Create(imagePath);
        imageStream.CopyTo(fileStream);

        return OperationResult.Success();
    }
}
