using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Core.Models;
using ToDoApp.DB.Models;

namespace ToDoApp.DB
{
    public class AppDbContext : DbContext
    {
        public DbSet<TaskItemDbModel> TaskItems { get; set; } = null!;

        public DbSet<CalendarDay> CalendarDays { get; set; }

        public DbSet<PointsHistoryDbModel> PointsHistory { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            // (opcjonalnie) konfiguracje
            mb.Entity<TaskItemDbModel>()
              .Property(t => t.Title)
              .IsRequired()
            .HasMaxLength(200);

            mb.Entity<PointsHistoryDbModel>()
                .HasOne(p => p.TaskItem)          // nawigacja
                .WithMany()                      // TaskItem nie musi mieć kolekcji
                .HasForeignKey(p => p.TaskItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
