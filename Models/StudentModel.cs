using System.ComponentModel.DataAnnotations;

namespace student_study_planner.Models;

public class StudentModel
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? FirstName { get; set; }

    [Required] 
    public string? LastName { get; set; }
    
    public CoursesModel? Course { get; set; }
    public int? CourseId { get; set;  }
}