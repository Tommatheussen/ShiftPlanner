using System;
using Microsoft.EntityFrameworkCore;

namespace ShiftPlanner.Server.Models
{
    public class ShiftPlannerContext : DbContext
    {
        public DbSet<Shift> Shifts { get; set; }

        public string DbPath { get; private set; }

        public ShiftPlannerContext()
        {
            DbPath = $"./shiftplanner.db";
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}