using System;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using ShiftPlanner.Shared;

using static System.Net.WebRequestMethods;
using System.Net.Http.Json;

namespace ShiftPlanner.Client.Services
{
    public class EventService : IEventService
    {
        private readonly HttpClient _http;

        public EventService(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<CalendarEvent>> GetEventsForMonth(int month, int year)
        {
            var events = await _http.GetFromJsonAsync<IEnumerable<CalendarEvent>>($"api/event?month={month}&year={year}");

            return events;
        }

        public async Task UpdateEventsForEvents(int month, int year, IEnumerable<CalendarEvent> eventList)
        {
            var events = await _http.PutAsJsonAsync<UpdateEvents>("api/event", new UpdateEvents
            {
                Events = eventList,
                Month = month,
                Year = year
            });
        }
    }
}