using System;
using ShiftPlanner.Server.Models;
using ShiftPlanner.Shared;

namespace ShiftPlanner.Server.Services
{
    public interface IShiftService
    {
        Task<IEnumerable<ShiftDefinition>> GetShifts();
        Task CreateNewShift(Shift shift);
        Task UpdateExistingShift(Shift shift);
    }
}

