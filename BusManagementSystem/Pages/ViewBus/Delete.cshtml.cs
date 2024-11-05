using BusinessObject.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using SystemService.Interface;

namespace BusManagementSystem.Pages.ViewBus
{
    public class DeleteModel : PageModel
    {
        private readonly IBusService _busService;

        public DeleteModel(IBusService busService)
        {
            _busService = busService;
        }

        [BindProperty]
        public Bus Bus { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            if (!CheckSession())
                return RedirectToPage("/Login");

            if (id == null)
                return NotFound();

            // Fetch the bus by ID
            Bus = _busService.GetBusById(id.Value);
            if (Bus == null)
                return NotFound();

            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (!CheckSession())
                return RedirectToPage("/Login");

            if (id == null)
                return NotFound();

            // Find and delete the bus by ID
            try
            {
                var busToDelete = _busService.GetBusById(id.Value);
                if (busToDelete == null)
                    return NotFound();

                _busService.DeleteBus(busToDelete);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error while deleting the bus.");
            }

            return RedirectToPage("/ViewBus/Index");
        }

        public bool CheckSession()
        {
            var loginAccount = HttpContext.Session.GetString("LoginSession");
            if (loginAccount != null)
            {
                var account = JsonSerializer.Deserialize<User>(loginAccount);
                if (account != null && account.RoleId == 2)
                    return true;
            }
            return false;
        }
    }
}
