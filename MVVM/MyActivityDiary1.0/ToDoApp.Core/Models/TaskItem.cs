using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Core.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public int TaskProgres { get; set; }

        public TaskItem()
        {
            
        }

        public TaskItem(string title, string description, DateTime dueTime, bool isMarkded)
        {
            Title = title;
            Description = description;
            DueDate = dueTime;
            IsCompleted = isMarkded;
        }

        public bool IsOverdue => !IsCompleted && DueDate.Date < DateTime.Now.Date;//metoda sprawdzajaca czy zadanie jest zaległe

        public int SetProgres(int progres)
        {
           return this.TaskProgres = progres;
        }

        public int AddProgress()
        {
            return TaskProgres++;
        }

        public int MinusProgress()
        {
            return TaskProgres--;
        }

    }
}
