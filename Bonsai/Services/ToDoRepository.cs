using Bonsai.Models;
using SQLite;

namespace Bonsai.Services;

public class ToDoRepository : IRepository<ToDo>
{
    private SQLiteAsyncConnection _connection;

    private async Task InitAsync()
    {
        if (_connection != null)
        {
            return;
        }

        _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "todos.sqlite3"));
        await _connection.CreateTableAsync<ToDo>();
    }
    
    public async Task<ToDo> GetAsync(int id)
    {
        await InitAsync();
        return await _connection.Table<ToDo>().FirstAsync(toDo => toDo.Id == id);
    }

    public async Task<List<ToDo>> GetAllAsync()
    {
        await InitAsync();
        return await _connection.Table<ToDo>().ToListAsync();
    }

    public async Task AddAsync(ToDo toDo)
    {
        await InitAsync();
        await DeleteOutdated();
        await _connection.InsertAsync(toDo);
    }

    public async Task UpdateAsync(ToDo toDo)
    {
        if (toDo.Date < DateTime.Today)
        {
            return;
        }

        await InitAsync();
        await _connection.UpdateAsync(toDo);
    }

    public async Task DeleteAsync(ToDo toDo)
    {
        await InitAsync();
        await _connection.DeleteAsync(toDo);
    }

    private async Task DeleteOutdated()
    {
        await _connection.DeferredQueryAsync<ToDo>("DELETE FROM ToDos WHERE Date < ?", DateTime.Today.AddDays(-30));
    }
}