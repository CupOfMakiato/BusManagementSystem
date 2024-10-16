using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BusManagementSystem.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            
            HttpContext.Session.Remove("UserEmail");

            return RedirectToPage("/Login"); 
        }
    }
}
