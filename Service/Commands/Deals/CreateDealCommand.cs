using MediatR;
using Data.Models;

namespace Service.Commands.Deals;

public class CreateDealCommand : IRequest<bool>
{
    public Deal Deal { get; set; }
    public string? UserId { get; set; }
}