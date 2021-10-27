using System;
using Microsoft.AspNetCore.Components;
using ShiftPlanner.Shared;

namespace ShiftPlanner.Client.Components
{
    public partial class ScheduleEvent : ComponentBase
    {
        [Parameter]
        public CalendarEvent Event { get; set; } = default!;
    }
}

