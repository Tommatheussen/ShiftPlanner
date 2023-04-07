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

        public async Task ShiftClicked()
        {
            if (ShiftSelected.HasDelegate)
            {
                await ShiftSelected.InvokeAsync(Shift);
            }
        }
    }
}

