namespace BookingHotel.Models.Response
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }

        public BaseResponse(bool success, string message, T? data = default)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public static BaseResponse<T> Fail(string message)
        {
            return new BaseResponse<T>(false, message);
        }

        public static BaseResponse<T> Ok(T data, string message = "")
        {
            return new BaseResponse<T>(true, message, data);
        }
    }

}
