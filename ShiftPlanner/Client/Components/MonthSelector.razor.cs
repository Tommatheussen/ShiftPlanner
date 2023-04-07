using System;
using System.Globalization;
using Microsoft.AspNetCore.Components;
using ShiftPlanner.Client.Services;

namespace ShiftPlanner.Client.Components
{
    public partial class MonthSelector : ComponentBase
    {
        [Inject]
        private IAppStateService appStateService { get; set; } = default!;

        private int CurrentYear;
        private string CurrentMonth;

        protected override void OnInitialized()
        {
            appStateService.OnChange += _updateVisibleDate;
            _updateVisibleDate();

            base.OnInitialized();
        }

        private void _updateVisibleDate()
        {
            CurrentYear = appStateService.SelectedDate.Year;
            CurrentMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(appStateService.SelectedDate.Month);
        }

        public void Dispose()
        {
            appStateService.OnChange -= StateHasChanged;
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
            DateOnly currentDate = appStateService.SelectedDate;
            appStateService.SelectedDate = currentDate.AddMonths(monthToAdd);
        }
    }
}

