namespace exercise.wwwapi.DataTransferObjects
{
    public class GetCourseDTO
    {
        public int Id { get; set; }


        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public ICollection<StudentForCourseDTO> students { get; set; } = new List<StudentForCourseDTO>();
    }
}
