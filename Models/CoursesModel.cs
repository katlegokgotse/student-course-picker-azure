using System.ComponentModel.DataAnnotations;

namespace student_study_planner.Models;

public class CoursesModel
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? CourseName { get; set; }
    [Required]
    public int CourseCredits{ get; set; }
}