namespace WebApp.Common;

public class ResultWithData<T>
{
    public Result? Result { get; set; }

    public T? Data { get; set; }

    public static ResultWithData<T> Success(T data, string? message = null)
    {
        return new ResultWithData<T>
        {
            Result = Result.Success(message),
            Data = data,
        };
    }

    public static ResultWithData<T> Error(string? message = null)
    {
        return new ResultWithData<T>
        {
            Result = Result.Error(message),
            Data = default,
        };
    }

    public static ResultWithData<T> NotFound(string? message = null)
    {
        return new ResultWithData<T>
        {
            Result = Result.NotFound(message),
            Data = default,
        };
    }

    public static ResultWithData<T> Forbidden(string? message = null)
    {
        return new ResultWithData<T>
        {
            Result = Result.Forbidden(message),
            Data = default,
        };
    }

    public static ResultWithData<T> Unauthorized(string? message = null)
    {
        return new ResultWithData<T>
        {
            Result = Result.Unauthorized(message),
            Data = default,
        };
    }
}
