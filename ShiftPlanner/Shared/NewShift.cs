using System;
using System.ComponentModel.DataAnnotations;

namespace ShiftPlanner.Shared
{
    public class NewShift
    {
        [Required]
        public string ShiftName { get; set; }

        [Required]
        public TimeOnly StartTime { get; set; }

        [Required]
        public TimeOnly EndTime { get; set; }

        [Required]
        public ShiftType ShiftType { get; set; }
    }
}