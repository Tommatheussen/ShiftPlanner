using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using ShiftPlanner.Shared;

namespace ShiftPlanner.Server.Models
{
    public class Shift
    {
        [Key]
        public Guid Id { get; set; }

        public string ShiftName { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public ShiftType ShiftType { get; set; }

        public Shift(ShiftDefinition shiftDefinition)
        {
            Id = shiftDefinition.ShiftId;
            ShiftName = shiftDefinition.ShiftName;
            StartTime = shiftDefinition.StartTime;
            EndTime = shiftDefinition.EndTime;
            ShiftType = shiftDefinition.ShiftType;
        }

        public Shift(NewShift newShift)
        {
            Id = Guid.NewGuid();
            ShiftName = newShift.ShiftName;
            StartTime = newShift.StartTime;
            EndTime = newShift.EndTime;
            ShiftType = newShift.ShiftType;
        }

        public Shift() {}
    }
}

