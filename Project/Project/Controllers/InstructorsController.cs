using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Models;
using Project.Repositories.Interfaces;
using X.PagedList;
using X.PagedList.Extensions;

namespace Project.Controllers
{
    // Only users in the "Admin" or "HR" roles can access this controller.
    [Authorize(Roles = "Admin,HR")]
    public class InstructorsController : Controller
    {
        private readonly IInstructorRepository _instructorRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public InstructorsController(IInstructorRepository instructorRepository, IDepartmentRepository departmentRepository)
        {
            _instructorRepository = instructorRepository;
            _departmentRepository = departmentRepository;
        }

        // GET: Instructors
        public IActionResult Index(string searchString, string sortOrder, int? page)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DeptSortParm"] = sortOrder == "Dept" ? "dept_desc" : "Dept";

            var instructors = _instructorRepository.GetAll(); // Includes Department data

            // Filtering
            if (!string.IsNullOrEmpty(searchString))
            {
                instructors = instructors.Where(i =>
                    i.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    (i.Department != null && i.Department.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)));
            }

            // Sorting
            switch (sortOrder)
            {
                case "name_desc":
                    instructors = instructors.OrderByDescending(i => i.Name);
                    break;
                case "Dept":
                    instructors = instructors.OrderBy(i => i.Department.Name);
                    break;
                case "dept_desc":
                    instructors = instructors.OrderByDescending(i => i.Department.Name);
                    break;
                default:
                    instructors = instructors.OrderBy(i => i.Name);
                    break;
            }

            // Pagination
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(instructors.ToPagedList(pageNumber, pageSize));
        }

        // Details, Create, Edit, Delete actions follow the same pattern as DepartmentsController...
        // ... ensuring to populate ViewData["DeptId"] for Create/Edit dropdowns.

        // GET: Instructors/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();
            var instructor = _instructorRepository.GetById(id.Value);
            if (instructor == null) return NotFound();
            return View(instructor);
        }

        // GET: Instructors/Create
        public IActionResult Create()
        {
            ViewData["DeptId"] = new SelectList(_departmentRepository.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Salary,Address,Image,DeptId")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                _instructorRepository.Add(instructor);
                _instructorRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeptId"] = new SelectList(_departmentRepository.GetAll(), "Id", "Name", instructor.DeptId);
            return View(instructor);
        }

        // GET: Instructors/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var instructor = _instructorRepository.GetById(id.Value);
            if (instructor == null) return NotFound();
            ViewData["DeptId"] = new SelectList(_departmentRepository.GetAll(), "Id", "Name", instructor.DeptId);
            return View(instructor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Salary,Address,Image,DeptId")] Instructor instructor)
        {
            if (id != instructor.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _instructorRepository.Update(instructor);
                _instructorRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeptId"] = new SelectList(_departmentRepository.GetAll(), "Id", "Name", instructor.DeptId);
            return View(instructor);
        }

        // GET: Instructors/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var instructor = _instructorRepository.GetById(id.Value);
            if (instructor == null) return NotFound();
            return View(instructor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var instructor = _instructorRepository.GetById(id);
            if (instructor != null)
            {
                _instructorRepository.Delete(instructor);
                _instructorRepository.Save();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

