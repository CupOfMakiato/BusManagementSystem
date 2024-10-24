using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Entity;
using SystemDAO;
using SystemService.Interface;

namespace BusManagementSystem.Pages.ViewDrivers
{
    public class EditModel : PageModel
    {
        private readonly IDriverService _driverService;
        private readonly IRoleService _roleService;

        public EditModel(IDriverService driverService, IRoleService roleService)
        {
            _driverService = driverService;
            _roleService = roleService;
        }

        [BindProperty]
        public Driver Driver { get; set; } = default!;
        public string? Message { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (!CheckSession())
                return RedirectToPage("/Login");

            if (id == null)
            {
                Message = "Not Found";
                return Page();
            }

            Driver = _driverService.GetAllDrivers().FirstOrDefault(m => m.DriverId == id);
            if (Driver == null)
            {
                Message = "Not Found";
                return Page();
            }

            ViewData["RoleId"] = new SelectList(_roleService.GetAllRoles(), "RoleId", "RoleName", Driver.RoleId);
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!CheckSession())
                return RedirectToPage("/Login");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var existingDriver = _driverService.GetAllDrivers().FirstOrDefault(a => a.DriverId == Driver.DriverId);
                if (existingDriver == null)
                {
                    Message = "Not Found";
                    return Page();
                }

                existingDriver.Name = Driver.Name;
                existingDriver.PhoneNumber = Driver.PhoneNumber;
                existingDriver.Status = Driver.Status;
                existingDriver.Shift = Driver.Shift;
                existingDriver.Email = Driver.Email;
                if (!string.IsNullOrEmpty(Driver.Password))
                {
                    existingDriver.Password = Driver.Password;
                }
                existingDriver.RoleId = Driver.RoleId;

                _driverService.UpdateDriver(existingDriver);
                Message = "Update successful!";
                return RedirectToPage("/ViewDrivers/Index");
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return Page();
            }
        }

        private bool CheckSession()
        {
            var loginAccount = HttpContext.Session.GetString("LoginSession");
            if (loginAccount != null)
            {
                var account = System.Text.Json.JsonSerializer.Deserialize<User>(loginAccount);
                return account?.RoleId == 1;
            }
            return false;
        }
    }
}
