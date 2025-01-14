using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.FeatureManagement.Mvc;

namespace TestAppConfig.Pages
{
    [FeatureGate("blue")]
    public class NewFeatureModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}   