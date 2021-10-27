using System;
using Microsoft.AspNetCore.Components;
using ShiftPlanner.Client.Services;
using ShiftPlanner.Shared;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Schedule;

using Microsoft.AspNetCore.Components.Web;

namespace ShiftPlanner.Client.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        private IShiftService _shiftService { get; set; } = default!;

        private SfSchedule<CalendarEvent> ScheduleRef;

        public IEnumerable<ShiftDefinition> ShiftList;
        public List<CalendarEvent> CalendarEventList;

        private ShiftDefinition _draggedShift;

        protected override async Task OnInitializedAsync()
        {
            ShiftList = await _shiftService.GetShifts();
            CalendarEventList = await _shiftService.GetCalendarEvents() as List<CalendarEvent>;

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

            CalendarEventList.Add(eventData);

            await ScheduleRef.RefreshEventsAsync();
        }

        private DateTime GenerateEventDatetime(DateTime datetime1, string time)
        {
            var Date = datetime1.Date;
            var Time = TimeSpan.Parse(time);

            return new DateTime(Date.Year, Date.Month, Date.Day, Time.Hours, Time.Minutes, Time.Seconds);
        }
    }
}
