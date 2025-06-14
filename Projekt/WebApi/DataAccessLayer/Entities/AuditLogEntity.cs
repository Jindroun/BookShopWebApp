namespace DataAccessLayer.Entities;

public record AuditLogEntity : BaseEntity
{
    public int UserId { get; set; }
    public required string Action { get; set; }
    public required string EntityName { get; set; }
    public required string EntityId { get; set; }
    public required DateTime Timestamp { get; set; }
    public required string Changes { get; set; }
}
