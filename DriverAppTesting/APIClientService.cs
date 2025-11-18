using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverAppTesting
{
    using System.Net.Http.Headers;
    using System.Security.Policy;
    using System.Text;
    using System.Text.Json;

    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private string _baseUrl;
        public static string _token;

        private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

        public ApiClient(string baseUrl)
        {
            _baseUrl = baseUrl;
            _httpClient = new HttpClient();
        }

        // 🔹 Common POST
        private async Task<T?> PostAsync<T>(string endpoint, object data, bool authRequired = true) where T : class 
        {
            if (authRequired && !string.IsNullOrEmpty(_token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var strContent = JsonSerializer.Serialize(data);
            var content = new StringContent(strContent, Encoding.UTF8, "application/json");
            string url = $"{_baseUrl}{endpoint}";
            var response = await _httpClient.PostAsync(url, content);

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json, _jsonOptions);

            return null;
        }

        // 🔹 Common GET
        private async Task<T> GetAsync<T>(string endpoint, bool authRequired = true)
        {
            if (authRequired && !string.IsNullOrEmpty(_token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var response = await _httpClient.GetAsync($"{_baseUrl}{endpoint}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        // 🔹 Verify OTP
        public async Task<VerifyOtpResponse> VerifyOtpAsync(VerifyOtpRequest request)
        {
            var result = await PostAsync<VerifyOtpResponse>("api/Account/verify-otp", request, authRequired: false);
            _token = result?.TokenResponse?.Token; // save for next calls
            return result;
        }

        // 🔹 Update Driver Status
        public async Task<bool> UpdateDriverStatusAsync(object statusPayload)
        {
            var response = await PostAsync<string>("api/DriverStatus/update", statusPayload);
            return true;
        }

        // 🔹 Update Booking
        public async Task<bool> UpdateRideStatusAsync(UpdateBookingRequest request)
        {
            try
            {
                var response = await PostAsync<ApiResponse<bool>>("api/ride-trips/update-status", request);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // 🔹 Cancel Ride
        public async Task<RideServiceResponse> CancelRideAsync(int rideRequestId)
        {
            var response = await PostAsync<RideServiceResponse>($"api/taxi/cancel-ride/{rideRequestId}", null);
            return response;
        }
    }
}
