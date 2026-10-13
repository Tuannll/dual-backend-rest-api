using System.Text.Json.Serialization;

namespace DemoWebApi.Models.Common
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public int StatusCode { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; }

        public static ApiResponse<T> Success(T data, string message = "Thành công", int statusCode = 200)
        {
            return new ApiResponse<T>
            {
                IsSuccess = true,
                Message = message,
                StatusCode = statusCode,
                Data = data
            };
        }

        public static ApiResponse<T> Failure(string message, int statusCode = 400)
        {
            return new ApiResponse<T>
            {
                IsSuccess = false,
                Message = message,
                StatusCode = statusCode,
                Data = default
            };
        }
    }

    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 0;
    }
}
