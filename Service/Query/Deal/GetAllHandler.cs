using Data;
using MediatR;


namespace Service.Query.Deal;

public class GetAllHandler(SpontanizeContext context) : IRequestHandler<GetAllQuery, List<Data.Models.Deal>>
{
    public async Task<List<Data.Models.Deal>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        List<Data.Models.Deal> foundDeals = context.Deals.ToList();
        
        Console.WriteLine("Found: " + foundDeals.Count + " deals!");

        return foundDeals;
    }
}