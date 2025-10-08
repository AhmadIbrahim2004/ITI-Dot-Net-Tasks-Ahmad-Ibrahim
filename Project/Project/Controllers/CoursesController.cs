using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Models;
using Project.Repositories.Interfaces;
using X.PagedList;
using X.PagedList.Extensions;

// --- STEP 1: GENERAL AUTHORIZATION ---
// Allow ANY user who is logged in to access this controller's pages.
// Specific actions will have stricter rules.
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

    // This Index action is open to any logged-in user (including Students).
    public IActionResult Index(string searchString, int? page)
    {
        ViewData["CurrentFilter"] = searchString;
        var courses = _courseRepository.GetAll();

        if (!string.IsNullOrEmpty(searchString))
        {
            courses = courses.Where(c => c.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase));
        }

        int pageSize = 5;
        int pageNumber = (page ?? 1);
        return View(courses.OrderBy(c => c.Name).ToPagedList(pageNumber, pageSize));
    }

    // A student can "Join" a course.
    public IActionResult Join(int id)
    {
        HttpContext.Session.SetInt32("SelectedCourseId", id);
        return RedirectToAction(nameof(Index));
    }

    // --- STEP 2: STRICTER AUTHORIZATION FOR SPECIFIC ACTIONS ---

    // Only Admins and Instructors can see the details page.
    [Authorize(Roles = "Admin,Instructor")]
    public IActionResult Details(int? id)
    {
        if (id == null) return NotFound();
        var course = _courseRepository.GetById(id.Value);
        if (course == null) return NotFound();
        return View(course);
    }

    // Only Admins and Instructors can access the Create page.
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

    // Only Admins and Instructors can access the Edit page.
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

    // Only Admins and Instructors can access the Delete page.
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

