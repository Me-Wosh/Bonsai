namespace Bonsai.Services;

public interface IRepository<T>
{
    Task<T> GetAsync(int id);
    Task<List<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}