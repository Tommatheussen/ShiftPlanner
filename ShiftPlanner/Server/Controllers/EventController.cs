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
    public class EventController : Controller
    {
        private readonly IGoogleService _googleService;

        public EventController(IGoogleService googleService)
        {
            _googleService = googleService;
        }

        [HttpGet]
        public async Task<IEnumerable<ExistingCalendarEvent>> GetEvents([FromQuery]int month, [FromQuery]int year)
        {
            var list = await _googleService.GetEventsForMonth(month, year);

            return list;
        }

        [HttpPut]
        public async Task UpdateEventsForMonth([FromBody] UpdateEvents events)
        {
            var oldEvents = await _googleService.GetEventsForMonth(events.Month, events.Year);

            await _googleService.UpdateEventsForMonth(oldEvents, events.Events);
        }
    }
}

