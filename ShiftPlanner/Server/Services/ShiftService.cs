using System;
using Microsoft.EntityFrameworkCore;
using ShiftPlanner.Server.Models;
using ShiftPlanner.Shared;

namespace ShiftPlanner.Server.Services
{
    public class ShiftService : IShiftService
    {
        public ShiftService()
        {
            using (var db = new ShiftPlannerContext())
            {
                db.Database.EnsureCreated();
            }
        }

        public async Task<IEnumerable<ShiftDefinition>> GetShifts()
        {
            using (var db = new ShiftPlannerContext())
            {
                var shifts = await db.Shifts
                    .Select(shift => new ShiftDefinition
                    {
                        ShiftId = shift.Id,
                        ShiftName = shift.ShiftName,
                        StartTime = shift.StartTime,
                        EndTime = shift.EndTime,
                        ShiftType = shift.ShiftType
                    }).ToListAsync();

                return shifts;
            }
        }

        public async Task CreateNewShift(Shift shift)
        {
            using (var db = new ShiftPlannerContext())
            {
                await db.AddAsync(shift);
                await db.SaveChangesAsync();
            }
        }

        public async Task UpdateExistingShift(Shift shift)
        {
            using (var db = new ShiftPlannerContext())
            {
                db.Update(shift);
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteShift(Guid id)
        {
            using (var db = new ShiftPlannerContext())
            {
                db.Remove(new Shift()
                {
                    Id = id
                });
                await db.SaveChangesAsync();
            }
        }
    }
}