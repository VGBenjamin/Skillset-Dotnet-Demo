using Microsoft.AspNetCore.Mvc;
using SkillsetAPI.Models;
using SkillsetAPI.Services;

namespace SkillsetAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SkillsetController : ControllerBase
    {
        private readonly IExternalApiService _externalApiService;

        public SkillsetController(IExternalApiService externalApiService)
        {
            _externalApiService = externalApiService;
        }

        [HttpPost("commit-message")]
        public async Task<IActionResult> GetRandomCommitMessage()
        {
            var message = await _externalApiService.GetRandomCommitMessage();
            return Ok(message);
        }

        [HttpPost("lorem-ipsum")]
        public async Task<IActionResult> GetLoremIpsum([FromBody] LoremIpsumRequest request)
        {
            var text = await _externalApiService.GetLoremIpsum(request);
            return Ok(text);
        }

        [HttpGet("random-user")]
        public async Task<IActionResult> GetRandomUser()
        {
            var userData = await _externalApiService.GetRandomUser();
            return Ok(userData);
        }
    }
}