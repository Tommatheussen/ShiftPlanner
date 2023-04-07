using System;
using System.ComponentModel.DataAnnotations;

namespace ShiftPlanner.Shared
{
    public class ShiftDefinition
    {
        [Required]
        public Guid ShiftId { get; set; }

        [Required]
        public string ShiftName { get; set; }

        [Required]
        public TimeOnly StartTime { get; set; }

        [Required]
        public TimeOnly EndTime { get; set; }

        public string FullTimeSlot => $"{StartTime:HH:mm} - {EndTime:HH:mm}";

        [Required]
        public ShiftType ShiftType { get; set; }
    }
}

