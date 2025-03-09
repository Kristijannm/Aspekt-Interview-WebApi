namespace CompanyContacts.Shared;

public sealed class PagedResult<T>(IEnumerable<T> items, int totalCount, int pageSize)
{
    public IEnumerable<T> Items { get; set; } = items;
    public int TotalCount { get; set; } = totalCount;
    public int TotalPages { get; set; } = (int)Math.Ceiling(totalCount / (double)pageSize);
}
