using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Repositories.Interfaces;
using X.PagedList;
using X.PagedList.Extensions;

namespace Project.Controllers
{
    // Only users in the "Admin" role can access this controller.
    [Authorize(Roles = "Admin")]
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        // GET: Departments
        public IActionResult Index(string searchString, string sortOrder, int? page)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["ManagerSortParm"] = sortOrder == "Manager" ? "manager_desc" : "Manager";

            var departments = _departmentRepository.GetAll();

            // Filtering
            if (!string.IsNullOrEmpty(searchString))
            {
                departments = departments.Where(d =>
                    d.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    d.ManagerName.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            // Sorting
            switch (sortOrder)
            {
                case "name_desc":
                    departments = departments.OrderByDescending(d => d.Name);
                    break;
                case "Manager":
                    departments = departments.OrderBy(d => d.ManagerName);
                    break;
                case "manager_desc":
                    departments = departments.OrderByDescending(d => d.ManagerName);
                    break;
                default:
                    departments = departments.OrderBy(d => d.Name);
                    break;
            }

            // Pagination
            int pageSize = 5; // Show 5 items per page
            int pageNumber = (page ?? 1);
            return View(departments.ToPagedList(pageNumber, pageSize));
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

