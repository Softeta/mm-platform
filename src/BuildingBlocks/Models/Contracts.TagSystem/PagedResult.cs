namespace Contracts.TagSystem;

public class PagedResult<T>
{
    public IEnumerable<T> Data { get; set; } = default!;
}