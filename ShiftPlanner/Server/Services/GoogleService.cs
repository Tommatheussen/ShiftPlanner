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
            credential = GoogleCredential.FromFile(Constants.GoogleAPIKeysFile)
                .CreateScoped(new[] { CalendarService.Scope.Calendar });

            calendarService = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = Constants.GoogleApplicationName
            });
        }

        public async Task<IEnumerable<ExistingCalendarEvent>> GetEventsForMonth(int month, int year)
        {
            var calendarRequest = calendarService.Events.List(Constants.CalendarId);

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
                    request.Queue<string>(
                        calendarService.Events.Delete(Constants.CalendarId, oldEvent.Id),
                        (_, _, _, _) => { });
                }
                else if (newEvent.ShiftId != oldEvent.ShiftId)
                {
                    Event eventToUpdate = new Event
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
                    };

                    request.Queue<Event>(
                        calendarService.Events.Update(eventToUpdate, Constants.CalendarId, oldEvent.Id),
                        (_, _, _, _) => { });
                }
            }

            foreach (var newEvent in newEvents)
            {
                var hasOldEvent = oldEvents.Any(e => e.Date == newEvent.Date);

                if (hasOldEvent) continue;

                Event newEventToInsert = new Event
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
                };

                request.Queue<Event>(
                    calendarService.Events.Insert(newEventToInsert, Constants.CalendarId),
                    (_, _, _, _) => { });
            }

            await request.ExecuteAsync();
        }
    }
}
