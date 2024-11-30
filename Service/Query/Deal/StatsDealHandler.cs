using System.Data.Entity.Core.Mapping;
using MediatR;

namespace Service.Commands.Deals;

public class StatsDealHandler : IRequestHandler<StatsDealRequest, StatsDealResponse>
{
    public async Task<StatsDealResponse> Handle(StatsDealRequest request, CancellationToken cancellationToken)
    {
        var response = new StatsDealResponse();

        response.ActivatedDeals = new Dictionary<string, float>();
        response.RevenueMade = new Dictionary<string, float>();
        
        foreach (var date in GetDates())
        {
            int random = new Random().Next(5, 15);
            float price = 9.99F;
            
            response.RevenueMade.Add(date, random * price);
            response.ActivatedDeals.Add(date, random);
        }
        
        return response;
    }

    private List<String> GetDates()
    {
        var dates = new List<String>();
        var date = DateTime.Today;

        for (int i = 7 - 1; i >= 0; i--)
        {
            dates.Add(date.ToString("d - MMM"));

            date = date.AddHours(-24);
        }

        dates.Reverse();
        return dates;
    }
}