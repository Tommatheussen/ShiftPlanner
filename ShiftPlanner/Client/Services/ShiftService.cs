using System;
using System.Net.Http.Json;
using ShiftPlanner.Shared;

namespace ShiftPlanner.Client.Services
{
    public class ShiftService : IShiftService
    {
        private readonly HttpClient _http;

        public ShiftService(HttpClient http)
        {
            _http = http;
        }
        public async Task<IEnumerable<ShiftDefinition>> GetShifts()
        {
            return await _http.GetFromJsonAsync<IEnumerable<ShiftDefinition>>("api/shift");
        }

        public Task<IEnumerable<CalendarEvent>> GetCalendarEvents()
        {
            var calendarEvents = new List<CalendarEvent>()
            {
                new CalendarEvent
                {
                    Id = Guid.NewGuid().ToString("N"),
                    ShiftName = "L'",
                    StartTime = new DateTime(2022, 09, 01, 13, 30, 00),
                    EndTime = new DateTime(2022, 09, 01, 21, 00, 00),
                }
            };
            return Task.FromResult<IEnumerable<CalendarEvent>>(calendarEvents);
        }
    }
}
