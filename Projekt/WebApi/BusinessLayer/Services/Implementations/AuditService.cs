using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLayer.Models.DisplayModels;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Data;

namespace BusinessLayer.Services.Implementations;

public class AuditService : IAuditService
{
    private readonly BookHubDbContext dbContext;
    private readonly IMapper mapper;

    public AuditService(BookHubDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public IQueryable<AuditDisplay> GetLogs()
    {
        return dbContext.AuditLogs.ProjectTo<AuditDisplay>(mapper.ConfigurationProvider);
    }
}
