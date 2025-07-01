using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.DB.Models
{
    public class PointsHistoryDbModel
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int Points { get; set; }

        public int TaskItemId { get; set; }

        public TaskItemDbModel TaskItem { get; set; } = null!;
    }
}
