namespace DataAccessLayer.Entities;
public record WishlistEntryEntity:BaseEntity
{
    public int UserId { get; set; }
    public UserEntity User { get; set; }
    public int BookId { get; set; }
    public BookEntity Book { get; set; }
}
