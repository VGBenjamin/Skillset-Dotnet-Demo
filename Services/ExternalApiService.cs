using SkillsetAPI.Models;

namespace SkillsetAPI.Services
{
    public class ExternalApiService : IExternalApiService
    {
        private readonly HttpClient _httpClient;

        public ExternalApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetRandomCommitMessage()
        {
            var response = await _httpClient.GetStringAsync("https://whatthecommit.com/index.txt");
            return response.Trim();
        }

        public async Task<string> GetLoremIpsum(LoremIpsumRequest request)
        {
            var url = $"https://baconipsum.com/api/?type=all-meat&paras={request.Paragraphs}";
            var response = await _httpClient.GetStringAsync(url);
            return response.Trim();
        }

        public async Task<string> GetRandomUser()
        {
            var response = await _httpClient.GetStringAsync("https://randomuser.me/api/");
            return response;
        }
    }
}