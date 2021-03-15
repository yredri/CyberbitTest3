using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DataContext _context;
        public TaskRepository(DataContext context)
        {
            _context = context;

        }

        public async Task<IEnumerable<TodoTask>> GetTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public void Insert(TodoTask task)
        {
            _context.Tasks.Add(task); 
        }

        public void Update(TodoTask task)
        {
            _context.Entry(task).State = EntityState.Modified;
        }

        public void Save()        
        {            
            _context.SaveChangesAsync();        
        }

        public async Task<TodoTask> GetTaskByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public void DeleteTask(int id)
        {
            TodoTask task = _context.Tasks.Find(id);                    
            _context.Tasks.Remove(task);    
        }
    }
}