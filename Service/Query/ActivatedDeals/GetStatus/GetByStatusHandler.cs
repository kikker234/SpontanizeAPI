using Data;
using Data.Enums;
using Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Service.Query.ActivatedDeals.GetStatus;

public class GetByStatusHandler(SpontanizeContext context) : IRequestHandler<GetByStatusQuery, IList<ActivatedDeal>>
{
    public async Task<IList<ActivatedDeal>> Handle(GetByStatusQuery request, CancellationToken cancellationToken)
    {
        if (request.Status == null)
            return new List<ActivatedDeal>();

        string preparedStatus = PrepareStatus(request.Status);
        if (!Enum.IsDefined(typeof(ActivatedDealStatus), preparedStatus))
            return new List<ActivatedDeal>();

        ActivatedDealStatus status = Enum.Parse<ActivatedDealStatus>(preparedStatus);
        
        return context.ActivatedDeals
            .Include(activated => activated.Deal)
            .ThenInclude(deal => deal.Organization)
            .Where(deal => deal.UserId == request.UserId && deal.Status == status && deal.Deal.IsActive)
            .ToList();
    }

    private string PrepareStatus(string inputStatus)
    {
        string preparedStatus = inputStatus.ToLower();
        return char.ToUpper(preparedStatus[0]) + preparedStatus.Substring(1);
    }
}