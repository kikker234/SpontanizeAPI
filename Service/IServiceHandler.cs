using MediatR;

namespace Service;

public interface IServiceHandler
{
    Task<BaseResponse<T>> Submit<T>(IRequest<T> request);
}