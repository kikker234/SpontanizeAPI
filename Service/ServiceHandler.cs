using MediatR;

namespace Service;

public class ServiceHandler(IMediator mediator) : IServiceHandler
{
    public async Task<BaseResponse<T>> Submit<T>(IRequest<T> request)
    {
        try
        {
            // Send the request via the mediator
            T result = await mediator.Send(request);

            // Wrap the result in a BaseResponse and return it
            return new BaseResponse<T>
            {
                Body = result,
                Error = false,
                Success = true,
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new BaseResponse<T>
            {
                Success = false,
                Error = true,
                ErrorMessage = "An error occurred"
            };
        }
    }
}