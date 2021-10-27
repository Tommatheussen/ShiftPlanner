using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ShiftPlanner.Shared;
using Syncfusion.Blazor.Schedule;

namespace ShiftPlanner.Client.Components
{
    public partial class ScheduleCell: ComponentBase
    {
        private string dropCss = "";

        [Parameter]
        public TemplateContext Context { get; set; } = default!;

        [Parameter]
        public EventCallback<DateTime> ShiftDropped { get; set; }

        private void HandleDragEnter()
        {
            dropCss = "can-drop";
        }

        private void HandleDragLeave()
        {
            dropCss = "";
        }

        private async Task HandleDrop(DragEventArgs args)
        {
            dropCss = "";

            await ShiftDropped.InvokeAsync(Context.Date);
        }
    }
}