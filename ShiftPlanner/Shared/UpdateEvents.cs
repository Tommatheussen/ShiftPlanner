using System;
using System.ComponentModel.DataAnnotations;

namespace ShiftPlanner.Shared
{
    public class UpdateEvents
    {
        [Required]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public IEnumerable<CalendarEvent> Events { get; set; }
    }
}

