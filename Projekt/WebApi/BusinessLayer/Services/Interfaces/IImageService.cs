using BusinessLayer.Models;
using System.Text.Json.Serialization;

namespace BusinessLayer.Services.Interfaces;

public interface IImageService
{
    public OperationResult<FileStream> GetImage(int entityId, EntityType entityType, string suffix);
    public OperationResult SaveImage(int entityId, EntityType entityType, string suffix, Stream imageStream);

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EntityType
    {
        Book,
        User,
        Author,
        Publisher
    }
}
