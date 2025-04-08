namespace LudoModels;
public class Result<T>
{
    public T? Value { get; set; }
    public bool Success { get; set; } = false;
    public ErrorType ErrorType { get; set; }


    public Result() { }
    public Result(T value)
    {
        Success = true;
        Value = value; 
    }
}
