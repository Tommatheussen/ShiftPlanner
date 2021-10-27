using System;
using ShiftPlanner.Shared;

namespace ShiftPlanner.Client.Services
{
    public class ShiftService : IShiftService
    {
        public Task<IEnumerable<ShiftDefinition>> GetShifts()
        {
            var shifts = new List<ShiftDefinition>()
            {
                new ShiftDefinition()
                {
                    ShiftName = "L'",
                    StartTime = "15:30",
                    EndTime = "21:00"
                },
                new ShiftDefinition()
                {
                    ShiftName = "V'",
                    StartTime = "07:00",
                    EndTime = "12:00"
                }
            };
            return Task.FromResult<IEnumerable<ShiftDefinition>>(shifts);
        }

        public Task<IEnumerable<CalendarEvent>> GetCalendarEvents()
        {
            var calendarEvents = new List<CalendarEvent>()
            {
                new CalendarEvent
                {
                    Id = Guid.NewGuid().ToString("N"),
                    ShiftName = "L'",
                    StartTime = new DateTime(2021, 10, 14, 13, 30, 00),
                    EndTime = new DateTime(2021, 10, 14, 21, 00, 00),
                }
            };
            return Task.FromResult<IEnumerable<CalendarEvent>>(calendarEvents);
        }
    }
}
