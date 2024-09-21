using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using student_study_planner.Models;

namespace student_study_planner.Controllers
{
    public class StudentModelsController : Controller
    {
        private readonly StudentDbContext _context;

        public StudentModelsController(StudentDbContext context)
        {
            _context = context;
        }

        // GET: StudentModels
        public async Task<IActionResult> Index()
        {
            var studentDbContext = _context.StudentModel.Include(s => s.Course);
            return View(await studentDbContext.ToListAsync());
        }

        // GET: StudentModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentModel = await _context.StudentModel
                .Include(s => s.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentModel == null)
            {
                return NotFound();
            }

            return View(studentModel);
        }

        // GET: StudentModels/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.CoursesModel, "Id", "CourseName");
            return View();
        }

        // POST: StudentModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,CourseId")] StudentModel studentModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.CoursesModel, "Id", "CourseName", studentModel.CourseId);
            return View(studentModel);
        }

        // GET: StudentModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentModel = await _context.StudentModel.FindAsync(id);
            if (studentModel == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.CoursesModel, "Id", "CourseName", studentModel.CourseId);
            return View(studentModel);
        }

        // POST: StudentModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,CourseId")] StudentModel studentModel)
        {
            if (id != studentModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentModelExists(studentModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.CoursesModel, "Id", "CourseName", studentModel.CourseId);
            return View(studentModel);
        }

        // GET: StudentModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentModel = await _context.StudentModel
                .Include(s => s.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentModel == null)
            {
                return NotFound();
            }

            return View(studentModel);
        }

        // POST: StudentModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentModel = await _context.StudentModel.FindAsync(id);
            if (studentModel != null)
            {
                _context.StudentModel.Remove(studentModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentModelExists(int id)
        {
            return _context.StudentModel.Any(e => e.Id == id);
        }
    }
}
