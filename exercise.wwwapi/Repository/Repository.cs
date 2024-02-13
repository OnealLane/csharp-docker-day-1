using exercise.wwwapi.Data;
using exercise.wwwapi.DataModels;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repository
{
    public class Repository : IRepository
    {
        private DataContext _db;
        public Repository(DataContext db)
        {
            _db = db;
        }


        public async Task<IEnumerable<Course>> GetCourses()
        {
            return await _db.Courses.Include(x => x.Students).ToListAsync();
        }

        public async Task<Student> GetStudentById(int id)
        {
            return await _db.Students.Where(s => s.Id == id).FirstOrDefaultAsync()
                ?? throw new ArgumentException($"Student with id: {id} does not exist...");
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await _db.Students.ToListAsync();
        }

        public async Task<Student> UpdateStudent(Student student)
        {
            _db.Entry(student).State = EntityState.Modified;

            await _db.SaveChangesAsync();
            return student;
        }
        public async Task<Student> CreateStudent(Student student)
        {
            Course course = await GetCourseById(student.CourseId);
            student.Course = course;
            course.Students.Add(student);
            await _db.SaveChangesAsync();
            
            _db.Students.Add(student);
            await _db.SaveChangesAsync();
            return student;
        }

        public async Task<Student> DeleteStudent(Student student)
        {
            _db.Students.Remove(student);
            await _db.SaveChangesAsync();
            return student;
        }

        public async Task<Course> CreateCourse(Course course)
        {
            _db.Courses.Add(course);
            await _db.SaveChangesAsync();
            return course;
        }

        public async Task<Course> GetCourseById(int id)
        {
            return await _db.Courses.Where(s => s.Id == id).FirstOrDefaultAsync()
             ?? throw new ArgumentException($"Curse with id: {id} does not exist...");
        }

        public async Task<Course> UpdateCourse(Course course)
        {
            _db.Entry(course).State = EntityState.Modified;

            await _db.SaveChangesAsync();
            return course;
        }

        public async Task<Course> DeleteCourse(Course course)
        {
           _db.Courses.Remove(course);
           await _db.SaveChangesAsync();
            return course;
        }
    }
}
