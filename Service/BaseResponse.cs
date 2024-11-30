namespace Service;

public class BaseResponse<T>
{
    public T Body { get; set; }
    public bool Error { get; set; }
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
}