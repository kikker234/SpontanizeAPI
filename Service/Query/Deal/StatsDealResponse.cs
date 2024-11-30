namespace Service.Commands.Deals;

public class StatsDealResponse
{
    public Dictionary<string, float> ActivatedDeals { get; set; }
    public Dictionary<string, float> RevenueMade { get; set; }
}