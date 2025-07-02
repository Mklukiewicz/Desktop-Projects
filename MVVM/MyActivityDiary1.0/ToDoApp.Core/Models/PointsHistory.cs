using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Core.Models
{
    public class PointsHistory
    {
        public int Id { get; set; }   
        public DateTime Date { get; set; }
        public int Points { get; set; } 
        public int TaskItemId { get; set; }
    }
}
