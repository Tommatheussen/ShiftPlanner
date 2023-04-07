using System;
namespace ShiftPlanner.Client.Services
{
    public class AppStateService : IAppStateService
    {
        // Selected date
        public event Action? OnDateChange;

        private DateOnly selectedDate = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly SelectedDate
        {
            get => selectedDate;
            set
            {
                selectedDate = value;
                NotifyDateStateChanged();
            }
        }

        private void NotifyDateStateChanged() => OnDateChange?.Invoke();

        // Opened date popup

        public event Action<DateOnly>? OnPopupChange;
        public void NotifyDateOpenedChanged(DateOnly date) => OnPopupChange?.Invoke(date);


        public event Action<bool>? OnOverlayChange;
        public void SetOverlayState(bool state) =>OnOverlayChange?.Invoke(state);
    }
}
