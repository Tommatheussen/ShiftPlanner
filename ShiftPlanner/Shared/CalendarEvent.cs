using System;
using System.ComponentModel.DataAnnotations;

namespace ShiftPlanner.Shared
{
    public class CalendarEvent
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string ShiftName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        //public bool FullDay => true;

        public string FullTimeSlot => $"{StartTime:HH:mm} - {EndTime:HH:mm}";
    }
}