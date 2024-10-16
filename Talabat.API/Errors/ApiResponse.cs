
namespace Talabat.API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode,string? msg= null)
        {
            StatusCode = statusCode;
            Msg = msg?? GetMessageByStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string? Msg { get; set; }

        private string? GetMessageByStatusCode(int statusCode)
        {
            return statusCode switch
            {
                100 => "Continue",
                101 => "Switching Protocols",
                200 => "OK",
                201 => "Created",
                202 => "Accepted",
                203 => "Non-Authoritative Information",
                204 => "No Content",
                205 => "Reset Content",
                206 => "Partial Content",
                300 => "Multiple Choices",
                301 => "Moved Permanently",
                302 => "Found",
                303 => "See Other",
                304 => "Not Modified",
                305 => "Use Proxy",
                307 => "Temporary Redirect",
                400 => "Bad Request",
                401 => "Unauthorized",
                402 => "Payment Required",
                403 => "Forbidden",
                404 => "Not Found",
                500 => "Internal Server Error",
                501 => "Not Implemented",
                502 => "Bad Gateway",
                503 => "Service Unavailable",
                504 => "Gateway Timeout",
                505 => "HTTP Version Not Supported",
                _ => "Unknown Status Code"
            };
        }
    }
}
