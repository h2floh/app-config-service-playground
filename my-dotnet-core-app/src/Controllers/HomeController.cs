using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MyDotNetCoreApp.Services;

namespace MyDotNetCoreApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly FeatureFlagService _featureFlagService;

        public HomeController(FeatureFlagService featureFlagService)
        {
            _featureFlagService = featureFlagService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var featureFlags = await _featureFlagService.GetFeatureFlagsAsync();
            return Ok(featureFlags);
        }
    }
}