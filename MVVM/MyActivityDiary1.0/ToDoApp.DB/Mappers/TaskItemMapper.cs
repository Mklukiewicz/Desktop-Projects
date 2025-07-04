using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Core.Models;
using ToDoApp.DB.Models;

namespace ToDoApp.DB.Mappers
{
    public static class TaskItemMapper
    {
        public static TaskItemDbModel ToDb(this TaskItem src) => new()
        {
            Id = src.Id,
            Title = src.Title,
            Description = src.Description,
            StartDate = DateTime.SpecifyKind(src.StartDate, DateTimeKind.Utc),
            FinishDate = src.FinishDate.HasValue
                        ? DateTime.SpecifyKind(src.FinishDate.Value, DateTimeKind.Utc)
                        : (DateTime?)null,
            IsFinished  = src.IsFinished,
            TaskProgress = src.TaskProgress,
            ProgressMaxInt = src.ProgressMaxInt,
            ProgressCurrentInt = src.ProgressCurrentInt,
            ProgressString = src.ProgressString,
            Priority = src.Priority,
            RemainingTime = src.RemainingTime,
            IsPointsGranted = src.IsPointsGranted,
        };

        public static TaskItem ToDomain(this TaskItemDbModel db) => new(
            title: db.Title,
            description: db.Description ?? "",
            startTime: db.StartDate,
            isMarkded: db.IsCompleted,
            taskPriority: db.Priority)
        {
            Id = db.Id,
            FinishDate = db.FinishDate,
            TaskProgress = db.TaskProgress,
            ProgressMaxInt = db.ProgressMaxInt,
            ProgressCurrentInt = db.ProgressCurrentInt,
            ProgressString = db.ProgressString,
            RemainingTime = db.RemainingTime ?? TimeSpan.Zero,
            IsPointsGranted = db.IsPointsGranted
        };
    }
}
