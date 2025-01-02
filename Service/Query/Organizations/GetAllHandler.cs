using Data;
using Data.Models;
using MediatR;

namespace Service.Commands.Organizations;

public class GetAllHandler(SpontanizeContext context) : IRequestHandler<GetAllRequest, IList<Organization>>
{
    public async Task<IList<Organization>> Handle(GetAllRequest request, CancellationToken cancellationToken)
    {
        return context.Organization.ToList();
    }
}