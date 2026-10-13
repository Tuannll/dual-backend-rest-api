namespace DemoWebApi.Models
{
    /// <summary>
    /// Mô hình sinh viên
    /// </summary>
    public class Student
    {
        /// <summary>
        /// Mã số sinh viên (khóa chính)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Họ và tên sinh viên
        /// </summary>
        public required string FullName { get; set; }

        /// <summary>
        /// Năm sinh
        /// </summary>
        public int YearOfBirth { get; set; }
    }
}
