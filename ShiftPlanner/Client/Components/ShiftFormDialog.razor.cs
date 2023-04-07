using System;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using ShiftPlanner.Client.Services;
using ShiftPlanner.Shared;

namespace ShiftPlanner.Client.Components
{
    public partial class ShiftFormDialog : ComponentBase
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public ShiftDefinition shiftDefinition { get; set; } = new ShiftDefinition();

        [Inject]
        private IShiftService shiftService { get; set; } = default!;

        private async Task SaveShift()
        {
            if(shiftDefinition.ShiftId == Guid.Empty)
            {
                await shiftService.CreateNewShift(new NewShift()
                {
                    ShiftName = shiftDefinition.ShiftName,
                    StartTime = shiftDefinition.StartTime,
                    EndTime = shiftDefinition.EndTime,
                    ShiftType = shiftDefinition.ShiftType
                });
            }
            else
            {
                await shiftService.UpdateShift(shiftDefinition);

            }
            //Snackbar.Add("Server Deleted", Severity.Success);
            MudDialog.Close();
        }

        protected override void OnInitialized()
        {
            startTime = shiftDefinition.StartTime.ToTimeSpan();
            endTime = shiftDefinition.EndTime.ToTimeSpan();

            base.OnInitialized();
        }

        private TimeSpan? startTime;
        public TimeSpan? StartTime
        {
            get => startTime;
            set
            {
                startTime = value;

                Console.WriteLine("Updating starttime");
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
                Console.WriteLine("Updating endtime");

                shiftDefinition.EndTime = TimeOnly.FromTimeSpan(endTime!.Value);
            }
        }

        //private 
    }
}

