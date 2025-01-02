using System.Collections;
using Data.Models;
using MediatR;

namespace Service.Query.ActivatedDeals.GetAll;

public class GetAllActivatedDeals : IRequest<IList<ActivatedDeal>>
{
    public string? UserId { get; set; }
}