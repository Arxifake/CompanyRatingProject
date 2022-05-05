namespace DTO.ModelViewsObjects;

public class Page
{
    public List<CompanyDto> Companies { get; set; }
    public int PageIndex { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public bool HasPrevPage { get; set; }
    public bool HasNextPage { get; set; }
    public string Top { get; set; }
    public string SearchString { get; set; }
}