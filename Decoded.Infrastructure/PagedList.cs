using Microsoft.EntityFrameworkCore;

namespace Decoded.Poke.Infrastructure;

public sealed class PagedList<T>
    where T : class
{
    public PagedList(IList<T> items, int page, int pageSize, int totalCount)
    {
        this.Items = items;
        this.PageSize = pageSize;
        this.Page = page;
        this.TotalCount = totalCount;
    }

    public IList<T> Items { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public bool HasNextPage => this.Page * this.PageSize < this.TotalCount;
    public bool HasPreviousPage => this.Page > 1;

    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> query, int page, int pageSize)
    {
        var totalCount = await query.CountAsync();
        var items = await query.Skip((page -1) * pageSize).Take(pageSize).ToListAsync();

        return new(items, page, pageSize, totalCount);
    }
}
