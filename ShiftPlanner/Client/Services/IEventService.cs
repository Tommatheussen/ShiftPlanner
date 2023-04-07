using System;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using ShiftPlanner.Shared;
using static System.Net.WebRequestMethods;

namespace ShiftPlanner.Client.Services
{
    public interface IEventService
    {
        public Task<IEnumerable<CalendarEvent>> GetEventsForMonth(int month, int year);
        public Task UpdateEventsForEvents(int month, int year, IEnumerable<CalendarEvent> eventList);
    }
}