using System;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using ShiftPlanner.Client.Components;
using ShiftPlanner.Client.Services;
using ShiftPlanner.Shared;

namespace ShiftPlanner.Client.Pages
{
    public partial class ConfigurePage : ComponentBase
    {
        [Inject]
        private IShiftService _shiftService { get; set; } = default!;

        [Inject]
        private NavigationManager navigationManager { get; set; } = default!;

        [Inject]
        private IDialogService dialogService { get; set; } = default!;



        public IEnumerable<ShiftDefinition> ShiftList = new List<ShiftDefinition>();

        protected override async Task OnInitializedAsync()
        {
            ShiftList = await _shiftService.GetShifts();
        }

        private async Task CreateNewShift()
        {
            await _shiftService.CreateNewShift(new NewShift()
            {
                StartTime = TimeOnly.Parse("15:00"),
                EndTime = TimeOnly.Parse("21:00"),
                ShiftName = "ShiftTest",
                ShiftType = ShiftType.Late
            });
        }

        private void NavigateToMainPage()
        {
            navigationManager.NavigateTo("/");
        }

        private async Task OpenShiftUpdateDialog(ShiftDefinition shift)
        {
            var parameters = new DialogParameters();
            parameters.Add("shiftDefinition", shift);


            var dialog = await dialogService.ShowAsync<ShiftFormDialog>("Shift aanpassen", parameters);
            var result = await dialog.Result;
            
            if(!result.Canceled)
            {
                ShiftList = await _shiftService.RenewCachedShiftList();
            }
            //if (!result.Canceled)
            //    {
            //        //In a real world scenario we would reload the data from the source here since we "removed" it in the dialog already.
            //        Guid.TryParse(result.Data.ToString(), out Guid deletedServer);
            //        Servers.RemoveAll(item => item.Id == deletedServer);
            //    }
            //}
        }

        private async Task AddNewShift()
        {
            var dialog = await dialogService.ShowAsync<ShiftFormDialog>("Nieuwe shift");
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                ShiftList = await _shiftService.RenewCachedShiftList();
            }
        }
    }
}

