using System.Text.Json;
using System.Text.Json.Serialization;

namespace Core.Common;

public class GenericResponse<T>
{
    public T Data { get; set; }

    [JsonIgnore]
    public int StatusCode { get; set; }

    public string Message { get; set; }

    public static GenericResponse<T> Success(T data, int statusCode)
    {
        return new GenericResponse<T> { Data = data, StatusCode = statusCode };
    }

    public static GenericResponse<T> Success(int statusCode)
    {
        return new GenericResponse<T> { Data = default(T), StatusCode = statusCode };
    }

    public static GenericResponse<T> Success(string message, int statusCode)
    {
        return new GenericResponse<T> { Data = default(T), StatusCode = statusCode, Message = message };
    }
    public static GenericResponse<T> Success(T data, string message, int statusCode)
    {
        return new GenericResponse<T> { Data = data, StatusCode = statusCode, Message = message };
    }


    public static GenericResponse<T> Fail(string error, int statusCode)
    {
        return new GenericResponse<T>
        {
            Message = error,
            StatusCode = statusCode
        };
    }

    public static GenericResponse<T> NotFoundException(string error, int statusCode)
    {
        return new GenericResponse<T>
        {
            Message = error,
            StatusCode = statusCode
        };
    }

    public static GenericResponse<T> NotFoundException(string name, object key, int statusCode)
    {
        return new GenericResponse<T>
        {
            Message = $"Entity \"{name}\" ({key}) was not found.",
            StatusCode = statusCode
        };
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
