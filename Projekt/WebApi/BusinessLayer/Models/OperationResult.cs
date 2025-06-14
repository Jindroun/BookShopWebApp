namespace BusinessLayer.Models;
public class OperationResult
{
    public bool IsSuccess { get; private set; }
    public string? CustomErrorMessage { get; private set; }
    public Exception? Exception { get; private set; }

    protected OperationResult(bool isSuccess, string? customErrorMessage = null, Exception? exception = null)
    {
        IsSuccess = isSuccess;
        CustomErrorMessage = customErrorMessage;
        Exception = exception;
    }

    public static OperationResult Success()
    {
        return new OperationResult(true);
    }

    public static OperationResult Failure(string? customErrorMessage, Exception? exception = null)
    {
        return new OperationResult(false, customErrorMessage, exception);
    }
}

public class OperationResult<T> : OperationResult
{
    public T? Data { get; private set; }

    private OperationResult(bool isSuccess, T? Data = default, string? customErrorMessage = null, Exception? exception = null)
        : base(isSuccess, customErrorMessage, exception)
    {
        this.Data = Data;
    }

    public static OperationResult<T> Success(T Data)
    {
        return new OperationResult<T>(true, Data);
    }

    public static OperationResult<T> Failure(string? customErrorMessage, Exception? exception = null)
    {
        return new OperationResult<T>(false, default, customErrorMessage, exception);
    }
}


