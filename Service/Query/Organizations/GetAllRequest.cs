using Data.Models;
using MediatR;

namespace Service.Commands.Organizations;

public class GetAllRequest : IRequest<IList<Organization>>
{
    
}