using System.ComponentModel.DataAnnotations;

namespace DemoWebApi.Dtos
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ValidYearOfBirthAttribute : ValidationAttribute
    {
        private readonly int _minYear;

        public ValidYearOfBirthAttribute(int minYear = 1900)
        {
            _minYear = minYear;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return ValidationResult.Success;
            }

            if (value is int year)
            {
                int currentYear = DateTime.UtcNow.Year;
                if (year < _minYear || year > currentYear)
                {
                    return new ValidationResult($"Năm sinh phải nằm trong khoảng từ {_minYear} đến {currentYear}.");
                }
                return ValidationResult.Success;
            }

            return new ValidationResult("Năm sinh không hợp lệ.");
        }
    }

    public abstract class BaseStudentDto
    {
        private string _fullName = string.Empty;

        [Required(ErrorMessage = "Họ và tên không được để trống.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Họ và tên phải từ 2 đến 100 ký tự.")]
        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "Họ và tên không được chỉ chứa khoảng trắng.")]
        public string FullName
        {
            get => _fullName;
            set => _fullName = value?.Trim() ?? string.Empty;
        }

        [Required(ErrorMessage = "Năm sinh không được để trống.")]
        [ValidYearOfBirth(1900)]
        public int? YearOfBirth { get; set; }
    }

    public class CreateStudentDto : BaseStudentDto
    {
    }

    public class UpdateStudentDto : BaseStudentDto
    {
    }

    public class StudentResponseDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int YearOfBirth { get; set; }
        public int Age => Math.Max(0, DateTime.UtcNow.Year - YearOfBirth);
    }
}
