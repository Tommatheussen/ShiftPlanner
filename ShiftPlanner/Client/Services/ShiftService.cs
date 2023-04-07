using System;
using System.Net.Http.Json;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using ShiftPlanner.Shared;

namespace ShiftPlanner.Client.Services
{
    public class ShiftService : IShiftService
    {
        private readonly HttpClient _http;

        private IEnumerable<ShiftDefinition>? _cachedShifts;

        public ShiftService(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<ShiftDefinition>> GetShifts()
        {
            if(_cachedShifts == null)
            {
                this._cachedShifts = await _http.GetFromJsonAsync<IEnumerable<ShiftDefinition>>("api/shift");
            }

            return this._cachedShifts!;
        }

        public async Task CreateNewShift(NewShift shift)
        {
            await _http.PostAsJsonAsync<NewShift>("api/shift", shift);
        }

        public async Task UpdateShift(ShiftDefinition shift)
        {
            await _http.PutAsJsonAsync<ShiftDefinition>("api/shift", shift);
        }

        public async Task<IEnumerable<ShiftDefinition>> RenewCachedShiftList()
        {
            _cachedShifts = null;

           return await GetShifts();
        }

        public async Task DeleteShift(ShiftDefinition shift)
        {
            await _http.DeleteAsync($"api/shift/{shift.ShiftId}");
        }
    }
}
