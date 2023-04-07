using System;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using ShiftPlanner.Client.Services;
using ShiftPlanner.Shared;

namespace ShiftPlanner.Client.Components
{
    public partial class ShiftFormDialog : ComponentBase
    {
        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public ShiftDefinition shiftDefinition { get; set; } = new ShiftDefinition();

        [Inject]
        private IShiftService _shiftService { get; set; } = default!;

        [Inject]
        private IAppStateService _appStateService { get; set; } = default!;

        private async Task SaveShift()
        {
            _appStateService.SetOverlayState(true);

            if (shiftDefinition.ShiftId == Guid.Empty)
            {
                await _shiftService.CreateNewShift(new NewShift()
                {
                    ShiftName = shiftDefinition.ShiftName,
                    StartTime = shiftDefinition.StartTime,
                    EndTime = shiftDefinition.EndTime,
                    ShiftType = shiftDefinition.ShiftType
                });
            }
            else
            {
                await _shiftService.UpdateShift(shiftDefinition);
            }

            _appStateService.SetOverlayState(false);

            MudDialog.Close();
        }

        protected override void OnInitialized()
        {
            startTime = shiftDefinition.StartTime.ToTimeSpan();
            endTime = shiftDefinition.EndTime.ToTimeSpan();

            base.OnInitialized();
        }

        public string shiftTypeName(ShiftType type)
        {
            return type switch
            {
                ShiftType.Early => "Vroege",
                ShiftType.Late => "Late",
                ShiftType.Other => "Andere",
                _ => string.Empty
            };
        }

        private TimeSpan? startTime;
        public TimeSpan? StartTime
        {
            get => startTime;
            set
            {
                startTime = value;
                shiftDefinition.StartTime = TimeOnly.FromTimeSpan(startTime!.Value);
            }
        }

        private TimeSpan? endTime;
        public TimeSpan? EndTime
        {
            get => endTime;
            set
            {
                endTime = value;
                shiftDefinition.EndTime = TimeOnly.FromTimeSpan(endTime!.Value);
            }
        }
    }
}