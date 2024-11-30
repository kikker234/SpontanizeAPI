using Data.Models;
using MediatR;

namespace Service.Commands.Deals;

public class UpdateDealCommand : IRequest<bool>
{
    public Deal Deal { get; set; }
}