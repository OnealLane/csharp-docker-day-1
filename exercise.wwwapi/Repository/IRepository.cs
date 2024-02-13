using exercise.wwwapi.DataModels;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {
        Task<IEnumerable<Student>> GetStudents();

        Task<Student> CreateStudent(Student student);

        Task<Student> UpdateStudent(Student student);

        Task<Student> GetStudentById(int id);

        Task<Student> DeleteStudent(Student student);

        Task<IEnumerable<Course>> GetCourses();

        Task<Course> CreateCourse(Course course);

        Task<Course> UpdateCourse(Course course);

        Task<Course> DeleteCourse(Course course);

        Task<Course> GetCourseById(int id);
    }

}
