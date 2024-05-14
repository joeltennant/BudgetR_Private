using FluentValidation.Results;

namespace BudgetR.Core;
public class Result<T>
{
    public T? Value { get; private set; }

    public bool IsSuccess { get; }
    public ErrorType ErrorType { get; }
    public IList<ValidationFailure>? Errors { get; }

    public Result() { }

    #region ---Constructors---

    //Success Constructors
    public Result(T value)
    {
        Value = value;
        IsSuccess = true;
    }

    public Result(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    //Error Constructor
    public Result(IList<ValidationFailure> errors)
    {
        Errors = errors;
        ErrorType = ErrorType.Validation;
    }

    //Error with type only
    public Result(ErrorType errorType)
    {
        ErrorType = errorType;
    }

    //Error with type only
    public Result(ErrorType errorType, IList<ValidationFailure> errors)
    {
        Errors = errors;
        ErrorType = errorType;
    }

    #endregion

    #region ---Result Methods---

    public Result<T> Error(IList<ValidationFailure> errors)
    {
        return new Result<T>(errors);
    }

    public Result<T> Error()
    {
        return new Result<T>();
    }

    //Success Method
    public Result<T> Success(T value)
    {
        return new Result<T>(value);
    }

    public Result<T> Success()
    {
        return new Result<T>(true);
    }

    //Not Authorized Method
    public Result<T> NotAuthorized()
    {
        return new Result<T>(ErrorType.NotAuthorized);
    }

    //Not Found Method  
    public Result<T> NotFound()
    {
        return new Result<T>(ErrorType.NotFound);
    }

    //System Error
    public Result<T> SystemError(string message)
    {
        var errors = new List<ValidationFailure>
        {
            new ValidationFailure(string.Empty, message)
        };

        return new Result<T>(ErrorType.SystemError, errors);
    }

    #endregion
}

public record NoValue();