using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShiftPlanner.Server.Models;
using ShiftPlanner.Server.Services;
using ShiftPlanner.Shared;

namespace ShiftPlanner.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShiftController : Controller
    {
        private readonly IShiftService _shiftService;

        public ShiftController(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        [HttpGet]
        public async Task<IEnumerable<ShiftDefinition>> GetShifts()
        {
            var list = await _shiftService.GetShifts();
            return list;
        }

        [HttpPost]
        public async Task CreateNewShift([FromBody] Shift shift)
        {
            await _shiftService.CreateNewShift(shift);
            return;
        }

        [HttpPut]
        public async Task UpdateExistingShift([FromBody] Shift shift)
        {
            await _shiftService.UpdateExistingShift(shift);
            return;
        }
    }
}

