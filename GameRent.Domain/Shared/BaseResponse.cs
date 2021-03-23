using System.Net;

namespace GameRent.Domain.Shared
{
    public class BaseResponse
    {
        public dynamic Data { get; protected set; }
        public string Message { get; protected set; }
        public bool Success { get; protected set; }
        public HttpStatusCode StatusCode { get; protected set; }


        public BaseResponse(bool success, string message, HttpStatusCode statusCode, object data = null)
        {
            Success = success;
            Message = message;
            Data = data;
            StatusCode = statusCode;
        }
    }
}