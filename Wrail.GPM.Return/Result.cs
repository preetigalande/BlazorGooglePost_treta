namespace Wrail.GPM.Return;

public class Result<T>
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public Exception? Exception { get; set; }
    public T Content { get; set; } = default!;
}

public class Result
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public Exception? Exception { get; set; }
}
