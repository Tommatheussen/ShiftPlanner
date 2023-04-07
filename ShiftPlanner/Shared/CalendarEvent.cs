using System;
using System.ComponentModel.DataAnnotations;

namespace ShiftPlanner.Shared
{
    public class CalendarEvent
    {
        [Required]
        public string ShiftName { get; set; }

        [Required]
        public Guid ShiftId { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public TimeOnly StartTime { get; set; }

        [Required]
        public TimeOnly EndTime { get; set; }
    }
}