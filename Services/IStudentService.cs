using DemoWebApi.Dtos;
using DemoWebApi.Models.Common;

namespace DemoWebApi.Services
{
    public interface IStudentService
    {
        PagedResult<StudentResponseDto> GetStudents(string? keyword, int pageIndex, int pageSize);
        StudentResponseDto? GetById(int id);
        StudentResponseDto Create(CreateStudentDto dto);
        StudentResponseDto? Update(int id, UpdateStudentDto dto);
        bool Delete(int id);
    }
}
