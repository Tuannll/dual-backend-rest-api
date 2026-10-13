using System.Collections.Concurrent;
using DemoWebApi.Dtos;
using DemoWebApi.Models;
using DemoWebApi.Models.Common;

namespace DemoWebApi.Services
{
    public class StudentService : IStudentService
    {
        private readonly ConcurrentDictionary<int, Student> _students = new();
        private int _lastId;

        public StudentService()
        {
            var initial = new[]
            {
                new Student { Id = 1, FullName = "Nguyễn Văn A", YearOfBirth = 2003 },
                new Student { Id = 2, FullName = "Trần Thị B", YearOfBirth = 2004 },
                new Student { Id = 3, FullName = "Lê Văn C", YearOfBirth = 2002 }
            };

            foreach (var student in initial)
            {
                _students.TryAdd(student.Id, student);
            }

            _lastId = 3;
        }

        private static StudentResponseDto MapToDto(Student s) => new()
        {
            Id = s.Id,
            FullName = s.FullName,
            YearOfBirth = s.YearOfBirth
        };

        public PagedResult<StudentResponseDto> GetStudents(string? keyword, int pageIndex, int pageSize)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1 || pageSize > 100) pageSize = 10;

            var query = _students.Values.OrderBy(s => s.Id).AsEnumerable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var term = keyword.Trim();
                query = query.Where(s => s.FullName.Contains(term, StringComparison.OrdinalIgnoreCase));
            }

            var materialized = query.ToList();
            int totalCount = materialized.Count;
            int totalPages = pageSize > 0 ? (int)Math.Ceiling((double)totalCount / pageSize) : 0;

            List<StudentResponseDto> items;
            if (totalCount == 0 || pageIndex > totalPages)
            {
                items = new List<StudentResponseDto>();
            }
            else
            {
                items = materialized
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Select(MapToDto)
                    .ToList();
            }

            return new PagedResult<StudentResponseDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        }

        public StudentResponseDto? GetById(int id)
        {
            return _students.TryGetValue(id, out var student) ? MapToDto(student) : null;
        }

        public StudentResponseDto Create(CreateStudentDto dto)
        {
            int nextId = Interlocked.Increment(ref _lastId);
            var student = new Student
            {
                Id = nextId,
                FullName = dto.FullName,
                YearOfBirth = dto.YearOfBirth!.Value
            };

            _students[nextId] = student;
            return MapToDto(student);
        }

        public StudentResponseDto? Update(int id, UpdateStudentDto dto)
        {
            if (!_students.ContainsKey(id))
            {
                return null;
            }

            var updated = new Student
            {
                Id = id,
                FullName = dto.FullName,
                YearOfBirth = dto.YearOfBirth!.Value
            };

            _students[id] = updated;
            return MapToDto(updated);
        }

        public bool Delete(int id)
        {
            return _students.TryRemove(id, out _);
        }
    }
}
