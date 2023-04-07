using System;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using ShiftPlanner.Shared;

namespace ShiftPlanner.Client.Services
{
    public interface IEventService
    {
        public Task<IEnumerable<CalendarEvent>> GetEventsForMonth(int month, int year);
        public Task UpdateEventsForEvents(int month, int year, IEnumerable<CalendarEvent> eventList);
    }
}