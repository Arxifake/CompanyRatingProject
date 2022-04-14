namespace DTO.ModelViewsObjects;

public class RatingDto
{
    public string Id { get; set; }
    public int Grade1 { get; set; }
    public int Grade2 { get; set; }
    public int Grade3 { get; set; }
    public int Grade4 { get; set; }
    public int Grade5 { get; set; }
    public double Total { get; set; }
    public string Text { get; set; }
    public DateTime DateTime { get; set; }
    public CompanyDto Company { get; set; }
    public string CompanyId { get; set; }
    public UserDto User { get; set; }
    public string UserId { get; set; }
}