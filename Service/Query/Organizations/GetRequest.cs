using Data.Models;
using MediatR;

namespace Service.Commands.Organizations;

public class GetRequest : IRequest<Organization>
{
    public int Id { get; set; }
}