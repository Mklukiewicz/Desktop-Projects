using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;
using ToDoApp.Core.Models;

namespace ToDoApp.Core.Services
{
    public class InMemoryTaskService
    {
        private readonly List<TaskItem> TaskList = new List<TaskItem>();

        private int _nextid = 0;

        public IEnumerable<TaskItem> GetAll()
        {         
            return TaskList.OrderBy(x => x.Id).ToList();
        }

        public TaskItem? GetById(int id) { return TaskList.FirstOrDefault(x => x.Id == id); }

        public void Add(TaskItem task)// tutaj bedzie trzeba zrobic mapowanie 
        {
            int id = TaskList.LastOrDefault().Id;
            task.Id = ++_nextid;
            TaskList.Add(task);       
        }

        public void Delete()// USUWANIE po checkmarku
        {
            var task = TaskList.FirstOrDefault(x =>x.CheckMarked == true);
            TaskList.Remove(task);
        }

        public void Update(TaskItem newTask)// sprawdz czy task nie jest nullem
        {
            var task = TaskList.FirstOrDefault(x => x.Id == newTask.Id);
            task.TaskProgres = newTask.TaskProgres;
            task.Description = newTask.Description;

        }

        public void CheckMarkTask(TaskItem newTask)// sprawdz czy task nie jest nullem
        {
            var task = TaskList.FirstOrDefault(x => x.Id == newTask.Id);
            task.CheckMarked = true;
        }
    }
}
