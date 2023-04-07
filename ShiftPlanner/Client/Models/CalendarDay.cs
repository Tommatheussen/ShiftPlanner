using System;
using ShiftPlanner.Shared;

namespace ShiftPlanner.Client.Models
{
    public class CalendarDay
    {
        public int DayNumber { get; set; }

        public DateOnly Date { get; set; }

        public bool IsEmpty { get; set; }

        public ShiftDefinition? ExistingShift { get; set; }
    }
}

