using System;
using System.ComponentModel.DataAnnotations;
using ShiftPlanner.Shared;

namespace ShiftPlanner.Server.Models
{
    public class ExistingCalendarEvent : CalendarEvent
    {
        [Required]
        public string Id { get; set; }
    }
}