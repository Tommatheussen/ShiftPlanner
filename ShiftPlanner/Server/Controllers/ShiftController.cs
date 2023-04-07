using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
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
        public async Task CreateNewShift([FromBody] NewShift shift)
        {
            await _shiftService.CreateNewShift(new Shift(shift));

            return;
        }

        [HttpPut]
        public async Task UpdateExistingShift([FromBody] ShiftDefinition shiftDefinition)
        {
            await _shiftService.UpdateExistingShift(new Shift(shiftDefinition));

            return;
        }
    }
}

