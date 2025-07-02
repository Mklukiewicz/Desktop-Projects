using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ToDoApp.Core.Models.TaskItem;

namespace ToDoApp.DB.Models
{
    public class TaskItemDbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsFinished { get; set; }
        public bool TaskProgress { get; set; }
        public int ProgressMaxInt { get; set; }
        public int ProgressCurrentInt { get; set; }
        public string? ProgressString { get; set; }
        public TaskPriority Priority { get; set; }
        public TimeSpan? RemainingTime { get; set; }
    }
}
