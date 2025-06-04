using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Core.Models;

namespace ToDoApp.Core.Services.Interfaces
{
    public interface ITaskService : IDisposable
    {
        public TaskItem? GetById(int id);
        public IEnumerable<TaskItem> GetAll();
        public void Add(TaskItem task);
        public void Delete();
        public void Update(TaskItem newTask);
        public void CheckMarkTask(TaskItem newTask);

    }
}
