using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObject.Entity;
using SystemService.Interface;

namespace BusManagementSystem.Pages.ViewDrivers
{
    public class DeleteModel : PageModel
    {
        private readonly IDriverService _driverService;
        private readonly IRoleService _roleService;
        private readonly IBusService _busService;

        public DeleteModel(IDriverService driverService, IRoleService roleService, IBusService busService)
        {
            _driverService = driverService;
            _roleService = roleService;
            _busService = busService;
        }

        [BindProperty]
        public Driver Driver { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Driver = _driverService.GetDriverById(id.Value);

            if (Driver == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driver = _driverService.GetDriverById(id.Value);
            if (driver != null)
            {
                _driverService.DeleteDriver(id.Value);
            }

            return RedirectToPage("./Index");
        }
    }
}
