namespace Talabat.API.Errors
{
    public class ApiExceptionResponse : ApiResponse
    {
        public ApiExceptionResponse(int statusCode,string?msg=null,string?details=null):base(statusCode,msg)
        {
            Details = details;
        }
        public string? Details { get; set; }
    }
}
