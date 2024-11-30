using Data;
using MediatR;
using Service.Commands.Deals;

namespace Service.Commands.Deals;

public class CreateDealHandler(SpontanizeContext context) : IRequestHandler<CreateDealCommand, bool>
{
    public async Task<bool> Handle(CreateDealCommand request, CancellationToken cancellationToken)
    {
        context.Deals.Add(request.Deal);

        return context.SaveChanges() > 0;
    }
}