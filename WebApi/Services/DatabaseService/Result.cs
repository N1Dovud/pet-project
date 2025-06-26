namespace WebApi.Services.DatabaseService;

public class Result
{
    public ResultStatus Status { get; }

    public string? Message { get; }

    private Result(ResultStatus status, string? message)
    {
        this.Status = status;
        this.Message = message;
    }

    public static Result Success(string? message = null) => new(ResultStatus.Success, message);

    public static Result NotFound(string? message = null) => new(ResultStatus.NotFound, message);

    public static Result Forbidden(string? message = null) => new(ResultStatus.Forbidden, message);

    public static Result Error(string? message = null) => new(ResultStatus.Error, message);
}
