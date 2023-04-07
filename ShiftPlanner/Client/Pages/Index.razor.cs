using System;
using Microsoft.AspNetCore.Components;
using ShiftPlanner.Client.Services;
using ShiftPlanner.Shared;

using Microsoft.AspNetCore.Components.Web;
using System.Collections.ObjectModel;
using ShiftPlanner.Client.Models;
using ShiftPlanner.Client.Components;

namespace ShiftPlanner.Client.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        private NavigationManager _navigationManager { get; set; } = default!;

        [Inject]
        private IAppStateService _appStateService { get; set; } = default!;

        public Calendar calendar;

        public void TriggerShiftSave()
        {
            calendar.SaveEvents();
        }

        public void NavigateToConfigurePage()
        {
            _navigationManager.NavigateTo("configure");
        }
    }
}
