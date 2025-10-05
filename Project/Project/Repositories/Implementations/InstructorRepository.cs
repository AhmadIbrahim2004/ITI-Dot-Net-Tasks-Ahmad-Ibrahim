using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using Project.Repositories.Interfaces;

namespace Project.Repositories.Implementations
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly ApplicationDbContext _context;

        public InstructorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Instructor> GetAll() => _context.Instructors.Include(i => i.Department).ToList();

        public Instructor? GetById(int id) => _context.Instructors.Include(i => i.Department).FirstOrDefault(i => i.Id == id);

        public void Add(Instructor entity) => _context.Instructors.Add(entity);

        public void Update(Instructor entity) => _context.Instructors.Update(entity);

        public void Delete(Instructor entity) => _context.Instructors.Remove(entity);

        public void Save() => _context.SaveChanges();
    }
}
