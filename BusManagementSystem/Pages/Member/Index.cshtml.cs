using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BusManagementSystem.Pages.Member
{
    public class IndexModel : PageModel
    {
        // Optional: Add any homepage-related properties if needed, like welcome messages or dynamic content.
        public string WelcomeMessage { get; set; } = "Welcome to the City Bus Management Center!";
        public string ContactInfo { get; set; } = "📞 Hotline: 19006836 | 📍 Address: 1 Kim Ma, Ba Dinh, Ha Noi";

        public IActionResult OnGet()
        {
            // This method only serves to load the homepage, so we don't need to fetch or filter any route data here.
            return Page();
        }
    }
}
