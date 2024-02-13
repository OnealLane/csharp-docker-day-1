using exercise.wwwapi.DataModels;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Data
{
    public class DataContext : DbContext
    {
        private string _connectionString;
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnectionString")!;
            //this.Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Title = "C#", StartDate = DateTime.UtcNow },
                new Course { Id = 2, Title = "Java", StartDate = DateTime.UtcNow }
                );

            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, FirstName = "Oneal", LastName = "Lane", AverageGrade = 3.0, CourseId = 1, Dob = DateTime.UtcNow }
                );
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}
