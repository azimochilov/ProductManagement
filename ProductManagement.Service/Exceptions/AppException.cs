namespace ProductManagement.Service.Exceptions;
public class AppException : Exception
{
    public int Code { get; set; }
    public AppException(int code = 500, string message = "Something went wrong") : base(message)
    {
        this.Code = code;
    }
}
