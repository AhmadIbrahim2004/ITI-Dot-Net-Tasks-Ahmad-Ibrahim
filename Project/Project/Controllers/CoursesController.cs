using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Models;
using Project.Repositories.Interfaces;

namespace Project.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IInstructorRepository _instructorRepository;

        public CoursesController(ICourseRepository courseRepository, IDepartmentRepository departmentRepository, IInstructorRepository instructorRepository)
        {
            _courseRepository = courseRepository;
            _departmentRepository = departmentRepository;
            _instructorRepository = instructorRepository;
        }

        // GET: Courses
        public IActionResult Index(string searchString)
        {
            var courses = _courseRepository.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                courses = courses.Where(c => c.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }
            return View(courses.ToList());
        }

        // --- Action for State Management (Session) ---
        public IActionResult Join(int id)
        {
            // Store the selected course ID in the session
            HttpContext.Session.SetInt32("SelectedCourseId", id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Courses/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();
            var course = _courseRepository.GetById(id.Value);
            if (course == null) return NotFound();
            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            ViewData["DeptId"] = new SelectList(_departmentRepository.GetAll(), "Id", "Name");
            ViewData["InstructorId"] = new SelectList(_instructorRepository.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Degree,MinimumDegree,Hours,DeptId,InstructorId")] Course course)
        {
            if (ModelState.IsValid)
            {
                _courseRepository.Add(course);
                _courseRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeptId"] = new SelectList(_departmentRepository.GetAll(), "Id", "Name", course.DeptId);
            ViewData["InstructorId"] = new SelectList(_instructorRepository.GetAll(), "Id", "Name", course.InstructorId);
            return View(course);
        }

        // GET: Courses/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var course = _courseRepository.GetById(id.Value);
            if (course == null) return NotFound();
            ViewData["DeptId"] = new SelectList(_departmentRepository.GetAll(), "Id", "Name", course.DeptId);
            ViewData["InstructorId"] = new SelectList(_instructorRepository.GetAll(), "Id", "Name", course.InstructorId);
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Degree,MinimumDegree,Hours,DeptId,InstructorId")] Course course)
        {
            if (id != course.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _courseRepository.Update(course);
                _courseRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeptId"] = new SelectList(_departmentRepository.GetAll(), "Id", "Name", course.DeptId);
            ViewData["InstructorId"] = new SelectList(_instructorRepository.GetAll(), "Id", "Name", course.InstructorId);
            return View(course);
        }

        // GET: Courses/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var course = _courseRepository.GetById(id.Value);
            if (course == null) return NotFound();
            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var course = _courseRepository.GetById(id);
            if (course != null)
            {
                _courseRepository.Delete(course);
                _courseRepository.Save();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

