using System;
using Microsoft.AspNetCore.Components;
using ShiftPlanner.Client.Services;
using ShiftPlanner.Shared;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Schedule;

namespace ShiftPlanner.Client.Pages
{

    public class AppointmentData
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsAllDay { get; set; }
        public string RecurrenceRule { get; set; }
        public string RecurrenceException { get; set; }
        public Nullable<int> RecurrenceID { get; set; }
    }

    public partial class Index: ComponentBase
    {
        [Inject]
        private IShiftService _shiftService { get; set; } = default!;


        //private SfSchedule ScheduleRef;
        private SfTreeView<ShiftDefinition> TreeViewRef;

        public IEnumerable<ShiftDefinition> ShiftList;

        protected override async Task OnInitializedAsync()
        {
            ShiftList = await _shiftService.GetShifts();

            await base.OnInitializedAsync();
        }
    }
}



//    SfSchedule<AppointmentData> ScheduleRef;
//    SfTreeView<AppointmentData> TreeViewRef;

//    public List<AppointmentData> TreeViewData = new List<AppointmentData>()
//{
//        new AppointmentData
//        {
//            Id = 1,
//            Subject = "DL",
//            IsAllDay = true
//        },
//        new AppointmentData
//        {
//            Id = 2,
//            Subject = "DL2",
//            IsAllDay = true
//        }
//    };

//public class AppointmentData
//{
//    public int Id { get; set; }
//    public string Subject { get; set; }
//    public Nullable<bool> IsAllDay { get; set; }
//    public DateTime StartTime { get; set; }
//    public DateTime EndTime { get; set; }
//}

//public async void OnTreeViewDragStop(DragAndDropEventArgs args)
//{
//    args.Cancel = true;

//    CellClickEventArgs cellData = await ScheduleRef.GetTargetCellAsync((int)args.Left, (int)args.Top);

//    if (cellData != null)
//    {
//        var resourceDetails = ScheduleRef.GetResourceByIndex(cellData.GroupIndex);
//        Random rnd = new Random();
//        int Id = rnd.Next(1000);
//        AppointmentData TreeData = TreeViewData.Where(data => data.Id.ToString() == args.DraggedNodeData.Id).First();
//        AppointmentData eventData = new AppointmentData
//        {
//            Id = Id,
//            Subject = TreeData.Subject,
//            IsAllDay = cellData.IsAllDay,
//            StartTime = cellData.StartTime,
//            EndTime = cellData.EndTime
//        };

//        await ScheduleRef.AddEventAsync(eventData);
//    }
//}
//}