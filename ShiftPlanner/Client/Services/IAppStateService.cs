using System;
namespace ShiftPlanner.Client.Services
{
    public interface IAppStateService
    {
        event Action? OnChange;

        DateOnly SelectedDate { get; set; }
    }
}
