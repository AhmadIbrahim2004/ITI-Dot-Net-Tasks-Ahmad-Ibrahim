using Project.Data;
using Project.Models;
using Project.Repositories.Interfaces;

namespace Project.Repositories.Implementations
{
    // SRP: This class is solely responsible for data operations related to the Department entity.
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Department> GetAll() => _context.Departments.ToList();

        public Department? GetById(int id) => _context.Departments.Find(id);

        public void Add(Department entity) => _context.Departments.Add(entity);

        public void Update(Department entity) => _context.Departments.Update(entity);

        public void Delete(Department entity) => _context.Departments.Remove(entity);

        public void Save() => _context.SaveChanges();
    }
}
