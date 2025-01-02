using Data;
using Data.Models;
using MediatR;
using Service.Commands.Deals;

namespace Service.Commands.Deals;

public class CreateDealHandler(SpontanizeContext context) : IRequestHandler<CreateDealCommand, bool>
{
    public async Task<bool> Handle(CreateDealCommand request, CancellationToken cancellationToken)
    {
        Deal deal = request.Deal;
        User? user = context.Users.FirstOrDefault(user => user.Id == request.UserId);

        if (user == null || user.OrganizationId == null)
        {
            throw new Exception();
        }
        
        deal.OrganizationId = user.OrganizationId ?? 0;
        
        context.Deals.Add(deal);

        return context.SaveChanges() > 0;
    }
}