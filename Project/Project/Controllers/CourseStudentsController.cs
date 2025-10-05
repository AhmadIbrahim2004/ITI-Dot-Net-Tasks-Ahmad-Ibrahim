using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Models;
using Project.Repositories.Interfaces;

namespace Project.Controllers
{
    public class CourseStudentsController : Controller
    {
        private readonly ICourseStudentRepository _courseStudentRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;

        public CourseStudentsController(ICourseStudentRepository courseStudentRepository, IStudentRepository studentRepository, ICourseRepository courseRepository)
        {
            _courseStudentRepository = courseStudentRepository;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
        }

        // GET: CourseStudents
        public IActionResult Index(string searchString)
        {
            var enrollments = _courseStudentRepository.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                enrollments = enrollments.Where(cs =>
                    (cs.Student != null && cs.Student.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)) ||
                    (cs.Course != null && cs.Course.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)));
            }
            return View(enrollments.ToList());
        }

        // Note: Details/Edit/Delete for composite keys require a more specific approach
        // than what the generic repository provides. For this task, we will create them
        // but a real-world scenario might add specific methods to the repository.

        // GET: CourseStudents/Details?crsId=1&stdId=1
        public IActionResult Details(int? crsId, int? stdId)
        {
            if (crsId == null || stdId == null) return NotFound();
            // Find the specific enrollment
            var enrollment = _courseStudentRepository.GetAll().FirstOrDefault(cs => cs.CrsId == crsId && cs.StdId == stdId);
            if (enrollment == null) return NotFound();
            return View(enrollment);
        }


        // GET: CourseStudents/Create
        public IActionResult Create()
        {
            ViewData["CrsId"] = new SelectList(_courseRepository.GetAll(), "Id", "Name");
            ViewData["StdId"] = new SelectList(_studentRepository.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Degree,CrsId,StdId")] CourseStudent courseStudent)
        {
            // The Id property for CourseStudent is not a key, so we can ignore it on create.
            // ModelState might be invalid if it tries to bind a composite key in a simple way.
            // For this task, we'll proceed if the foreign keys are valid.
            if (courseStudent.CrsId > 0 && courseStudent.StdId > 0)
            {
                _courseStudentRepository.Add(courseStudent);
                _courseStudentRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CrsId"] = new SelectList(_courseRepository.GetAll(), "Id", "Name", courseStudent.CrsId);
            ViewData["StdId"] = new SelectList(_studentRepository.GetAll(), "Id", "Name", courseStudent.StdId);
            return View(courseStudent);
        }

        // GET: CourseStudents/Edit?crsId=1&stdId=1
        public IActionResult Edit(int? crsId, int? stdId)
        {
            if (crsId == null || stdId == null) return NotFound();
            var enrollment = _courseStudentRepository.GetAll().FirstOrDefault(cs => cs.CrsId == crsId && cs.StdId == stdId);
            if (enrollment == null) return NotFound();
            // We only allow editing the degree, so no dropdowns are needed here.
            return View(enrollment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int crsId, int stdId, [Bind("Id,Degree,CrsId,StdId")] CourseStudent courseStudent)
        {
            if (crsId != courseStudent.CrsId || stdId != courseStudent.StdId) return NotFound();

            if (ModelState.IsValid)
            {
                _courseStudentRepository.Update(courseStudent);
                _courseStudentRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(courseStudent);
        }


        // GET: CourseStudents/Delete?crsId=1&stdId=1
        public IActionResult Delete(int? crsId, int? stdId)
        {
            if (crsId == null || stdId == null) return NotFound();
            var enrollment = _courseStudentRepository.GetAll().FirstOrDefault(cs => cs.CrsId == crsId && cs.StdId == stdId);
            if (enrollment == null) return NotFound();
            return View(enrollment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int crsId, int stdId)
        {
            var enrollment = _courseStudentRepository.GetAll().FirstOrDefault(cs => cs.CrsId == crsId && cs.StdId == stdId);
            if (enrollment != null)
            {
                _courseStudentRepository.Delete(enrollment);
                _courseStudentRepository.Save();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

