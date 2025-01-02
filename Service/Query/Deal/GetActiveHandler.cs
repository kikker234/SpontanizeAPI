using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Service.Query.Deal;

public class GetActiveHandler(SpontanizeContext context) : IRequestHandler<GetActiveQuery, IList<Data.Models.Deal>>
{
    public async Task<IList<Data.Models.Deal>> Handle(GetActiveQuery request, CancellationToken cancellationToken)
    {
        return context.Deals
            .Include(deal => deal.Organization)
            .Where(deal => deal.OrganizationId == request.OrganizationId)
            .Where(deal => deal.IsActive)
            .ToList();
    }
}