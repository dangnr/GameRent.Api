namespace GameRent.Domain.Shared
{
    public class BaseResponse
    {
        public dynamic Data { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }

        public BaseResponse(bool success, string message, object data = null)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}