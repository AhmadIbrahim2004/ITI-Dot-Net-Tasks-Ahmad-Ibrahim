using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Models;
using Project.Repositories.Interfaces;
using System.Text.Json;
using X.PagedList;
using X.PagedList.Extensions;

namespace Project.Controllers
{
    // Allows any authenticated user to access this controller.
    // Specific actions have stricter role requirements.
    [Authorize]
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

        // GET: Courses - Accessible to any logged-in user.
        public IActionResult Index(string searchString, int? page)
        {
            ViewData["CurrentFilter"] = searchString;
            var courses = _courseRepository.GetAll().AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                courses = courses.Where(c => c.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(courses.OrderBy(c => c.Name).ToPagedList(pageNumber, pageSize));
        }

        // Adds a course ID to the list of joined courses in the session.
        public IActionResult Join(int id)
        {
            List<int> joinedCourseIds = HttpContext.Session.Get<List<int>>("JoinedCourses") ?? new List<int>();

            if (!joinedCourseIds.Contains(id))
            {
                joinedCourseIds.Add(id);
            }

            HttpContext.Session.Set("JoinedCourses", joinedCourseIds);
            return RedirectToAction(nameof(Index));
        }

        // Removes a course ID from the list of joined courses in the session.
        public IActionResult Leave(int id)
        {
            List<int> joinedCourseIds = HttpContext.Session.Get<List<int>>("JoinedCourses") ?? new List<int>();

            if (joinedCourseIds.Contains(id))
            {
                joinedCourseIds.Remove(id);
            }

            HttpContext.Session.Set("JoinedCourses", joinedCourseIds);
            return RedirectToAction(nameof(Index));
        }

        // GET: Courses/Details/5 - Restricted to Admin and Instructor roles.
        [Authorize(Roles = "Admin,Instructor")]
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();
            var course = _courseRepository.GetById(id.Value);
            if (course == null) return NotFound();
            return View(course);
        }

        // GET: Courses/Create - Restricted to Admin and Instructor roles.
        [Authorize(Roles = "Admin,Instructor")]
        public IActionResult Create()
        {
            ViewData["DeptId"] = new SelectList(_departmentRepository.GetAll(), "Id", "Name");
            ViewData["InstructorId"] = new SelectList(_instructorRepository.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Instructor")]
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

        // GET: Courses/Edit/5 - Restricted to Admin and Instructor roles.
        [Authorize(Roles = "Admin,Instructor")]
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
        [Authorize(Roles = "Admin,Instructor")]
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

        // GET: Courses/Delete/5 - Restricted to Admin and Instructor roles.
        [Authorize(Roles = "Admin,Instructor")]
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var course = _courseRepository.GetById(id.Value);
            if (course == null) return NotFound();
            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Instructor")]
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

    // Helper static class to allow saving and retrieving complex objects (like a List) from the session.
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T? Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}

