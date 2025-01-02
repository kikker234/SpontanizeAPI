using Data;
using Data.Models;
using MediatR;

namespace Service.Query.ActivatedDeals.GetAll;

public class GetAllActivatedDealsHandler(SpontanizeContext context) : IRequestHandler<GetAllActivatedDeals, IList<ActivatedDeal>>
{
    public async Task<IList<ActivatedDeal>> Handle(GetAllActivatedDeals request, CancellationToken cancellationToken)
    {
        if (request.UserId == null)
            throw new UnauthorizedAccessException();
        
        return context.ActivatedDeals.Where(activatedDeal => activatedDeal.UserId == request.UserId).ToList();
    }
}