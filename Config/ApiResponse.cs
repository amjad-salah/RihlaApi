namespace Backend.Config;

public class ApiResponse
{
    public bool IsSuccess { get; set; }
    public object Data { get; set; } = new();
    public List<string> Errors { get; set; } = new();
}