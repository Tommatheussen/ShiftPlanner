using System;
using ShiftPlanner.Shared;

namespace ShiftPlanner.Client.Services
{
    public interface IShiftService
    {
        Task<IEnumerable<ShiftDefinition>> GetShifts();
        Task CreateNewShift(NewShift shift);
        Task UpdateShift(ShiftDefinition shift);
        Task<IEnumerable<ShiftDefinition>> RenewCachedShiftList();
        Task DeleteShift(ShiftDefinition shift);
    }
}

