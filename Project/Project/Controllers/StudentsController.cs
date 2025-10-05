using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Filters; // Import the filter namespace
using Project.Models;
using Project.Repositories.Interfaces;

namespace Project.Controllers
{
    // Applying the custom filter to ensure a user is "logged in" to access student pages.
    [AuthorizeStudentFilter]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IDepartmentRepository _departmentRepository; // Needed for the department dropdown

        public StudentsController(IStudentRepository studentRepository, IDepartmentRepository departmentRepository)
        {
            _studentRepository = studentRepository;
            _departmentRepository = departmentRepository;
        }

        // GET: Students
        public IActionResult Index(string searchString)
        {
            var students = _studentRepository.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(s =>
                    s.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    (s.Department != null && s.Department.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)));
            }
            return View(students.ToList());
        }

        // GET: Students/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();
            var student = _studentRepository.GetById(id.Value);
            if (student == null) return NotFound();
            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            // Provide a list of departments for the dropdown
            ViewData["DeptId"] = new SelectList(_departmentRepository.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Image,Address,Grade,DeptId")] Student student)
        {
            if (ModelState.IsValid)
            {
                _studentRepository.Add(student);
                _studentRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeptId"] = new SelectList(_departmentRepository.GetAll(), "Id", "Name", student.DeptId);
            return View(student);
        }

        // GET: Students/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var student = _studentRepository.GetById(id.Value);
            if (student == null) return NotFound();
            ViewData["DeptId"] = new SelectList(_departmentRepository.GetAll(), "Id", "Name", student.DeptId);
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Image,Address,Grade,DeptId")] Student student)
        {
            if (id != student.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _studentRepository.Update(student);
                _studentRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeptId"] = new SelectList(_departmentRepository.GetAll(), "Id", "Name", student.DeptId);
            return View(student);
        }

        // GET: Students/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var student = _studentRepository.GetById(id.Value);
            if (student == null) return NotFound();
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = _studentRepository.GetById(id);
            if (student != null)
            {
                _studentRepository.Delete(student);
                _studentRepository.Save();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

