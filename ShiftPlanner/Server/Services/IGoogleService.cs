using System;
using ShiftPlanner.Server.Models;
using ShiftPlanner.Shared;

namespace ShiftPlanner.Server.Services
{
    public interface IGoogleService
    {
        Task<IEnumerable<ExistingCalendarEvent>> GetEventsForMonth(int month, int year);
        Task UpdateEventsForMonth(IEnumerable<ExistingCalendarEvent> oldEvents, IEnumerable<CalendarEvent> newEvents);
    }
}
