using System;
using ShiftPlanner.Shared;

namespace ShiftPlanner.Client.Services
{
    public interface IShiftService
    {
        Task<IEnumerable<ShiftDefinition>> GetShifts();
        Task<IEnumerable<CalendarEvent>> GetCalendarEvents();
    }
}

