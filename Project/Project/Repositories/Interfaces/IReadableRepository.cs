namespace Project.Repositories.Interfaces
{
    public interface IReadableRepository<T> where T : class
    {
        T? GetById(int id);
        IEnumerable<T> GetAll();
    }
}
