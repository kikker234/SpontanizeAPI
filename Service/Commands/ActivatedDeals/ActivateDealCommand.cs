using MediatR;

namespace Service.Commands.ActivatedDeals;

public class ActivateDealCommand : IRequest<Boolean>
{
    public String? UserId { get; set; }
    public int DealId { get; set; }
    public int Quantity { get; set; }
}