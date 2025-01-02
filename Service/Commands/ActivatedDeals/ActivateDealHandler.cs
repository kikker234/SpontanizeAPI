using Data;
using Data.Enums;
using Data.Models;
using MediatR;

namespace Service.Commands.ActivatedDeals;

public class ActivateDealHandler(SpontanizeContext context) : IRequestHandler<ActivateDealCommand, Boolean>
{
    public async Task<bool> Handle(ActivateDealCommand request, CancellationToken cancellationToken)
    {
        if (request.UserId == null)
            throw new UnauthorizedAccessException();
        
        ActivatedDeal activated = new ActivatedDeal()
        {
            UserId = request.UserId,
            DealId = request.DealId,
            Status = ActivatedDealStatus.Pending,
            Quantity = request.Quantity,
            ActivatedAt = DateTime.Now,
        };
        
        context.ActivatedDeals.Add(activated);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }
}