namespace Project.Repositories.Interfaces
{
    public interface IRepository<T> : IReadableRepository<T>, IWritableRepository<T> where T : class
    {
    }
}
