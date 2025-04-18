using Microsoft.AspNetCore.Mvc;
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

    }
}