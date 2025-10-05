using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using Project.Repositories.Interfaces;

namespace Project.Repositories.Implementations
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Student> GetAll() => _context.Students.Include(s => s.Department).ToList();

        public Student? GetById(int id) => _context.Students.Include(s => s.Department).FirstOrDefault(s => s.Id == id);

        public void Add(Student entity) => _context.Students.Add(entity);

        public void Update(Student entity) => _context.Students.Update(entity);

        public void Delete(Student entity) => _context.Students.Remove(entity);

        public void Save() => _context.SaveChanges();
    }
}
