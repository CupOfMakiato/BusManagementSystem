using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Entity;
using SystemDAO;
using SystemService.Implementation;

namespace BusManagementSystem.Pages.ViewUsers
{
    public class DeleteModel : PageModel
    {
        private readonly UserService _userService;

        [BindProperty]
        public User User { get; set; }

        public DeleteModel(UserService userService)
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
            _userService.DeleteUser(User.UserId);
            return RedirectToPage("Index");
        }
    }
}
