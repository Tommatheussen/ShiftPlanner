﻿using System;
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
        private NavigationManager navigationManager { get; set; }

        public Calendar calendar;

        public void TriggerShiftSave()
        {
            calendar.SaveEvents();
        }

        public void NavigateToConfigurePage()
        {
            navigationManager.NavigateTo("configure");
        }
    }
}
