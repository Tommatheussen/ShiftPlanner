using System;
using Microsoft.AspNetCore.Components;
using ShiftPlanner.Client.Services;
using ShiftPlanner.Shared;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Schedule;

using Microsoft.AspNetCore.Components.Web;
using System.Collections.ObjectModel;

namespace ShiftPlanner.Client.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        private IShiftService _shiftService { get; set; } = default!;

        private SfSchedule<CalendarEvent> ScheduleRef;

        public IEnumerable<ShiftDefinition> ShiftList;
        public ObservableCollection<CalendarEvent> CalendarEventList = new ObservableCollection<CalendarEvent>();

        private ShiftDefinition _draggedShift;

        protected override async Task OnInitializedAsync()
        {
            ShiftList = await _shiftService.GetShifts();

            var events = await _shiftService.GetCalendarEvents();

            foreach (var e in events)
            {
                CalendarEventList.Add(e);
            }

            await base.OnInitializedAsync();
        }

        private void HandleDragStart(ShiftDefinition shift)
        {
            _draggedShift = shift;
        }

        private async Task HandleShiftDropped(DateTime dateTime)
        {
            CalendarEvent eventData = new CalendarEvent
            {
                Id = Guid.NewGuid().ToString("N"),
                ShiftName = _draggedShift.ShiftName,
                StartTime = GenerateEventDatetime(dateTime, _draggedShift.StartTime),
                EndTime = GenerateEventDatetime(dateTime, _draggedShift.EndTime),
                Description = $"{_draggedShift.StartTime} - {_draggedShift.EndTime}"
            };

            Console.WriteLine(dateTime);
            Console.WriteLine(_draggedShift.EndTime);
            Console.WriteLine(_draggedShift.StartTime);
            Console.WriteLine(_draggedShift.ShiftName);
            Console.WriteLine(eventData.StartTime);
            Console.WriteLine(eventData.EndTime);

            CalendarEventList.Add(eventData);

            //await ScheduleRef.RefreshEventsAsync();
        }

        private DateTime GenerateEventDatetime(DateTime datetime1, string time)
        {
            var Date = datetime1.Date;
            var Time = TimeSpan.Parse(time);

            return new DateTime(Date.Year, Date.Month, Date.Day, Time.Hours, Time.Minutes, Time.Seconds);
        }
    }
}
