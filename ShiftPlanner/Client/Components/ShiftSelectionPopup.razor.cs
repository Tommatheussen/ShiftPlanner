using System;
using Microsoft.AspNetCore.Components;
using ShiftPlanner.Client.Services;
using ShiftPlanner.Shared;

namespace ShiftPlanner.Client.Components
{
    public partial class ShiftSelectionPopup : ComponentBase
    {
        [Inject]
        private IShiftService _shiftService { get; set; } = default!;

        [Parameter]
        public EventCallback<ShiftDefinition?> ShiftSelected { get; set; }


        private IEnumerable<ShiftDefinition> Shifts { get; set; } = new List<ShiftDefinition>();


        protected async override Task OnInitializedAsync()
        {
            Shifts = await _shiftService.GetShifts();
            await base.OnInitializedAsync();
        }

        public IEnumerable<ShiftDefinition> EarlyShifts
        {
            get => Shifts.Where(shift => shift.ShiftType == ShiftType.Early);
        }

        public IEnumerable<ShiftDefinition> LateShifts
        {
            get => Shifts.Where(shift => shift.ShiftType == ShiftType.Late);
        }

        public IEnumerable<ShiftDefinition> OtherShifts
        {
            get => Shifts.Where(shift => shift.ShiftType == ShiftType.Other);
        }

        public async void ClearSelectedShift()
        {
            if(ShiftSelected.HasDelegate)
            {
                await ShiftSelected.InvokeAsync();
            }
        }
    }
}

