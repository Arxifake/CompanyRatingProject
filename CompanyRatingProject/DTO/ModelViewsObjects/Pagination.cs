namespace DTO.ModelViewsObjects;

public class Pagination<T>:List<T>
{
    public int PageIndex { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }

    public Pagination(List<T> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalPages = (int) Math.Ceiling(count / (double) pageSize);
        this.AddRange(items);
    }

    public bool HasPrevPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;

    public static  Pagination<T> Create(IEnumerable<T> source, int pageIndex, int pageSize)
    {
        var count =  source.Count();
        var items = source.Skip((pageIndex-1) * pageSize).Take(pageSize).ToList();
        return new Pagination<T>(items, count, pageIndex, pageSize);
    }
}