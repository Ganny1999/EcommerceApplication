namespace EcommerceProductModule.Models.Dtos
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public ApiResponse(int statusCode,bool status,T data, string message)
        {
            StatusCode= statusCode;
            Message= message;
            Data = data;
            Status = status;
        }
        public ApiResponse(int statusCode, bool status, string message)
        {
            StatusCode = statusCode;
            Message = message;
            Status = status;
        }
    }
}
