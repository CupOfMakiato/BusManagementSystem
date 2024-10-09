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
using SystemService.Implementation;

namespace BusManagementSystem.Pages.ViewUsers
{
    public class EditModel : PageModel
    {
        private readonly UserService _userService;

        [BindProperty]
        public User User { get; set; }

        public EditModel(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult OnGet(int id)
        {
            User = _userService.GetUserById(id);
            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _userService.UpdateUser(User);
            return RedirectToPage("Index");
        }
    }
}
