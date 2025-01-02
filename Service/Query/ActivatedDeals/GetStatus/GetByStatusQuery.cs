using Data.Enums;
using Data.Models;
using MediatR;

namespace Service.Query.ActivatedDeals.GetStatus;

public class GetByStatusQuery : IRequest<IList<ActivatedDeal>>
{
    public String? UserId { get; set; }
    public String? Status { get; set; }
}