using SkillsetAPI.Models;

namespace SkillsetAPI.Services
{
    public interface IExternalApiService
    {
        Task<string> GetRandomCommitMessage();
        Task<string> GetLoremIpsum(LoremIpsumRequest request);
        Task<string> GetRandomUser();
    }
}