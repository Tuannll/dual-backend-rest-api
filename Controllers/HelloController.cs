using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using DemoWebApi.Models.Common;

namespace DemoWebApi.Controllers
{
    /// <summary>
    /// API thông điệp chào hỏi
    /// </summary>
    [ApiController]
    [Route("api/hello")]
    public class HelloController : ControllerBase
    {
        /// <summary>
        /// Lấy thông điệp chào hỏi mặc định
        /// </summary>
        [HttpGet]
        public ActionResult<ApiResponse<object>> Get()
        {
            return Ok(ApiResponse<object>.Success(new { message = "Xin chào từ API!" }, "Lấy thông điệp thành công"));
        }

        /// <summary>
        /// Gửi thông điệp kèm tên người dùng
        /// </summary>
        /// <param name="name">Tên người dùng</param>
        [HttpGet("{name}")]
        public ActionResult<ApiResponse<object>> GetWithName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest(ApiResponse<object>.Failure("Tên không được để trống", 400));
            }

            return Ok(ApiResponse<object>.Success(new { message = $"Xin chào, {name.Trim()}!" }, "Lấy thông điệp thành công"));
        }

        /// <summary>
        /// Gửi và phản hồi thông điệp
        /// </summary>
        /// <param name="request">Nội dung thông điệp</param>
        [HttpPost]
        public ActionResult<ApiResponse<MessageResponse>> Post([FromBody] MessageRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Text))
            {
                return BadRequest(ApiResponse<MessageResponse>.Failure("Thông điệp không được để trống", 400));
            }

            var responseData = new MessageResponse
            {
                Message = request.Text.Trim(),
                Timestamp = DateTime.UtcNow
            };

            return Ok(ApiResponse<MessageResponse>.Success(responseData, "Gửi thông điệp thành công", 200));
        }
    }

    public class MessageRequest
    {
        [Required(ErrorMessage = "Thông điệp không được để trống.")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Thông điệp phải từ 1 đến 500 ký tự.")]
        public string Text { get; set; } = string.Empty;
    }

    public class MessageResponse
    {
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}
