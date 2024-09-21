using Microsoft.EntityFrameworkCore;

namespace student_study_planner.Models;

public class StudentDbContext : DbContext
{
    public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
    {
    }
    public DbSet<StudentModel> StudentModel { get; set; }
    public DbSet<CoursesModel> CoursesModel { get; set; }
}