using MediatR;

namespace Service.Query.Deal;

public class GetAllQuery : IRequest<List<Data.Models.Deal>>
{
    public string? UserId { get; set; }
}