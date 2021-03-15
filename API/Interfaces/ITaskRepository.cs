using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface ITaskRepository
    {
        void Update(TodoTask task);
        void Insert(TodoTask task);
        Task<IEnumerable<TodoTask>> GetTasksAsync();
        Task<TodoTask> GetTaskByIdAsync(int id);
        void DeleteTask(int id);
        void Save();
    }
}