public class ApiResult<T>
{
    public bool Success { get; set; }
    public T Data { get; set; }
    public ProblemDetailsResponse ProblemDetails { get; set; }
}
