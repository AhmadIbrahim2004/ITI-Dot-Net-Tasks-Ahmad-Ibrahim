using Project.Models;

namespace Project.Repositories.Interfaces
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        // You can add department-specific methods here later
        // e.g., IEnumerable<Department> GetDepartmentsWithMostStudents();
    }
}
