using System;

using Microsoft.AspNetCore.Components;
using ShiftPlanner.Client.Models;
using ShiftPlanner.Client.Services;
using ShiftPlanner.Shared;

namespace ShiftPlanner.Client.Components
{
    public partial class Calendar: ComponentBase
    {
        [Inject]
        private IAppStateService _appStateService { get; set; } = default!;

        [Inject]
        private IEventService _eventService { get; set; } = default!;

        [Inject]
        private IShiftService _shiftService { get; set; } = default!;

        private List<CalendarDay> days = new List<CalendarDay>();

        protected async override Task OnInitializedAsync()
        { 
            await UpdateEvents();

            await base.OnInitializedAsync();
        }

        protected override void OnInitialized()
        {
            UpdateCalendar();

            _appStateService.OnDateChange += currentDateHasChanged;
        }

        public void OnDispose()
        {
            _appStateService.OnDateChange -= currentDateHasChanged;
        }

        private async void currentDateHasChanged()
        {
            UpdateCalendar();

            await UpdateEvents();
        }

        async Task UpdateEvents()
        {
            _appStateService.SetOverlayState(true);

            var events = await _eventService.GetEventsForMonth(_appStateService.SelectedDate.Month, _appStateService.SelectedDate.Year);
            var shifts = await _shiftService.GetShifts();

            foreach (var existingEvent in events)
            {
                var shiftDefinition = shifts.FirstOrDefault(shift => shift.ShiftId == existingEvent.ShiftId);
                if(shiftDefinition == null) { continue; }

                var dayForEvent = days.Find(day => day.Date == existingEvent.Date);
                if(dayForEvent == null) { continue; }

                dayForEvent.ExistingShift = shiftDefinition;
            }

            StateHasChanged();

            _appStateService.SetOverlayState(false);
        }

        void UpdateCalendar()
        {
            days = new List<CalendarDay>();

            // Calculate the number of empty days
            DateOnly currentMonth = _appStateService.SelectedDate;

            var firstDayDate = new DateTime(currentMonth.Year, currentMonth.Month, 1);
            DayOfWeek weekDayNumber = firstDayDate.DayOfWeek;
            int numberOfEmptyDays = 0;
            if (weekDayNumber == DayOfWeek.Monday) 
            {
                numberOfEmptyDays = 0;
            }
            else
            {
                numberOfEmptyDays = (int)weekDayNumber - 1;
            }

            // Add the empty days 
            for (int i = 0; i < numberOfEmptyDays; i++)
            {
                days.Add(new CalendarDay
                {
                    DayNumber = 0,
                    IsEmpty = true
                });
            }

            // Add the month days 
            int numberOfDaysInMonth = DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month);

            for (int i = 0; i < numberOfDaysInMonth; i++)
            {
                days.Add(new CalendarDay
                {
                    DayNumber = i + 1,
                    IsEmpty = false,
                    Date = new DateOnly(currentMonth.Year, currentMonth.Month, i + 1),
                    //Events = new List<CalendarEvent>()
                });
            }

            StateHasChanged();
        }

        public async void SaveEvents()
        {
            _appStateService.SetOverlayState(true);

            var eventList = new List<CalendarEvent>();
            foreach (var day in days)
            {
                if(day.ExistingShift != null)
                {
                    eventList.Add(new CalendarEvent
                    {
                        ShiftId = day.ExistingShift.ShiftId,
                        ShiftName = day.ExistingShift.ShiftName,
                        Date = day.Date,
                        StartTime = (day.ExistingShift.StartTime),
                        EndTime = (day.ExistingShift.EndTime)
                    });
                }
            }

            DateOnly currentMonth = _appStateService.SelectedDate;
            await _eventService.UpdateEventsForEvents(currentMonth.Month, currentMonth.Year, eventList);

            _appStateService.SetOverlayState(false);
        }
    }
}