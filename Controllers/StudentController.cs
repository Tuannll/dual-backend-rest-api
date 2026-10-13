using Microsoft.AspNetCore.Mvc;
using DemoWebApi.Dtos;
using DemoWebApi.Models.Common;
using DemoWebApi.Services;

namespace DemoWebApi.Controllers
{
    /// <summary>
    /// API quản lý sinh viên
    /// </summary>
    [ApiController]
    [Route("api/students")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        /// <summary>
        /// Lấy danh sách sinh viên có tìm kiếm và phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm theo họ tên</param>
        /// <param name="pageIndex">Số trang (mặc định 1)</param>
        /// <param name="pageSize">Số phần tử trên mỗi trang (mặc định 10, tối đa 100)</param>
        [HttpGet]
        public ActionResult<ApiResponse<PagedResult<StudentResponseDto>>> GetAllStudents(
            [FromQuery] string? keyword = null,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = _studentService.GetStudents(keyword, pageIndex, pageSize);
            return Ok(ApiResponse<PagedResult<StudentResponseDto>>.Success(result, "Lấy danh sách sinh viên thành công"));
        }

        /// <summary>
        /// Lấy thông tin chi tiết một sinh viên theo ID
        /// </summary>
        /// <param name="id">Mã số sinh viên (ID > 0)</param>
        [HttpGet("{id:int}")]
        public ActionResult<ApiResponse<StudentResponseDto>> GetStudentById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(ApiResponse<StudentResponseDto>.Failure("ID sinh viên phải lớn hơn 0", 400));
            }

            var student = _studentService.GetById(id);
            if (student == null)
            {
                return NotFound(ApiResponse<StudentResponseDto>.Failure($"Không tìm thấy sinh viên với ID {id}", 404));
            }

            return Ok(ApiResponse<StudentResponseDto>.Success(student, "Lấy thông tin sinh viên thành công"));
        }

        /// <summary>
        /// Tạo sinh viên mới
        /// </summary>
        /// <param name="dto">Dữ liệu tạo sinh viên</param>
        [HttpPost]
        public ActionResult<ApiResponse<StudentResponseDto>> CreateStudent([FromBody] CreateStudentDto dto)
        {
            var created = _studentService.Create(dto);

            return CreatedAtAction(
                nameof(GetStudentById),
                new { id = created.Id },
                ApiResponse<StudentResponseDto>.Success(created, "Tạo sinh viên thành công", 201)
            );
        }

        /// <summary>
        /// Cập nhật thông tin sinh viên
        /// </summary>
        /// <param name="id">Mã số sinh viên cần cập nhật</param>
        /// <param name="dto">Dữ liệu cập nhật sinh viên</param>
        [HttpPut("{id:int}")]
        public ActionResult<ApiResponse<StudentResponseDto>> UpdateStudent(int id, [FromBody] UpdateStudentDto dto)
        {
            if (id <= 0)
            {
                return BadRequest(ApiResponse<StudentResponseDto>.Failure("ID sinh viên phải lớn hơn 0", 400));
            }

            var updated = _studentService.Update(id, dto);
            if (updated == null)
            {
                return NotFound(ApiResponse<StudentResponseDto>.Failure($"Không tìm thấy sinh viên với ID {id}", 404));
            }

            return Ok(ApiResponse<StudentResponseDto>.Success(updated, "Cập nhật sinh viên thành công"));
        }

        /// <summary>
        /// Xóa sinh viên
        /// </summary>
        /// <param name="id">Mã số sinh viên cần xóa</param>
        [HttpDelete("{id:int}")]
        public ActionResult<ApiResponse<object>> DeleteStudent(int id)
        {
            if (id <= 0)
            {
                return BadRequest(ApiResponse<object>.Failure("ID sinh viên phải lớn hơn 0", 400));
            }

            bool deleted = _studentService.Delete(id);
            if (!deleted)
            {
                return NotFound(ApiResponse<object>.Failure($"Không tìm thấy sinh viên với ID {id}", 404));
            }

            return Ok(ApiResponse<object>.Success(new { deletedId = id }, "Xóa sinh viên thành công"));
        }
    }
}
