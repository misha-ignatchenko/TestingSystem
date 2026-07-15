using LoadTestingSystem.Desktop.DTOs;
using System.Net.Http;
using System.Net.Http.Json;

namespace LoadTestingSystem.Desktop.Services
{
    public class LoadTestResultService
    {
        private const string BaseUrl = "https://localhost:7209";
        private readonly HttpClient _httpClient;

        public LoadTestResultService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<PagedResult<LoadTestResultDto>> GetAllAsync(int page = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/api/LoadTestResults?page={page}&pageSize={pageSize}");

            if (!response.IsSuccessStatusCode)
            {
                return new PagedResult<LoadTestResultDto>();
            }

            var result = await response.Content.ReadFromJsonAsync<PagedResult<LoadTestResultDto>>();

            return result ?? new PagedResult<LoadTestResultDto>();
        }

        public async Task<bool> CreateAsync(CreateLoadTestResult request)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/api/LoadTestResults",request);

            return response.IsSuccessStatusCode;
        }
    }
}
