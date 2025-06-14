using BusinessLayer.Models.DisplayModels;

namespace BusinessLayer.Services.Interfaces;
public interface IPublisherService
{
    public IQueryable<PublisherDisplay> GetPublishers();
}
