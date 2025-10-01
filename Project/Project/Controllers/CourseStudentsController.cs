using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;

namespace Project.Controllers
{
    public class CourseStudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseStudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CourseStudents
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var courseStudents = from cs in _context.CourseStudents.Include(c => c.Course).Include(c => c.Student)
                                 select cs;

            if (!String.IsNullOrEmpty(searchString))
            {
                courseStudents = courseStudents.Where(cs => cs.Student.Name.Contains(searchString)
                                                        || cs.Course.Name.Contains(searchString));
            }

            return View(await courseStudents.AsNoTracking().ToListAsync());
        }

        // GET: CourseStudents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseStudent = await _context.CourseStudents
                .Include(c => c.Course)
                .Include(c => c.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseStudent == null)
            {
                return NotFound();
            }

            return View(courseStudent);
        }

        // GET: CourseStudents/Create
        public IActionResult Create()
        {
            ViewData["CrsId"] = new SelectList(_context.Courses, "Id", "Name");
            ViewData["StdId"] = new SelectList(_context.Students, "Id", "Name");
            return View();
        }

        // POST: CourseStudents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Degree,CrsId,StdId")] CourseStudent courseStudent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseStudent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CrsId"] = new SelectList(_context.Courses, "Id", "Name", courseStudent.CrsId);
            ViewData["StdId"] = new SelectList(_context.Students, "Id", "Name", courseStudent.StdId);
            return View(courseStudent);
        }

        // GET: CourseStudents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseStudent = await _context.CourseStudents.FindAsync(id);
            if (courseStudent == null)
            {
                return NotFound();
            }
            ViewData["CrsId"] = new SelectList(_context.Courses, "Id", "Name", courseStudent.CrsId);
            ViewData["StdId"] = new SelectList(_context.Students, "Id", "Name", courseStudent.StdId);
            return View(courseStudent);
        }

        // POST: CourseStudents/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Degree,CrsId,StdId")] CourseStudent courseStudent)
        {
            if (id != courseStudent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseStudent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseStudentExists(courseStudent.Id))
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
            ViewData["CrsId"] = new SelectList(_context.Courses, "Id", "Name", courseStudent.CrsId);
            ViewData["StdId"] = new SelectList(_context.Students, "Id", "Name", courseStudent.StdId);
            return View(courseStudent);
        }

        // GET: CourseStudents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseStudent = await _context.CourseStudents
                .Include(c => c.Course)
                .Include(c => c.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseStudent == null)
            {
                return NotFound();
            }

            return View(courseStudent);
        }

        // POST: CourseStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseStudent = await _context.CourseStudents.FindAsync(id);
            if (courseStudent != null)
            {
                _context.CourseStudents.Remove(courseStudent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseStudentExists(int id)
        {
            return _context.CourseStudents.Any(e => e.Id == id);
        }
    }
}

