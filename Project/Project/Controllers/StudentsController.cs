using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Repositories.Interfaces;
using X.PagedList;
using X.PagedList.Extensions;

namespace Project.Controllers
{
    [Authorize(Roles = "Admin,HR,Instructor")]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentsController(
            IStudentRepository studentRepository,
            IDepartmentRepository departmentRepository,
            UserManager<ApplicationUser> userManager)
        {
            _studentRepository = studentRepository;
            _departmentRepository = departmentRepository;
            _userManager = userManager;
        }

        public IActionResult Index(string searchString, string sortOrder, int? page)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DeptSortParm"] = sortOrder == "Dept" ? "dept_desc" : "Dept";

            // --- THIS IS THE CRUCIAL FIX ---
            // We convert the IEnumerable to an IQueryable so that ToPagedList() can find it.
            var students = _studentRepository.GetAll().AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(s =>
                    s.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    (s.Department != null && s.Department.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.Name);
                    break;
                case "Dept":
                    students = students.OrderBy(s => s.Department.Name);
                    break;
                case "dept_desc":
                    students = students.OrderByDescending(s => s.Department.Name);
                    break;
                default:
                    students = students.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));
        }

        // ... all other actions remain the same ...

        public async Task<IActionResult> Create()
        {
            var assignedUserIds = _studentRepository.GetAll().Select(s => s.ApplicationUserId).ToList();
            var availableUsers = await _userManager.Users
                .Where(u => !assignedUserIds.Contains(u.Id))
                .ToListAsync();

            ViewData["ApplicationUserId"] = new SelectList(availableUsers, "Id", "UserName");
            ViewData["DeptId"] = new SelectList(_departmentRepository.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Image,Address,Grade,DeptId,ApplicationUserId")] Student student)
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

        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();
            var student = _studentRepository.GetById(id.Value);
            if (student == null) return NotFound();
            return View(student);
        }

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
        public IActionResult Edit(int id, [Bind("Id,Name,Image,Address,Grade,DeptId,ApplicationUserId")] Student student)
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

