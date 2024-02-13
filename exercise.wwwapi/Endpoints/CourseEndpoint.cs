using exercise.wwwapi.DataModels;

using exercise.wwwapi.DataTransferObjects;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    /// <summary>
    /// Extension endpoint
    /// </summary>
    public static class CourseEndpoint
    {
        public static void CourseEndpointConfiguration(this WebApplication app)
        {
            var students = app.MapGroup("courses");
            students.MapGet("/get", GetCourses);
            students.MapPost("/post", CreateCourse);
            students.MapPut("/update/{id}", UpdateCourse);
            students.MapDelete("/delete/{id}", DeleteCourse);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetCourses(IRepository repository)
        {
            var results = await repository.GetCourses();
            var courseDtos = new List<GetCourseDTO>();

            foreach (var course in results)
            {
                var courseDto = new GetCourseDTO
                {
                    Id = course.Id,
                    Title = course.Title,
                    StartDate = DateTime.Now,
                };
                foreach (var student in course.Students)
                {
                    courseDto.students.Add(new StudentForCourseDTO
                    {
                        FirstName = student.FirstName,
                        LastName = student.LastName,
                        AverageGrade = student.AverageGrade,
                        Dob = student.Dob
                    });
                }
                courseDtos.Add(courseDto);
            }
            return TypedResults.Ok(courseDtos);
        }



        public static async Task<IResult> CreateCourse(IRepository repository, Payload<CreateCourseDTO> payload)
        {
            Course course = new Course
            {
                StartDate = payload.data.StartDate,
                Title = payload.data.Title

            };
            await repository.CreateCourse(course);
            return TypedResults.Ok(payload);
        }

        public static async Task<IResult> UpdateCourse(IRepository repository, Payload<UpdateCourseDTO> payload, int id)
        {

            var course = await repository.GetCourseById(id);
            course.Title = !string.IsNullOrEmpty(payload.data.Title) ? payload.data.Title : course.Title;
            course.StartDate = payload.data.StartDate ?? course.StartDate;
            await repository.UpdateCourse(course);
            return TypedResults.Ok(payload);
        }

        public static async Task<IResult> DeleteCourse(IRepository repository, int id)
        {
            Course course = await repository.GetCourseById(id);
            CourseDTO returnCourse = new CourseDTO { Id = id, StartDate = course.StartDate, Title = course.Title };
            await repository.DeleteCourse(course);
            return TypedResults.Ok(new Payload<CourseDTO> { data = returnCourse });
        }
    }
}
