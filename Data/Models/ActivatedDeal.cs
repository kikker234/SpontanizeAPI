using Data.Enums;

namespace Data.Models;

public class ActivatedDeal
{
    public int Id { get; set; }

    public ActivatedDealStatus Status { get; set; } = ActivatedDealStatus.Pending;
    public DateTime ActivatedAt { get; set; }
    public int Quantity { get; set; }
    
    public string UserId { get; set; }
    public User User { get; set; }
    
    public int DealId { get; set; }
    public Deal Deal { get; set; }
}