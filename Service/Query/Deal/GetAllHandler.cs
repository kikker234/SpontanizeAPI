using Data;
using MediatR;


namespace Service.Query.Deal;

public class GetAllHandler(SpontanizeContext context) : IRequestHandler<GetAllQuery, List<Data.Models.Deal>>
{
    public async Task<List<Data.Models.Deal>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        return context.Deals.ToList();
    }
}