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
    public class PointsHistoryRepository : IPointsHistoryRepository
    {
        private readonly AppDbContext _ctx;
        public PointsHistoryRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task AddAsync(PointsHistoryDbModel entry)
        {
            _ctx.PointsHistory.Add(entry);
            await _ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<PointsHistoryDbModel>> GetAllAsync()
            => await _ctx.PointsHistory.AsNoTracking().ToListAsync();

        public async Task<IEnumerable<PointsHistoryDbModel>> GetAllPointsHistoryAsync()
        {
            return await _ctx.PointsHistory
                                 .AsNoTracking()
                                 .ToListAsync();
        }
    }
}
