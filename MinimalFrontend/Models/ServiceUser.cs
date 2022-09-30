namespace MinimalFrontend.Models;

public class ServiceUser
{
    public int Id { get; set; }
    public string UserName { get; init; } = "";
    public string Mail { get; init; } = "";
    public int Age { get; init; }
}