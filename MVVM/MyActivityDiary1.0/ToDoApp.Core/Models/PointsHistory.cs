using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Core.Models
{
    public class PointsHistory
    {
        public int Id { get; set; }              // Unikalny identyfikator
        public DateTime Date { get; set; }        // Data zdobycia punktów
        public int Points { get; set; }           // Liczba zdobytych punktów
        public int TaskItemId { get; set; }      // Id zadania, które wygenerowało punkty
    }
}
