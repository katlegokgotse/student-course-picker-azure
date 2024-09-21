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
    public class CoursesModelsController : Controller
    {
        private readonly StudentDbContext _context;

        public CoursesModelsController(StudentDbContext context)
        {
            _context = context;
        }

        // GET: CoursesModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.CoursesModel.ToListAsync());
        }

        // GET: CoursesModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coursesModel = await _context.CoursesModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coursesModel == null)
            {
                return NotFound();
            }

            return View(coursesModel);
        }

        // GET: CoursesModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CoursesModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseName,CourseCredits")] CoursesModel coursesModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coursesModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(coursesModel);
        }

        // GET: CoursesModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coursesModel = await _context.CoursesModel.FindAsync(id);
            if (coursesModel == null)
            {
                return NotFound();
            }
            return View(coursesModel);
        }

        // POST: CoursesModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseName,CourseCredits")] CoursesModel coursesModel)
        {
            if (id != coursesModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coursesModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoursesModelExists(coursesModel.Id))
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
            return View(coursesModel);
        }

        // GET: CoursesModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coursesModel = await _context.CoursesModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coursesModel == null)
            {
                return NotFound();
            }

            return View(coursesModel);
        }

        // POST: CoursesModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coursesModel = await _context.CoursesModel.FindAsync(id);
            if (coursesModel != null)
            {
                _context.CoursesModel.Remove(coursesModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoursesModelExists(int id)
        {
            return _context.CoursesModel.Any(e => e.Id == id);
        }
    }
}
