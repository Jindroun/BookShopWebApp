using BusinessLayer.Models;
using BusinessLayer.Models.DisplayModels;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace BusinessLayer.Services.Interfaces;

public interface IAuditService
{
    public IQueryable<AuditDisplay> GetLogs();
    public IQueryable<AuditDisplay> GetLogs(int entityId, AuditedEntityName entityType) =>
        GetLogs().Where(x => x.EntityId == entityId.ToString() && x.EntityName == entityType.ToString());

    public async Task<OperationResult<int>> ModifiedCount(int entityId, AuditedEntityName entityType)
        => OperationResult<int>.Success(await GetLogs(entityId, entityType).CountAsync());

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AuditedEntityName
    {
        BookEntity,
        UserEntity,
        AuthorEntity,
        ShopItemEntity
    }
}
