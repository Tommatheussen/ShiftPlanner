using System;
using Microsoft.AspNetCore.Components;
using ShiftPlanner.Shared;

namespace ShiftPlanner.Client.Components
{
    public partial class ShiftTemplate: ComponentBase
    {
        [Parameter]
        public ShiftDefinition Shift { get; set; } = default!;

        [Parameter]
        public EventCallback<ShiftDefinition> ShiftSelected { get; set; }

        [Parameter]
        public EventCallback<ShiftDefinition> OnShiftDelete { get; set; }

        private async Task ShiftClicked()
        {
            if (ShiftSelected.HasDelegate)
            {
                await ShiftSelected.InvokeAsync(Shift);
            }
        }

        private bool ShowDelete
        {
            get => OnShiftDelete.HasDelegate;
        }

        private async Task ShiftDeleteClicked()
        {
            if (OnShiftDelete.HasDelegate)
            {
                await OnShiftDelete.InvokeAsync(Shift);
            }
        }
    }
}

