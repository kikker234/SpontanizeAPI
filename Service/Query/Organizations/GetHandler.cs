using Data;
using Data.Models;
using MediatR;

namespace Service.Commands.Organizations;

public class GetHandler(SpontanizeContext context) : IRequestHandler<GetRequest, Organization>
{
    public async Task<Organization> Handle(GetRequest request, CancellationToken cancellationToken)
    {
        return context.Organization.FirstOrDefault(organization => organization.Id == request.Id);
    }
}