using BusinessObject.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using SystemService.Interface;

namespace BusManagementSystem.Pages.ViewDrivers
{
    public class CreateModel : PageModel
    {
        private readonly IDriverService _driverService;
        private readonly IRoleService _roleService;
        private readonly IBusService _busService; // Dịch vụ Bus

        public CreateModel(IDriverService driverService, IRoleService roleService, IBusService busService)
        {
            _driverService = driverService;
            _roleService = roleService;
            _busService = busService;
        }

        [BindProperty]
        public Driver Driver { get; set; } = default!;

        public string? Message { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please enter a Role")]
        public int RoleId { get; set; }

        public IActionResult OnGet()
        {
            if (!CheckSession())
                return RedirectToPage("/Login");

            ViewData["RoleId"] = new SelectList(_roleService.GetAllRoles(), "RoleId", "RoleName");
            ViewData["Buses"] = new SelectList(_busService.GetAllBuses(), "BusId", "BusName"); // Lấy danh sách buses
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!CheckSession())
                return RedirectToPage("/Login");

            if (!ModelState.IsValid)
            {
                ViewData["RoleId"] = new SelectList(_roleService.GetAllRoles(), "RoleId", "RoleName");
                ViewData["Buses"] = new SelectList(_busService.GetAllBuses(), "BusId", "BusName");
                return Page();
            }

            if (Driver == null)
            {
                Message = "Driver object is null.";
                ModelState.AddModelError(string.Empty, Message);
                return Page();
            }

            if (string.IsNullOrEmpty(Driver.Name) || string.IsNullOrEmpty(Driver.Email))
            {
                Message = "Name and Email are required.";
                ModelState.AddModelError(string.Empty, Message);
                return Page();
            }

            try
            {
                var existingDriver = _driverService.GetAllDrivers().FirstOrDefault(a => a.Email == Driver.Email);
                if (existingDriver != null)
                {
                    Message = "A driver with this email already exists";
                    ModelState.AddModelError(string.Empty, Message);
                    ViewData["RoleId"] = new SelectList(_roleService.GetAllRoles(), "RoleId", "RoleName");
                    ViewData["Buses"] = new SelectList(_busService.GetAllBuses(), "BusId", "BusName");
                    return Page();
                }

                Driver.RoleId = RoleId;
                // Lưu driver vào cơ sở dữ liệu
                _driverService.AddDriver(Driver);
                Message = "Driver created successfully";
                return RedirectToPage("/ViewDrivers/Index");
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                ModelState.AddModelError(string.Empty, Message);
                ViewData["RoleId"] = new SelectList(_roleService.GetAllRoles(), "RoleId", "RoleName");
                ViewData["Buses"] = new SelectList(_busService.GetAllBuses(), "BusId", "BusName");
                return Page();
            }
        }

        public bool CheckSession()
        {
            var loginAccount = HttpContext.Session.GetString("LoginSession");
            if (loginAccount != null)
            {
                var account = JsonSerializer.Deserialize<User>(loginAccount);
                return account?.RoleId == 1;
            }
            return false;
        }
    }
}