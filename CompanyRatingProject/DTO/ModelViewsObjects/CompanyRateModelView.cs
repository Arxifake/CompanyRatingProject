using DataAccess.Models;

namespace DTO.ModelViewsObjects;

public class CompanyRateModelView
{
    public Company Company { get; set; }
    public Rating Rating { get; set; }
}