using System.Collections;
using MediatR;

namespace Service.Query.Deal;

public class GetActiveQuery: IRequest<IList<Data.Models.Deal>>
{
    public int OrganizationId { get; set; }
}