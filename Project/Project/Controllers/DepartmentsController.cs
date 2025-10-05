using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Repositories.Interfaces;

namespace Project.Controllers
{
    public class DepartmentsController : Controller
    {
        // Dependency Injection: Injecting the repository interface, not a concrete class or DbContext.
        // This makes the controller testable and decoupled from the data access logic.
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        // GET: Departments
        public IActionResult Index(string searchString)
        {
            var departments = _departmentRepository.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                departments = departments.Where(d =>
                    d.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    d.ManagerName.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }
            return View(departments.ToList());
        }

        // GET: Departments/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();
            var department = _departmentRepository.GetById(id.Value);
            if (department == null) return NotFound();
            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,ManagerName")] Department department)
        {
            if (ModelState.IsValid)
            {
                _departmentRepository.Add(department);
                _departmentRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Departments/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var department = _departmentRepository.GetById(id.Value);
            if (department == null) return NotFound();
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,ManagerName")] Department department)
        {
            if (id != department.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _departmentRepository.Update(department);
                _departmentRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Departments/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var department = _departmentRepository.GetById(id.Value);
            if (department == null) return NotFound();
            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var department = _departmentRepository.GetById(id);
            if (department != null)
            {
                _departmentRepository.Delete(department);
                _departmentRepository.Save();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

