using System;
using Microsoft.AspNetCore.Components;
using ShiftPlanner.Client.Services;

namespace ShiftPlanner.Client.Shared
{
    public partial class MainLayout : LayoutComponentBase
    {
        [Inject]
        private IAppStateService _appStateService { get; set; } = default!;

        protected override void OnInitialized()
        {
            _appStateService.OnOverlayChange += SetOverlayState;

            base.OnInitialized();
        }

        public void OnDispose()
        {
            _appStateService.OnOverlayChange -= SetOverlayState;
        }

        public void SetOverlayState(bool state)
        {
            IsOverlayVisible = state;
            StateHasChanged();
        }

        public bool IsOverlayVisible;
    }
}

