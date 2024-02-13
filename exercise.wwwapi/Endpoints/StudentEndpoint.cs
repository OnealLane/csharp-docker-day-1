using exercise.wwwapi.DataModels;
using exercise.wwwapi.DataTransferObjects;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace exercise.wwwapi.Endpoints
{
    /// <summary>
    /// Core Endpoint
    /// </summary>
    public static class StudentEndpoint
    {
        public static void StudentEndpointConfiguration(this WebApplication app)
        {
            var students = app.MapGroup("students");
            students.MapGet("/get", GetStudents);
            students.MapPost("/post", CreateStudent);
            students.MapPut("/update/{id}", UpdateStudent); 
            students.MapDelete("/delete/{id}", DeleteStudent);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetStudents(IRepository repository)
        {
            var results = await repository.GetStudents();
            var studentsDTO = new List<GetStudentsDTO>();

            foreach (var student in results)
            {
                studentsDTO.Add(new GetStudentsDTO()
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    AverageGrade = student.AverageGrade,
                    Dob = student.Dob,
                    Course = new CourseForStudentDTO()
                    {
                        Title = repository.GetCourseById(student.CourseId).Result.Title,
                        StartDate = repository.GetCourseById(student.CourseId).Result.StartDate
                    }
                });
            }
            return TypedResults.Ok(studentsDTO);
        }


        public static async Task<IResult> CreateStudent(IRepository repository, Payload<CreateStudentDTO> payload)
        {
            var student = new Student()
            {
                FirstName = payload.data.FirstName,
                LastName = payload.data.LastName,
                Dob = payload.data.Dob,
                CourseId = payload.data.CourseId,
                AverageGrade = payload.data.AverageGrade
            };

            await repository.CreateStudent(student);
            return TypedResults.Ok(payload);
        }


        public static async Task<IResult> UpdateStudent(IRepository repository, Payload<UpdateStudentDTO> payload, int id)
        {
            var student = await repository.GetStudentById(id);

            student.FirstName = string.IsNullOrEmpty(payload.data.FirstName) ? student.FirstName : payload.data.FirstName;
            student.LastName = string.IsNullOrEmpty(payload.data.LastName) ? student.LastName : payload.data.LastName;
            student.Dob = payload.data.Dob ?? student.Dob;

            await repository.UpdateStudent(student);
            return TypedResults.Ok(payload);

        }

        public static async Task<IResult> DeleteStudent(IRepository repository, int id)
        {
            var student = await repository.GetStudentById(id);
            await repository.DeleteStudent(student);
            return TypedResults.Ok(student);
        }
    }


}
