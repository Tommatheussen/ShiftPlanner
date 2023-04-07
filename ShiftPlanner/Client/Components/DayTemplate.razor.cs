using System;
using System.Globalization;
using Microsoft.AspNetCore.Components;
using ShiftPlanner.Client.Models;
using ShiftPlanner.Client.Services;
using ShiftPlanner.Shared;

namespace ShiftPlanner.Client.Components
{
    public partial class DayTemplate: ComponentBase
    {
        [Parameter]
        public CalendarDay Day { get; set; } = default!;

        [Parameter]
        public IEnumerable<ShiftDefinition> Shifts { get; set; } = default!;

        public string DayName
        {
            get => CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(Day.Date.DayOfWeek);
        }
        
        
        public void ShiftSelected(ShiftDefinition? shift)
        {
            Day.ExistingShift = shift;
            this._isOpen = false;
        }

        public bool _isOpen;

        public void ToggleOpen()
        {
            if (_isOpen)
                _isOpen = false;
            else
                _isOpen = true;
        }
    }
}

