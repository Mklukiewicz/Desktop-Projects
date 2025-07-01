using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Core.Models;
using ToDoApp.DB.Models;

namespace ToDoApp.DB.Mappers
{
    public static class PointsHistoryMapper
    {
        public static PointsHistoryDbModel ToDb(PointsHistory src) => new()
        {
            Id = src.Id,          // int lub Guid – zależnie od modelu
            Date = src.Date,
            Points = src.Points,
            TaskItemId = src.TaskItemId
        };

        public static PointsHistory ToDomain(PointsHistoryDbModel db) => new()
        {
            Id = db.Id,
            Date = db.Date,
            Points = db.Points,
            TaskItemId = db.TaskItemId
        };
    }
}
