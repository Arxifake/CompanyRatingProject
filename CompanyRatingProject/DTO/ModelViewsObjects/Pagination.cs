namespace DTO.ModelViewsObjects;

public class Pagination<T>
{
    public int PageIndex { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public string Top { get; set; }
    public string SearchString { get; set; }
    public List<T> Companies { get; set; }

    public Pagination(List<T> items, int count, int pageIndex, int pageSize,string top, string searchString)
    {
        Top = top;
        SearchString = searchString;
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalPages = (int) Math.Ceiling(count / (double) pageSize);
        Companies=items;
    }

    public bool HasPrevPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;

    public static  Pagination<T> Create(IEnumerable<T> source, int pageIndex, int pageSize, string top, string searchString)
    {
        var count =  source.Count();
        if (pageIndex>(int) Math.Ceiling(count / (double) pageSize))
        {
            pageIndex = (int) Math.Ceiling(count / (double) pageSize);
        }
        var items = source.Skip((pageIndex-1) * pageSize).Take(pageSize).ToList();
        return new Pagination<T>(items, count, pageIndex, pageSize,top, searchString);
    }
}