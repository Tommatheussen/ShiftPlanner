using System;
namespace ShiftPlanner.Client.Services
{
    public class AppStateService : IAppStateService
    {
        public event Action? OnChange;

        private DateOnly selectedDate = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly SelectedDate
        {
            get => selectedDate;
            set
            {
                selectedDate = value;
                NotifyStateChanged();
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
