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

        [Inject]
        private IAppStateService _appStateService { get; set; } = default!;

        public void ShiftSelected(ShiftDefinition? shift)
        {
            Day.ExistingShift = shift;
            IsPopoverOpen = false;
        }

        protected override void OnInitialized()
        {
            _appStateService.OnPopupChange += PopupDateChanged;

            base.OnInitialized();
        }

        public void OnDispose()
        {
            _appStateService.OnPopupChange -= PopupDateChanged;
        }

        public void PopupDateChanged(DateOnly date)
        {
            if (IsPopoverOpen && Day.Date != date)
            {
                IsPopoverOpen = false;
                StateHasChanged();
            }
        }

        private bool _isPopoverOpen;
        public bool IsPopoverOpen
        {
            get => _isPopoverOpen;
            set => _isPopoverOpen = value;
        }

        public void ToggleOpen()
        {
            IsPopoverOpen = !IsPopoverOpen;

            if (IsPopoverOpen)
            {
                _appStateService.NotifyDateOpenedChanged(Day.Date);
            }
        }
    }
}

