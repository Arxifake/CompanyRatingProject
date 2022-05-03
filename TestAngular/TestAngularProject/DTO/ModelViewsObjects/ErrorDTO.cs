namespace DTO.ModelViewsObjects;

public class ErrorDTO
{
    public string ExceptionPath { get; set; }
    public string ExceptionMessage { get; set; }
    public string StackTrace { get; set; }
    public string QueryString { get; set; }
}