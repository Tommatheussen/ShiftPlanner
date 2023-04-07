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
        private IAppStateService _appStateService { get; set; } = default!;

        [Inject]
        private NavigationManager _navigationManager { get; set; } = default!;

        [Inject]
        private IDialogService _dialogService { get; set; } = default!;

        public IEnumerable<ShiftDefinition> ShiftList = new List<ShiftDefinition>();

        protected override async Task OnInitializedAsync()
        {
            _appStateService.SetOverlayState(true);

            ShiftList = await _shiftService.GetShifts();

            _appStateService.SetOverlayState(false);
        }

        private void NavigateToMainPage()
        {
            _navigationManager.NavigateTo("/");
        }

        private async Task OpenShiftUpdateDialog(ShiftDefinition shift)
        {
            var parameters = new DialogParameters();
            parameters.Add("shiftDefinition", shift);

            await OpenDialog("Shift aanpassen", parameters);
        }

        private async Task AddNewShift()
        {
            await OpenDialog("Nieuwe shift");
        }

        private async Task OpenDialog(string title, DialogParameters? parameters = null)
        {
            if(parameters == null)
            {
                parameters = new DialogParameters();
            }

            var dialog = await _dialogService.ShowAsync<ShiftFormDialog>(title, parameters);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                _appStateService.SetOverlayState(true);

                ShiftList = await _shiftService.RenewCachedShiftList();
                _appStateService.SetOverlayState(false);
            }
        }

        private async Task ConfirmShiftDelete(ShiftDefinition shift)
        {
            bool? deleteResult = await _dialogService.ShowMessageBox(
                "Shift verwijderen?",
                new MarkupString($"Ben je zeker dat je de shift <b>{shift.ShiftName}</b> wil verwijderen?"),
                yesText: "Ja",
                cancelText: "Nee"
                );

            if (deleteResult.HasValue && deleteResult.Value)
            {
                _appStateService.SetOverlayState(true);

                await _shiftService.DeleteShift(shift);

                ShiftList = await _shiftService.RenewCachedShiftList();

                _appStateService.SetOverlayState(false);
            }
        }
    }
}

