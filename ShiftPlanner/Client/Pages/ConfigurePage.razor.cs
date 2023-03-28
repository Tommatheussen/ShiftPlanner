using System;
using Microsoft.AspNetCore.Components;
using ShiftPlanner.Client.Services;
using ShiftPlanner.Shared;

namespace ShiftPlanner.Client.Pages
{
    public partial class ConfigurePage : ComponentBase
    {
        [Inject]
        private IShiftService _shiftService { get; set; } = default!;


        public IEnumerable<ShiftDefinition> ShiftList;

        protected override async Task OnInitializedAsync()
        {
            ShiftList = await _shiftService.GetShifts();
        }
    }
}

