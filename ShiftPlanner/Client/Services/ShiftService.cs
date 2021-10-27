using System;
using ShiftPlanner.Shared;

namespace ShiftPlanner.Client.Services
{
    public class ShiftService : IShiftService
    {
        public Task<IEnumerable<ShiftDefinition>> GetShifts()
        {
            var shifts = new List<ShiftDefinition>()
            {
                new ShiftDefinition()
                {
                    ShiftName = "L'",
                    StartTime = new TimeOnly(15, 30).ToString(),
                    EndTime = new TimeOnly(21, 00).ToString()
                },
                new ShiftDefinition()
                {
                    ShiftName = "V'",
                    StartTime = new TimeOnly(07, 00).ToString(),
                    EndTime = new TimeOnly(12, 00).ToString()
                }
            };
            return Task.FromResult<IEnumerable<ShiftDefinition>>(shifts);
        }
    }
}
