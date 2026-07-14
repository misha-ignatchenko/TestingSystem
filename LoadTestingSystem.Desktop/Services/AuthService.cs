using LoadTestingSystem.Desktop.DTOs;
using System.Net.Http;
using System.Net.Http.Json;

namespace LoadTestingSystem.Desktop.Services
{
    public class AuthService
    {
        private const string BaseUrl = "https://localhost:7209";
        private readonly HttpClient _httpClient;

        public AuthService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<bool> LoginAsync(LoginRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/api/Auth/login", request);

            return response.IsSuccessStatusCode;
        }
    }
}