using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using System;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageController : ControllerBase
{
    private static readonly IDictionary<string, string> filetypes = new Dictionary<string, string>
    {
        ["image/jpeg"] = ".jpg",
        ["image/png"] = ".png",
        ["image/gif"] = ".gif",
        ["image/bmp"] = ".bmp"
    };
    private IImageService imageService;

    public ImageController(IImageService service)
    {
        imageService = service;
    }

    [HttpGet("{entityType}/{entityId}")]
    public IActionResult GetImage(int entityId, IImageService.EntityType entityType, [FromQuery] string fileType = "image/jpeg")
    {
        
        if (!fileType.StartsWith("image/") || !filetypes.TryGetValue(fileType, out var ext))
        {
            return BadRequest(new { Message = $"Unsupported Content-Type {fileType}" });
        }
        var result = imageService.GetImage(entityId, entityType, ext);
        if (!result.IsSuccess)
        {
            return NotFound(new { Message = result.CustomErrorMessage });
        }
        
        return File(result.Data!, fileType);
    }

    [HttpPost("{entityType}/{entityId}")]
    public IActionResult SaveImage(int entityId, IImageService.EntityType entityType, IFormFile image)
    {
        Console.Error.WriteLine($"Content-Type: {image.ContentType}");
        if (!image.ContentType.StartsWith("image/") || !filetypes.TryGetValue(image.ContentType, out var ext))
        {
            return BadRequest(new { Message = "Unsupported image type" });
        }

        using var stream = image.OpenReadStream();
        var result = imageService.SaveImage(entityId, entityType, ext, stream);
        if (!result.IsSuccess)
        {
            return BadRequest(new { Message = result.CustomErrorMessage });
        }

        return Ok();
    }
}
