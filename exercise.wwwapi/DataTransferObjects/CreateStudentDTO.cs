
namespace exercise.wwwapi.DataTransferObjects
{
    public class CreateStudentDTO
    {

      //  public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime? Dob { get; set; }

        public double AverageGrade { get; set; }

        public int CourseId { get; set; }

      //  public CourseForStudentDTO Course { get; set; }
    }
}
