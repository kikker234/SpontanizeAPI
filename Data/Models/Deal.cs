namespace Data.Models;

public class Deal
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; } = 0;
    public bool IsActive { get; set; }

    public int OrganizationId { get; set; }
    public Organization Organization { get; set; }
}