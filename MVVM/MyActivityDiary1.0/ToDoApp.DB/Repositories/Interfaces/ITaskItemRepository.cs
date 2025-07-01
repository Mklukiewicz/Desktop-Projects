using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.DB.Models;

namespace ToDoApp.DB.Repositories.Interfaces
{
    public interface ITaskItemRepository
    {
        Task<IEnumerable<TaskItemDbModel>> GetAllAsync();
        Task<IEnumerable<TaskItemDbModel>> GetFinishedAsync();
        Task<IEnumerable<TaskItemDbModel>> GetOngoingAsync();
        Task<TaskItemDbModel?> GetByIdAsync(int id);
        Task AddAsync(TaskItemDbModel task);
        Task UpdateAsync(TaskItemDbModel task);
        Task DeleteAsync(int id);
    }
}
