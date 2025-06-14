using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLayer.Models.DisplayModels;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Data;

namespace BusinessLayer.Services.Implementations
{
    public class PublisherService : IPublisherService
    {
        private IMapper mapper;
        private BookHubDbContext dbContext;

        public PublisherService(IMapper mapper, BookHubDbContext context)
        {
            this.mapper = mapper;
            dbContext = context;
        }

        public IQueryable<PublisherDisplay> GetPublishers()
        {
            return dbContext.Publishers
                .ProjectTo<PublisherDisplay>(mapper.ConfigurationProvider);
        }
    }
}