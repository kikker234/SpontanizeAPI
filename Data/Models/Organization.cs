namespace Data.Models;

public class Organization
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string? Description { get; set; }
    public string? Logo { get; set; }
    public string? Banner { get; set; }
}