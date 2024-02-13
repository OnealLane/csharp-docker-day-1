namespace exercise.wwwapi.DataTransferObjects
{
    public class GetStudentsDTO
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime? Dob { get; set; }

        public double AverageGrade { get; set; }

        public CourseForStudentDTO Course { get; set;}
    }
}
