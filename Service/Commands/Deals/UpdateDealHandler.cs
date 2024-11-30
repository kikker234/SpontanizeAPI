using Data;
using Data.Models;
using MediatR;

namespace Service.Commands.Deals;

public class UpdateDealHandler(SpontanizeContext context) : IRequestHandler<UpdateDealCommand, Boolean>
{
    public async Task<bool> Handle(UpdateDealCommand request, CancellationToken cancellationToken)
    {
        Deal updatedDeal = request.Deal;
        Deal existingDeal = context.Deals.Find(updatedDeal.Id);

        if (existingDeal == null)
            return false;

        existingDeal.Name = updatedDeal.Name;
        existingDeal.Description = updatedDeal.Description;
        existingDeal.IsActive = updatedDeal.IsActive;

        context.Deals.Update(existingDeal);

        return await context.SaveChangesAsync(cancellationToken) > 0;
    }
}