namespace BusinessLayer.Models.DisplayModels;

public class AuditDisplay
{
    public int UserId { get; set; }
    public required string Action { get; set; }
    public required string EntityName { get; set; }
    public required string EntityId { get; set; }
    public DateTime Timestamp { get; set; }
    public required string Changes { get; set; }
}
