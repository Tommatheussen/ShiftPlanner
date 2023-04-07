using System;
using System.Collections.Generic;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Requests;
using Google.Apis.Services;
using ShiftPlanner.Server.Models;
using ShiftPlanner.Shared;

namespace ShiftPlanner.Server.Services
{
    public class GoogleService : IGoogleService
    {
        private CalendarService calendarService;
        private GoogleCredential credential;

        public GoogleService()
        {
            credential = GoogleCredential.FromFile("shiftplanner-382814-8a893f652d6e.json")
                .CreateScoped(new[] { CalendarService.Scope.Calendar });

            calendarService = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Books API Sample"
            });
        }

        public async Task<IEnumerable<ExistingCalendarEvent>> GetEventsForMonth(int month, int year)
        {
            var calendarRequest = calendarService.Events.List("18bb572e660eeb33bbddb0f0a6addef4c8b6e2548607f41428b8f50bde74b36d@group.calendar.google.com");

            DateTime minTime = new DateTime(year, month, 1);

            calendarRequest.TimeMin = minTime;
            calendarRequest.TimeMax = minTime.AddMonths(1).AddTicks(-1);

            Events results = await calendarRequest.ExecuteAsync();

            var events = results.Items
                .Select(googleEvent => new ExistingCalendarEvent
                {
                    Id = googleEvent.Id,
                    ShiftName = googleEvent.Summary,
                    Date = DateOnly.FromDateTime(googleEvent.Start.DateTime.GetValueOrDefault()),
                    StartTime = TimeOnly.FromDateTime(googleEvent.Start.DateTime.GetValueOrDefault()),
                    EndTime = TimeOnly.FromDateTime(googleEvent.End.DateTime.GetValueOrDefault()),
                    ShiftId = Guid.Parse(googleEvent.Description)
                });

            return events;
        }

        public async Task UpdateEventsForMonth(IEnumerable<ExistingCalendarEvent> oldEvents, IEnumerable<CalendarEvent> newEvents)
        {
            var request = new BatchRequest(calendarService);

            foreach (var oldEvent in oldEvents)
            {
                var newEvent = newEvents.FirstOrDefault(e => oldEvent.Date == e.Date);

                // The event was deleted
                if (newEvent == null)
                {
                    request.Queue<string>(calendarService.Events.Delete("18bb572e660eeb33bbddb0f0a6addef4c8b6e2548607f41428b8f50bde74b36d@group.calendar.google.com", oldEvent.Id),
                        (content, error, i, message) => { });
                }
                else if (newEvent.ShiftId != oldEvent.ShiftId)
                {
                    request.Queue<Event>(calendarService.Events.Update(new Event
                    {
                        Id = oldEvent.Id,
                        Summary = newEvent.ShiftName,
                        Start = new EventDateTime()
                        {
                            DateTime = newEvent.Date.ToDateTime(newEvent.StartTime)
                        },
                        End = new EventDateTime()
                        {
                            DateTime = newEvent.Date.ToDateTime(newEvent.EndTime)
                        },
                        Description = newEvent.ShiftId.ToString()
                    },
                    "18bb572e660eeb33bbddb0f0a6addef4c8b6e2548607f41428b8f50bde74b36d@group.calendar.google.com", oldEvent.Id
                    ),
                      (content, error, i, message) => { });
                }
            }

            foreach (var newEvent in newEvents)
            {
                var hasOldEvent = oldEvents.Any(e => e.Date == newEvent.Date);

                if (hasOldEvent) continue;

                request.Queue<Event>(calendarService.Events.Insert(new Event
                {
                    Summary = newEvent.ShiftName,
                    Start = new EventDateTime()
                    {
                        DateTime = newEvent.Date.ToDateTime(newEvent.StartTime)
                    },
                    End = new EventDateTime()
                    {
                        DateTime = newEvent.Date.ToDateTime(newEvent.EndTime)
                    },
                    Description = newEvent.ShiftId.ToString()
                },
                    "18bb572e660eeb33bbddb0f0a6addef4c8b6e2548607f41428b8f50bde74b36d@group.calendar.google.com"
                ),
                      (content, error, i, message) => { });
            }

            await request.ExecuteAsync();
        }
    }
}
