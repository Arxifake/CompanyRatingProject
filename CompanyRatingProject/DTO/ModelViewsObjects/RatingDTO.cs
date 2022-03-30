namespace DTO.ModelViewsObjects;

public class RatingDto
{
    public int Id { get; set; }
    public int Grade1 { get; set; }
    public int Grade2 { get; set; }
    public int Grade3 { get; set; }
    public int Grade4 { get; set; }
    public int Grade5 { get; set; }
    public double Total { get; set; }
    public string Text { get; set; }
    public DateTime DateTime { get; set; }
    public CompanyDto Company { get; set; }
    public int CompanyId { get; set; }
    public AuthorDto Author { get; set; }
    public int AuthorId { get; set; }
}