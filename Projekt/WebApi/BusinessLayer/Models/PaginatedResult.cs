namespace BusinessLayer.Models;
public class PaginatedResult<T>
{
    public List<T> Items { get; set; } = new List<T>();
    public int TotalItemsCount { get; set; } = 0;
}
