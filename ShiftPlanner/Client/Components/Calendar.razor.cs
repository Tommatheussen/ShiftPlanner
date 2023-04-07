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
        private IAppStateService appStateService { get; set; } = default!;

        [Inject]
        private IEventService eventService { get; set; } = default!;

        [Inject]
        private IShiftService shiftService { get; set; } = default!;

        private List<CalendarDay> days = new List<CalendarDay>();

        protected async override Task OnInitializedAsync()
        { 
            await UpdateEvents();

            await base.OnInitializedAsync();
        }

        protected override void OnInitialized()
        {
            UpdateCalendar();

            appStateService.OnChange += currentDateHasChanged;
        }

        public void OnDispose()
        {
            appStateService.OnChange -= currentDateHasChanged;
        }

        private async void currentDateHasChanged()
        {
            UpdateCalendar();
            await UpdateEvents();
        }

        async Task UpdateEvents()
        {
            var events = await eventService.GetEventsForMonth(appStateService.SelectedDate.Month, appStateService.SelectedDate.Year);
            var shifts = await shiftService.GetShifts();

            foreach (var existingEvent in events)
            {
                var shiftDefinition = shifts.FirstOrDefault(shift => shift.ShiftId == existingEvent.ShiftId);
                if(shiftDefinition == null) { continue; }

                var dayForEvent = days.Find(day => day.Date == existingEvent.Date);
                if(dayForEvent == null) { continue; }

                dayForEvent.ExistingShift = shiftDefinition;
            }

            StateHasChanged();
        }

        void UpdateCalendar()
        {
            days = new List<CalendarDay>();

            // Calculate the number of empty days
            DateOnly currentMonth = appStateService.SelectedDate;

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

            DateOnly currentMonth = appStateService.SelectedDate;
            await eventService.UpdateEventsForEvents(currentMonth.Month, currentMonth.Year, eventList);
        }
    }
}