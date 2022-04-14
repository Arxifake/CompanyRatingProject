namespace DTO.ModelViewsObjects;

public class CompanyDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Grade1Avg { get; set; }
    public double Grade2Avg { get; set; }
    public double Grade3Avg { get; set; }
    public double Grade4Avg { get; set; }

    public double Grade5Avg { get; set; }
    public double TotalAvg { get; set; }
    public ICollection<RatingDto> Ratings { get; set; }
}