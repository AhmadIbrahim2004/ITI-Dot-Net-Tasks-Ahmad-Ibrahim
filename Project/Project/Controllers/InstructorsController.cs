using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Models;
using Project.Repositories.Interfaces;

namespace Project.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly IInstructorRepository _instructorRepository;
        private readonly IDepartmentRepository _departmentRepository; // Needed for dropdowns

        public InstructorsController(IInstructorRepository instructorRepository, IDepartmentRepository departmentRepository)
        {
            _instructorRepository = instructorRepository;
            _departmentRepository = departmentRepository;
        }

        // GET: Instructors
        public IActionResult Index(string searchString)
        {
            var instructors = _instructorRepository.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                instructors = instructors.Where(i =>
                    i.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    (i.Department != null && i.Department.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)));
            }
            return View(instructors.ToList());
        }

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

