using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using student_study_planner.Models;

namespace student_study_planner.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly StudentDbContext _context;
    public HomeController(StudentDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var students = await _context.StudentModel.Include(s => s.Course).ToListAsync();
        if (students == null || !students.Any())
        {
            // Handle the case where there are no students
            ViewBag.CourseCounts = new List<dynamic>(); // Empty list
        }
        else
        {
            var courseCounts = students.GroupBy(s => s.Course?.CourseName ?? "No Course Assigned")
                                       .Select(g => new { CourseName = g.Key, Count = g.Count() })
                                       .ToList();
            ViewBag.CourseCounts = courseCounts;
        }

        return View(students);
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}