using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.DB.Models;
using ToDoApp.DB.Repositories.Interfaces;

namespace ToDoApp.DB.Repositories
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly AppDbContext _context;

        public TaskItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItemDbModel>> GetAllAsync()
        {
            return await _context.TaskItems.ToListAsync();
        }

        public async Task<TaskItemDbModel?> GetByIdAsync(int id)
        {
            return await _context.TaskItems.FindAsync(id);
        }

        public async Task AddAsync(TaskItemDbModel task)
        {
            _context.TaskItems.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TaskItemDbModel task)
        {
            var trackedEntity = _context.ChangeTracker.Entries<TaskItemDbModel>()
                .FirstOrDefault(e => e.Entity.Id == task.Id);

            task.StartDate = EnsureUtc(task.StartDate);
            task.FinishDate = task.FinishDate.HasValue
                              ? EnsureUtc(task.FinishDate.Value)
                              : null;

            if (trackedEntity != null)
            {
                _ = trackedEntity.State = EntityState.Detached;
            }

            _context.TaskItems.Update(task);
            await _context.SaveChangesAsync();
        }

        static DateTime EnsureUtc(DateTime dt)
                => dt.Kind == DateTimeKind.Utc ? dt
                   : DateTime.SpecifyKind(dt, DateTimeKind.Utc);

        public async Task DeleteAsync(int id)
        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task != null)
            {
                _context.TaskItems.Remove(task);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TaskItemDbModel>> GetFinishedAsync()
        {
            return await _context.TaskItems                 
                                 .Where(t => t.IsFinished)  
                                 .AsNoTracking()            
                                 .ToListAsync();
        }

        public async Task<IEnumerable<TaskItemDbModel>> GetOngoingAsync()
        {
            return await _context.TaskItems
                                 .Where(t => !t.IsFinished)  
                                 .AsNoTracking()
                                 .ToListAsync();
        }
    }
}
