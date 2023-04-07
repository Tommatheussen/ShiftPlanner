using System;
namespace ShiftPlanner.Client.Services
{
    public interface IAppStateService
    {
        event Action? OnDateChange;

        DateOnly SelectedDate { get; set; }


        event Action<DateOnly>? OnPopupChange;
        void NotifyDateOpenedChanged(DateOnly date);

        event Action<bool>? OnOverlayChange;
        void SetOverlayState(bool state);
    }
}
