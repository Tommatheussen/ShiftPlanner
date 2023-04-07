using System;
using System.Globalization;
using Microsoft.AspNetCore.Components;
using ShiftPlanner.Client.Services;

namespace ShiftPlanner.Client.Components
{
    public partial class MonthSelector : ComponentBase
    {
        [Inject]
        private IAppStateService _appStateService { get; set; } = default!;

        private int CurrentYear;
        private string CurrentMonth;

        protected override void OnInitialized()
        {
            _appStateService.OnDateChange += _updateVisibleDate;
            _updateVisibleDate();

            base.OnInitialized();
        }

        private void _updateVisibleDate()
        {
            CurrentYear = _appStateService.SelectedDate.Year;
            CurrentMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(_appStateService.SelectedDate.Month);
        }

        public void Dispose()
        {
            _appStateService.OnDateChange -= StateHasChanged;
        }

        private void PreviousMonthClicked()
        {
            _adjustDateByMonth(-1);
        }

        private void NextMonthClicked()
        {
            _adjustDateByMonth(1);
        }

        private void _adjustDateByMonth(int monthToAdd)
        {
            DateOnly currentDate = _appStateService.SelectedDate;
            _appStateService.SelectedDate = currentDate.AddMonths(monthToAdd);
        }
    }
}

