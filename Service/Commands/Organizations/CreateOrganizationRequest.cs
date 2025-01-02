using MediatR;

namespace Service.Commands.Organizations;

public class CreateOrganizationRequest : IRequest<bool>
{
    public string Name { get; set; }
    public string? UserId { get; set; }
}