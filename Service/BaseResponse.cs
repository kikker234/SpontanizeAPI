using System.Collections;
using System.Text.Json.Serialization;
using FluentValidation.Results;

namespace Service;

public class BaseResponse<T>
{
    public T Body { get; set; }
    public bool Error { get; set; }
    public bool Success { get; set; }
    public IEnumerable<ValidationFailure> ErrorMessages { get; set; }
    [JsonIgnore]
    public int StatusCode { get; set; }
    public string ErrorMessage { get; set; }
}