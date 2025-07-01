using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.DB.Models;

namespace ToDoApp.DB.Repositories.Interfaces
{
    public interface IPointsHistoryRepository
    {
        Task AddAsync(PointsHistoryDbModel entry);
        Task<IEnumerable<PointsHistoryDbModel>> GetAllAsync();
        Task<IEnumerable<PointsHistoryDbModel>> GetAllPointsHistoryAsync();
    }
}
